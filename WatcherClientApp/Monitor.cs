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
using WatcherClient.ScreenShotReceiverReference;
using System.IO;
using System.ComponentModel;

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
            Bitmap bm = new Bitmap(new Bitmap("icon.png"), new Size(32, 32));
            components = new System.ComponentModel.Container();
            trayIcon = new NotifyIcon(components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = Icon.FromHandle(bm.GetHicon()),
                Text = "Hi Friend",
                Visible = true 
            };

            int periodInMS = int.Parse(ConfigurationManager.AppSettings["period"]) * 1000;
            timer = new System.Timers.Timer(periodInMS);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(ScanImage);
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Start();
        }

        public void ScanImage(object source, ElapsedEventArgs eventArgs)
        {
            Debug.WriteLine("Monitor.ScanImage enter {0}", DateTime.Now);
            try
            {
                int screenWidth = Screen.GetBounds(new Point(0, 0)).Width;
                int screenHeight = Screen.GetBounds(new Point(0, 0)).Height;
                using (Bitmap screenShot = new Bitmap(screenWidth, screenHeight))
                using (Graphics gfx = Graphics.FromImage(screenShot))
                {
                    gfx.CopyFromScreen(0, 0, 0, 0, new Size(screenWidth, screenHeight));
                    //screenShot.Save(@"c:\temp\clienttest.png", ImageFormat.Png);
                    MemoryStream stream = new MemoryStream();
                    screenShot.Save(stream, ImageFormat.Png);
                    ImageUpload image = new ImageUpload();
                    image.ImageData = stream.ToArray();
                    new ScreenShotReceiverClient().Upload(image);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("{0} - {1}", e, DateTime.Now);                
            }
            Debug.WriteLine("Monitor.ScanImage exit {0}", DateTime.Now);
            Debug.Flush();
        }
    }
}