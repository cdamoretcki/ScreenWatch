using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using System.Drawing;
using System.Configuration;
using System.Drawing.Imaging;

namespace WatcherClient
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            TextWriterTraceListener debugListener = new TextWriterTraceListener(@"c:\temp\WatcherClient.log");
            Debug.Listeners.Add(debugListener);
            Debug.AutoFlush = true;
            Debug.WriteLine("------------------------------------------------------------------------------");
            Debug.WriteLine("started client");

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Monitor());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

    }
}
