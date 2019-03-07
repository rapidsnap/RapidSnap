namespace RapidSnap.Forms
{
    partial class HotkeyRecorderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_hotkeys = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_hotkeys
            // 
            this.lbl_hotkeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_hotkeys.Location = new System.Drawing.Point(0, 0);
            this.lbl_hotkeys.Name = "lbl_hotkeys";
            this.lbl_hotkeys.Size = new System.Drawing.Size(200, 100);
            this.lbl_hotkeys.TabIndex = 0;
            this.lbl_hotkeys.Text = "Recording hotkey...\r\n(ESC for NONE)";
            this.lbl_hotkeys.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HotkeyRecorderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(200, 100);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_hotkeys);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HotkeyRecorderForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HotkeyRecorderForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_hotkeys;
    }
}