﻿using System;
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
                //connect to datalayer for triggers
                ScreenShotActions dataLayer = new ScreenShotActions();
                HashSet<string> triggers = new HashSet<string>();
                int confidence = 255;
                //TODO: talk Chris about confidence of trigger and the associated email being outside of the list
                foreach (var triggerInfo in dataLayer.getTextTriggers())
                {
                    if (!triggers.Contains(triggerInfo.tokenString))
                    {
                        triggers.Add(triggerInfo.tokenString);
                    }
                }

                //load image
                MemoryStream stream = new MemoryStream(upload.ImageData);
                Image image = Bitmap.FromStream(stream);
                image.Save(@"c:\temp\servicetest.png", ImageFormat.Png); //TODO: remove this eventually

                //Analyze the image
                ImageAnalysis analyzer = ImageAnalysis.Instance;
                Debug.WriteLine("ScreenShotReceiver.Upload retrieved analyzer");
                analyzer.ProcessImage((Bitmap)image, upload.CaptureTime, confidence, triggers);

                //send image to database
                ScreenShot screenShot = new ScreenShot();
                screenShot.image = image;
                dataLayer.insertImage(screenShot);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                Debug.WriteLine("ScreenShotReceiver.Upload exit");
            }
        }
    }
}
