using RapidShot;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RapidSnap.Forms
{
    public partial class MenuForm : Form
    {
        private SnippingTool _snippingTool;
        private Container _container;
        private NotifyIcon _notifyIcon;
        private Keyboard _keyboard;
        
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

            InitializeNotifyIcon();

            _keyboard = new Keyboard();
            _keyboard.AddHotKey(Keyboard.ModifierKeys.Alt, Keys.OemPipe, OnSnap);

            MinimizeFootprint();
        }

        private void InitializeNotifyIcon()
        {
            _container = new Container();

            _notifyIcon = new NotifyIcon(_container)
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

            _notifyIcon.ContextMenuStrip = contextMenuStrip;

            _notifyIcon.DoubleClick += OnDoubleClick;
            _notifyIcon.BalloonTipClicked += OnOption;
        }

        private void OnSnap(object sender, EventArgs e)
        {
            if (_snippingTool?.IsSnapping ?? false)
                return;

            _snippingTool = new SnippingTool();
            _snippingTool.TakeSnap();
        }

        private void OnOption(object sender, EventArgs e) => new OptionForm().Show();

        private void OnUpdate(object sender, EventArgs e) => _notifyIcon.ShowBalloonTip(1000, "New Version", "Click here to get it now!", ToolTipIcon.Info);

        private void OnDoubleClick(object sender, EventArgs e) => new OptionForm().Show();
        
        private void OnApplicationExit(object sender, EventArgs e)
        {
            _notifyIcon.Dispose();
            _keyboard.Dispose();
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
