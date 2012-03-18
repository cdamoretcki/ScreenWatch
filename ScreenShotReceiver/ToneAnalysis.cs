using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ScreenWatchData;
using System.Diagnostics;

namespace ScreenShotReceiver
{
    public class ToneAnalysis
    {
        public static Dictionary<string, List<ToneTrigger>> ProcessTone(Bitmap img, List<ToneTrigger> triggers)
        {
            var firedTriggers = new Dictionary<string, List<ToneTrigger>>();
            foreach (var trigger in triggers)
            {
                Color color1 = trigger.lowerColorBound;
                Color color2 = trigger.upperColorBound;
                Color average = CalculateAverageColor(img, 100);

                if (IsBetween(color1.R, average.R, color2.R)
                    && IsBetween(color1.G, average.G, color2.G)
                    && IsBetween(color1.B, average.B, color2.B))
                {
                    string email = trigger.userEmail.ToUpper();
                    if (!firedTriggers.ContainsKey(email))
                    {
                        firedTriggers[email] = new List<ToneTrigger>();
                    }
                    firedTriggers[email].Add(trigger);
                }
            }
            return firedTriggers;
        }

        private static bool IsBetween(byte limit1, byte mid, byte limit2)
        {
            Debug.WriteLine("ToneAnalysis.IsBetween(limit1={0} image={1} limit2={2})", limit1, mid, limit2);
            return (limit1 <= mid && mid <= limit2) || (limit2 <= mid && mid <= limit1);
        }

        private static System.Drawing.Color CalculateAverageColor(Bitmap img, int sensitivity)
        {
            int width = img.Width;
            int height = img.Height;
            int red = 0;
            int green = 0;
            int blue = 0;
            int minDiversion = 15; // drop pixels that do not differ by at least minDiversion between color values (white, gray or black)
            int droppedPixels = 0;
            long[] totals = new long[] { 0, 0, 0 };
            int bppModifier = img.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb ? 3 : 4; //only 32 and 24 bit images

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
                        droppedPixels++;
                    }
                }
            }

            int count = width * height - droppedPixels;
            int avgR = (int)(totals[2] / count);
            int avgG = (int)(totals[1] / count);
            int avgB = (int)(totals[0] / count);

            return System.Drawing.Color.FromArgb(avgR, avgG, avgB);
        }
    }
}