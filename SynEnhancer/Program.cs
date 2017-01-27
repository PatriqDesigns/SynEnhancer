using System;
using System.Drawing;
using System.Windows.Forms;

namespace SynEnhancer
{

    internal class Program
    {
        
        private static void Main()
        {
            // Start the touchpad and the listener.
            var touchpad = new Touchpad();
            touchpad.OnPacket += (new TouchpadPacketListener()).OnPacket;

            // And run the application context.
            Application.Run(new TrayContext());
        }
    }

    class TrayContext : ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;

        public TrayContext()
        {
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            _notifyIcon = new NotifyIcon();
            //_notifyIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            _notifyIcon.ContextMenu = new ContextMenu(new []{ exitMenuItem });
            _notifyIcon.Visible = true;
        }

        void Exit(object sender, EventArgs e)
        {
            // We must manually tidy up and remove the icon before we exit.
            // Otherwise it will be left behind until the user mouses over.
            _notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}