using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Image = System.Drawing.Image;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace ScreenWatchData
{
    public class ImageActions
    {
        /// <summary>
        /// Inserts an Image object
        /// </summary>
        public void insertImage(String id, Image image)
        {
            image.Save(getImagePath(id), ImageFormat.Png);
        }

        /// <summary>
        /// Returns an image object for the given id - this will eventually be used for image analysis
        /// </summary>
        /// <returns>System.Drawing.Image</returns>
        public Image getImage(String id)
        {
            String fileName = createFilePathFromId(id);
            Image image = Image.FromFile(fileName);
            return image;
        }

        public String getImagePath(String id)
        {
            // Eventually this will do something more meaningful
            string pathToImage = string.Format(@"~\Images\{0}.png", id);
            getImage(id).Save(HttpContext.Current.Request.MapPath(pathToImage), ImageFormat.Png);
            return pathToImage;
        }

        private String createFilePathFromId(String id)
        {
            // For now, hard code the path based on the ID - we need a better way to do this though
            String fileName = @"c:\ScreenWatch\Images\" + id + @".png";

            return fileName;
        }
    }
}
