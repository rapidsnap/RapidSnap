using Microsoft.Win32;
using RapidSnap.Input;
using RapidSnap.Properties;
using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace RapidSnap.Forms
{
    public partial class OptionForm : Form
    {
        private MenuForm menuForm;

        public readonly Hotkey HKSnap;

        public OptionForm(MenuForm menuForm)
        {
            InitializeComponent();

            this.menuForm = menuForm;
            HKSnap = new Hotkey(menuForm.KeyboardHook, btn_hkSnap, menuForm.OnSnap);

            cb_autoStart.CheckedChanged += OnAutoStartToggle;

            LoadVersion();
            LoadSettings();
        }

        private void LoadVersion()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                lbl_version.Text = $"v{ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4)}";
                return;
            }

            lbl_version.Text = $"v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
        }

        public void LoadSettings()
        {
            cb_autoStart.Checked = Settings.Default.Autostart;
            cb_autoUpdate.Checked = Settings.Default.AutoUpdate;
            rb_clipboard.Checked = Settings.Default.SaveToClipboard;
            rb_disk.Checked = Settings.Default.SaveToDisk;
            HKSnap.Load(Settings.Default.HotkeySnap);
        }

        private void SaveSettings()
        {
            Settings.Default.Autostart = cb_autoStart.Checked;
            Settings.Default.AutoUpdate = cb_autoUpdate.Checked;
            Settings.Default.SaveToClipboard = rb_clipboard.Checked;
            Settings.Default.SaveToDisk = rb_disk.Checked;
            Settings.Default.HotkeySnap = HKSnap.Save();
            Settings.Default.Save();
        }

        private void OnAutoStartToggle(object sender, EventArgs e)
        {
            var rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if ((sender as CheckBox).Checked)
                rk.SetValue("RapidSnap", Application.ExecutablePath);
            else
                rk.DeleteValue("RapidSnap", false);
        }

        private void OnCloseButtonClick(object sender, EventArgs e)
        {
            SaveSettings();
            this.Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            e.Cancel = true;

            SaveSettings();
            this.Hide();
        }

        private void OnUpdateButtonClick(object sender, EventArgs e)
        {
            UpdateCheckInfo info = null;

            try
            {
                info = ApplicationDeployment.CurrentDeployment.CheckForDetailedUpdate();
            }
            catch (Exception)
            {
                return;
            }

            if (!info.UpdateAvailable)
            {
                MessageBox.Show("Hooray! Rapid Snap is up-to-date.");
                return;
            }

            if (!info.IsUpdateRequired)
            {
                DialogResult result = MessageBox.Show("An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel);

                if (result != DialogResult.OK)
                    return;
            }
            else
            {
                MessageBox.Show("This application has detected a mandatory update from your current " +
                    "version to version " + info.MinimumRequiredVersion.ToString() +
                    ". The application will now install the update and restart.",
                    "Update Available", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            var success = menuForm.UpdateApplication();

            if (!success)
                MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later.");

            MessageBox.Show("The application has been upgraded, and will now restart.");
            menuForm.RestartApplication();
        }
    }
}
