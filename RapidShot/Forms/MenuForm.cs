using RapidShot;
using RapidShot.Input;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RapidSnap.Forms
{
    public partial class MenuForm : Form
    {
        private OptionForm optionForm;
        private SnippingTool snippingTool;
        private Container container;
        private NotifyIcon notifyIcon;

        public KeyboardHook KeyboardHook { get; private set; }

        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        public static void MinimizeFootprint() => EmptyWorkingSet(Process.GetCurrentProcess().Handle);

        public MenuForm()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.IconLogoDark;
            this.Text = Properties.Resources.StringApplicationName;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            KeyboardHook = new KeyboardHook();
            optionForm = new OptionForm(this);
            optionForm.Hide();
            optionForm.LoadSettings();

            InitializeNotifyIcon();

            MinimizeFootprint();
        }

        public void RegisterHotkeys()
        {
            optionForm.HKSnap.Register(OnSnap);
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
            contextMenuStrip.Items.Add("Notify", null, OnUpdate); 
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add("Exit", null, OnApplicationExit);

            notifyIcon.ContextMenuStrip = contextMenuStrip;

            notifyIcon.DoubleClick += OnDoubleClick;
            notifyIcon.BalloonTipClicked += OnOption;
        }

        private void OnSnap(object sender, EventArgs e)
        {
            if (snippingTool?.IsSnapping ?? false)
                return;

            snippingTool = new SnippingTool();
            snippingTool.TakeSnap();
        }

        private void OnOption(object sender, EventArgs e)
            => optionForm.Show();

        private void OnUpdate(object sender, EventArgs e)
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
    }
}
