namespace Meme_Oven_Data
{
    partial class DesOven1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txt1 = new Label();
            Update1ChartTimer = new System.Windows.Forms.Timer(components);
            datePickerFrom = new DateTimePicker();
            datePickerTo = new DateTimePicker();
            search_btn = new Button();
            timePickerFrom = new DateTimePicker();
            timePickerTo = new DateTimePicker();
            Live_btn = new Button();
            tmrReadColor = new System.Windows.Forms.Timer(components);
            timer1 = new System.Windows.Forms.Timer(components);
            btExport = new Button();
            SuspendLayout();
            // 
            // txt1
            // 
            txt1.Anchor = AnchorStyles.None;
            txt1.AutoSize = true;
            txt1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 161);
            txt1.ForeColor = Color.Red;
            txt1.Location = new Point(20, 13);
            txt1.Name = "txt1";
            txt1.Size = new Size(242, 30);
            txt1.TabIndex = 1;
            txt1.Text = "Καταγραφές Μηχανής 1";
            // 
            // Update1ChartTimer
            // 
            Update1ChartTimer.Enabled = true;
            Update1ChartTimer.Interval = 500;
            Update1ChartTimer.Tick += Update1ChartTimer_Tick;
            // 
            // datePickerFrom
            // 
            datePickerFrom.Font = new Font("Arial", 10F);
            datePickerFrom.Format = DateTimePickerFormat.Short;
            datePickerFrom.Location = new Point(284, 19);
            datePickerFrom.Name = "datePickerFrom";
            datePickerFrom.Size = new Size(200, 23);
            datePickerFrom.TabIndex = 3;
            // 
            // datePickerTo
            // 
            datePickerTo.Font = new Font("Arial", 10F);
            datePickerTo.Format = DateTimePickerFormat.Short;
            datePickerTo.Location = new Point(623, 19);
            datePickerTo.Name = "datePickerTo";
            datePickerTo.Size = new Size(200, 23);
            datePickerTo.TabIndex = 4;
            // 
            // search_btn
            // 
            search_btn.Location = new Point(943, 15);
            search_btn.Name = "search_btn";
            search_btn.Size = new Size(84, 30);
            search_btn.TabIndex = 5;
            search_btn.Text = "Αναζήτηση";
            search_btn.UseVisualStyleBackColor = true;
            search_btn.Click += search_btn_Click;
            // 
            // timePickerFrom
            // 
            timePickerFrom.Font = new Font("Arial", 10F);
            timePickerFrom.Format = DateTimePickerFormat.Time;
            timePickerFrom.Location = new Point(490, 19);
            timePickerFrom.Name = "timePickerFrom";
            timePickerFrom.ShowUpDown = true;
            timePickerFrom.Size = new Size(105, 23);
            timePickerFrom.TabIndex = 6;
            // 
            // timePickerTo
            // 
            timePickerTo.Font = new Font("Arial", 10F);
            timePickerTo.Format = DateTimePickerFormat.Time;
            timePickerTo.Location = new Point(829, 19);
            timePickerTo.Name = "timePickerTo";
            timePickerTo.ShowUpDown = true;
            timePickerTo.Size = new Size(108, 23);
            timePickerTo.TabIndex = 7;
            // 
            // Live_btn
            // 
            Live_btn.Location = new Point(1122, 15);
            Live_btn.Name = "Live_btn";
            Live_btn.Size = new Size(91, 30);
            Live_btn.TabIndex = 8;
            Live_btn.Text = "Live";
            Live_btn.UseVisualStyleBackColor = true;
            Live_btn.Visible = false;
            Live_btn.Click += Live_btn_Click;
            // 
            // tmrReadColor
            // 
            tmrReadColor.Interval = 500;
            tmrReadColor.Tick += tmrReadColor_Tick;
            // 
            // btExport
            // 
            btExport.Location = new Point(1033, 15);
            btExport.Name = "btExport";
            btExport.Size = new Size(84, 30);
            btExport.TabIndex = 9;
            btExport.Text = "Εκτύπωση";
            btExport.UseVisualStyleBackColor = true;
            btExport.Visible = false;
            btExport.Click += btExport_Click;
            // 
            // DesOven1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 48, 64);
            Controls.Add(btExport);
            Controls.Add(Live_btn);
            Controls.Add(timePickerTo);
            Controls.Add(timePickerFrom);
            Controls.Add(search_btn);
            Controls.Add(datePickerTo);
            Controls.Add(datePickerFrom);
            Controls.Add(txt1);
            Name = "DesOven1";
            Size = new Size(1720, 980);
            Load += DesOven1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txt1;
        private System.Windows.Forms.Timer Update1ChartTimer;
        private DateTimePicker datePickerFrom;
        private DateTimePicker datePickerTo;
        private Button search_btn;
        private DateTimePicker timePickerFrom;
        private DateTimePicker timePickerTo;
        private Button Live_btn;
        private System.Windows.Forms.Timer tmrReadColor;
        private System.Windows.Forms.Timer timer1;
        private Button button1;
        private Button btExport;
    }
}
