namespace RapidSnap.Forms
{
    partial class OptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionForm));
            this.gb_hotkeys = new System.Windows.Forms.GroupBox();
            this.btn_hkSnap = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_update = new System.Windows.Forms.Button();
            this.cb_autoStart = new System.Windows.Forms.CheckBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.cb_autoUpdate = new System.Windows.Forms.CheckBox();
            this.gb_settings = new System.Windows.Forms.GroupBox();
            this.lbl_version = new System.Windows.Forms.Label();
            this.gb_save = new System.Windows.Forms.GroupBox();
            this.rb_disk = new System.Windows.Forms.RadioButton();
            this.rb_clipboard = new System.Windows.Forms.RadioButton();
            this.gb_hotkeys.SuspendLayout();
            this.gb_settings.SuspendLayout();
            this.gb_save.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_hotkeys
            // 
            this.gb_hotkeys.Controls.Add(this.btn_hkSnap);
            this.gb_hotkeys.Controls.Add(this.label1);
            this.gb_hotkeys.Location = new System.Drawing.Point(18, 18);
            this.gb_hotkeys.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gb_hotkeys.Name = "gb_hotkeys";
            this.gb_hotkeys.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gb_hotkeys.Size = new System.Drawing.Size(320, 75);
            this.gb_hotkeys.TabIndex = 0;
            this.gb_hotkeys.TabStop = false;
            this.gb_hotkeys.Text = "Hotkeys";
            // 
            // btn_hkSnap
            // 
            this.btn_hkSnap.Location = new System.Drawing.Point(90, 29);
            this.btn_hkSnap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_hkSnap.Name = "btn_hkSnap";
            this.btn_hkSnap.Size = new System.Drawing.Size(220, 35);
            this.btn_hkSnap.TabIndex = 1;
            this.btn_hkSnap.Text = "NONE";
            this.btn_hkSnap.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Snap";
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(346, 125);
            this.btn_update.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(156, 35);
            this.btn_update.TabIndex = 1;
            this.btn_update.Text = "Update";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.OnUpdateButtonClick);
            // 
            // cb_autoStart
            // 
            this.cb_autoStart.AutoSize = true;
            this.cb_autoStart.Location = new System.Drawing.Point(9, 29);
            this.cb_autoStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_autoStart.Name = "cb_autoStart";
            this.cb_autoStart.Size = new System.Drawing.Size(101, 24);
            this.cb_autoStart.TabIndex = 2;
            this.cb_autoStart.Text = "Autostart";
            this.cb_autoStart.UseVisualStyleBackColor = true;
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(346, 162);
            this.btn_close.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(156, 35);
            this.btn_close.TabIndex = 3;
            this.btn_close.Text = "Save";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.OnCloseButtonClick);
            // 
            // cb_autoUpdate
            // 
            this.cb_autoUpdate.AutoSize = true;
            this.cb_autoUpdate.Location = new System.Drawing.Point(9, 65);
            this.cb_autoUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_autoUpdate.Name = "cb_autoUpdate";
            this.cb_autoUpdate.Size = new System.Drawing.Size(123, 24);
            this.cb_autoUpdate.TabIndex = 4;
            this.cb_autoUpdate.Text = "Auto update";
            this.cb_autoUpdate.UseVisualStyleBackColor = true;
            // 
            // gb_settings
            // 
            this.gb_settings.Controls.Add(this.cb_autoUpdate);
            this.gb_settings.Controls.Add(this.cb_autoStart);
            this.gb_settings.Location = new System.Drawing.Point(346, 18);
            this.gb_settings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gb_settings.Name = "gb_settings";
            this.gb_settings.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gb_settings.Size = new System.Drawing.Size(156, 97);
            this.gb_settings.TabIndex = 5;
            this.gb_settings.TabStop = false;
            this.gb_settings.Text = "Settings";
            // 
            // lbl_version
            // 
            this.lbl_version.AutoSize = true;
            this.lbl_version.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_version.Location = new System.Drawing.Point(14, 202);
            this.lbl_version.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(72, 20);
            this.lbl_version.TabIndex = 6;
            this.lbl_version.Text = "vX.X.X.X";
            // 
            // gb_save
            // 
            this.gb_save.Controls.Add(this.rb_disk);
            this.gb_save.Controls.Add(this.rb_clipboard);
            this.gb_save.Location = new System.Drawing.Point(18, 103);
            this.gb_save.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gb_save.Name = "gb_save";
            this.gb_save.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gb_save.Size = new System.Drawing.Size(320, 94);
            this.gb_save.TabIndex = 7;
            this.gb_save.TabStop = false;
            this.gb_save.Text = "Save Screenshot";
            // 
            // rb_disk
            // 
            this.rb_disk.AutoSize = true;
            this.rb_disk.Location = new System.Drawing.Point(14, 58);
            this.rb_disk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rb_disk.Name = "rb_disk";
            this.rb_disk.Size = new System.Drawing.Size(123, 24);
            this.rb_disk.TabIndex = 1;
            this.rb_disk.TabStop = true;
            this.rb_disk.Text = "Save to Disk";
            this.rb_disk.UseVisualStyleBackColor = true;
            // 
            // rb_clipboard
            // 
            this.rb_clipboard.AutoSize = true;
            this.rb_clipboard.Location = new System.Drawing.Point(14, 29);
            this.rb_clipboard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rb_clipboard.Name = "rb_clipboard";
            this.rb_clipboard.Size = new System.Drawing.Size(159, 24);
            this.rb_clipboard.TabIndex = 0;
            this.rb_clipboard.TabStop = true;
            this.rb_clipboard.Text = "Save to Clipboard";
            this.rb_clipboard.UseVisualStyleBackColor = true;
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(518, 231);
            this.Controls.Add(this.gb_save);
            this.Controls.Add(this.lbl_version);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.gb_settings);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.gb_hotkeys);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.gb_hotkeys.ResumeLayout(false);
            this.gb_hotkeys.PerformLayout();
            this.gb_settings.ResumeLayout(false);
            this.gb_settings.PerformLayout();
            this.gb_save.ResumeLayout(false);
            this.gb_save.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_hotkeys;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_hkSnap;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.CheckBox cb_autoStart;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.CheckBox cb_autoUpdate;
        private System.Windows.Forms.GroupBox gb_settings;
        private System.Windows.Forms.Label lbl_version;
        private System.Windows.Forms.GroupBox gb_save;
        private System.Windows.Forms.RadioButton rb_disk;
        private System.Windows.Forms.RadioButton rb_clipboard;
    }
}