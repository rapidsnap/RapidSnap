using RapidShot.Input;
using RapidSnap.Forms;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;
using static RapidShot.Input.KeyboardHook;

namespace RapidSnap.Input
{
    public class Hotkey
    {
        public int ID { get; private set; } = -1;
        public Button UIButton { get; private set; }
        public bool IsAssigning { get; private set; } = false;

        private List<ModifierKeys> modifierKeys = new List<ModifierKeys>();
        private Keys triggerKey = Keys.None;
        private KeyboardHook hook;

        public Hotkey(KeyboardHook hook, Button uiButton)
        {
            this.hook = hook;
            UIButton = uiButton;
            UIButton.Click += (s, e) => Assign();
        }

        private void Assign()
        {
            UIButton.Text = "Assigning...";
            var hkRecorder = new HotkeyRecorderForm();
            hkRecorder.Show();
            hkRecorder.SetHotkey += (s, e) =>
            {
                if (!e.Failed)
                {
                    modifierKeys = e.ModifierKeys;
                    triggerKey = e.TriggerKey;
                }

                UIButton.Text = this.ToString();

                hkRecorder.Close();
                hkRecorder.Dispose();
            };
        }

        public void Load(StringCollection setting)
        {
            if (setting == null || setting.Count < 1)
                return;

            triggerKey = (Keys)Enum.Parse(typeof(Keys), setting[0], true);
            
            for (int i = 1; i < setting.Count; i++)
                modifierKeys.Add((ModifierKeys)Enum.Parse(typeof(ModifierKeys), setting[i], true));

            UIButton.Text = this.ToString();
        }

        public StringCollection Save()
        {
            if (triggerKey == Keys.None)
                return null;

            var setting = new StringCollection();
            setting.Add($"{triggerKey}");

            if (modifierKeys == null)
                modifierKeys = new List<ModifierKeys>() { ModifierKeys.Control };

            foreach (var key in modifierKeys)
                setting.Add($"{key}");

            return setting;
        }

        public void Register(Action<object, EventArgs> action)
        {
            if (ID >= 0)
                hook.RemoveHotKey(ID);

            switch (modifierKeys.Count)
            {
                case 1: ID = hook.AddHotKey(modifierKeys[0], triggerKey, action); break;
                case 2: ID = hook.AddHotKey(modifierKeys[0], modifierKeys[1], triggerKey, action); break;
                case 3: ID = hook.AddHotKey(modifierKeys[0], modifierKeys[1], modifierKeys[2], triggerKey, action); break;
            }
        }

        public void Unregister()
            => hook.RemoveHotKey(ID);

        public override string ToString()
        {
            if (triggerKey == Keys.None || modifierKeys == null)
                return "NONE";

            string text = "";

            foreach (var mKey in modifierKeys)
                text += (string.IsNullOrEmpty(text)) ? $"{mKey}" : $" + {mKey}";

            text += $" + {triggerKey}";

            return text;
        }
    }
}
