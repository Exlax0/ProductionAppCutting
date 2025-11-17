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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            txt1 = new Label();
            ChartOven1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            Update1ChartTimer = new System.Windows.Forms.Timer(components);
            datePickerFrom = new DateTimePicker();
            datePickerTo = new DateTimePicker();
            search_btn = new Button();
            timePickerFrom = new DateTimePicker();
            timePickerTo = new DateTimePicker();
            Live_btn = new Button();
            tmrReadColor = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)ChartOven1).BeginInit();
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
            // ChartOven1
            // 
            chartArea1.Name = "ChartArea1";
            ChartOven1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            ChartOven1.Legends.Add(legend1);
            ChartOven1.Location = new Point(1216, 13);
            ChartOven1.Name = "ChartOven1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Temp1";
            ChartOven1.Series.Add(series1);
            ChartOven1.Size = new Size(195, 48);
            ChartOven1.TabIndex = 2;
            ChartOven1.Text = "chart1";
            ChartOven1.Visible = false;
            // 
            // Update1ChartTimer
            // 
            Update1ChartTimer.Enabled = true;
            Update1ChartTimer.Interval = 1000;
            Update1ChartTimer.Tick += Update1ChartTimer_Tick;
            // 
            // datePickerFrom
            // 
            datePickerFrom.Format = DateTimePickerFormat.Short;
            datePickerFrom.Location = new Point(284, 19);
            datePickerFrom.Name = "datePickerFrom";
            datePickerFrom.Size = new Size(200, 23);
            datePickerFrom.TabIndex = 3;
            // 
            // datePickerTo
            // 
            datePickerTo.Format = DateTimePickerFormat.Short;
            datePickerTo.Location = new Point(664, 19);
            datePickerTo.Name = "datePickerTo";
            datePickerTo.Size = new Size(200, 23);
            datePickerTo.TabIndex = 4;
            // 
            // search_btn
            // 
            search_btn.Location = new Point(1015, 21);
            search_btn.Name = "search_btn";
            search_btn.Size = new Size(75, 23);
            search_btn.TabIndex = 5;
            search_btn.Text = "Search";
            search_btn.UseVisualStyleBackColor = true;
            search_btn.Click += search_btn_Click;
            // 
            // timePickerFrom
            // 
            timePickerFrom.Format = DateTimePickerFormat.Time;
            timePickerFrom.Location = new Point(490, 19);
            timePickerFrom.Name = "timePickerFrom";
            timePickerFrom.ShowUpDown = true;
            timePickerFrom.Size = new Size(105, 23);
            timePickerFrom.TabIndex = 6;
            // 
            // timePickerTo
            // 
            timePickerTo.Format = DateTimePickerFormat.Time;
            timePickerTo.Location = new Point(870, 19);
            timePickerTo.Name = "timePickerTo";
            timePickerTo.ShowUpDown = true;
            timePickerTo.Size = new Size(108, 23);
            timePickerTo.TabIndex = 7;
            // 
            // Live_btn
            // 
            Live_btn.Location = new Point(1122, 21);
            Live_btn.Name = "Live_btn";
            Live_btn.Size = new Size(75, 23);
            Live_btn.TabIndex = 8;
            Live_btn.Text = "Live";
            Live_btn.UseVisualStyleBackColor = true;
            Live_btn.Click += Live_btn_Click;
            // 
            // tmrReadColor
            // 
            tmrReadColor.Interval = 500;
            tmrReadColor.Tick += tmrReadColor_Tick;
            // 
            // DesOven1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 48, 64);
            Controls.Add(Live_btn);
            Controls.Add(timePickerTo);
            Controls.Add(timePickerFrom);
            Controls.Add(search_btn);
            Controls.Add(datePickerTo);
            Controls.Add(datePickerFrom);
            Controls.Add(txt1);
            Controls.Add(ChartOven1);
            Name = "DesOven1";
            Size = new Size(1720, 980);
            Load += DesOven1_Load;
            ((System.ComponentModel.ISupportInitialize)ChartOven1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txt1;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartOven1;
        private System.Windows.Forms.Timer Update1ChartTimer;
        private DateTimePicker datePickerFrom;
        private DateTimePicker datePickerTo;
        private Button search_btn;
        private DateTimePicker timePickerFrom;
        private DateTimePicker timePickerTo;
        private Button Live_btn;
        private System.Windows.Forms.Timer tmrReadColor;
    }
}
