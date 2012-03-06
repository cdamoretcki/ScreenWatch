using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using tessnet2;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Configuration;

namespace ScreenShotReceiver
{
    public class ImageAnalysis
    {
        private Tesseract tessocr = new Tesseract();

        private static ImageAnalysis _instance;

        private static Object lockObject = new Object();

        public static ImageAnalysis Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new ImageAnalysis();
                        }
                    }
                }
                return _instance;
            }
        }

        private ImageAnalysis()
        {
            tessocr.Init(ConfigurationManager.AppSettings["tessdata"], "eng", false);
        }

        /// <summary>
        /// analyse image for text and return key filtered words
        /// </summary>
        /// <param name="bmp">bitmap to be analyzed</param>
        /// <param name="confidenceFilter">confidence cutoff, 0 is certain 255 is low confidence</param>
        /// <param name="badWords">words that should trigger the filter</param>
        /// <returns></returns>
        public void ProcessImage(Bitmap bmp, string captureTime)
        {
            int confidenceFilter = 240;
            string email = "jared.tait@gmail.com";
            HashSet<string> filters = new HashSet<string>() { "bad" };

            AnalysisResult result = ProcessText(bmp, confidenceFilter, filters);

            if (result.FaultFound)
            {
                Debug.WriteLine("ScreenShotReceiver.Upload fault found in text");
                //construct the email
                string emailSubject = string.Format("At {0} ScreenWatch detected the following filtered words", captureTime);
                StringBuilder emailBody = new StringBuilder();
                emailBody.AppendLine(emailSubject);
                Debug.WriteLine(emailSubject);

                foreach (var badWord in result.FaultsFound)
                {
                    emailBody.AppendLine(badWord);
                    Debug.WriteLine(badWord);
                }
                Debug.WriteLine("In the following instances");
                foreach (var context in result.FaultedWords)
                    Debug.WriteLine(context);

                MailUtil.SendEmail(emailSubject, emailBody.ToString(), email);
                Debug.WriteLine("ScreenShotReceiver.Upload email sent for fault found in text");
            }
        }

        private AnalysisResult ProcessText(Bitmap bmp, int confidenceFilter, HashSet<string> filters)
        {
            List<Word> results = tessocr.DoOCR(bmp, Rectangle.Empty);
            AnalysisResult analysisResult = new AnalysisResult();
            //lower number for confidence is greater certainty, don't ask, i don't know why.
            foreach (var resultWord in results.Where(word => word.Confidence < confidenceFilter))
            {
                string word = resultWord.Text;
                analysisResult.AddWord(word);
                foreach (var badWord in filters)
                {
                    if (Regex.IsMatch(word.ToUpperInvariant(), badWord.ToUpperInvariant()))
                    {
                        analysisResult.AddFault(badWord, word, (int)resultWord.Confidence);
                    }
                }
            }
            return analysisResult;
        }
    }
}