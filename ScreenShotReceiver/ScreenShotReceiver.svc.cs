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
using System.Threading;
using System.ServiceModel.Activation;

namespace ScreenShotReceiver
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)] 
    public class ScreenShotReceiver : IScreenShotReceiver
    {
        static ScreenShotReceiver()
        {
            TextWriterTraceListener debugListener = new TextWriterTraceListener(@"c:\temp\ScreenShotReceiver.log");
            Debug.Listeners.Add(debugListener);
            Debug.AutoFlush = true;
        }

        /// <summary>
        /// Upload your screenshot here
        /// </summary>
        /// <param name="upload"></param>
        public void Upload(ImageUpload upload)
        {
            Debug.WriteLine("ScreenShotReceiver.Upload entered " + DateTime.Now);
            ImageAnalysis.Init();
            if (upload == null)
            {
                Debug.WriteLine("upload is null");
                throw new ArgumentNullException("upload is null");
            }
            //rethread here so that the client is never waiting for analysis completion.
            new Thread(() => RethreadedUpload(upload)).Start();            
        }

        private void RethreadedUpload(ImageUpload incomingUpload)
        {
            Debug.WriteLine("ScreenShotReceiver.RethreadedUpload entered " + DateTime.Now);
            try
            {
                ImageUpload upload = (ImageUpload)incomingUpload;

                //TODO: connect to datalayer for triggers
                ScreenShotActions dataLayer = new ScreenShotActions();
                var textTriggers = dataLayer.getAllTextTriggers();

                //load image
                using (MemoryStream stream = new MemoryStream(upload.ImageData))
                using (Image image = Bitmap.FromStream(stream))
                {
                    image.Save(@"c:\temp\servicetest.png", ImageFormat.Png);

                    //Analyze the image
                    ImageAnalysis.ProcessImage((Bitmap)image, upload.CaptureTime);

                    //send image to database
                    ScreenShot screenShot = new ScreenShot();
                    screenShot.image = image;
                    screenShot.timeStamp = DateTime.Parse(upload.CaptureTime);
                    screenShot.user = upload.UserID;
                    
                    dataLayer.insertScreenShot(screenShot);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                Debug.WriteLine("ScreenShotReceiver.RethreadedUpload exit " + DateTime.Now);
            }
        }
    }
}
