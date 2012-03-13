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
    public class ScreenShotActions: IScreenShotActions
    {
        private const string APP_NAME = "ScreenWatchData";
        private const string SQL_CONNECTION_STRING = @"Data Source=HANSOLO\SQLEXPRESS;Integrated Security=True;Pooling=False;MultipleActiveResultSets=False;Packet Size=4096";

        public ScreenShotActions()
        {
            ScreenWatchDataLogger.init();
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

            StringBuilder connectionString = new StringBuilder();
            connectionString.Append(SQL_CONNECTION_STRING);

            using (SqlConnection connection = new SqlConnection(connectionString.ToString()))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand("", connection);
                insertCommand.CommandText = "INSERT INTO ScreenWatch.dbo.ScreenShot (id, userName, timeStamp, image) VALUES (@id, @userName, @timeStamp, (0x))";
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

                SqlCommand command = new SqlCommand("", connection);

                SqlTransaction transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                command.Transaction = transaction;

                command.CommandText = "select image.PathName(), GET_FILESTREAM_TRANSACTION_CONTEXT() from ScreenWatch.dbo.ScreenShot WHERE id = @id";
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

        // This is a test implementation - When the final implemetation is finished, the code will be
        // part of this method - See the _IMPL method the current state of the final implementation
        public List<ScreenShot> getScreenShotsByDateRange(DateTime fromDate, DateTime toDate)
        {            
            List<String> ids = getScreenShotIdsByDateRange(fromDate, toDate);
            List<ScreenShot> screenShots = new List<ScreenShot>();
            foreach (String id in ids)
            {
                screenShots.Add(getScreenShotById_IMPL(new Guid(id)));
            }
            
            return screenShots;
        }

        /**
         * Triggers API
         * 
         */

        // This is a test implementation - When the final implemetation is finished, the code will be
        // part of this method - See the _IMPL method the current state of the final implementation
        public List<TextTrigger> getTextTriggers()
        {
            List<TextTrigger> textTriggers = new List<TextTrigger>();
            TextTrigger textTrigger = new TextTrigger();
            textTrigger.triggerString = "TEST";
            textTriggers.Add(textTrigger);
            return textTriggers;
        }

        public Guid insertTextTrigger(TextTrigger textTrigger)
        {
            if (textTrigger == null)
            {
                throw new System.ArgumentException("Input to method cannot be null", "textTrigger");
            }

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand("", connection);

                insertCommand.CommandText = "INSERT INTO ScreenWatch.dbo.TextTrigger (id, userName, triggerString) VALUES (@id, @userName, @triggerString)";
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

                insertCommand.CommandText = @"INSERT INTO ScreenWatch.dbo.ToneTrigger (id, userName, lowerColorBound, upperColorBound, sensitivity) VALUES (@id, @userName, @lowerColorBound, @upperColorBound, @sensitivity)";
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

            StringBuilder connectionString = new StringBuilder();
            connectionString.Append(SQL_CONNECTION_STRING);

            using (SqlConnection connection = new SqlConnection(connectionString.ToString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("", connection);
                command.CommandText = "SELECT tt.id, u.email, tt.triggerString FROM ScreenWatch.dbo.TextTrigger tt INNER JOIN ScreenWatch.dbo.[User] u ON tt.userName = u.userName WHERE tt.userName = @userName";
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

            StringBuilder connectionString = new StringBuilder();
            connectionString.Append(SQL_CONNECTION_STRING);

            using (SqlConnection connection = new SqlConnection(connectionString.ToString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("", connection);
                command.CommandText = @"SELECT tt.id, u.email, tt.lowerColorBound, tt.upperColorBound, tt.sensitivity FROM ScreenWatch.dbo.ToneTrigger tt INNER JOIN ScreenWatch.dbo.[User] u ON tt.userName = u.userName WHERE tt.userName = @userName";
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
                        toneTrigger.lowerColorBound = Color.FromArgb(color, color, color);
                        color = reader.GetInt32(3);
                        toneTrigger.upperColorBound = Color.FromArgb(color, color, color);
                        toneTrigger.sensitivity = reader.GetString(4);
                        toneTriggers.Add(toneTrigger);
                    }
                }

                return toneTriggers;
            }
        }

        /**
         * WORK IN PROGRESS
         */

        private Guid insertScreenShot_IMPL(ScreenShot screenShot)
        {
            if (screenShot == null)
            {
                throw new System.ArgumentException("Input to method cannot be null", "screenShot");
            }
            
            StringBuilder connectionString = new StringBuilder();
            connectionString.Append(SQL_CONNECTION_STRING);

            using (SqlConnection connection = new SqlConnection(connectionString.ToString()))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand("", connection);
                insertCommand.CommandText = "INSERT INTO [ScreenWatch].[dbo].[ScreenShot] ([id], [userName], [timeStamp], [image]) VALUES (@id, @userName, @timeStamp, @image)";
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
                MemoryStream memoryStream = new MemoryStream();
                screenShot.image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                parameter.Value = memoryStream.ToArray();
                insertCommand.Parameters.Add(parameter);

                insertCommand.ExecuteNonQuery();

                return id;
            }
        }

        private ScreenShot getScreenShotById_IMPL(Guid id)
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

                    using (SqlFileStream sfs =
                      new SqlFileStream(serverPath, serverTxn, FileAccess.Read))
                    {
                        screenShot.image = Image.FromStream(sfs);
                        sfs.Close();
                    }
                }

                ts.Complete();
            }

            // Create temporary image info
            screenShot.filePath = @"~/ScreenWatchImageCache/images/" + id.ToString() + @".png";
            //String absolutePath = HttpContext.Current.Request.MapPath(screenShot.filePath);
            String absolutePath = @"C:\temp\test\ScreenWatchImageCache\images\" + id.ToString() + @".png";
            screenShot.image.Save(absolutePath, ImageFormat.Png);
            screenShot.thumbnailFilePath = @"~/ScreenWatchImageCache/thumbnails/" + id.ToString() + @".png";
            absolutePath = @"C:\temp\test\ScreenWatchImageCache\thumbnails\" + id.ToString() + @".png";
            ScreenWatchDataLogger.log("the thumb absolute path is: " + absolutePath);
            
            Bitmap bitmap = new Bitmap(128, 128);
            Graphics graphics = Graphics.FromImage((Image) bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(screenShot.image, 0, 0, 128, 128);
            graphics.Dispose();
            screenShot.thumbnail = (Image) bitmap;
            screenShot.thumbnail.Save(absolutePath, ImageFormat.Png);
            
            return screenShot;
        }

        private List<String> getScreenShotIdsByDateRange(DateTime fromDate, DateTime toDate)
        {
            List<String> ids = new List<String>();
            
            const string query = @"SELECT ss.id FROM ScreenWatch.dbo.ScreenShot ss WHERE ss.timeStamp BETWEEN @fromDate AND @toDate";

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = fromDate;
                    cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = toDate;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
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

        private List<TextTrigger> getTextTriggers_IMPL()
        {
            List<TextTrigger> textTriggers = new List<TextTrigger>();
            return textTriggers;
        }

    }

    internal class ScreenWatchDataLogger
    {
        private const string APP_NAME = "ScreenWatchData";

        public static void init(){
            Trace.Listeners.Add(new TextWriterTraceListener(@"C:\temp\ScreenWatchData.log", "ScreenWatchDataListener"));
        }

        public static void log(string message)
        {
            Trace.WriteLine(string.Format("{0}, {1}, {2}, {3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "info", APP_NAME, message));
            //Trace.TraceInformation("the thumb absolute path is: " + absolutePath);
            Trace.Flush();
        }
    }
}
