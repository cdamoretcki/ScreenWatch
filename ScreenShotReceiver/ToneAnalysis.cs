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

        /// <summary>
        /// processes an image and a list of tone triggers returns fired triggers in a dictionary sorted by the receiving email
        /// </summary>
        /// <param name="img"></param>
        /// <param name="triggers"></param>
        /// <returns></returns>
        public static Dictionary<string, List<ToneTrigger>> ProcessTone(Bitmap img, List<ToneTrigger> triggers)
        {
            var firedTriggers = new Dictionary<string, List<ToneTrigger>>();
            foreach (var trigger in triggers)
            {
                var averages = CalculateAverageColor(img);
                int cellsTriggered = averages.Count(color => IsBetween(trigger.lowerColorBound, color, trigger.upperColorBound));
                if ((cellsTriggered / averages.Count) > 1 - trigger.sensitivity)
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

        private static bool IsBetween(Color color1, Color average, Color color2)
        {
            return (IsBetween(color1.R, average.R, color2.R)
                        && IsBetween(color1.G, average.G, color2.G)
                        && IsBetween(color1.B, average.B, color2.B));
        }

        private static bool IsBetween(byte limit1, byte mid, byte limit2)
        {
            Debug.WriteLine("ToneAnalysis.IsBetween(limit1={0} image={1} limit2={2})", limit1, mid, limit2);
            return (limit1 <= mid && mid <= limit2) || (limit2 <= mid && mid <= limit1);
        }

        public static List<Color> CalculateAverageColor(Bitmap img)
        {
            int width = img.Width;
            int height = img.Height;
            long[] totals = new long[] { 0, 0, 0 };
            int bppModifier = img.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb ? 3 : 4; //only 32 and 24 bit images

            BitmapData srcData = img.LockBits(new System.Drawing.Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, img.PixelFormat);
            int size = srcData.Stride * img.Height;
            byte[] bytes = new byte[size];
            Marshal.Copy(srcData.Scan0, bytes, 0, bytes.Length);
            img.UnlockBits(srcData);
            int stride = srcData.Stride;

            return CalculateAverageColor(bytes, height, width, size, stride, bppModifier);
        }

        public static List<Color> CalculateAverageColor(byte[] bytes, int height, int width, int size, int stride, int bppModifier)
        {
            int red = 0;
            int green = 0;
            int blue = 0;
            int minDiversion = 5; // drop pixels that do not differ by at least minDiversion between color values (white, gray or black)

            int xCellCount = 10;
            int yCellCount = 10;
            int cellWidth = width / xCellCount;
            int cellHeight = height / yCellCount;
            RGBHolder[,] cells = new RGBHolder[xCellCount, yCellCount];
            List<Color> colors = new List<Color>();

            for (int yCell = 0; yCell < yCellCount; yCell++)
            {
                int yCellLoc = yCell * cellHeight;
                for (int xCell = 0; xCell < xCellCount; xCell++)
                {
                    int xCellLoc = xCell * cellWidth;
                    int droppedPixels = 0;
                    RGBHolder holder = new RGBHolder();

                    for (int y = 0; y < cellHeight; y++)
                    {
                        for (int x = 0; x < cellWidth; x++)
                        {
                            int idx = ((y + yCellLoc) * stride) + ((x + xCellLoc) * bppModifier);
                            red = bytes[idx + 2];
                            green = bytes[idx + 1];
                            blue = bytes[idx];
                            if (Math.Abs(red - green) > minDiversion || Math.Abs(red - blue) > minDiversion || Math.Abs(green - blue) > minDiversion)
                            {
                                holder.Red += red;
                                holder.Green += green;
                                holder.Blue += blue;
                            }
                            else
                            {
                                droppedPixels++;
                            }
                        }
                    }
                    holder.Count = cellWidth * cellHeight - droppedPixels;
                    cells[xCell, yCell] = holder;
                    colors.Add(holder.AverageColor);
                }
            }

            return colors;
        }

        internal class RGBHolder
        {
            internal long Red { get; set; }
            internal long Green { get; set; }
            internal long Blue { get; set; }
            internal int Count { get; set; }

            internal RGBHolder()
            {
                Red = 0;
                Green = 0;
                Blue = 0;
            }

            internal Color AverageColor
            {
                get
                {
                    if (Count == 0)
                    {
                        return Color.Black;
                    }
                    int avgR = (int)(Red / Count);
                    int avgG = (int)(Green / Count);
                    int avgB = (int)(Blue / Count);

                    return Color.FromArgb(avgR, avgG, avgB);
                }
            }
        }
    }
}