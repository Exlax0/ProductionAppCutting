using Meme_Oven_Data.Pages;

namespace Meme_Oven_Data
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        private void AddedSettings()
        {
            Settings = new Settings(_dbContext);
            this.Controls.Add(Settings);
            Settings.Dock = DockStyle.Fill;
        }

        private void AddDesOven1Page()
        {
            desOven11 = new DesOven1(_dbContext); 
            this.Controls.Add(desOven11);         
            desOven11.Dock = DockStyle.Fill;      
        }

        private void AddDesOven2Page()
        {
            desOven21 = new DesOven2(_dbContext);
            this.Controls.Add(desOven21);
            desOven21.Dock = DockStyle.Fill;
        }

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            NavPanel = new Panel();
            btSettings = new Button();
            btInfo = new Button();
            reconnect_btn = new Button();
            btOven2 = new Button();
            btOven1 = new Button();
            btClose = new Button();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            InfoPanel = new Panel();
            lblTime = new Label();
            pictureBox3 = new PictureBox();
            MainPanel = new Panel();
            pictureBox2 = new PictureBox();
            ReadData1 = new System.Windows.Forms.Timer(components);
            WriteToSQL = new System.Windows.Forms.Timer(components);
            tmrLiveTime = new System.Windows.Forms.Timer(components);
            NavPanel.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            InfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // NavPanel
            // 
            NavPanel.BackColor = Color.FromArgb(24, 30, 54);
            NavPanel.Controls.Add(btSettings);
            NavPanel.Controls.Add(btInfo);
            NavPanel.Controls.Add(reconnect_btn);
            NavPanel.Controls.Add(btOven2);
            NavPanel.Controls.Add(btOven1);
            NavPanel.Controls.Add(btClose);
            NavPanel.Controls.Add(panel2);
            NavPanel.Dock = DockStyle.Left;
            NavPanel.Location = new Point(0, 0);
            NavPanel.Name = "NavPanel";
            NavPanel.Size = new Size(200, 1080);
            NavPanel.TabIndex = 0;
            // 
            // btSettings
            // 
            btSettings.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btSettings.Location = new Point(23, 964);
            btSettings.Name = "btSettings";
            btSettings.Size = new Size(155, 51);
            btSettings.TabIndex = 5;
            btSettings.Text = "Ρυθμίσεις Συστήματος";
            btSettings.UseVisualStyleBackColor = true;
            btSettings.Click += btSettings_Click;
            // 
            // btInfo
            // 
            btInfo.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btInfo.Location = new Point(23, 150);
            btInfo.Name = "btInfo";
            btInfo.Size = new Size(155, 34);
            btInfo.TabIndex = 4;
            btInfo.Text = "Πληροφορίες";
            btInfo.UseVisualStyleBackColor = true;
            btInfo.Click += btInfo_Click;
            // 
            // reconnect_btn
            // 
            reconnect_btn.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            reconnect_btn.Location = new Point(23, 267);
            reconnect_btn.Name = "reconnect_btn";
            reconnect_btn.Size = new Size(155, 34);
            reconnect_btn.TabIndex = 3;
            reconnect_btn.Text = "Επανασύνδεση";
            reconnect_btn.UseVisualStyleBackColor = true;
            reconnect_btn.Visible = false;
            reconnect_btn.Click += reconnect_btn_Click;
            // 
            // btOven2
            // 
            btOven2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btOven2.Location = new Point(23, 228);
            btOven2.Name = "btOven2";
            btOven2.Size = new Size(155, 33);
            btOven2.TabIndex = 2;
            btOven2.Text = "Κοπτικό 2";
            btOven2.UseVisualStyleBackColor = true;
            btOven2.Click += btOven2_Click;
            // 
            // btOven1
            // 
            btOven1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 161);
            btOven1.Location = new Point(23, 190);
            btOven1.Name = "btOven1";
            btOven1.Size = new Size(155, 33);
            btOven1.TabIndex = 0;
            btOven1.Text = "Κοπτικό 1";
            btOven1.UseVisualStyleBackColor = true;
            btOven1.Click += btOven1_Click;
            // 
            // btClose
            // 
            btClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btClose.BackColor = Color.Red;
            btClose.FlatAppearance.BorderSize = 2;
            btClose.FlatStyle = FlatStyle.Flat;
            btClose.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 161);
            btClose.ImageAlign = ContentAlignment.MiddleLeft;
            btClose.Location = new Point(33, 1036);
            btClose.Name = "btClose";
            btClose.Size = new Size(132, 32);
            btClose.TabIndex = 0;
            btClose.Text = "EXIT";
            btClose.TextAlign = ContentAlignment.TopCenter;
            btClose.TextImageRelation = TextImageRelation.TextAboveImage;
            btClose.UseVisualStyleBackColor = false;
            btClose.Click += btClose_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(pictureBox1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(200, 100);
            panel2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.MainOldLogo;
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(194, 94);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // InfoPanel
            // 
            InfoPanel.BackColor = Color.FromArgb(150, 150, 200);
            InfoPanel.Controls.Add(lblTime);
            InfoPanel.Controls.Add(pictureBox3);
            InfoPanel.Dock = DockStyle.Top;
            InfoPanel.Location = new Point(200, 0);
            InfoPanel.Name = "InfoPanel";
            InfoPanel.Size = new Size(1720, 100);
            InfoPanel.TabIndex = 1;
            // 
            // lblTime
            // 
            lblTime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTime.AutoSize = true;
            lblTime.Location = new Point(1257, 66);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(143, 15);
            lblTime.TabIndex = 1;
            lblTime.Text = "Τρίτη 19/11/2025 18:42:15";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(637, 5);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(288, 91);
            pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // MainPanel
            // 
            MainPanel.Controls.Add(pictureBox2);
            MainPanel.Dock = DockStyle.Fill;
            MainPanel.Location = new Point(200, 100);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new Size(1720, 980);
            MainPanel.TabIndex = 2;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(143, 78);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(1444, 805);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // ReadData1
            // 
            ReadData1.Interval = 200;
            ReadData1.Tick += ReadData1_Tick;
            // 
            // WriteToSQL
            // 
            WriteToSQL.Interval = 2000;
            WriteToSQL.Tick += WriteToSQL_Tick;
            // 
            // tmrLiveTime
            // 
            tmrLiveTime.Enabled = true;
            tmrLiveTime.Interval = 1000;
            tmrLiveTime.Tick += tmrLiveTime_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 48, 64);
            ClientSize = new Size(1920, 1080);
            Controls.Add(MainPanel);
            Controls.Add(InfoPanel);
            Controls.Add(NavPanel);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            NavPanel.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            InfoPanel.ResumeLayout(false);
            InfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel NavPanel;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Panel InfoPanel;
        private Panel MainPanel;
        private Button btClose;
        private Button btOven2;
        private Button btOven1;
        private DesOven2 desOven21;
        private DesOven1 desOven11;
        private Settings Settings;
        private System.Windows.Forms.Timer ReadData1;
        private System.Windows.Forms.Timer WriteToSQL;
        private PictureBox pictureBox2;
        private Button reconnect_btn;
        private PictureBox pictureBox3;
        private Button btInfo;
        private Button btSettings;
        private Label lblTime;
        private System.Windows.Forms.Timer tmrLiveTime;
    }
}
