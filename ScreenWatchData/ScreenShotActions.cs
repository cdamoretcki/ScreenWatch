using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Drawing;
using System.Transactions;
using System.Data;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace ScreenWatchData
{
    public class ScreenShotActions : IScreenShotActions
    {
        private bool unitTest = false;
        private Boolean USE_REMOTE = false;
        private string SQL_CONNECTION_STRING = String.Empty;
        private string SQL_DATA_SOURCE_REMOTE = @"146.186.87.88";
        private string SQL_DB_NAME_LOCAL = @"ScreenWatch";
        private string SQL_DB_NAME_REMOTE = @"SE500S12S2";
        private string SQL_TABLE_SCREENSHOT = String.Empty;
        private string SQL_TABLE_TEXT_TRIGGER = String.Empty;
        private string SQL_TABLE_TONE_TRIGGER = String.Empty;
        private string SQL_TABLE_USER = String.Empty;

        public ScreenShotActions(bool unit)
            : this()
        {
            unitTest = unit;
        }

        public ScreenShotActions()
        {
            ScreenShotActionsLogger.init();
            string sqlDbName;
            if (USE_REMOTE)
            {
                sqlDbName = SQL_DB_NAME_REMOTE;
                SQL_CONNECTION_STRING = @"Data Source=" + SQL_DATA_SOURCE_REMOTE + @";Pooling=False;MultipleActiveResultSets=False;Packet Size=4096;User Id=SE500S12S2;Password=SE500S12S2";
            }
            else
            {
                sqlDbName = SQL_DB_NAME_LOCAL;
                SQL_CONNECTION_STRING = @"Data Source=" + Environment.MachineName + @"\SQLEXPRESS;Pooling=False;MultipleActiveResultSets=False;Packet Size=4096;Integrated Security=true";
            }
            SQL_TABLE_SCREENSHOT = sqlDbName + @".[dbo].[ScreenShot]";
            SQL_TABLE_TEXT_TRIGGER = sqlDbName + @".[dbo].[TextTrigger]";
            SQL_TABLE_TONE_TRIGGER = sqlDbName + @".[dbo].[ToneTrigger]";
            SQL_TABLE_USER = sqlDbName + @".[dbo].[User]";
        }

        # region ScreenShot API


        public Guid insertScreenShot(ScreenShot screenShot)
        {
            if (screenShot == null)
            {
                throw new System.ArgumentException("Input to method cannot be null", "screenShot");
            }

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand insertCommand = new SqlCommand("", connection))
                {
                    insertCommand.CommandText = "INSERT INTO " + SQL_TABLE_SCREENSHOT + " (id, userName, timeStamp, image) VALUES (@id, @userName, @timeStamp, @image)";
                    insertCommand.CommandType = System.Data.CommandType.Text;

                    SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.UniqueIdentifier);
                    Guid id = Guid.NewGuid();
                    parameter.Value = id;
                    insertCommand.Parameters.Add(parameter);

                    parameter = new System.Data.SqlClient.SqlParameter("@userName", System.Data.SqlDbType.VarChar, 256);
                    parameter.Value = screenShot.user;
                    insertCommand.Parameters.Add(parameter);

                    parameter = new System.Data.SqlClient.SqlParameter("@timeStamp", System.Data.SqlDbType.DateTime);
                    parameter.Value = screenShot.timeStamp;
                    insertCommand.Parameters.Add(parameter);

                    parameter = new System.Data.SqlClient.SqlParameter("@image", System.Data.SqlDbType.VarBinary);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        screenShot.image.Save(memoryStream, ImageFormat.Png);
                        Byte[] imageBytes = memoryStream.ToArray();
                        memoryStream.Close();
                        parameter.Value = imageBytes;
                        insertCommand.Parameters.Add(parameter);
                    }

                    insertCommand.ExecuteNonQuery();

                    return id;
                }
            }
        }

        public List<ScreenShot> getScreenShotsByDateRange(DateTime fromDate, DateTime toDate)
        {
            List<Guid> ids = getScreenShotIdsByDateRange(fromDate, toDate);
            List<ScreenShot> screenShots = new List<ScreenShot>();
            foreach (var id in ids)
            {
                screenShots.Add(getScreenShotById(id));
            }

            return screenShots;
        }

        private ScreenShot getScreenShotById(Guid id)
        {
            ScreenShot screenShot = new ScreenShot();

            // Create temporary image info
            String dir, thumbDir, absolutePath, thumbAbsolutePath;
            if (!unitTest)
            {
                dir = @"~/ScreenWatchImageCache/images/";
                thumbDir = @"~/ScreenWatchImageCache/thumbnails/";
                screenShot.filePath = dir + id.ToString() + @".png";
                screenShot.thumbnailFilePath = thumbDir + id.ToString() + @".png";
                absolutePath = HttpContext.Current.Request.MapPath(screenShot.filePath);
                thumbAbsolutePath = HttpContext.Current.Request.MapPath(screenShot.thumbnailFilePath);
                dir = HttpContext.Current.Request.MapPath(dir);
                thumbDir = HttpContext.Current.Request.MapPath(thumbDir);
            }
            else
            {
                dir = @"C:\temp\test\ScreenWatchImageCache\images\";
                thumbDir = @"C:\temp\test\ScreenWatchImageCache\thumbnails\";
                screenShot.filePath = dir + id.ToString() + @".png";
                screenShot.thumbnailFilePath = thumbDir + id.ToString() + @".png";
                absolutePath = screenShot.filePath;
                thumbAbsolutePath = screenShot.thumbnailFilePath;
            }

            //Make sure the paths exists, else create them
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (!Directory.Exists(thumbDir))
            {
                Directory.CreateDirectory(thumbDir);
            }

            ScreenShotActionsLogger.log("the image absolute path is: " + absolutePath);
            ScreenShotActionsLogger.log("the thumbnail absolute path is: " + thumbAbsolutePath);

            if (File.Exists(absolutePath) && File.Exists(thumbAbsolutePath))
            {
                screenShot.image = Image.FromFile(absolutePath);
                screenShot.thumbnail = Image.FromFile(thumbAbsolutePath);

                string SelectTSql = @"SELECT ss.userName, ss.timeStamp FROM " + SQL_TABLE_SCREENSHOT + " ss WHERE ss.id = @id";
                using (SqlConnection conn = new SqlConnection(SQL_CONNECTION_STRING))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(SelectTSql, conn))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            rdr.Read();
                            screenShot.user = rdr.GetSqlString(0).Value;
                            screenShot.timeStamp = rdr.GetSqlDateTime(1).Value;
                            rdr.Close();
                        }
                    }
                }
            }
            else
            {
                string SelectTSql = @"SELECT ss.userName, ss.timeStamp, ss.image FROM " + SQL_TABLE_SCREENSHOT + " ss WHERE ss.id = @id";

                using (SqlConnection conn = new SqlConnection(SQL_CONNECTION_STRING))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(SelectTSql, conn))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            rdr.Read();
                            screenShot.user = rdr.GetSqlString(0).Value;
                            screenShot.timeStamp = rdr.GetSqlDateTime(1).Value;
                            Byte[] imageBytes = rdr.GetSqlBytes(2).Value;
                            using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                            {
                                screenShot.image = Image.FromStream(memoryStream);
                            }
                            rdr.Close();
                        }
                    }
                }

                screenShot.image.Save(absolutePath, ImageFormat.Png);

                Bitmap bitmap = new Bitmap(160, 90);
                using (Graphics graphics = Graphics.FromImage((Image)bitmap))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(screenShot.image, 0, 0, 160, 90);
                    screenShot.thumbnail = (Image)bitmap;
                    screenShot.thumbnail.Save(thumbAbsolutePath, ImageFormat.Png);
                }
            }
            return screenShot;
        }

        private List<Guid> getScreenShotIdsByDateRange(DateTime fromDate, DateTime toDate)
        {
            List<Guid> ids = new List<Guid>();

            string query = @"SELECT ss.id FROM " + SQL_TABLE_SCREENSHOT + " ss WHERE ss.timeStamp BETWEEN @fromDate AND @toDate";

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = fromDate;
                    cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = toDate;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ids.Add(reader.GetGuid(0));
                        }
                        reader.Close();
                    }
                }
                connection.Close();
            }

            return ids;
        }

        # endregion

        # region Triggers API

        /// <summary>
        /// inserts a new text trigger
        /// </summary>
        /// <param name="textTrigger"></param>
        /// <returns></returns>
        public Guid insertTextTrigger(TextTrigger textTrigger)
        {
            if (textTrigger == null)
            {
                throw new System.ArgumentException("Input to method cannot be null", "textTrigger");
            }

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand insertCommand = new SqlCommand("", connection))
                {

                    insertCommand.CommandText = @"INSERT INTO " + SQL_TABLE_TEXT_TRIGGER + " (id, userName, triggerString) VALUES (@id, @userName, @triggerString)";
                    insertCommand.CommandType = System.Data.CommandType.Text;

                    SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.UniqueIdentifier);
                    Guid id = Guid.NewGuid();
                    parameter.Value = id;
                    insertCommand.Parameters.Add(parameter);

                    parameter = new System.Data.SqlClient.SqlParameter("@userName", System.Data.SqlDbType.VarChar, 256);
                    parameter.Value = textTrigger.userName;
                    insertCommand.Parameters.Add(parameter);

                    parameter = new System.Data.SqlClient.SqlParameter("@triggerString", System.Data.SqlDbType.VarChar, 2048);
                    parameter.Value = textTrigger.triggerString;
                    insertCommand.Parameters.Add(parameter);

                    insertCommand.ExecuteNonQuery();

                    return id;
                }
            }
        }

        /// <summary>
        /// inserts a new tone trigger
        /// </summary>
        /// <param name="toneTrigger"></param>
        /// <returns></returns>
        public Guid insertToneTrigger(ToneTrigger toneTrigger)
        {
            if (toneTrigger == null)
            {
                throw new System.ArgumentException("Input to method cannot be null", "toneTrigger");
            }

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand("", connection);

                insertCommand.CommandText = @"INSERT INTO " + SQL_TABLE_TONE_TRIGGER + " (id, userName, lowerColorBound, upperColorBound, sensitivity) VALUES (@id, @userName, @lowerColorBound, @upperColorBound, @sensitivity)";
                insertCommand.CommandType = System.Data.CommandType.Text;

                SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.UniqueIdentifier);
                Guid id = Guid.NewGuid();
                parameter.Value = id;
                insertCommand.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter("@userName", System.Data.SqlDbType.VarChar, 256);
                parameter.Value = toneTrigger.userName;
                insertCommand.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter("@lowerColorBound", System.Data.SqlDbType.Int);
                parameter.Value = toneTrigger.lowerColorBound.ToArgb();
                insertCommand.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter("@upperColorBound", System.Data.SqlDbType.Int);
                parameter.Value = toneTrigger.upperColorBound.ToArgb();
                insertCommand.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter("@sensitivity", System.Data.SqlDbType.VarChar, 128);
                parameter.Value = toneTrigger.sensitivity;
                insertCommand.Parameters.Add(parameter);

                insertCommand.ExecuteNonQuery();

                return id;
            }
        }

        /// updates a text trigger based on its guid
        public void updateTextTrigger(TextTrigger textTrigger)
        {
            if (textTrigger == null)
            {
                throw new System.ArgumentException("Input to method cannot be null", "textTrigger");
            }

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand updateCommand = new SqlCommand(@"UPDATE " + SQL_TABLE_TEXT_TRIGGER
                    + " SET userName=@userName, triggerString=@triggerString WHERE (id=@id)"
                    , connection))
                {
                    updateCommand.CommandType = System.Data.CommandType.Text;

                    SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.UniqueIdentifier);
                    parameter.Value = textTrigger.id;
                    updateCommand.Parameters.Add(parameter);

                    parameter = new System.Data.SqlClient.SqlParameter("@userName", System.Data.SqlDbType.VarChar, 256);
                    parameter.Value = textTrigger.userName;
                    updateCommand.Parameters.Add(parameter);

                    parameter = new System.Data.SqlClient.SqlParameter("@triggerString", System.Data.SqlDbType.VarChar, 2048);
                    parameter.Value = textTrigger.triggerString;
                    updateCommand.Parameters.Add(parameter);

                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// updates a tone trigger based on its guid
        /// </summary>
        /// <param name="toneTrigger"></param>
        public void updateToneTrigger(ToneTrigger toneTrigger)
        {
            if (toneTrigger == null)
            {
                throw new System.ArgumentException("Input to method cannot be null", "toneTrigger");
            }

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand updateCommand = new SqlCommand(@"UPDATE " + SQL_TABLE_TONE_TRIGGER
                    + " SET userName=@userName, lowerColorBound=@lowerColorBound, upperColorBound=@upperColorBound, sensitivity=@sensitivity WHERE (id=@id)"
                    , connection))
                {
                    updateCommand.CommandType = System.Data.CommandType.Text;

                    SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.UniqueIdentifier);
                    parameter.Value = toneTrigger.id;
                    updateCommand.Parameters.Add(parameter);

                    parameter = new System.Data.SqlClient.SqlParameter("@userName", System.Data.SqlDbType.VarChar, 256);
                    parameter.Value = toneTrigger.userName;
                    updateCommand.Parameters.Add(parameter);

                    parameter = new System.Data.SqlClient.SqlParameter("@lowerColorBound", System.Data.SqlDbType.Int);
                    parameter.Value = toneTrigger.lowerColorBound.ToArgb();
                    updateCommand.Parameters.Add(parameter);

                    parameter = new System.Data.SqlClient.SqlParameter("@upperColorBound", System.Data.SqlDbType.Int);
                    parameter.Value = toneTrigger.upperColorBound.ToArgb();
                    updateCommand.Parameters.Add(parameter);

                    parameter = new System.Data.SqlClient.SqlParameter("@sensitivity", System.Data.SqlDbType.VarChar, 128);
                    parameter.Value = toneTrigger.sensitivity;
                    updateCommand.Parameters.Add(parameter);

                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// kills it dead
        /// </summary>
        /// <param name="id"></param>
        public void deleteTextTrigger(Guid id)
        {
            if (id == null)
            {
                throw new System.ArgumentException("Input to method cannot be null", id.ToString());
            }

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand updateCommand = new SqlCommand(@"DELETE FROM " + SQL_TABLE_TEXT_TRIGGER + " WHERE (id=@id)", connection))
                {
                    updateCommand.CommandType = System.Data.CommandType.Text;
                    SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.UniqueIdentifier);
                    parameter.Value = id;
                    updateCommand.Parameters.Add(parameter);
                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// if find your lack of faith... disturbing
        /// </summary>
        /// <param name="toneTrigger"></param>
        public void deleteToneTrigger(Guid id)
        {
            if (id == null)
            {
                throw new System.ArgumentException("Input to method cannot be null", "toneTrigger");
            }

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand updateCommand = new SqlCommand(@"DELETE FROM " + SQL_TABLE_TONE_TRIGGER + " WHERE (id=@id)", connection))
                {
                    updateCommand.CommandType = System.Data.CommandType.Text;
                    SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.UniqueIdentifier);
                    parameter.Value = id;
                    updateCommand.Parameters.Add(parameter);
                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// returns a list of text triggers associated to a user that is being watched
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<TextTrigger> getTextTriggersByUser(String user)
        {
            if (user == null)
            {
                throw new System.ArgumentException("Input to method cannot be null", "user");
            }

            List<TextTrigger> textTriggers = new List<TextTrigger>();

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("", connection))
                {
                    command.CommandText = "SELECT tt.id, u.email, tt.triggerString FROM " + SQL_TABLE_TEXT_TRIGGER + " tt INNER JOIN " + SQL_TABLE_USER + @" u ON tt.userName = u.userName WHERE tt.userName = @userName";
                    command.CommandType = System.Data.CommandType.Text;

                    SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@userName", System.Data.SqlDbType.VarChar, 256);
                    parameter.Value = user;
                    command.Parameters.Add(parameter);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        TextTrigger textTrigger = new TextTrigger();

                        while (reader.Read())
                        {
                            textTrigger = new TextTrigger();
                            textTrigger.userName = user;
                            textTrigger.id = reader.GetGuid(0);
                            textTrigger.userEmail = reader.GetString(1);
                            textTrigger.triggerString = reader.GetString(2);
                            textTriggers.Add(textTrigger);
                        }
                    }
                }
                return textTriggers;
            }
        }

        /// <summary>
        /// returns a list of tone triggers associated to a user that is being watched
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<ToneTrigger> getToneTriggersByUser(String user)
        {
            if (user == null)
            {
                throw new System.ArgumentException("Input to method cannot be null", "user");
            }

            List<ToneTrigger> toneTriggers = new List<ToneTrigger>();

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("", connection))
                {
                    command.CommandText = @"SELECT tt.id, u.email, tt.lowerColorBound, tt.upperColorBound, tt.sensitivity FROM " + SQL_TABLE_TONE_TRIGGER + " tt INNER JOIN " + SQL_TABLE_USER + " u ON tt.userName = u.userName WHERE tt.userName = @userName";
                    command.CommandType = System.Data.CommandType.Text;

                    SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@userName", System.Data.SqlDbType.VarChar, 256);
                    parameter.Value = user;
                    command.Parameters.Add(parameter);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ToneTrigger toneTrigger = new ToneTrigger();
                        int color = 0;

                        while (reader.Read())
                        {
                            toneTrigger = new ToneTrigger();
                            toneTrigger.userName = user;
                            toneTrigger.id = reader.GetGuid(0);
                            toneTrigger.userEmail = reader.GetString(1);
                            color = reader.GetInt32(2);
                            toneTrigger.lowerColorBound = Color.FromArgb(color);
                            color = reader.GetInt32(3);
                            toneTrigger.upperColorBound = Color.FromArgb(color);
                            toneTrigger.sensitivity = reader.GetInt32(4);
                            toneTriggers.Add(toneTrigger);
                        }
                    }
                }
                return toneTriggers;
            }
        }

        /// <summary>
        /// returns all text triggers in the system, this is meant for an admin
        /// </summary>
        /// <returns></returns>
        public List<TextTrigger> getAllTextTriggers()
        {
            List<TextTrigger> textTriggers = new List<TextTrigger>();

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("", connection))
                {
                    command.CommandText = "SELECT tt.id, tt.userName, u.email, tt.triggerString FROM " + SQL_TABLE_TEXT_TRIGGER + " tt INNER JOIN " + SQL_TABLE_USER + " u ON tt.userName = u.userName ORDER BY u.userName";
                    command.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        TextTrigger textTrigger = new TextTrigger();

                        while (reader.Read())
                        {
                            textTrigger = new TextTrigger();
                            textTrigger.id = reader.GetGuid(0);
                            textTrigger.userName = reader.GetString(1);
                            textTrigger.userEmail = reader.GetString(2);
                            textTrigger.triggerString = reader.GetString(3);
                            textTriggers.Add(textTrigger);
                        }
                    }
                }
                return textTriggers;
            }
        }

        /// <summary>
        /// returns all text triggers in the system, this is meant for an admin
        /// </summary>
        /// <returns></returns>
        public List<ToneTrigger> getAllToneTriggers()
        {
            List<ToneTrigger> toneTriggers = new List<ToneTrigger>();

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("", connection))
                {
                    command.CommandText = @"SELECT tt.id, tt.userName, u.email, tt.lowerColorBound, tt.upperColorBound, tt.sensitivity FROM " + SQL_TABLE_TONE_TRIGGER + " tt INNER JOIN " + SQL_TABLE_USER + " u ON tt.userName = u.userName ORDER BY u.userName";
                    command.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ToneTrigger toneTrigger = new ToneTrigger();
                        int color = 0;

                        while (reader.Read())
                        {
                            toneTrigger = new ToneTrigger();
                            toneTrigger.id = reader.GetGuid(0);
                            toneTrigger.userName = reader.GetString(1);
                            toneTrigger.userEmail = reader.GetString(2);
                            color = reader.GetInt32(3);
                            toneTrigger.lowerColorBound = Color.FromArgb(color);
                            color = reader.GetInt32(4);
                            toneTrigger.upperColorBound = Color.FromArgb(color);
                            toneTrigger.sensitivity = reader.GetInt32(5);
                            toneTriggers.Add(toneTrigger);
                        }
                    }
                }
                return toneTriggers;
            }
        }

        /// <summary>
        /// Get a TextTrigger object based on passed id
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public TextTrigger getTextTriggerById(String input)
        {
            if (input == null || input == String.Empty)
            {
                throw new System.ArgumentException("Input to method cannot be null or an empty string: " + input, "id");
            }

            Guid id = new Guid(input);

            StringBuilder connectionString = new StringBuilder();
            connectionString.Append(SQL_CONNECTION_STRING);

            using (SqlConnection connection = new SqlConnection(connectionString.ToString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("", connection);
                command.CommandText = "SELECT tt.id, tt.userName, u.email, tt.triggerString FROM " + SQL_TABLE_TEXT_TRIGGER + " tt INNER JOIN " + SQL_TABLE_USER + " u ON tt.userName = u.userName WHERE tt.id = @id";
                command.CommandType = System.Data.CommandType.Text;

                SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.UniqueIdentifier);
                parameter.Value = id;
                command.Parameters.Add(parameter);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    TextTrigger textTrigger = null;

                    if (reader.Read())
                    {
                        Guid queriedId = (Guid)reader["id"];
                        if (!queriedId.Equals(null))
                        {
                            textTrigger = new TextTrigger();
                            textTrigger.id = queriedId;
                            textTrigger.userName = (String)reader["userName"];
                            textTrigger.userEmail = (String)reader["email"];
                            textTrigger.triggerString = (String)reader["triggerString"];
                        }
                    }
                    reader.Close();
                    return textTrigger;
                }
            }
        }

        /// <summary>
        /// Get a ToneTrigger object based on passed id
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ToneTrigger getToneTriggerById(String input)
        {
            if (input == null || input == String.Empty)
            {
                throw new System.ArgumentException("Input to method cannot be null or an empty string: " + input, "id");
            }

            Guid id = new Guid(input);

            StringBuilder connectionString = new StringBuilder();
            connectionString.Append(SQL_CONNECTION_STRING);

            using (SqlConnection connection = new SqlConnection(connectionString.ToString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("", connection);
                command.CommandText = @"SELECT tt.id, tt.userName, u.email, tt.lowerColorBound, tt.upperColorBound, tt.sensitivity FROM " + SQL_TABLE_TONE_TRIGGER + " tt INNER JOIN " + SQL_TABLE_USER + " u ON tt.userName = u.userName  WHERE tt.id = @id";
                command.CommandType = System.Data.CommandType.Text;

                SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.UniqueIdentifier);
                parameter.Value = id;
                command.Parameters.Add(parameter);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    ToneTrigger toneTrigger = null;

                    if (reader.Read())
                    {
                        Guid queriedId = (Guid)reader["id"];
                        int color = 0;
                        if (!queriedId.Equals(null))
                        {
                            toneTrigger = new ToneTrigger();
                            toneTrigger.id = queriedId;
                            toneTrigger.userName = (String)reader["userName"];
                            toneTrigger.userEmail = (String)reader["email"];
                            color = (int)reader["lowerColorBound"];
                            toneTrigger.lowerColorBound = Color.FromArgb(color);
                            color = (int)reader["upperColorBound"];
                            toneTrigger.upperColorBound = Color.FromArgb(color);
                            toneTrigger.sensitivity = (int)reader["sensitivity"];
                        }
                    }
                    reader.Close();
                    return toneTrigger;
                }
            }
        }

        #endregion

        # region Users API

        /// <summary>
        /// Persist user object in db
        /// </summary>
        /// <param name="user"></param>
        public void insertUser(User user)
        {
            if (user == null)
            {
                throw new System.ArgumentException("Input to method cannot be null", "user");
            }

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand("", connection);

                insertCommand.CommandText = @"INSERT INTO " + SQL_TABLE_USER + " (userName, email, isMonitored, isAdmin) VALUES (@userName, @email, @isMonitored, @isAdmin)";
                insertCommand.CommandType = System.Data.CommandType.Text;

                SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@userName", System.Data.SqlDbType.VarChar, 256);
                parameter.Value = user.userName;
                insertCommand.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter("@email", System.Data.SqlDbType.VarChar, 256);
                parameter.Value = user.email;
                insertCommand.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter("@isMonitored", System.Data.SqlDbType.Bit);
                parameter.Value = user.isMonitored;
                insertCommand.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter("@isAdmin", System.Data.SqlDbType.Bit);
                parameter.Value = user.isAdmin;
                insertCommand.Parameters.Add(parameter);

                insertCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Update user in db
        /// </summary>
        /// <param name="user"></param>
        public void updateUser(User user)
        {
            deleteUser(user.userName);
            insertUser(user);
        }

        /// <summary>
        /// Delete user from db
        /// </summary>
        /// <param name="userName"></param>
        public void deleteUser(String userName)
        {
            if (userName == null)
            {
                throw new System.ArgumentException("Input to method cannot be null", "userName");
            }

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand deleteCommand = new SqlCommand("", connection);

                deleteCommand.CommandText = @"DELETE FROM " + SQL_TABLE_USER + " WHERE userName = @userName";
                deleteCommand.CommandType = System.Data.CommandType.Text;

                SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@userName", System.Data.SqlDbType.VarChar, 256);
                parameter.Value = userName;
                deleteCommand.Parameters.Add(parameter);

                deleteCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Get a user object based on passed username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User getUserByUserName(String userName)
        {
            ScreenShotActionsLogger.log("" + userName);

            if (userName == null || userName == String.Empty)
            {
                throw new System.ArgumentException("Input to method cannot be null or an empty string: " + userName, "userName");
            }

            StringBuilder connectionString = new StringBuilder();
            connectionString.Append(SQL_CONNECTION_STRING);

            using (SqlConnection connection = new SqlConnection(connectionString.ToString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("", connection);
                command.CommandText = "SELECT u.userName, u.email, u.isMonitored, u.isAdmin FROM " + SQL_TABLE_USER + " u WHERE u.userName = @userName";
                command.CommandType = System.Data.CommandType.Text;

                SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@userName", System.Data.SqlDbType.VarChar, 256);
                parameter.Value = userName;
                command.Parameters.Add(parameter);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    User user = null;

                    if (reader.Read())
                    {
                        String queriedUserName = (String)reader["userName"];
                        if (!queriedUserName.Equals(null) && !queriedUserName.Equals(String.Empty))
                        {
                            user = new User();
                            user.userName = queriedUserName;
                            user.email = (String)reader["email"];
                            user.isMonitored = (Boolean)reader["isMonitored"];
                            user.isAdmin = (Boolean)reader["isAdmin"];
                        }
                    }
                    reader.Close();
                    return user;
                }
            }
        }

        /// <summary>
        /// I am your father
        /// </summary>
        /// <returns></returns>
        public List<string> getUsers()
        {
            List<string> users = new List<string>();
            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT u.userName FROM " + SQL_TABLE_USER + " u ORDER BY u.userName", connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(reader.GetString(0));
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return users;
        }

        public List<User> getAllUsers()
        {
            List<User> users = new List<User>();

            StringBuilder connectionString = new StringBuilder();
            connectionString.Append(SQL_CONNECTION_STRING);

            using (SqlConnection connection = new SqlConnection(connectionString.ToString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("", connection);
                command.CommandText = "SELECT u.userName, u.email, u.isMonitored, u.isAdmin FROM " + SQL_TABLE_USER + " u";
                command.CommandType = System.Data.CommandType.Text;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    User user;
                    while (reader.Read())
                    {
                        user = new User();
                        user.userName = (String)reader["userName"];
                        user.email = (String)reader["email"];
                        user.isMonitored = (Boolean)reader["isMonitored"];
                        user.isAdmin = (Boolean)reader["isAdmin"];
                        users.Add(user);
                    }
                }

                return users;
            }
        }


        # endregion
    }

    internal class ScreenShotActionsLogger
    {
        private const string APP_NAME = "ScreenShotActions";

        public static void init()
        {
            Debug.Listeners.Add(new TextWriterTraceListener(@"C:\temp\" + APP_NAME + ".log"));
            Debug.AutoFlush = true;
        }

        public static void log(string message)
        {
            //Debug.WriteLine(string.Format("{0}, {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message));
        }
    }
}
