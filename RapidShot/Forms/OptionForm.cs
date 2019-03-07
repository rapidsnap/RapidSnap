using Microsoft.Win32;
using RapidShot.Input;
using RapidSnap.Input;
using RapidSnap.Properties;
using System;
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
            
            HKSnap = new Hotkey(menuForm.KeyboardHook, btn_hkSnap);

            cb_autoStart.CheckedChanged += OnAutoStartToggle;
        }

        public void LoadSettings()
        {
            cb_autoStart.Checked = Settings.Default.Autostart;
            cb_autoUpdate.Checked = Settings.Default.AutoUpdate;
            rb_clipboard.Checked = Settings.Default.SaveToClipboard;
            rb_disk.Checked = Settings.Default.SaveToDisk;
            HKSnap.Load(Settings.Default.HotkeySnap);

            menuForm.RegisterHotkeys();
        }

        private void SaveSettings()
        {
            Settings.Default.Autostart = cb_autoStart.Checked;
            Settings.Default.AutoUpdate = cb_autoUpdate.Checked;
            Settings.Default.SaveToClipboard = rb_clipboard.Checked;
            Settings.Default.SaveToDisk = rb_disk.Checked;
            Settings.Default.HotkeySnap = HKSnap.Save();
            Settings.Default.Save();

            menuForm.RegisterHotkeys();
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
    }
}
