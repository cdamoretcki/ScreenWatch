using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using ScreenWatchData;

namespace ScreenShotReceiver
{
    public class ScreenShotReceiver : IScreenShotReceiver
    {
        static ScreenShotReceiver()
        {
            TextWriterTraceListener debugListener = new TextWriterTraceListener(@"c:\temp\ScreenShotReceiver.log");
            Debug.Listeners.Add(debugListener);
            Debug.AutoFlush = true;
        }

        public void Upload(ImageUpload upload)
        {
            Debug.WriteLine("ScreenShotReceiver.Upload entered");
            if (upload == null)
            {
                Debug.WriteLine("upload is null");
                throw new ArgumentNullException("upload is null");
            }
            try
            {
                MemoryStream stream = new MemoryStream(upload.ImageData);
                Image image = Bitmap.FromStream(stream);
                image.Save(@"c:\temp\servicetest.png", ImageFormat.Png);
                new ImageActions().insertImage("1", image);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            Debug.WriteLine("ScreenShotReceiver.Upload exit");
        }
    }
}
