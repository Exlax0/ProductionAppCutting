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

        public DesOven1(MicrOvenContext dbContext)
        {
            _dbContext = dbContext;
            //_plc = plc;
            InitializeComponent();

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

            

            // Configure the On/Off series
            this.onOffSeries = new Series("OnOffSeries")
            {
                ChartType = SeriesChartType.StepLine, // Binary data fits better with StepLine
                BorderWidth = 2,
                Color = Color.Red,
                YAxisType = AxisType.Secondary // Use AxisY2
            };


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
            this.chart.Series.Add(onOffSeries);
            //UpdateChart();
            this.Controls.Add(chart);
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
