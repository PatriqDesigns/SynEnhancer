using System;
using System.Windows.Forms;

namespace SynEnhancer
{
    public class TrayContext : ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;

        public TrayContext()
        {
            var configurationItem = new MenuItem("Configuration", ShowConfiguration);
            var exitMenuItem = new MenuItem("Exit", Exit);

            _notifyIcon = new NotifyIcon
            {
                Icon = _configWindow.Icon,
                Text = "SynEnhancer",
                ContextMenu = new ContextMenu(new[] {configurationItem, exitMenuItem}),
                Visible = true
            };
            _notifyIcon.Click += ShowConfiguration;
        }

        private readonly ConfigurationForm _configWindow = new ConfigurationForm();

        private void ShowConfiguration(object sender, EventArgs e)
        {
            if (_configWindow.Visible) _configWindow.Activate();
            else _configWindow.ShowDialog();
        }

        private void Exit(object sender, EventArgs e)
        {
            // We must manually tidy up and remove the icon before we exit.
            // Otherwise it will be left behind until the user mouses over.
            _notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}