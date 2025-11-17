using Meme_Oven_Data.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meme_Oven_Data.Pages
{
    public partial class Settings : UserControl
    {
        private readonly MicrOvenContext _dbContext;
        private ComboBox cmbMachine;
        private MaskedTextBox dtAStart, dtAEnd, dtBStart, dtBEnd, dtCStart, dtCEnd;
        //private NumericUpDown numAPlan, numBPlan, numCPlan;
        private Button btnSave;
        public Settings(MicrOvenContext dbContext)
        {
            _dbContext = dbContext;
            InitializeComponent();
            InitControls();
            LoadMachines();
        }

        private void LoadMachines()
        {
            // Hard-coded list
            cmbMachine.Items.Clear();
            cmbMachine.Items.Add("Cutting - Machine 01");
            cmbMachine.Items.Add("Cutting - Machine 02");
            cmbMachine.Items.Add("Cutting - Machine 03");
            // Add more if needed

            if (cmbMachine.Items.Count > 0)
                cmbMachine.SelectedIndex = 0;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cmbMachine.SelectedItem == null)
            {
                MessageBox.Show("Please select a machine.");
                return;
            }

            string machine = cmbMachine.SelectedItem.ToString();

            // Helper: upsert one shift row
            void SaveShift(string shiftCode, string shiftName,
                           MaskedTextBox txtStart, MaskedTextBox txtEnd)
            {
                if (!TimeSpan.TryParse(txtStart.Text, out TimeSpan start))
                {
                    MessageBox.Show($"Invalid start time for {shiftName}");
                    return;
                }

                if (!TimeSpan.TryParse(txtEnd.Text, out TimeSpan end))
                {
                    MessageBox.Show($"Invalid end time for {shiftName}");
                    return;
                }

                var plan = _dbContext.MachineShiftPlan
                    .SingleOrDefault(x => x.Machine == machine && x.ShiftCode == shiftCode);

                if (plan == null)
                {
                    plan = new ShiftPlan
                    {
                        Machine = machine,
                        ShiftCode = shiftCode
                    };
                    _dbContext.MachineShiftPlan.Add(plan);
                }

                plan.ShiftName = shiftName;
                plan.StartTime = start;
                plan.EndTime = end;
                plan.LastUpdated = DateTime.Now;
            }

            SaveShift("A", "Shift A", dtAStart, dtAEnd);
            SaveShift("B", "Shift B", dtBStart, dtBEnd);
            SaveShift("C", "Shift C", dtCStart, dtCEnd);

            _dbContext.SaveChanges();

            MessageBox.Show("Shift plan saved for " + machine);
        }



        private void CmbMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMachine.SelectedItem == null)
                return;

            string machine = cmbMachine.SelectedItem.ToString();

            var plans = _dbContext.MachineShiftPlan
                .Where(x => x.Machine == machine)
                .ToList();

            void FillShift(string shiftCode, MaskedTextBox txtStart, MaskedTextBox txtEnd)
            {
                var p = plans.FirstOrDefault(x => x.ShiftCode == shiftCode);
                if (p != null)
                {
                    txtStart.Text = p.StartTime.ToString(@"hh\:mm");
                    txtEnd.Text = p.EndTime.ToString(@"hh\:mm");
                }
                else
                {
                    // Defaults: empty shift or some default times
                    txtStart.Text = "06:00";
                    txtEnd.Text = "14:00";
                }
            }

            FillShift("A", dtAStart, dtAEnd);
            FillShift("B", dtBStart, dtBEnd);
            FillShift("C", dtCStart, dtCEnd);
        }

        private void InitControls()
        {
            // Machine selector
            cmbMachine = new ComboBox
            {
                Location = new Point(50, 30),
                Size = new Size(250, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 12)
            };
            cmbMachine.SelectedIndexChanged += CmbMachine_SelectedIndexChanged;
            this.Controls.Add(cmbMachine);

            // Labels
            var lblA = new Label { Text = "Shift A", Location = new Point(50, 90), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold),ForeColor = Color.GreenYellow };
            var lblB = new Label { Text = "Shift B", Location = new Point(50, 140), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.GreenYellow };
            var lblC = new Label { Text = "Shift C", Location = new Point(50, 190), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.GreenYellow };

            this.Controls.Add(lblA);
            this.Controls.Add(lblB);
            this.Controls.Add(lblC);

            MaskedTextBox MakeMaskedTextBox(Point p)
            {
                return new MaskedTextBox("00:00")
                {
                    Location = p,
                    Size = new Size(70, 25),
                    Font = new Font("Segoe UI", 10),
                    TextAlign = HorizontalAlignment.Center,
                    PromptChar = '_'    // δείχνει κενό ως _
                };
            }

            
        

        // Helper to create time-only DateTimePicker
        DateTimePicker MakeTimePicker(Point p)
            {
                return new DateTimePicker
                {
                    Format = DateTimePickerFormat.Time,
                    ShowUpDown = true,
                    Location = p,
                    Size = new Size(90, 25),
                    Font = new Font("Segoe UI", 10)
                    
                };
            }

            // Shift A time pickers
            dtAStart = MakeMaskedTextBox(new Point(120, 85));
            dtAEnd = MakeMaskedTextBox(new Point(220, 85));
            
            // Shift B
            dtBStart = MakeMaskedTextBox(new Point(120, 135));
            dtBEnd = MakeMaskedTextBox(new Point(220, 135));
            
            // Shift C
            dtCStart = MakeMaskedTextBox(new Point(120, 185));
            dtCEnd = MakeMaskedTextBox(new Point(220, 185));
            
            this.Controls.AddRange(new Control[] { dtAStart, dtAEnd, dtBStart, dtBEnd, dtCStart, dtCEnd });
                        

            // Save button
            btnSave = new Button
            {
                Text = "Save Shift Plan",
                Location = new Point(50, 240),
                Size = new Size(200, 35),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);
        }
    }
}
