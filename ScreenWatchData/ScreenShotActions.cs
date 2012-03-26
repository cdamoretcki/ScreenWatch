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
        private string SQL_DATA_SOURCE_LOCAL = @"JAREDSTOY\SQLEXPRESS";
        private string SQL_DATA_SOURCE = String.Empty;
        private string SQL_DB_NAME_LOCAL = @"ScreenWatch";
        private string SQL_DB_NAME_REMOTE = @"SE500S12S2";
        private string SQL_DB_NAME = String.Empty;
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

            if (USE_REMOTE)
            {
                SQL_DATA_SOURCE = SQL_DATA_SOURCE_REMOTE;
                SQL_DB_NAME = SQL_DB_NAME_REMOTE;
                SQL_CONNECTION_STRING = @"Data Source=" + SQL_DATA_SOURCE + @";Pooling=False;MultipleActiveResultSets=False;Packet Size=4096;User Id=SE500S12S2;Password=SE500S12S2";
            }
            else
            {
                SQL_DATA_SOURCE = SQL_DATA_SOURCE_LOCAL;
                SQL_DB_NAME = SQL_DB_NAME_LOCAL;
                SQL_CONNECTION_STRING = @"Data Source=" + SQL_DATA_SOURCE + @";Pooling=False;MultipleActiveResultSets=False;Packet Size=4096;Integrated Security=true";
            }
            SQL_TABLE_SCREENSHOT = SQL_DB_NAME + @".[dbo].[ScreenShot]";
            SQL_TABLE_TEXT_TRIGGER = SQL_DB_NAME + @".[dbo].[TextTrigger]";
            SQL_TABLE_TONE_TRIGGER = SQL_DB_NAME + @".[dbo].[ToneTrigger]";
            SQL_TABLE_USER = SQL_DB_NAME + @".[dbo].[User]";
        }

        /**
         * ScreenShot API
         */

        // This is a test implementation - When the final implemetation is finished, the code will be
        // part of this method - See the _IMPL method the current state of the final implementation
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
                    insertCommand.CommandText = "INSERT INTO " + SQL_TABLE_SCREENSHOT + " (id, userName, timeStamp, image) VALUES (@id, @userName, @timeStamp, (0x))";
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

                    insertCommand.ExecuteNonQuery();

                    using (SqlCommand command = new SqlCommand("", connection))
                    using (SqlTransaction transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                    {
                        command.Transaction = transaction;

                        command.CommandText = "select image.PathName(), GET_FILESTREAM_TRANSACTION_CONTEXT() from " + SQL_TABLE_SCREENSHOT + " WHERE id = @id";
                        command.CommandType = System.Data.CommandType.Text;

                        parameter = new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.UniqueIdentifier);
                        parameter.Value = id;
                        command.Parameters.Add(parameter);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Get the pointer for file 
                                string path = reader.GetString(0);
                                byte[] transactionContext = reader.GetSqlBytes(1).Buffer;

                                const int BlockSize = 1024 * 512;
                                String clientPath = @"c:\temp\temp.png";
                                screenShot.image.Save(clientPath, ImageFormat.Png);
                                using (FileStream source = new FileStream(clientPath, FileMode.Open, FileAccess.Read))
                                {
                                    using (SqlFileStream dest = new SqlFileStream(path, (byte[])reader.GetValue(1), FileAccess.Write))
                                    {
                                        byte[] buffer = new byte[BlockSize];
                                        int bytesRead;
                                        while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                                        {
                                            dest.Write(buffer, 0, bytesRead);
                                            dest.Flush();
                                        }
                                        dest.Close();
                                    }
                                    source.Close();
                                }
                            }
                        }
                        transaction.Commit();
                        return id;
                    }
                }
            }
        }

        // This is a test implementation - When the final implemetation is finished, the code will be
        // part of this method - See the _IMPL method the current state of the final implementation
        public List<ScreenShot> getScreenShotsByDateRange(DateTime fromDate, DateTime toDate)
        {
            List<String> ids = getScreenShotIdsByDateRange(fromDate, toDate);
            List<ScreenShot> screenShots = new List<ScreenShot>();
            foreach (String id in ids)
            {
                screenShots.Add(getScreenShotById(new Guid(id)));
            }

            return screenShots;
        }

        /**
         * Triggers API
         * 
         */

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

        public List<TextTrigger> getAllTextTriggers()
        {
            List<TextTrigger> textTriggers = new List<TextTrigger>();
                        
            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("", connection))
                {
                    command.CommandText = "SELECT tt.id, tt.userName, u.email, tt.triggerString FROM " + SQL_TABLE_TEXT_TRIGGER + " tt INNER JOIN " + SQL_TABLE_USER + " u ON tt.userName = u.userName";
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

        public List<ToneTrigger> getAllToneTriggers()
        {
            List<ToneTrigger> toneTriggers = new List<ToneTrigger>();

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("", connection))
                {
                    command.CommandText = @"SELECT tt.id, tt.userName, u.email, tt.lowerColorBound, tt.upperColorBound, tt.sensitivity FROM " + SQL_TABLE_TONE_TRIGGER + " tt INNER JOIN " + SQL_TABLE_USER + " u ON tt.userName = u.userName";
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

        private ScreenShot getScreenShotById(Guid id)
        {
            ScreenShot screenShot = new ScreenShot();

            const string SelectTSql = @"
                SELECT
                    userName,
                    timeStamp,
                    image.PathName(),
                    GET_FILESTREAM_TRANSACTION_CONTEXT()
                  FROM ScreenWatch.dbo.ScreenShot
                  WHERE id = @id";

            string serverPath;
            byte[] serverTxn;

            using (TransactionScope ts = new TransactionScope())
            {
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
                            serverPath = rdr.GetSqlString(2).Value;
                            serverTxn = rdr.GetSqlBinary(3).Value;
                            rdr.Close();
                        }
                    }

                    using (SqlFileStream sfs = new SqlFileStream(serverPath, serverTxn, FileAccess.Read))
                    {
                        screenShot.image = Image.FromStream(sfs);
                        sfs.Close();
                    }
                }

                ts.Complete();
            }

            // Create temporary image info
            screenShot.filePath = @"~/ScreenWatchImageCache/images/" + id.ToString() + @".png";
            screenShot.thumbnailFilePath = @"~/ScreenWatchImageCache/thumbnails/" + id.ToString() + @".png";
            String absolutePath;
            String thumbAbsolutePath;
            if (!unitTest)
            {
                absolutePath = HttpContext.Current.Request.MapPath(screenShot.filePath);
                thumbAbsolutePath = HttpContext.Current.Request.MapPath(screenShot.thumbnailFilePath);
            }
            else
            {
                absolutePath = @"C:\temp\test\ScreenWatchImageCache\images\" + id.ToString() + @".png";
                thumbAbsolutePath = @"C:\temp\test\ScreenWatchImageCache\thumbnails\" + id.ToString() + @".png";
            }
            ScreenShotActionsLogger.log("the image absolute path is: " + absolutePath);
            ScreenShotActionsLogger.log("the thumbnail absolute path is: " + thumbAbsolutePath);
            screenShot.image.Save(absolutePath, ImageFormat.Png);

            Bitmap bitmap = new Bitmap(128, 128);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(screenShot.image, 0, 0, 128, 128);
                screenShot.thumbnail = (Image)bitmap;
                screenShot.thumbnail.Save(thumbAbsolutePath, ImageFormat.Png);
            }

            return screenShot;
        }

        private List<String> getScreenShotIdsByDateRange(DateTime fromDate, DateTime toDate)
        {
            List<String> ids = new List<String>();

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
                            ids.Add(reader.GetSqlString(0).Value);
                        }
                        reader.Close();
                    }
                }
                connection.Close();
            }

            return ids;
        }
    }

    internal class ScreenShotActionsLogger
    {
        private const string APP_NAME = "ScreenShotActions";

        public static void init()
        {
            Debug.Listeners.Add(new TextWriterTraceListener(@"C:\temp\" + APP_NAME + ".log", APP_NAME));
            Debug.AutoFlush = true;
        }

        public static void log(string message)
        {
            Debug.WriteLine(string.Format("{0}, {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message));
        }
    }
}
