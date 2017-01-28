using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynEnhancer
{
    class TrayContext : ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;

        public TrayContext()
        {
            MenuItem configurationItem = new MenuItem("Configuration", new EventHandler(ShowConfiguration));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = configWindow.Icon;
            _notifyIcon.Text = "SynEnhancer";
            _notifyIcon.ContextMenu = new ContextMenu(new[] { configurationItem, exitMenuItem });
            _notifyIcon.Visible = true;
        }

        ConfigurationForm configWindow = new ConfigurationForm();
        void ShowConfiguration(object sender, EventArgs e)
        {
            if (configWindow.Visible) configWindow.Activate();
            else configWindow.ShowDialog();
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
