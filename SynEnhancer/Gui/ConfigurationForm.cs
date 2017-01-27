using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RunRegKey, true))
            {
                key.SetValue(this.Text, Application.ExecutablePath);
            }
        }

        private void RemoveApplicationFromCurrentUserStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RunRegKey, true))
            {
                key.DeleteValue(this.Text, false);
            }
        }

        private bool ApplicationOnStartUp()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RunRegKey, true))
            {
                return key.GetValue(this.Text) != null;
            }
        }

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            startWithWindows.Checked = ApplicationOnStartUp();
        }
    }

}
