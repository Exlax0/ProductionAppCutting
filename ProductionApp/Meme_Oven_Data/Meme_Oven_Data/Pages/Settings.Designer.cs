namespace Meme_Oven_Data.Pages
{
    partial class Settings
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
            txt1 = new Label();
            TimePickerStart1Machine1 = new DateTimePicker();
            SuspendLayout();
            // 
            // txt1
            // 
            txt1.AutoSize = true;
            txt1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 161);
            txt1.ForeColor = Color.Red;
            txt1.Location = new Point(24, 23);
            txt1.Name = "txt1";
            txt1.Size = new Size(183, 30);
            txt1.TabIndex = 2;
            txt1.Text = "Σελίδα Ρυθμίσεων";
            txt1.Visible = false;
            // 
            // TimePickerStart1Machine1
            // 
            TimePickerStart1Machine1.Location = new Point(24, 145);
            TimePickerStart1Machine1.Name = "TimePickerStart1Machine1";
            TimePickerStart1Machine1.Size = new Size(200, 23);
            TimePickerStart1Machine1.TabIndex = 3;
            TimePickerStart1Machine1.Visible = false;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 48, 64);
            Controls.Add(TimePickerStart1Machine1);
            Controls.Add(txt1);
            ForeColor = SystemColors.ControlText;
            Name = "Settings";
            Size = new Size(1720, 980);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txt1;
        private DateTimePicker TimePickerStart1Machine1;
    }
}
