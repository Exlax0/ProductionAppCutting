using Meme_Oven_Data.Repository;
using Meme_Oven_Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;

namespace Meme_Oven_Data
{
    public partial class DesOven1 : UserControl
    {
        public static int TotalCounterMachine1 { get; set; }
        public static int EfficiencyHourMachine1 { get; set; }
        public string MachineName { get; set; } = "Cutting - Machine 01";


        private readonly MicrOvenContext _dbContext;
        private Chart chart;
        Series series;
        Series onOffSeries;
        Button btMain;
        Button btData;
        Label lblRecipe;
        Label lblEfficiency;
        private Chart efficiencyChart;
        private Series efficiencySeries;
        Button btSetValues;
        private NumericUpDown txtPlanHour;
        private NumericUpDown txtPlanShift;
        Label lblPlanHour;
        Label lblPlanShift;

        private ComboBox StopReasons;
        private MaskedTextBox StartEvent,StopEvent;
        private Button btSaveStopEvent;
        private Label StartTimeEvent, StopTimeEvent;


        private ComboBox cmbOperator;
        private Button btnChangeOperator;
        private Label lblCurrentOperator;

        private readonly BindingSource _operatorBindingSource = new BindingSource();
    

        public DesOven1(MicrOvenContext dbContext)
        {
            _dbContext = dbContext;
            InitializeComponent();
            InitOperatorControls();
            LoadOperators();
            InitStopEvent();
            LoadStopReasons();

            this.btSetValues = new Button()
            {
                Text = "Set Values",
                Size = new Size(100, 25),
                BackColor = Color.LightCyan,
                ForeColor = Color.Green,
                Location = new Point(1600, 220),
                Visible = true
            };

            this.txtPlanHour = new NumericUpDown()
            {
                Location = new Point(1480, 220),
                Size = new Size(100, 30),
                Font = new Font("Segoe UI", 16),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Maximum = 9999
                

            };
            txtPlanHour.Controls[0].Visible = false;

            this.lblPlanHour = new Label()
            {
                Text = "Κοπές την ώρα",
                Location = new Point(1300, 220),
                Size = new Size(180, 35),
                Font = new Font("Segoe UI", 16),
                ForeColor = Color.Red,
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None
               
            };

            this.txtPlanShift = new NumericUpDown()
            {
                Location = new Point(1480, 180),
                Size = new Size(100, 30),
                Font = new Font("Segoe UI", 16),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Maximum = 9999
            };
            txtPlanShift.Controls[0].Visible = false;

            this.lblPlanShift = new Label()
            {
                Text ="Κοπές Βάρδιας",
                Location = new Point(1300, 180),
                Size = new Size(180, 35),
                Font = new Font("Segoe UI", 16),
                ForeColor = Color.Red,
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None
            };


            this.lblEfficiency = new Label()
            {
                Text ="Working Efficiency 25%",
                ForeColor = Color.Green,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 14,FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(920, 220),
                Size = new Size(350,35),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(5),
                Visible = true
            };

            this.lblRecipe = new Label()
            {
                Text = "Recipe Name",
                ForeColor = Color.Green,
                BackColor = Color.Transparent,
                Font = new Font("Arial", 12, FontStyle.Bold | FontStyle.Italic),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(1470, 100),
                Size = new Size(200, 40),
                BorderStyle = BorderStyle.FixedSingle, // Border around label
                Padding = new Padding(5), // Adds space inside the label around text
                Visible = false // Ensures label is visible
            };

            this.btData = new Button()
            {
                Size = new Size(100, 25),
                Location = new Point(1600, 20),
                BackColor = Color.White,
                Text = "Data",
                Visible = false
            };

            this.btMain = new Button()
            {
                Size = new Size(100, 25),
                Location = new Point(1600, 20),
                BackColor = Color.White,
                Text = "Main",
                Visible = false
            };

            btMain.Click += BtMain_Click;
            btData.Click += BtData_Click;
            btSetValues.Click +=  btSetValues_Click;
            txtPlanHour.KeyPress += TxtNumericOnly_KeyPress;
            txtPlanShift.KeyPress += TxtNumericOnly_KeyPress;


            this.chart = new Chart
            {
                Size = new Size(1657, 550),
                Location = new Point(50, 400),
                BackColor = Color.White // Neutral background color
            };

            this.series = new Series("CutsPer15Min")
            {
                ChartType = SeriesChartType.Column,
                BorderWidth = 2,
                Color = Color.Blue,
                XValueType = ChartValueType.DateTime
            };

            
            


            ChartArea chartArea = new ChartArea("MainArea")
            {
                BackColor = Color.White, // Set chart background color to white

                AxisX = {
                            Title = "Time",
                            IntervalAutoMode = IntervalAutoMode.VariableCount,
                            TitleFont = new Font("Arial", 18, FontStyle.Bold),
                            LabelStyle = { ForeColor = Color.Black,
                                          Format= "dd/MM/yyyy\nHH:mm:ss"},
                            MajorGrid = { LineColor = Color.LightGray }
                        },

                AxisY = {
                            Title = "Cuts per 15 minutes",
                            TitleFont = new Font("Arial", 18, FontStyle.Bold),
                            LabelStyle = { ForeColor = Color.Black },
                            MajorGrid = { LineColor = Color.LightGray }
                        }
            };

            chartArea.Position.Auto = false;
            chartArea.Position.X = 2;
            chartArea.Position.Y = 2;
            chartArea.Position.Width = 96;
            chartArea.Position.Height = 96;
            chartArea.InnerPlotPosition.Auto = false;
            chartArea.InnerPlotPosition.X = 5;      // left padding
            chartArea.InnerPlotPosition.Y = 5;      // top padding
            chartArea.InnerPlotPosition.Width = 90; // graph width %
            chartArea.InnerPlotPosition.Height = 90; // graph height %

            chart.ChartAreas.Add(chartArea);

            // Set chart background color
            chart.BackColor = Color.White;

            

           

            this.Controls.Add(lblPlanShift);
            this.Controls.Add(lblPlanHour);
            this.Controls.Add(txtPlanHour);
            this.Controls.Add(txtPlanShift);
            this.Controls.Add(btSetValues);
            this.Controls.Add(lblEfficiency);
            this.Controls.Add(lblRecipe);
            this.Controls.Add(btData);
            this.Controls.Add(btMain);
            this.chart.Series.Add(series);
           
            //UpdateChart();
            this.Controls.Add(chart);
        }

