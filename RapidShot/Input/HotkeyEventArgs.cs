using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static RapidShot.Input.KeyboardHook;

namespace RapidSnap.Input
{
    public class HotkeyEventArgs : EventArgs
    {
        public List<ModifierKeys> ModifierKeys { get; set; }
        public Keys TriggerKey { get; set; }
        public bool Failed { get; set; }
    }
}
