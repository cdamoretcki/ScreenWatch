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
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ScreenShotReceiver
{
    public class ImageAnalysis
    {
        private static object lockObj = new object();

        private static string configPath = null;

        /// <summary>
        /// Init() needs to be called in the context of the thread used to invoke the service where 
        /// the current HttpContext exists.  Otherwise the current context will be null.
        /// </summary>
        public static void Init()
        {
            if (configPath == null)
            {
                configPath = HttpContext.Current.Request.MapPath("~/bin/tessdata");
                Debug.WriteLine(configPath);
            }
        }

        /// <summary>
        /// analyse image for text and return key filtered words
        /// </summary>
        /// <param name="bmp">bitmap to be analyzed</param>
        /// <param name="confidenceFilter">confidence cutoff, 0 is certain 255 is low confidence</param>
        /// <param name="badWords">words that should trigger the filter</param>
        /// <returns></returns>
        /// 
        public static void ProcessImage(Bitmap bmp, string captureTime)
        {
            int confidenceFilter = 240;
            string email = "jared.tait@gmail.com";
            HashSet<string> filters = new HashSet<string>() { "bad" };
            Color upperOrange = Color.FromArgb(-1150171);
            Color lowerOrange = Color.FromArgb(-3637420);


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

        private static bool ProcessTone(Bitmap img, Color color1, Color color2)
        {
            Color average = CalculateAverageColor(img);
            return IsBetween(color1.R, average.R, color2.R)
                && IsBetween(color1.G, average.G, color2.G)
                && IsBetween(color1.B, average.B, color2.B);
        }

        private static bool IsBetween(byte limit1, byte mid, byte limit2)
        {
            return (limit1 <= mid && mid <= limit2) || (limit2 <= mid && mid <= limit1);
        }

        private static System.Drawing.Color CalculateAverageColor(Bitmap img)
        {
            int width = img.Width;
            int height = img.Height;
            int red = 0;
            int green = 0;
            int blue = 0;
            int minDiversion = 15; // drop pixels that do not differ by at least minDiversion between color values (white, gray or black)
            int dropped = 0; // keep track of dropped pixels
            long[] totals = new long[] { 0, 0, 0 };
            int bppModifier = img.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb ? 3 : 4; // cutting corners, will fail on anything else but 32 and 24 bit images

            BitmapData srcData = img.LockBits(new System.Drawing.Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, img.PixelFormat);
            int size = srcData.Stride * img.Height;
            byte[] bytes = new byte[size];
            Marshal.Copy(srcData.Scan0, bytes, 0, bytes.Length);
            img.UnlockBits(srcData);
            int stride = srcData.Stride;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int idx = (y * stride) + x * bppModifier;
                    red = bytes[idx + 2];
                    green = bytes[idx + 1];
                    blue = bytes[idx];
                    if (Math.Abs(red - green) > minDiversion || Math.Abs(red - blue) > minDiversion || Math.Abs(green - blue) > minDiversion)
                    {
                        totals[2] += red;
                        totals[1] += green;
                        totals[0] += blue;
                    }
                    else
                    {
                        dropped++;
                    }
                }
            }

            int count = width * height - dropped;
            int avgR = (int)(totals[2] / count);
            int avgG = (int)(totals[1] / count);
            int avgB = (int)(totals[0] / count);

            return System.Drawing.Color.FromArgb(avgR, avgG, avgB);
        }

        private static AnalysisResult ProcessText(Bitmap bmp, int confidenceFilter, HashSet<string> filters)
        {
            List<Word> results;

            //perform OCR thread safe :/
            Stopwatch watch = new Stopwatch();
            watch.Start();
            lock (lockObj)
            {
                using (Tesseract tessocr = new Tesseract())
                {
                    tessocr.Init(configPath, "eng", false);
                    results = tessocr.DoOCR(bmp, Rectangle.Empty);
                }
            }
            watch.Stop();
            Debug.WriteLine("ImageAnalysis.ProcessText OCR took {0}ms", watch.ElapsedMilliseconds);

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