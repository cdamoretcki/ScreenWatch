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
using ScreenWatchData;

namespace ScreenShotReceiver
{
    public class TextAnalysis
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

        public static TextAnalysisResult ProcessText(Bitmap bmp, List<TextTrigger> filters)
        {
            double confidenceFilter = double.Parse(ConfigurationManager.AppSettings["textFilterConfidence"]);

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
            Debug.WriteLine("TextAnalysis.ProcessText OCR took {0}ms", watch.ElapsedMilliseconds);

            TextAnalysisResult analysisResult = new TextAnalysisResult();
            //lower number for confidence is greater certainty, don't ask, i don't know why.
            foreach (var resultWord in results.Where(word => word.Confidence < confidenceFilter))
            {
                string word = resultWord.Text;
                foreach (var trigger in filters)
                {
                    if (Regex.IsMatch(word.ToUpperInvariant(), trigger.triggerString.ToUpperInvariant()))
                    {
                        analysisResult.AddFault(trigger.userEmail, trigger.triggerString, word, (int)resultWord.Confidence);
                    }
                }
            }
            return analysisResult;
        }
    }
}