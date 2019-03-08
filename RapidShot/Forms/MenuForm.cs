using RapidShot;
using RapidShot.Input;
using RapidSnap.Properties;
using System;
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace RapidSnap.Forms
{
    public partial class MenuForm : Form
    {
        private OptionForm optionForm;
        private SnippingTool snippingTool;
        private Container container;
        private NotifyIcon notifyIcon;
        private Thread updateThread;

        public KeyboardHook KeyboardHook { get; private set; }

        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        public static void MinimizeFootprint()
            => EmptyWorkingSet(Process.GetCurrentProcess().Handle);

        public MenuForm()
        {
            InitializeComponent();

            if (Settings.Default.AutoUpdate && IsUpdateAvailable())
            {
                UpdateApplication();
                RestartApplication();
            }

            this.Icon = Resources.IconLogoDark;
            this.Text = Resources.StringApplicationName;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            KeyboardHook = new KeyboardHook();
            optionForm = new OptionForm(this);
            optionForm.Hide();

            InitializeNotifyIcon();

            updateThread = new Thread(new ThreadStart(() => {
                if (IsUpdateAvailable())
                    OnUpdate();

                Thread.Sleep(3600000); // 1 hour
            }));

            MinimizeFootprint();
        }

        private void InitializeNotifyIcon()
        {
            container = new Container();

            notifyIcon = new NotifyIcon(container)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = Properties.Resources.IconLogoLight,
                Text = Properties.Resources.StringApplicationName,
                Visible = true
            };

            var contextMenuStrip = new ContextMenuStrip();

            var menuStripTitle = new ToolStripMenuItem()
            {
                Image = Properties.Resources.IconLogoDark.ToBitmap(),
                Text = Properties.Resources.StringApplicationName,
                Enabled = false
            };

            contextMenuStrip.Items.Add(menuStripTitle);
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add("Snap", null, OnSnap);
            contextMenuStrip.Items.Add("Options", null, OnOption);
            contextMenuStrip.Items.Add("Update", null, (s, e) => { if (IsUpdateAvailable()) OnUpdate(); });
            contextMenuStrip.Items.Add("(╯°□°）╯︵ ┻━┻", null, OnTableFlip);
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add("Exit", null, OnApplicationExit);

            notifyIcon.ContextMenuStrip = contextMenuStrip;

            notifyIcon.DoubleClick += OnDoubleClick;
            notifyIcon.BalloonTipClicked += OnOption;
        }

        private void OnTableFlip(object sender, EventArgs e)
        {
            notifyIcon.ShowBalloonTip(1000, $"Tabel Flip #{++Settings.Default.TableFlips}", "┬──┬◡ﾉ(° -°ﾉ)", ToolTipIcon.Info);
            Settings.Default.Save();
        }

        public void OnSnap(object sender, EventArgs e)
        {
            if (snippingTool?.IsSnapping ?? false)
                return;

            snippingTool = new SnippingTool();
            snippingTool.TakeSnap();
        }

        private void OnOption(object sender, EventArgs e)
            => optionForm.Show();

        private void OnUpdate()
            => notifyIcon.ShowBalloonTip(1000, "Newer Version available", "Click here to get it now!", ToolTipIcon.Info);

        private void OnDoubleClick(object sender, EventArgs e)
            => OnOption(sender, e);
            
        private void OnApplicationExit(object sender, EventArgs e)
        {
            notifyIcon.Dispose();
            KeyboardHook.Dispose();
            Dispose();
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            e.Cancel = true;

            this.Hide();
        }

        public bool IsUpdateAvailable()
        {
            UpdateCheckInfo info = null;

            try
            {
                info = ApplicationDeployment.CurrentDeployment.CheckForDetailedUpdate();
            }
            catch (Exception)
            {
                return false;
            }

            return info.UpdateAvailable;
        }

        public bool UpdateApplication()
        {
            try
            {
                ApplicationDeployment.CurrentDeployment.Update();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void RestartApplication()
        {
            var Info = new ProcessStartInfo
            {
                Arguments = "/C ping 127.0.0.1 -n 2 && \"" + Application.ExecutablePath + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            };

            notifyIcon.Dispose();
            KeyboardHook.Dispose();
            optionForm.Dispose();
            Dispose();

            Process.Start(Info);
            Application.Exit();
        }
    }
}
