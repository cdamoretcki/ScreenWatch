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

namespace ScreenWatchData
{
    public class ScreenShot
    {
        public DateTime timeStamp { get; set; }
        public String user { get; set; }
        public Image image { get; set; }
        public Image thumbnail { get; set; }
        public String filePath { get; set; }
        public String thumbnailFilePath { get; set; }
    }

    public enum TriggerMatchType
    {
        Include,
        Exclude
    }

    public interface AnalysisTrigger
    {
        Guid id { get; set; }
        int matchThreshold { get; set; }
    }

    public class TextTrigger: AnalysisTrigger
    {
        public String tokenString { get; set; }
        public TriggerMatchType matchType { get; set; }
        public Guid id { get; set; }
        public int matchThreshold { get; set; }
    }

    public class ScreenShotActions
    {
        const string SQL_CONNECTION_STRING = @"Data Source=HANSOLO\SQLEXPRESS;Integrated Security=True;Pooling=False;MultipleActiveResultSets=False;Packet Size=4096";

        /**
         * ScreenShot API
         */

        // This is a test implementation - When the final implemetation is finished, the code will be
        // part of this method - See the _IMPL method the current state of the final implementation
        public Guid insertImage(ScreenShot screenShot)
        {
            Guid guid = Guid.NewGuid();
            String fileName = @"c:\temp\" + guid.ToString() + @".png";
            Image imageFromFile = Image.FromFile(fileName);
            string pathToImage = string.Format(@"~\Images\{0}.png", guid.ToString());
            screenShot.image.Save(HttpContext.Current.Request.MapPath(pathToImage), ImageFormat.Png);
            return guid;
        }

        // This is a test implementation - When the final implemetation is finished, the code will be
        // part of this method - See the _IMPL method the current state of the final implementation
        public List<ScreenShot> getScreenShotsByDateRange(DateTime fromDate, DateTime toDate)
        {
            List<ScreenShot> screenShots = new List<ScreenShot>();
            ScreenShot screenShot;

            for (int i = 1; i < 9; i++)
            {
                screenShot = new ScreenShot();
                screenShot.timeStamp = DateTime.Now;
                screenShot.user = "TESTUSER";
                screenShot.filePath = @"C:\temp\ScreenWatchTestImages\testImage" + i + @".png";
                screenShot.image = Image.FromFile(screenShot.filePath);
                screenShot.thumbnailFilePath = @"C:\temp\ScreenWatchTestImages\testImage" + i + @"_thumb.png";
                screenShot.thumbnail = Image.FromFile(screenShot.thumbnailFilePath);
                screenShots.Add(screenShot);
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
            textTrigger.matchType = TriggerMatchType.Include;
            textTrigger.tokenString = "TEST";
            textTriggers.Add(textTrigger);
            return textTriggers;
        }

        public Guid insertTextTrigger(TextTrigger textTrigger){
            return insertTextTrigger_IMPL(textTrigger);
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
                insertCommand.CommandText = "INSERT INTO [ScreenWatch].[dbo].[ScreenShot] ([id], [userName], [timeStamp], [image]) VALUES (@id, @userName, @timeStamp, (0x))";
                insertCommand.CommandType = System.Data.CommandType.Text;

                SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.UniqueIdentifier);
                Guid id = Guid.NewGuid();
                parameter.Value = id;
                Console.WriteLine("The id is: " + id.ToString());
                insertCommand.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter("@userName", System.Data.SqlDbType.VarChar, 256);
                parameter.Value = screenShot.user;
                insertCommand.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter("@timeStamp", System.Data.SqlDbType.DateTime);
                parameter.Value = screenShot.timeStamp;
                insertCommand.Parameters.Add(parameter);

                insertCommand.ExecuteNonQuery();

                return id;
            }
        }

        private ScreenShot getScreenShotById_IMPL(Guid id)
        {
            ScreenShot screenShot = new ScreenShot();

            StringBuilder connectionString = new StringBuilder();
            connectionString.Append(SQL_CONNECTION_STRING);

            using (SqlConnection connection = new SqlConnection(connectionString.ToString()))
            {
                connection.Open();

                SqlCommand selectCommand = new SqlCommand("", connection);
                selectCommand.CommandText = "SELECT * FROM [ScreenWatch].[dbo].[ScreenShot] WHERE id = @id";
                selectCommand.CommandType = System.Data.CommandType.Text;

                SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.UniqueIdentifier);
                parameter.Value = id;
                selectCommand.Parameters.Add(parameter);
                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        screenShot.user = (String) reader["userName"];
                        screenShot.timeStamp = (DateTime) reader["timeStamp"];
                    }
                }
            }


            return screenShot;
        }

        private List<TextTrigger> getTextTriggers_IMPL()
        {
            List<TextTrigger> textTriggers = new List<TextTrigger>();
            return textTriggers;
        }

        private Guid insertTextTrigger_IMPL(TextTrigger textTrigger)
        {
            if (textTrigger == null)
            {
                throw new System.ArgumentException("Input to method cannot be null", "textTrigger");
            }

            using (SqlConnection connection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand("", connection);

                insertCommand.CommandText = "INSERT INTO [ScreenWatch].[dbo].[TextTrigger] ([id], [matchThreshold], [matchType], [tokenString]) VALUES (@id, @matchThreshold, @matchType, @tokenString)";
                insertCommand.CommandType = System.Data.CommandType.Text;

                SqlParameter parameter = new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.UniqueIdentifier);
                Guid id = Guid.NewGuid();
                parameter.Value = id;
                insertCommand.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter("@matchThreshold", System.Data.SqlDbType.Int);
                parameter.Value = textTrigger.matchThreshold;
                insertCommand.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter("@matchType", System.Data.SqlDbType.VarChar, 20);
                parameter.Value = textTrigger.matchType;
                insertCommand.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter("@tokenString", System.Data.SqlDbType.VarChar, 2048);
                parameter.Value = textTrigger.tokenString;
                insertCommand.Parameters.Add(parameter);

                insertCommand.ExecuteNonQuery();

                return id;
            }
        }
    }
}
