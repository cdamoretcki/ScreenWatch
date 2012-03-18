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

        private ContextMenu menu;

        #endregion

        #region constructor

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public Monitor()
        {
            components = new System.ComponentModel.Container();
            menu = new ContextMenu(new MenuItem[] 
            { 
                new MenuItem("Nothing to see here", new System.EventHandler(Kill)) 
            });
            trayIcon = new NotifyIcon(components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = Icon.ExtractAssociatedIcon("icon.ico"),
                Text = "Dr. Barb is watchin'",
                Visible = true,
                ContextMenu = menu
            };

            int periodInMS = int.Parse(ConfigurationManager.AppSettings["period"]) * 1000;
            Debug.WriteLine("Scanning every " + periodInMS + " seconds");
            timer = new System.Timers.Timer(periodInMS);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(ScanImage);
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Start();
        }

        #endregion

        #region methods

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

        public void Kill(Object sender, System.EventArgs e)
        {
            Debug.WriteLine("Monitor.Kill " + DateTime.Now);
            Debug.Flush();
            Application.Exit();
        }

        #region overriden methods

        protected override void OnMainFormClosed(object sender, EventArgs e)
        {
            Debug.WriteLine("Monitor.OnMainFormClosed " + DateTime.Now);
            Debug.Flush();
            base.OnMainFormClosed(sender, e);
        }

        protected override void ExitThreadCore()
        {
            Debug.WriteLine("Monitor.ExitThreadCore " + DateTime.Now);
            Debug.Flush();
            base.ExitThreadCore();
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                Debug.WriteLine("Monitor.Dispose enter " + DateTime.Now);
                if (disposing)
                {
                    timer.Stop();
                    timer.Dispose();
                    trayIcon.Dispose();
                    components.Dispose();
                    menu.Dispose();
                }
                base.Dispose(disposing);
                Debug.WriteLine("Monitor.Dispose exit " + DateTime.Now);
            }
            finally
            {
                Debug.Flush();
            }
        }

        #endregion

        #endregion
    }
}