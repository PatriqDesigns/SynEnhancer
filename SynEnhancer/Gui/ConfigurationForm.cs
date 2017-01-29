using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SynEnhancer
{
    public partial class ConfigurationForm : Form
    {
        private const string RunRegKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public ConfigurationForm()
        {
            InitializeComponent();
        }

        private void startWithWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (startWithWindows.Checked) AddApplicationToCurrentUserStartup();
            else RemoveApplicationFromCurrentUserStartup();
            startWithWindows.Checked = ApplicationOnStartUp();
        }

        private void AddApplicationToCurrentUserStartup()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(RunRegKey, true))
            {
                key?.SetValue(Text, Application.ExecutablePath);
            }
        }

        private void RemoveApplicationFromCurrentUserStartup()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(RunRegKey, true))
            {
                key?.DeleteValue(Text, false);
            }
        }

        private bool ApplicationOnStartUp()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(RunRegKey, true))
            {
                return key?.GetValue(Text) != null;
            }
        }

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            startWithWindows.Checked = ApplicationOnStartUp();
        }
    }
}