        private void LoadStopReasons()
        {
            // Φόρτωσε από SQL στη μνήμη EF
            _dbContext.StopReasons.Load();

            // Δέσε τα δεδομένα στο ComboBox
            StopReasons.DataSource = _dbContext.StopReasons.Local.ToBindingList();
            StopReasons.DisplayMember = "Description";
            StopReasons.ValueMember = "Id";

            // Προαιρετικά: βάλε κενό default
            StopReasons.SelectedIndex = -1;
        }


        private void InitStopEvent()
        {
            StopReasons = new ComboBox()
            {
                Location = new Point(20, 250),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 12)
            };
            StopReasons.SelectedIndexChanged += StopReasons_SelectedIndexChanged;
            this.Controls.Add(StopReasons);

            StopTimeEvent = new Label()
            {
                Text = "Ώρα Σταματήματος",
                Location = new Point(400, 220),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Red
            };

            StartTimeEvent = new Label()
            {
                Text = "Ώρα Εκκίνησης",
                Location = new Point(230, 220),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.GreenYellow
            };


            this.Controls.Add(StartTimeEvent);
            this.Controls.Add(StopTimeEvent);

            MaskedTextBox MakeMaskedTextBox(Point p)
            {
                return new MaskedTextBox("00:00")
                {
                    Location = p,
                    Size = new Size(70, 30),
                    Font = new Font("Segoe UI", 10),
                    TextAlign = HorizontalAlignment.Center,
                    PromptChar = '_'    // δείχνει κενό ως _
                };
            }

            StopEvent = MakeMaskedTextBox(new Point(430,250));
            StartEvent = MakeMaskedTextBox(new Point(260, 250));

            this.Controls.Add(StopEvent);
            this.Controls.Add(StartEvent);

            btSaveStopEvent = new Button()
            {
                Text = "Αποθήκευση Αιτίας Παύσης",
                Location = new Point(200, 310),
                Size = new Size(250, 40),
                BackColor = Color.Bisque,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            btSaveStopEvent.Click += btSaveStopEvent_Click;
            this.Controls.Add(btSaveStopEvent);
        }

