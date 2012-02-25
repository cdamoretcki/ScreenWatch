using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Timers;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.IO;
using System.ComponentModel;
using WatcherClient.ScreenShotReceiverReference;

namespace WatcherClient
{
    sealed class Monitor : ApplicationContext
    {
        #region instance members

        private System.Timers.Timer timer;

        private NotifyIcon trayIcon;

        private Container components;

        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public Monitor()
        {
            components = new System.ComponentModel.Container();
            trayIcon = new NotifyIcon(components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = Icon.ExtractAssociatedIcon("icon.ico"),
                Text = "Hi Friend",
                Visible = true
            };

            int periodInMS = int.Parse(ConfigurationManager.AppSettings["period"]) * 1000;
            Debug.WriteLine("Scanning every " + periodInMS + " seconds");
            timer = new System.Timers.Timer(periodInMS);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(ScanImage);
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Start();
        }

        public void ScanImage(object source, ElapsedEventArgs eventArgs)
        {
            Debug.WriteLine("Monitor.ScanImage enter " + DateTime.Now);
            try
            {
                int screenWidth = Screen.GetBounds(new Point(0, 0)).Width;
                int screenHeight = Screen.GetBounds(new Point(0, 0)).Height;
                using (Bitmap screenShot = new Bitmap(screenWidth, screenHeight))
                using (Graphics gfx = Graphics.FromImage(screenShot))
                {
                    gfx.CopyFromScreen(0, 0, 0, 0, new Size(screenWidth, screenHeight));
                    //screenShot.Save(@"c:\temp\clienttest.png", ImageFormat.Png);
                    ImageUpload image = new ImageUpload();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        screenShot.Save(stream, ImageFormat.Png);
                        image.ImageData = stream.ToArray();
                    }
                    image.CaptureTime = DateTime.Now.ToString();
                    new ScreenShotReceiverClient().Upload(image);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(DateTime.Now);
                Debug.WriteLine(e);
            }
            Debug.WriteLine("Monitor.ScanImage exit " + DateTime.Now);
            Debug.Flush();
        }
    }
}