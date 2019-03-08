using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RapidShot.Input
{
    public class KeyboardHook : NativeWindow, IDisposable
    {
        [Flags]
        public enum ModifierKeys : uint
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Win = 8
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        
        private const int WM_HOTKEY = 0x0312;

        private Dictionary<int, Action<object, EventArgs>> _hotKeys;
        private static int _IDcounter;

        public KeyboardHook()
        {
            _hotKeys = new Dictionary<int, Action<object, EventArgs>>();
            CreateHandle(new CreateParams());
        }

        public int AddHotKey(ModifierKeys modifier, Keys key, Action<object, EventArgs> action)
        {
            RegisterHotKey(Handle, ++_IDcounter, (int)modifier, (int)key);
            _hotKeys.Add(_IDcounter, action);

            return _IDcounter;
        }

        public int AddHotKey(ModifierKeys modifier1, ModifierKeys modifier2, Keys key, Action<object, EventArgs> action)
            => AddHotKey(modifier1 | modifier2, key, action);

        public int AddHotKey(ModifierKeys modifier1, ModifierKeys modifier2, ModifierKeys modifier3, Keys key, Action<object, EventArgs> action)
            => AddHotKey(modifier1 | modifier2 | modifier3, key, action);

        public void RemoveHotKey(int id)
            => UnregisterHotKey(Handle, id);

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg != WM_HOTKEY)
                return;

            try
            {
                foreach (var hotKey in _hotKeys)
                {
                    if ((int)m.WParam == hotKey.Key)
                    {
                        hotKey.Value(this, new EventArgs());
                    }
                }
            }
            catch(Exception) { }
        }

        public void Dispose()
        {
            foreach (var hotKey in _hotKeys)
            {
                RemoveHotKey(hotKey.Key);
            }
        }
    }
}