        private void btSaveStopEvent_Click(object sender, EventArgs e)
        {
            // 1) Έλεγχος αν έχει επιλεγεί αιτία στάσης
            if (StopReasons.SelectedItem is not StopReason reason)
            {
                MessageBox.Show("Παρακαλώ επιλέξτε αιτία παύσης.",
                                "Αιτία παύσης",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // 2) Parse ωρών από τα MaskedTextBox
            if (!TimeSpan.TryParse(StopEvent.Text, out var stopTime))
            {
                MessageBox.Show("Μη έγκυρη ώρα σταματήματος.",
                                "Λάθος ώρα",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            if (!TimeSpan.TryParse(StartEvent.Text, out var startTime))
            {
                MessageBox.Show("Μη έγκυρη ώρα εκκίνησης.",
                                "Λάθος ώρα",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // 3) Συνθέτουμε DateTime (χρησιμοποιώ την σημερινή ημερομηνία)
            var today = DateTime.Today;
            var dtStop = today.Add(stopTime);
            var dtStart = today.Add(startTime);

            // Αν δεν θες να δέχεσαι "αναστροφές":
            if (dtStart > dtStop)
            {
                MessageBox.Show("Η ώρα εκκίνησης δεν μπορεί να είναι μετά την ώρα σταματήματος.",
                                "Λάθος ώρες",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // 4) Φτιάχνουμε το event
            var evt = new MachineStopEvent
            {
                Machine = "Cutting - Machine 01",
                StopReasonId = reason.Id,
                StartTime = dtStart,
                EndTime = dtStop,
                OperatorName = DataTags.CurrentOperator1 ?? "Unknown",
                Comment = ""   // μπορείς αργότερα να βάλεις TextBox για σχόλιο
            };

            // 5) Αποθήκευση στη βάση
            _dbContext.MachineStopEvents.Add(evt);
            _dbContext.SaveChanges();

            MessageBox.Show("Η αιτία παύσης αποθηκεύτηκε επιτυχώς.",
                            "Καταχώρηση",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            // 6) Προαιρετικό reset
            StopEvent.Text = "";
            StartEvent.Text = "";
            StopReasons.SelectedIndex = -1;
        }



        private void StopReasons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StopReasons.SelectedItem == null)
                return;

            string desc = StopReasons.Text;

            var reason = _dbContext.StopReasons
                .FirstOrDefault(x => x.Description == desc);

            // reason τώρα περιέχει το αντικείμενο StopReason
        }


        private void InitOperatorControls()
        {
            // Label τρέχον χειριστή
            lblCurrentOperator = new Label
            {
                Text = "Τρέχων Χειριστής: (κανένας)",
                Location = new Point(390, 155),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.LightPink
            };
            this.Controls.Add(lblCurrentOperator);

            // ComboBox χειριστών
            cmbOperator = new ComboBox
            {
                Location = new Point(20, 150),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cmbOperator.DataSource = _operatorBindingSource;
            cmbOperator.DisplayMember = "FullName";
            this.Controls.Add(cmbOperator);

            // Button αλλαγής χειριστή
            btnChangeOperator = new Button
            {
                Text = "Αλλαγή Χειριστή",
                Location = new Point(230, 150),
                Size = new Size(150, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.LightGreen
            };
            btnChangeOperator.Click += BtnChangeOperator_Click;
            this.Controls.Add(btnChangeOperator);
        }

        private void LoadOperators()
        {
            _dbContext.Operators.Load();

            _operatorBindingSource.DataSource =
                _dbContext.Operators.Local
                          .OrderBy(o => o.FullName)
                          .ToList();

            if (cmbOperator.Items.Count > 0)
                cmbOperator.SelectedIndex = 0;   // προαιρετικά
        }


        private void BtnChangeOperator_Click(object sender, EventArgs e)
        {
            if (cmbOperator.SelectedItem is Operators op)
            {
                DataTags.CurrentOperator1 = op.FullName;

                lblCurrentOperator.Text = $"Τρέχων Χειριστής: {DataTags.CurrentOperator1}";

                MessageBox.Show(
                    $"Ο νέος χειριστής του μηχανήματος 1 είναι: {DataTags.CurrentOperator1}",
                    "Αλλαγή χειριστή",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Παρακαλώ επιλέξτε χειριστή.");
            }
        }


        private void TxtNumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Block input
            }
        }


        private void BtMain_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Button Clicked!");
            chart.Visible = false;
            datePickerFrom.Visible = false;
            datePickerTo.Visible = false;
            timePickerFrom.Visible = false;
            timePickerTo.Visible = false;
            search_btn.Visible = false;
            Live_btn.Visible = false;
            txt1.Text = "Εικόνα Φούρνου 1";
            btMain.Visible = false;
            btData.Visible = true;
            lblRecipe.Visible = true;
            lblRecipe.BringToFront();
            this.BackColor = Color.FromArgb(20, 50, 100);
            //tmrReadColor.Enabled = true;
            tmrReadColor.Enabled = true;

        }

        private void BtData_Click(object sender, EventArgs e)
        {
            chart.Visible = true;
            datePickerFrom.Visible = true;
            datePickerTo.Visible = true;
            timePickerFrom.Visible = true;
            timePickerTo.Visible = true;
            search_btn.Visible = true;
            Live_btn.Visible = true;
            txt1.Text = "Θερμοκρασία Φούρνου 1";
            btMain.Visible = true;
            btData.Visible = false;
            lblRecipe.Visible = false;
            this.BackColor = Color.FromArgb(32, 48, 64);
            tmrReadColor.Enabled = false;
            tmrReadColor.Enabled = false;

            

        }


        private void UpdateChart()
        {
            try
            {
                DateTime oneHourAgo = DateTime.Now.AddHours(-1);
                var data = _dbContext.TempOven1
                   .Where(x =>  x.Cut == 1 && x.Date >= oneHourAgo)
                   .OrderByDescending(x => x.Date) // Get the most recent records
                    //.Take(20)                               //.Take(1000)                     // Limit to 100 records
                   .OrderBy(x => x.Date)          // Reorder to ascending by Date for proper charting
                   .ToList();

                TotalCounterMachine1 = data.Count();
                this.series.Points.Clear();
                
                // Add data points to the chart
                for (int i = 1; i < data.Count; i++)
                {
                    this.series.Points.AddXY(data.ElementAt(i).Date, data.ElementAt(i).Cut);
                    // this.onOffSeries.Points.AddXY(data.ElementAt(i).Date, data.ElementAt(i).OnOffOven);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating chart: {ex.Message}");
            }
        }

        private void btSetValues_Click(object sender, EventArgs e)
        {
            // Example: values from textboxes / numericUpDowns
            string machineName = "Cutting - Machine 01";  // or from a ComboBox
            int planHour = Convert.ToInt32(txtPlanHour.Value);                           // e.g. int.Parse(txtPlanHour.Text);
            int planShift = Convert.ToInt32(txtPlanShift.Value);                          // e.g. int.Parse(txtPlanShift.Text);

            // Try to find existing plan for this machine
            var plan = _dbContext.MachinePlan
                .SingleOrDefault(x => x.Machine == machineName);

            if (plan == null)
            {
                // No record yet -> create one
                plan = new MachinePlan
                {
                    Machine = machineName
                };
                _dbContext.MachinePlan.Add(plan);
            }

            // Update values (for new or existing row)
            plan.PlanHour = planHour;
            plan.PlanShift = planShift;
            plan.Date = DateTime.Now;  // last updated

            _dbContext.SaveChanges();
                        
        }

        private void Update1ChartTimer_Tick(object sender, EventArgs e)
        {
            UpdateChart();

            DateTime now = DateTime.Now;
            DateTime currentHourStart = new DateTime(
                now.Year, now.Month, now.Day,
                now.Hour, 0, 0);

            DateTime from = currentHourStart.AddHours(-1);
            DateTime to = currentHourStart;

            int totalCounterMachine1 = _dbContext.TempOven1
                .Count(x => x.Machine == "Cutting - Machine 01"
                         && x.Date >= from
                         && x.Date < to
                         && x.Cut == 1);

            var plan = _dbContext.MachinePlan
                .SingleOrDefault(x => x.Machine == "Cutting - Machine 01");

            if (plan == null || plan.PlanHour <= 0)
            {
                lblEfficiency.Text = "Efficiency: N/A";
                return;
            }

            double efficiencyPrevHour =
                (double)totalCounterMachine1 / plan.PlanHour * 100.0;

            lblEfficiency.Text =
     $"Efficiency: {efficiencyPrevHour:F1}%   |   {totalCounterMachine1}/{plan.PlanHour}";

            // Apply colors based on efficiency
            if (efficiencyPrevHour >= 80)
            {
                lblEfficiency.ForeColor = Color.LimeGreen;
                lblEfficiency.BackColor = Color.FromArgb(30, 60, 30);  // dark green background
            }
            else if (efficiencyPrevHour >= 50)
            {
                lblEfficiency.ForeColor = Color.Gold;
                lblEfficiency.BackColor = Color.FromArgb(60, 60, 20);  // dark yellow background
            }
            else
            {
                lblEfficiency.ForeColor = Color.Red;
                lblEfficiency.BackColor = Color.FromArgb(60, 20, 20); // dark red background
            }
        }


        private void search_btn_Click(object sender, EventArgs e)
        {
            Update1ChartTimer.Enabled = false;
            var dateTimePickerFrom = datePickerFrom.Value.Date + timePickerFrom.Value.TimeOfDay;
            var dateTimePickerTo = datePickerTo.Value.Date + timePickerTo.Value.TimeOfDay;

            var data = _dbContext.TempOven1
                    .Where(x => x.Date >= dateTimePickerFrom && x.Date <= dateTimePickerTo && x.Cut == 1)
                    .OrderByDescending(x => x.Date) // Get the most recent records
                                                    //.Take(1000)                     // Limit to 100 records
                    .OrderBy(x => x.Date)          // Reorder to ascending by Date for proper charting
                    .ToList();



            int count = data.Count;

            this.series.Points.Clear();

            // Add data points to the chart
            for (int i = 1; i < data.Count; i++)
            {
                this.series.Points.AddXY(data.ElementAt(i).Date, data.ElementAt(i).Cut);

            }

            // 3) Calculate time range in hours
            double hours = (dateTimePickerTo - dateTimePickerFrom).TotalHours;
            if (hours <= 0)
            {
                lblEfficiency.Text = "Efficiency: N/A";
                return;
            }

            // 4) Get plan for this machine
            var plan = _dbContext.MachinePlan
                .SingleOrDefault(x => x.Machine == "Cutting - Machine 01");

            if (plan == null || plan.PlanHour <= 0)
            {
                lblEfficiency.Text = "Efficiency: N/A";
                return;
            }

            // 5) Expected cuts in that period
            double expectedCuts = plan.PlanHour * hours;

            // 6) Efficiency for the searched period
            double efficiency = (expectedCuts > 0)
                ? (count / expectedCuts) * 100.0
                : 0.0;

            // 7) Update label text
            lblEfficiency.Text =
                $"Efficiency (search): {efficiency:F1}%   |   {count}/{expectedCuts:F0}";

            // 8) Color-coding based on efficiency
            if (efficiency >= 80)
            {
                lblEfficiency.ForeColor = Color.LimeGreen;
                lblEfficiency.BackColor = Color.FromArgb(30, 60, 30);
            }
            else if (efficiency >= 50)
            {
                lblEfficiency.ForeColor = Color.Gold;
                lblEfficiency.BackColor = Color.FromArgb(60, 60, 20);
            }
            else
            {
                lblEfficiency.ForeColor = Color.Red;
                lblEfficiency.BackColor = Color.FromArgb(60, 20, 20);
            }

        }

        private void Live_btn_Click(object sender, EventArgs e)
        {
            Update1ChartTimer.Enabled = true;
        }

        private void tmrReadColor_Tick(object sender, EventArgs e)
        {



        }

        

        private void DesOven1_Load(object sender, EventArgs e)
        {
            var plan = _dbContext.MachinePlan
                .Where(x => x.Machine == MachineName)
                .OrderByDescending(x => x.Date)
                .FirstOrDefault();

            if (plan != null)
            {
                // Make sure max is high enough
                if (plan.PlanHour > txtPlanHour.Maximum) txtPlanHour.Maximum = plan.PlanHour;
                if (plan.PlanShift > txtPlanShift.Maximum) txtPlanShift.Maximum = plan.PlanShift;

                txtPlanHour.Value = plan.PlanHour;
                txtPlanShift.Value = plan.PlanShift;
            }
            else
            {
                // No plan yet → optional defaults
                txtPlanHour.Value = 0;
                txtPlanShift.Value = 0;
            }

        }
               

    }
}
