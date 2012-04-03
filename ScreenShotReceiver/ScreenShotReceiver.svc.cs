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
        #region static constructor

        static ScreenShotReceiver()
        {
            TextWriterTraceListener debugListener = new TextWriterTraceListener(@"c:\temp\ScreenShotReceiver.log");
            Debug.Listeners.Add(debugListener);
            Debug.AutoFlush = true;
        }


        #endregion

        /// <summary>
        /// Upload your screenshot here
        /// </summary>
        /// <param name="upload">image data and meta data</param>
        public void Upload(ImageUpload upload)
        {
            Debug.WriteLine("ScreenShotReceiver.Upload entered " + DateTime.Now);
            TextAnalysis.Init();
            if (upload == null)
            {
                Debug.WriteLine("upload is null");
                throw new ArgumentNullException("upload is null");
            }
            //rethread here so that the client is never waiting for analysis completion.
            new Thread(() => RethreadedUpload(upload)).Start();
        }

        #region private methods

        private void RethreadedUpload(ImageUpload upload)
        {
            Debug.WriteLine("ScreenShotReceiver.RethreadedUpload entered {0}", DateTime.Now);
            try
            {
                ScreenShotDataAdapter dataLayer = new ScreenShotDataAdapter();

                //only process if the user is monitored
                if (dataLayer.GetUserByName(upload.UserID).isMonitored)
                {
                    //load image
                    using (MemoryStream stream = new MemoryStream(upload.ImageData))
                    using (Image image = Bitmap.FromStream(stream))
                    {
                        //Analyze the image for to see if it violates any triggers
                        ProcessImage((Bitmap)image, upload.UserID, upload.CaptureTime, dataLayer);

                        //send image to database
                        dataLayer.SaveImage(image, upload.UserID, upload.CaptureTime);
                    }
                }
                else
                {
                    Debug.WriteLine("ScreenShotReceiver.RethreadedUpload not monitoring " + upload.UserID);
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

        private static void ProcessImage(Bitmap image, string user, string captureTime, ScreenShotDataAdapter dataLayer)
        {
            Dictionary<string, List<ToneTrigger>> tonesTriggered = ToneAnalysis.ProcessTone(image, dataLayer.GetToneTriggers(user));
            foreach (var firedToneTrigger in tonesTriggered)
            {
                Debug.WriteLine("ScreenShotReceiver.Upload triggered tone");
                //construct the email
                string emailSubject = string.Format("At {0} ScreenWatch detected a specified tone trigger.", captureTime);
                StringBuilder emailBody = new StringBuilder();
                emailBody.AppendLine(emailSubject);
                Debug.WriteLine(emailSubject);

                foreach (ToneTrigger badTone in firedToneTrigger.Value)
                {
                    Debug.WriteLine("Tone {0} triggered for {1}", badTone.id, badTone.userName);
                }

                MailUtil.SendEmail(emailSubject, emailBody.ToString(), firedToneTrigger.Key, image);
                Debug.WriteLine("ScreenShotReceiver.Upload email sent for fault found in text");
            }

            TextAnalysisResult textResults = TextAnalysis.ProcessText(image, dataLayer.GetTextTriggers(user));
            foreach (var emailFault in textResults.FaultedWordsByEmail)
            {
                Debug.WriteLine("ScreenShotReceiver.Upload trigger found in text");
                //construct the email
                string emailSubject = string.Format("At {0} ScreenWatch detected the following filtered words", captureTime);
                StringBuilder emailBody = new StringBuilder();
                emailBody.AppendLine(emailSubject);
                Debug.WriteLine(emailSubject);

                foreach (var badWord in emailFault.Value.Keys)
                {
                    emailBody.AppendLine(badWord);
                    Debug.WriteLine(badWord);
                }
                Debug.WriteLine("In the following instances");
                foreach (var context in emailFault.Value.Values)
                    Debug.WriteLine(context);

                MailUtil.SendEmail(emailSubject, emailBody.ToString(), emailFault.Key, image);
                Debug.WriteLine("ScreenShotReceiver.Upload email sent for fault found in text");
            }
        }

        #endregion
    }
}
