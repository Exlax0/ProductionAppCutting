namespace Meme_Oven_Data.Pages
{
    partial class DesOven2
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
            label1 = new Label();
            datePickerFrom = new DateTimePicker();
            TimePickerFrom = new DateTimePicker();
            datePickerTo = new DateTimePicker();
            timePickerTo = new DateTimePicker();
            Search_Btn = new Button();
            Live_btn = new Button();
            timerUpdateChart = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 161);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(20, 13);
            label1.Name = "label1";
            label1.Size = new Size(245, 30);
            label1.TabIndex = 3;
            label1.Text = "Θερμοκασίες Φούρνου 2";
            // 
            // datePickerFrom
            // 
            datePickerFrom.Location = new Point(297, 19);
            datePickerFrom.Name = "datePickerFrom";
            datePickerFrom.Size = new Size(200, 23);
            datePickerFrom.TabIndex = 4;
            // 
            // TimePickerFrom
            // 
            TimePickerFrom.Format = DateTimePickerFormat.Time;
            TimePickerFrom.Location = new Point(513, 19);
            TimePickerFrom.Name = "TimePickerFrom";
            TimePickerFrom.ShowUpDown = true;
            TimePickerFrom.Size = new Size(144, 23);
            TimePickerFrom.TabIndex = 5;
            // 
            // datePickerTo
            // 
            datePickerTo.Location = new Point(710, 19);
            datePickerTo.Name = "datePickerTo";
            datePickerTo.Size = new Size(200, 23);
            datePickerTo.TabIndex = 6;
            // 
            // timePickerTo
            // 
            timePickerTo.Format = DateTimePickerFormat.Time;
            timePickerTo.Location = new Point(925, 20);
            timePickerTo.Name = "timePickerTo";
            timePickerTo.ShowUpDown = true;
            timePickerTo.Size = new Size(139, 23);
            timePickerTo.TabIndex = 7;
            // 
            // Search_Btn
            // 
            Search_Btn.Location = new Point(1099, 22);
            Search_Btn.Name = "Search_Btn";
            Search_Btn.Size = new Size(75, 23);
            Search_Btn.TabIndex = 8;
            Search_Btn.Text = "Search";
            Search_Btn.UseVisualStyleBackColor = true;
            Search_Btn.Click += Search_Btn_Click;
            // 
            // Live_btn
            // 
            Live_btn.Location = new Point(1197, 22);
            Live_btn.Name = "Live_btn";
            Live_btn.Size = new Size(75, 23);
            Live_btn.TabIndex = 9;
            Live_btn.Text = "Live";
            Live_btn.UseVisualStyleBackColor = true;
            Live_btn.Click += Live_btn_Click;
            // 
            // timerUpdateChart
            // 
            timerUpdateChart.Enabled = true;
            timerUpdateChart.Interval = 3000;
            timerUpdateChart.Tick += timerUpdateChart_Tick;
            // 
            // DesOven2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 48, 64);
            Controls.Add(Live_btn);
            Controls.Add(Search_Btn);
            Controls.Add(timePickerTo);
            Controls.Add(datePickerTo);
            Controls.Add(TimePickerFrom);
            Controls.Add(datePickerFrom);
            Controls.Add(label1);
            Name = "DesOven2";
            Size = new Size(1720, 980);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DateTimePicker datePickerFrom;
        private DateTimePicker TimePickerFrom;
        private DateTimePicker datePickerTo;
        private DateTimePicker timePickerTo;
        private Button Search_Btn;
        private Button Live_btn;
        private System.Windows.Forms.Timer timerUpdateChart;
    }
}
