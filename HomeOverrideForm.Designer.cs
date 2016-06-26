namespace HomeOverride {
    partial class HomeOverrideForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeOverrideForm));
            this.tooltip_application = new System.Windows.Forms.ToolTip(this.components);
            this.tooltip_startwithwindows = new System.Windows.Forms.ToolTip(this.components);
            this.tooltip_startminimized = new System.Windows.Forms.ToolTip(this.components);
            this.tooltip_hotkey = new System.Windows.Forms.ToolTip(this.components);
            this.button_selectYourApplication = new System.Windows.Forms.Button();
            this.checkbox_enabled = new System.Windows.Forms.CheckBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.checkBox_StartWithWindows = new System.Windows.Forms.CheckBox();
            this.checkBox_StartMinimized = new System.Windows.Forms.CheckBox();
            this.button_about = new System.Windows.Forms.Button();
            this.pictureBox_applicationIcon = new System.Windows.Forms.PictureBox();
            this.label_AppName = new System.Windows.Forms.TextBox();
            this.comboBox_KeyMod = new System.Windows.Forms.ComboBox();
            this.comboBox_Key = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_HotKeyInfo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_applicationIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // button_selectYourApplication
            // 
            this.button_selectYourApplication.Location = new System.Drawing.Point(72, 36);
            this.button_selectYourApplication.Name = "button_selectYourApplication";
            this.button_selectYourApplication.Size = new System.Drawing.Size(45, 23);
            this.button_selectYourApplication.TabIndex = 41;
            this.button_selectYourApplication.Text = "Select";
            this.button_selectYourApplication.UseVisualStyleBackColor = true;
            this.button_selectYourApplication.Click += new System.EventHandler(this.button_SelectApplication);
            // 
            // checkbox_enabled
            // 
            this.checkbox_enabled.AutoSize = true;
            this.checkbox_enabled.Location = new System.Drawing.Point(142, 40);
            this.checkbox_enabled.Name = "checkbox_enabled";
            this.checkbox_enabled.Size = new System.Drawing.Size(65, 17);
            this.checkbox_enabled.TabIndex = 47;
            this.checkbox_enabled.Text = "Enabled";
            this.checkbox_enabled.UseVisualStyleBackColor = true;
            this.checkbox_enabled.CheckedChanged += new System.EventHandler(this.checkbox_enabled_CheckedChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "TestText";
            this.notifyIcon.BalloonTipTitle = "TestTitle";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Home Override";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_DoubleClick);
            // 
            // checkBox_StartWithWindows
            // 
            this.checkBox_StartWithWindows.AutoSize = true;
            this.checkBox_StartWithWindows.Location = new System.Drawing.Point(12, 71);
            this.checkBox_StartWithWindows.Name = "checkBox_StartWithWindows";
            this.checkBox_StartWithWindows.Size = new System.Drawing.Size(117, 17);
            this.checkBox_StartWithWindows.TabIndex = 50;
            this.checkBox_StartWithWindows.Text = "Start with Windows";
            this.checkBox_StartWithWindows.UseVisualStyleBackColor = true;
            this.checkBox_StartWithWindows.CheckedChanged += new System.EventHandler(this.checkBox_StartWithWindows_CheckedChanged);
            // 
            // checkBox_StartMinimized
            // 
            this.checkBox_StartMinimized.AutoSize = true;
            this.checkBox_StartMinimized.Location = new System.Drawing.Point(12, 94);
            this.checkBox_StartMinimized.Name = "checkBox_StartMinimized";
            this.checkBox_StartMinimized.Size = new System.Drawing.Size(96, 17);
            this.checkBox_StartMinimized.TabIndex = 51;
            this.checkBox_StartMinimized.Text = "Start minimized";
            this.checkBox_StartMinimized.UseVisualStyleBackColor = true;
            this.checkBox_StartMinimized.CheckedChanged += new System.EventHandler(this.checkBox_StartMinimized_CheckedChanged);
            // 
            // button_about
            // 
            this.button_about.Location = new System.Drawing.Point(116, 36);
            this.button_about.Name = "button_about";
            this.button_about.Size = new System.Drawing.Size(23, 23);
            this.button_about.TabIndex = 52;
            this.button_about.Text = "?";
            this.button_about.UseVisualStyleBackColor = true;
            this.button_about.Click += new System.EventHandler(this.button_about_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox_applicationIcon.Location = new System.Drawing.Point(12, 12);
            this.pictureBox_applicationIcon.Name = "pictureBox1";
            this.pictureBox_applicationIcon.Size = new System.Drawing.Size(50, 50);
            this.pictureBox_applicationIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_applicationIcon.TabIndex = 54;
            this.pictureBox_applicationIcon.TabStop = false;
            // 
            // label_AppName
            // 
            this.label_AppName.Location = new System.Drawing.Point(72, 14);
            this.label_AppName.Name = "label_AppName";
            this.label_AppName.Size = new System.Drawing.Size(135, 20);
            this.label_AppName.TabIndex = 48;
            // 
            // comboBox_KeyMod
            // 
            this.comboBox_KeyMod.FormattingEnabled = true;
            this.comboBox_KeyMod.Location = new System.Drawing.Point(12, 130);
            this.comboBox_KeyMod.Name = "comboBox_KeyMod";
            this.comboBox_KeyMod.Size = new System.Drawing.Size(68, 21);
            this.comboBox_KeyMod.TabIndex = 55;
            this.comboBox_KeyMod.SelectedIndexChanged += new System.EventHandler(this.comboBox_KeyMod_SelectedIndexChanged);
            // 
            // comboBox_Key
            // 
            this.comboBox_Key.FormattingEnabled = true;
            this.comboBox_Key.Location = new System.Drawing.Point(105, 130);
            this.comboBox_Key.Name = "comboBox_Key";
            this.comboBox_Key.Size = new System.Drawing.Size(102, 21);
            this.comboBox_Key.TabIndex = 56;
            this.comboBox_Key.SelectedIndexChanged += new System.EventHandler(this.comboBox_Key_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "+";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Hotkey:";
            // 
            // label_HotKeyInfo
            // 
            this.label_HotKeyInfo.BackColor = System.Drawing.Color.White;
            this.label_HotKeyInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.label_HotKeyInfo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label_HotKeyInfo.Enabled = false;
            this.label_HotKeyInfo.Location = new System.Drawing.Point(136, 71);
            this.label_HotKeyInfo.Multiline = true;
            this.label_HotKeyInfo.Name = "label_HotKeyInfo";
            this.label_HotKeyInfo.ReadOnly = true;
            this.label_HotKeyInfo.Size = new System.Drawing.Size(71, 56);
            this.label_HotKeyInfo.TabIndex = 59;
            // 
            // StartupSeviceForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(219, 161);
            this.Controls.Add(this.label_HotKeyInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_Key);
            this.Controls.Add(this.comboBox_KeyMod);
            this.Controls.Add(this.pictureBox_applicationIcon);
            this.Controls.Add(this.button_about);
            this.Controls.Add(this.checkBox_StartMinimized);
            this.Controls.Add(this.checkBox_StartWithWindows);
            this.Controls.Add(this.label_AppName);
            this.Controls.Add(this.checkbox_enabled);
            this.Controls.Add(this.button_selectYourApplication);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(235, 200);
            this.MinimumSize = new System.Drawing.Size(235, 200);
            this.Name = "StartupSeviceForm";
            this.Text = "Home Override";
            this.Resize += new System.EventHandler(this.StartupSeviceForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_applicationIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip tooltip_application;
        private System.Windows.Forms.ToolTip tooltip_startwithwindows;
        private System.Windows.Forms.ToolTip tooltip_startminimized;
        private System.Windows.Forms.ToolTip tooltip_hotkey;
        private System.Windows.Forms.Button button_selectYourApplication;
        private System.Windows.Forms.CheckBox checkbox_enabled;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.CheckBox checkBox_StartWithWindows;
        private System.Windows.Forms.CheckBox checkBox_StartMinimized;
        private System.Windows.Forms.Button button_about;
        private System.Windows.Forms.PictureBox pictureBox_applicationIcon;
        private System.Windows.Forms.TextBox label_AppName;
        private System.Windows.Forms.ComboBox comboBox_KeyMod;
        private System.Windows.Forms.ComboBox comboBox_Key;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox label_HotKeyInfo;
    }
}

