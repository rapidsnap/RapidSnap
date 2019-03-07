using RapidShot.Input;
using RapidSnap.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static RapidShot.Input.KeyboardHook;

namespace RapidSnap.Forms
{
    public partial class HotkeyRecorderForm : Form
    {
        private readonly List<ModifierKeys> modifierKeys = new List<ModifierKeys>();
        private Keys triggerKey = Keys.None;

        public bool IsRecording = true;

        public event EventHandler<HotkeyEventArgs> SetHotkey;

        public HotkeyRecorderForm()
        {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!IsRecording)
                return;

            if (e.KeyCode == Keys.Escape)
                OnSetHotkey();

            lbl_hotkeys.Text = (e.Shift) ? "Shift" : "";
            lbl_hotkeys.Text += (e.Control) ? ((string.IsNullOrEmpty(lbl_hotkeys.Text)) ? "Control" : " + Control") : "";
            lbl_hotkeys.Text += (e.Alt) ? ((string.IsNullOrEmpty(lbl_hotkeys.Text)) ? "Alt" : " + Alt") : "";

            switch (e.KeyCode)
            {
                case Keys.ShiftKey: return;
                case Keys.ControlKey: return;
                case Keys.Menu: return;
            }

            lbl_hotkeys.Text += $" + {e.KeyCode}";

            triggerKey = e.KeyCode;

            if (e.Shift && !modifierKeys.Contains(KeyboardHook.ModifierKeys.Shift)) modifierKeys.Add(KeyboardHook.ModifierKeys.Shift);
            if (e.Control && !modifierKeys.Contains(KeyboardHook.ModifierKeys.Control)) modifierKeys.Add(KeyboardHook.ModifierKeys.Control);
            if (e.Alt && !modifierKeys.Contains(KeyboardHook.ModifierKeys.Alt)) modifierKeys.Add(KeyboardHook.ModifierKeys.Alt);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            OnSetHotkey(triggerKey != Keys.None && modifierKeys.Count() > 0);
        }

        private void OnSetHotkey(bool success = false)
        {
            IsRecording = false;

            if (SetHotkey == null)
                return;

            SetHotkey(this, new HotkeyEventArgs()
            {
                ModifierKeys = modifierKeys,
                TriggerKey = triggerKey,
                Failed = !success
            });

            this.Close();
            this.Dispose();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            OnSetHotkey();
        }
    }
}
