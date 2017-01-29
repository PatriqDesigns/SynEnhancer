using System;
using System.Windows.Forms;

namespace SynEnhancer
{
    internal class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Start the touchpad and the listener.
            var touchpad = new Touchpad();
            touchpad.OnPacket += (new TouchpadPacketListener()).OnPacket;

            // And run the application context.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayContext());
        }
    }
}