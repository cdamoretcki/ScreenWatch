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

    public class ScreenShotActions
    {
        const string SQL_CONNECTION_STRING = @"Data Source=HANSOLO\SQLEXPRESS;Integrated Security=True;Pooling=False;MultipleActiveResultSets=False;Packet Size=4096";
    
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

        /**
         * STUBS
         */

        // This is a stub method.  Eventually all the juicy goodness will be here.  For now, the 
        // method signature is defined, but we use a test stub handler to general the returned data.
        public Guid insertImage(ScreenShot screenShot)
        {
            Guid guid = Guid.NewGuid();
            theImagePutItForMe(guid.ToString(), screenShot.image);
            return Guid.Empty;
        }

        // This is a stub method.  Eventually all the juicy goodness will be here.  For now, the 
        // method signature is defined, but we use a test stub handler to general the returned data.
        public List<ScreenShot> getScreenShotsByDateRange(DateTime fromDate, DateTime toDate)
        {
            return theImagesGiveThemToMe();
        }

        /**
         * STUB HANDLERS
         */

        // This is a stub handler for inserting an image onto the local webserver
        private void theImagePutItForMe(String id, Image image)
        {
            String fileName = @"c:\temp\" + id + @".png";
            Image imageFromFile = Image.FromFile(fileName);
            string pathToImage = string.Format(@"~\Images\{0}.png", id);
            image.Save(HttpContext.Current.Request.MapPath(pathToImage), ImageFormat.Png);
            image.Save(pathToImage, ImageFormat.Png);
        }

        // This is a stub handler for getting a group of images
        private List<ScreenShot> theImagesGiveThemToMe()
        {
            List<ScreenShot> screenShots = new List<ScreenShot>();
            ScreenShot screenShot;

            for (int i = 1; i < 9; i++){
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
    }
}
