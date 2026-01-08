using Meme_Oven_Data.Repository;
using Meme_Oven_Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using System.Linq;




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
        Label lblEfficiency, lblEifficiencyShift;
        private Chart efficiencyChart;
        private Series efficiencySeries;
        Button btSetValues;
        private NumericUpDown txtPlanHour;
        private NumericUpDown txtPlanShift;
        private NumericUpDown txtPiecesPerCut;
        Label lblPlanHour;
        Label lblPlanShift;
        Label lblPiecesPerCut;
        Label lblPiecesLiveShift;

        private ComboBox StopReasons;
        private MaskedTextBox StartEvent, StopEvent;
        private Button btSaveStopEvent;
        private Label StartTimeEvent, StopTimeEvent;

        private Chart pieChart;

        private ComboBox ProductList;
        private Label CurrentProduct, lblDescProd;
        private Button btChangeProduct;

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
            InitLivePiecesCount();
            InitChartPie();
            InitProduct();


            this.lblPiecesPerCut = new Label()
            {
                Text = "Κομμάτια Ανά Κοπή",
                Location = new Point(20, 360),
                AutoSize = true,
                // Size = new Size(180, 35),
                Font = new Font("Segoe UI", 16),
                ForeColor = Color.Red,
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None
            };
            this.Controls.Add(lblPiecesPerCut);

            this.txtPiecesPerCut = new NumericUpDown()
            {
                Location = new Point(250, 360),
                Size = new Size(75, 30),
                Font = new Font("Segoe UI", 16),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Maximum = 99999
            };
            txtPiecesPerCut.Controls[0].Visible = false;
            this.Controls.Add(txtPiecesPerCut);

            this.btSetValues = new Button()
            {
                Text = "Αποθήκευση Τιμών",
                Size = new Size(250, 40),
                BackColor = Color.LightCyan,
                ForeColor = Color.Black,
                Location = new Point(80, 420),
                Font = new Font("Segoe UI", 16),
                Visible = true
            };

            this.txtPlanHour = new NumericUpDown()
            {
                Location = new Point(250, 320),
                Size = new Size(75, 30),
                Font = new Font("Segoe UI", 16),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Maximum = 99999


            };
            txtPlanHour.Controls[0].Visible = false;

            this.lblPlanHour = new Label()
            {
                Text = "Κοπές την ώρα",
                Location = new Point(20, 320),
                Size = new Size(180, 35),
                Font = new Font("Segoe UI", 16),
                ForeColor = Color.Red,
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None

            };

            this.txtPlanShift = new NumericUpDown()
            {
                Location = new Point(250, 280),
                Size = new Size(75, 30),
                Font = new Font("Segoe UI", 16),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Maximum = 99999
            };
            txtPlanShift.Controls[0].Visible = false;

            this.lblPlanShift = new Label()
            {
                Text = "Κοπές Βάρδιας",
                Location = new Point(20, 280),
                Size = new Size(180, 35),
                Font = new Font("Segoe UI", 16),
                ForeColor = Color.Red,
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None
            };


            this.lblEfficiency = new Label()
            {
                Text = "Working Efficiency 25%",
                ForeColor = Color.Green,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 17, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(1100, 200),
                Size = new Size(570, 45),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(5),
                Visible = true
            };

            this.lblEifficiencyShift = new Label()
            {
                Text = "Working Efficiency 25%",
                ForeColor = Color.Green,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 17, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(1100, 260),
                Size = new Size(570, 45),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(5),
                Visible = true
            };
            this.Controls.Add(lblEifficiencyShift);

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
            btSetValues.Click += btSetValues_Click;
            txtPlanHour.KeyPress += TxtNumericOnly_KeyPress;
            txtPlanShift.KeyPress += TxtNumericOnly_KeyPress;


            this.chart = new Chart
            {
                Size = new Size(1680, 420),
                Location = new Point(20, 550),
                BackColor = Color.White // Neutral background color
            };

            chart.ChartAreas.Clear();


            ChartArea chartArea = new ChartArea("MainArea")
            {
                BackColor = Color.White, // Set chart background color to white

                AxisX = {
                            Title = "",
                            IntervalAutoMode = IntervalAutoMode.VariableCount,
                            TitleFont = new Font("Arial", 20, FontStyle.Bold),
                            LabelStyle = { ForeColor = Color.Black,
                                          Format= "dd/MM/yyyy\nHH:mm"},
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
            chartArea.Position.X = 1;
            chartArea.Position.Y = 2;
            chartArea.Position.Width = 96;
            chartArea.Position.Height = 96;
            chartArea.InnerPlotPosition.Auto = false;
            chartArea.InnerPlotPosition.X = 0;      // left padding
            chartArea.InnerPlotPosition.Y = 2;      // top padding
            chartArea.InnerPlotPosition.Width = 100; // graph width %
            chartArea.InnerPlotPosition.Height = 90; // graph height %

            // Δεύτερος άξονας Υ για τα stops (0–1)
            chartArea.AxisY2.Enabled = AxisEnabled.False;
            chartArea.AxisY2.Title = "Stops (0/1)";
            chartArea.AxisY2.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            chartArea.AxisY2.Minimum = 0;
            chartArea.AxisY2.Maximum = 1;
            chartArea.AxisY2.MajorGrid.LineColor = Color.Transparent; // να μην γεμίζει το γράφημα με γραμμές


            // Set chart background color
            chart.BackColor = Color.White;


            chart.ChartAreas.Add(chartArea);

            this.series = new Series("Κοπές Κοπτικού 1")
            {
                ChartType = SeriesChartType.Column,
                BorderWidth = 0,
                Color = Color.Blue,
                XValueType = ChartValueType.DateTime,
                ChartArea = "MainArea"
            };
            series["PointWidth"] = "0.5";
            series.IsXValueIndexed = false;
            chart.Series.Add(series);


            this.Controls.Add(lblPlanShift);
            this.Controls.Add(lblPlanHour);
            this.Controls.Add(txtPlanHour);
            this.Controls.Add(txtPlanShift);
            this.Controls.Add(btSetValues);
            this.Controls.Add(lblEfficiency);
            this.Controls.Add(lblRecipe);
            this.Controls.Add(btData);
            this.Controls.Add(btMain);
            //this.chart.Series.Add(series);

            //UpdateChart();
            this.Controls.Add(chart);
        }

        private void InitProduct()
        {
            lblDescProd = new Label()
            {
                Location = new Point(1040, 400),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Text = "Επιλέξτε προϊόν:",
                ForeColor = Color.Red
            };
            this.Controls.Add(lblDescProd);

            btChangeProduct = new Button
            {
                Size = new Size(280, 40),
                Location = new Point(1240, 450),
                BackColor = Color.Aquamarine,
                Font = new Font("Segoe UI", 16),
                Text = "Αλλαγή Προιόντος"
            };
            this.Controls.Add(btChangeProduct);
            btChangeProduct.Click += btChangeProduct_Click;

            ProductList = new ComboBox()
            {
                Location = new Point(1210, 400),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 12)
            };

            // Φόρτωσε από SQL στη μνήμη EF
            _dbContext.ProductCutPlan.Load();

            // Δέσε τα δεδομένα στο ComboBox
            ProductList.DataSource = _dbContext.ProductCutPlan.Local.ToBindingList();
            ProductList.DisplayMember = "ProductCode";
            ProductList.ValueMember = "Id";

            // Προαιρετικά: βάλε κενό default
            ProductList.SelectedIndex = -1;

            ProductList.SelectedIndexChanged += ProductList_SelectedIndexChanged;
            this.Controls.Add(ProductList);

            CurrentProduct = new Label()
            {
                Location = new Point(1420, 400),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Text = "Τωρινός Κωδικός Κοπής",
                ForeColor = Color.LightGreen
            };
            this.Controls.Add(CurrentProduct);
        }

        private void ProductList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProductList.SelectedItem == null)
                return;

            string desc = ProductList.Text;

            var reason = _dbContext.ProductCutPlan
                .FirstOrDefault(x => x.ProductCode == desc);
        }

        private void btChangeProduct_Click(object sender, EventArgs e)
        {


            if (ProductList.SelectedItem is ProductCutPlan op)
            {
                string machineName = "Cutting - Machine 01";
                DataTags.CurrentCode1 = op.ProductCode;
                DataTags.CutPieces1 = op.PiecesPerCut;
                DataTags.HourCuts1 = op.CutsPerHour;
                DataTags.ShiftCuts1 = DataTags.HourCuts1 * 8;
                CurrentProduct.Text = $"Τρέχων Κωδικός: {DataTags.CurrentCode1}";

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
                plan.PlanHour = DataTags.HourCuts1;
                plan.PlanShift = DataTags.ShiftCuts1;
                plan.Date = DateTime.Now;  // last updated
                plan.PiecesPerCut = DataTags.CutPieces1;

                var stopEvent = new MachineStopEvent
                {
                    Machine = machineName,
                    StopReasonId = 7, // π.χ. "Αλλαγή προϊόντος"
                    StartTime = DateTime.Now.AddMinutes(-1), // αν θες 1' πριν
                    EndTime = DateTime.Now,                // και τώρα ως τέλος
                    OperatorName = DataTags.CurrentOperator1 ?? "Unknown",
                    Comment = "" // αργότερα μπορείς να το γεμίσεις από TextBox
                };

                _dbContext.MachineStopEvents.Add(stopEvent);
                _dbContext.SaveChanges();


                MessageBox.Show(
                    $"Ο νέος κωδικός κοπής είναι: {DataTags.CurrentCode1}",
                    "Αλλαγή κωδικού",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Παρακαλώ επιλέξτε κωδικό.");
            }
        }

        private void InitChartPie()
        {
            pieChart = new Chart
            {
                Size = new Size(400, 300),
                Location = new Point(600, 150),  // όπου σε βολεύει
                BackColor = Color.Transparent
            };

            pieChart.ChartAreas.Add(new ChartArea("PieArea"));

            this.Controls.Add(pieChart);

        }

        private void UpdatePieChart()
        {
            DateTime now = DateTime.Now;
            TimeSpan nowTime = now.TimeOfDay;

            // 1) Βρες τη βάρδια
            var shifts = _dbContext.MachineShiftPlan
                .Where(x => x.Machine == "Κοπτικό Μηχάνημα 01")
                .ToList();

            if (!shifts.Any())
                return;

            var currentShift = shifts.FirstOrDefault(s =>
                (s.StartTime <= s.EndTime && nowTime >= s.StartTime && nowTime < s.EndTime) ||
                (s.StartTime > s.EndTime && (nowTime >= s.StartTime || nowTime < s.EndTime))
            );

            if (currentShift == null)
                return;

            // 2) start–end shift datetime
            DateTime shiftStart;
            DateTime shiftEnd;

            if (currentShift.StartTime <= currentShift.EndTime)
            {
                shiftStart = now.Date + currentShift.StartTime;
                shiftEnd = now.Date + currentShift.EndTime;
            }
            else
            {
                shiftStart = nowTime < currentShift.EndTime
                    ? now.Date.AddDays(-1) + currentShift.StartTime
                    : now.Date + currentShift.StartTime;

                var span = (currentShift.EndTime > currentShift.StartTime)
                    ? currentShift.EndTime - currentShift.StartTime
                    : new TimeSpan(24, 0, 0) - (currentShift.StartTime - currentShift.EndTime);

                shiftEnd = shiftStart.Add(span);
            }

            double hoursPassed = (now - shiftStart).TotalHours;
            double totalHours = (shiftEnd - shiftStart).TotalHours;

            if (hoursPassed < 0 || totalHours <= 0)
                return;

            // 3) Κοπές στη βάρδια
            var shiftCuts = _dbContext.TempOven1
                .Where(x => x.Date >= shiftStart &&
                            x.Date <= now &&
                            x.Cut == 1)
                .ToList();

            int cutsThisShift = shiftCuts.Count;

            // 4) Πλάνο
            var plan = _dbContext.MachinePlan
                .FirstOrDefault(x => x.Machine == "Cutting - Machine 01");

            if (plan == null || plan.PlanShift <= 0 || plan.PiecesPerCut <= 0)
                return;

            int totalPlan = plan.PlanShift * plan.PiecesPerCut;
            int piecesProduced = cutsThisShift * plan.PiecesPerCut;

            if (piecesProduced > totalPlan)
                piecesProduced = totalPlan;

            double expectedSoFar = (hoursPassed / totalHours) * totalPlan;
            if (expectedSoFar < 0) expectedSoFar = 0;
            if (expectedSoFar > totalPlan) expectedSoFar = totalPlan;

            double lost = Math.Max(0, expectedSoFar - piecesProduced);   // άργησαν/χάθηκαν
            double remaining = Math.Max(0, totalPlan - expectedSoFar);        // απομένουν μέχρι τέλος βάρδιας

            // 5) Pie chart
            // 5) Pie chart
            pieChart.Series.Clear();
            pieChart.Legends.Clear();

            // προαιρετικά “διάφανο” φόντο
            pieChart.BackColor = Color.Transparent;
            if (pieChart.ChartAreas.Count > 0)
                pieChart.ChartAreas[0].BackColor = Color.Transparent;

            var pie = new Series("ShiftStats")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = false,              // 🔴 Όχι αυτόματα labels
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Label = ""                                // 🔴 Άδειο default label
            };

            // 🔴 Πλήρης απενεργοποίηση labels στην πίτα
            pie["PieLabelStyle"] = "Disabled";           // <-- Κλειδώνει να ΜΗΝ δείχνει labels
            pie["PieDrawingStyle"] = "SoftEdge";

            pie.Points.AddXY("Κομμένα", piecesProduced);
            pie.Points.AddXY("Αργοπορημένα", lost);
            pie.Points.AddXY("Εναπομείναντα", remaining);

            // 🔹 ΜΟΝΟ legend + tooltip, ΚΑΝΕΝΑ label στην πίτα
            foreach (var p in pie.Points)
            {
                double value = p.YValues[0];

                p.IsValueShownAsLabel = false;   // σίγουρα όχι label στο slice
                p.Label = "";                    // άδειο label

                // Υπόμνημα
                p.LegendText = $"{p.AxisLabel}: {value:0}";

                // Tooltip
                p.ToolTip = $"{p.AxisLabel}: {value:0}";
            }

            pieChart.Series.Add(pie);

            // Υπόμνημα δεξιά
            var legend = new Legend("PieLegend")
            {
                Docking = Docking.Right,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            pieChart.Legends.Add(legend);

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

        private void InitLivePiecesCount()
        {
            lblPiecesLiveShift = new Label()
            {
                Location = new Point(1100, 150),
                AutoSize = true,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.Green,
                Text = "1200"
            };
            this.Controls.Add(lblPiecesLiveShift);
        }
        private void InitStopEvent()
        {
            StopReasons = new ComboBox()
            {
                Location = new Point(20, 150),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 12)
            };
            StopReasons.SelectedIndexChanged += StopReasons_SelectedIndexChanged;
            this.Controls.Add(StopReasons);

            StopTimeEvent = new Label()
            {
                Text = "Ώρα Σταματήματος",
                Location = new Point(400, 120),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Red
            };

            StartTimeEvent = new Label()
            {
                Text = "Ώρα Εκκίνησης",
                Location = new Point(230, 120),
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

            StopEvent = MakeMaskedTextBox(new Point(430, 150));
            StartEvent = MakeMaskedTextBox(new Point(260, 150));

            this.Controls.Add(StopEvent);
            this.Controls.Add(StartEvent);

            btSaveStopEvent = new Button()
            {
                Text = "Αποθήκευση Αιτίας Παύσης",
                Location = new Point(170, 210),
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
                Location = new Point(1430, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.LightPink
            };
            this.Controls.Add(lblCurrentOperator);

            // ComboBox χειριστών
            cmbOperator = new ComboBox
            {
                Location = new Point(1260, 20),
                Size = new Size(160, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 12)
            };
            cmbOperator.DataSource = _operatorBindingSource;
            cmbOperator.DisplayMember = "FullName";
            this.Controls.Add(cmbOperator);

            // Button αλλαγής χειριστή
            btnChangeOperator = new Button
            {
                Text = "Αλλαγή Χειριστή",
                Location = new Point(1380, 60),
                Size = new Size(160, 30),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
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
        private void ApplyEfficiencyColor(Label label, double efficiency)
        {
            // Adjust thresholds if you used different ones in the previous-hour label
            if (efficiency >= 90)
            {
                label.ForeColor = Color.LimeGreen;
                label.BackColor = Color.FromArgb(30, 60, 30);  // dark green background
            }
            else if (efficiency >= 70)
            {
                label.ForeColor = Color.Goldenrod; // yellow/orange
                label.BackColor = Color.FromArgb(60, 60, 20);  // dark green background
            }
            else
            {
                label.ForeColor = Color.Red;
                label.BackColor = Color.FromArgb(60, 20, 20);  // dark green background
            }
        }

        private void CountingPiecesPerShift()
        {
            try
            {
                string machineName = "Cutting - Machine 01";  // Be careful: you use Greek name below

                DateTime now = DateTime.Now;
                TimeSpan nowTime = now.TimeOfDay;

                // 1. Load shift settings - here you used Greek machine name, so I keep it
                var shifts = _dbContext.MachineShiftPlan
                    .Where(x => x.Machine == "Κοπτικό Μηχάνημα 01")
                    .ToList();

                if (!shifts.Any())
                    return;

                // 2. Detect current shift
                var currentShift = shifts.FirstOrDefault(s =>
                    // Normal shift
                    (s.StartTime <= s.EndTime && nowTime >= s.StartTime && nowTime < s.EndTime)
                    ||
                    // Night shift (crosses midnight)
                    (s.StartTime > s.EndTime && (nowTime >= s.StartTime || nowTime < s.EndTime))
                );

                if (currentShift == null)
                    return;

                // 3. Calculate shift start DateTime
                DateTime shiftStart;

                if (currentShift.StartTime <= currentShift.EndTime)
                {
                    // Normal shift same day
                    shiftStart = now.Date + currentShift.StartTime;
                }
                else
                {
                    // Night shift (e.g. 22:00–06:00)
                    shiftStart = (nowTime < currentShift.EndTime)
                        ? now.Date.AddDays(-1) + currentShift.StartTime // after midnight
                        : now.Date + currentShift.StartTime;            // before midnight
                }

                // 4. Get cuts during this shift from TempOven1
                var shiftData = _dbContext.TempOven1
                    .Where(x => //x.Machine == machineName   // <- check that this matches your data exactly!
                                 x.Date >= shiftStart
                                && x.Date <= now
                                && x.Cut == 1)
                    .ToList();

                int cutsThisShift = shiftData.Count;

                // 5. Get PiecesPerCut and PlanShift from MachinePlan (for this machine)
                var machinePlan = _dbContext.MachinePlan
                    .FirstOrDefault(x => x.Machine == machineName);

                if (machinePlan == null)
                {
                    Console.WriteLine("No MachinePlan record found for this machine.");
                    return;
                }

                int piecesPerCut = machinePlan.PiecesPerCut;  // <-- your column
                int totalShiftTarget = machinePlan.PlanShift * piecesPerCut; // <-- replace with your real property name

                // 6. Calculate pieces produced
                int piecesProduced = cutsThisShift * piecesPerCut;

                // 7. Update labels
                lblPiecesLiveShift.Text = $"Κομμάτια που κόπηκαν στην βάρδια: {piecesProduced} / {totalShiftTarget}";

                // If you also want efficiency:
                double efficiency = totalShiftTarget > 0 ? (double)piecesProduced / totalShiftTarget * 100 : 0;
                lblEifficiencyShift.Text = $"Απόδοση Βάρδιας: {efficiency:F1} %";
                ApplyEfficiencyColor(lblEifficiencyShift, efficiency);

                Console.WriteLine($"Cuts this shift: {cutsThisShift}, Pieces: {piecesProduced} / {totalShiftTarget}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CountingPiecesPerShift: {ex.Message}");
            }
        }

        private void UpdateChart()
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime oneHourAgo = now.AddHours(-1);

                var data = _dbContext.TempOven1
                   .Where(x => x.Cut == 1 && x.Date >= oneHourAgo && x.Date <= now)
                   .OrderBy(x => x.Date)
                   .ToList();

                TotalCounterMachine1 = data.Count();
                this.series.Points.Clear();

                foreach (var item in data)
                {
                    this.series.Points.AddXY(item.Date, item.Cut);
                }
                var area = chart.ChartAreas["MainArea"];
                area.AxisX.Minimum = oneHourAgo.ToOADate();
                area.AxisX.Maximum = now.ToOADate();
                area.AxisX.IntervalType = DateTimeIntervalType.Minutes;
                area.AxisX.Interval = 5;
                area.AxisX.LabelStyle.Format = "HH:mm";

                //AddStopEventsToChart(oneHourAgo, now);



                // 🔥 ΠΑΝΤΑ ίδιο εύρος με τις κοπές: τελευταία 1 ώρα
                AddStopEventsToChart(oneHourAgo, now);
                CountingPiecesPerShift();


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating chart: {ex.Message}");
            }


        }

        private void btSetValues_Click(object sender, EventArgs e)
        {

            // 1️⃣ Find the row for the current product code
            var product = _dbContext.ProductCutPlan
                .FirstOrDefault(x => x.ProductCode == DataTags.CurrentCode1);

            // Example: values from textboxes / numericUpDowns
            string machineName = "Cutting - Machine 01";  // or from a ComboBox
            int planHour = DataTags.HourCuts1; //Convert.ToInt32(txtPlanHour.Value);                           // e.g. int.Parse(txtPlanHour.Text);
            int planShift = DataTags.ShiftCuts1; //Convert.ToInt32(txtPlanShift.Value);                          // e.g. int.Parse(txtPlanShift.Text);
            int PiecesPerCut = DataTags.CutPieces1;//Convert.ToInt32(txtPiecesPerCut.Value);

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
            plan.PiecesPerCut = PiecesPerCut;

            _dbContext.SaveChanges();

        }
        private void AddStopEventsToChart(DateTime from, DateTime to)
        {
            var area = chart.ChartAreas["MainArea"];

            // Καθαρίζουμε παλιά stop rectangles
            area.AxisX.StripLines.Clear();

            var events = _dbContext.MachineStopEvents
                .Include(e => e.StopReason)
                .Where(e => e.Machine == MachineName &&
                            e.StartTime < to &&
                            e.EndTime > from)
                .OrderBy(e => e.StartTime)
                .ToList();

            foreach (var evt in events)
            {
                DateTime start = evt.StartTime;
                DateTime end = evt.EndTime;

                if (end <= start)
                    end = start.AddMinutes(1);

                string text = evt.StopReason?.Description ?? "Stop";

                double startOa = start.ToOADate();
                double endOa = end.ToOADate();
                double widthDays = endOa - startOa;    // StripWidth είναι σε "days"

                var strip = new StripLine
                {
                    // ζωγραφίζει ΜΙΑ φορά (όχι επαναλαμβανόμενα)
                    Interval = 0,

                    // από πού ξεκινάει στον άξονα Χ
                    IntervalOffset = startOa,

                    // μέχρι πού φτάνει (πλάτος ορθογώνιου)
                    StripWidth = widthDays,

                    // η γέμιση του τετραγώνου
                    BackColor = Color.FromArgb(60, Color.Red),

                    // 🔹 ΚΕΙΜΕΝΟ ΠΑΝΩ ΣΤΟ RECTANGLE
                    Text = text,
                    ForeColor = Color.Black,
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    TextAlignment = StringAlignment.Center,      // οριζόντια στο κέντρο
                    TextLineAlignment = StringAlignment.Center,  // κατακόρυφα στο κέντρο

                    // Tooltip για λεπτομέρειες
                    ToolTip = $"{text}\n{start:HH:mm} - {end:HH:mm}"
                };

                area.AxisX.StripLines.Add(strip);
            }
        }


        private void Update1ChartTimer_Tick(object sender, EventArgs e)
        {
            UpdateChart();
            UpdatePieChart();


            DateTime now = DateTime.Now;
            DateTime currentHourStart = new DateTime(
                now.Year, now.Month, now.Day,
                now.Hour, 0, 0);

            DateTime from = currentHourStart.AddHours(-1);
            DateTime to = currentHourStart;

            //AddStopEventsToChart(from, to);

            int totalCounterMachine1 = _dbContext.TempOven1
                .Count(x => //x.Machine == "Cutting - Machine 01"
                          x.Date >= from
                         && x.Date < to
                         && x.Cut == 1);

            var plan = _dbContext.MachinePlan
                .SingleOrDefault(x => x.Machine == "Cutting - Machine 01");

            if (plan == null || plan.PlanHour <= 0)
            {
                lblEfficiency.Text = "Απόδοση: N/A";
                return;
            }

            double efficiencyPrevHour =
                (double)totalCounterMachine1 / plan.PlanHour * 100.0;

            lblEfficiency.Text =
     $"Απόδοση προηγούμενης ώρας: {efficiencyPrevHour:F1}%   |   {totalCounterMachine1}/{plan.PlanHour}";

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

        private DateTime _lastFrom;
        private DateTime _lastTo;
        private int _lastCount;
        private double _lastExpectedCuts;
        private double _lastEfficiency;
        private string _lastMachineName = "Cutting - Machine 01"; // όπως το χρησιμοποιείς στο plan
        private bool _hasSearchResult = false;


        private void search_btn_Click(object sender, EventArgs e)
        {



            // Κρύψε τα live labels όταν κάνεις αναζήτηση
            lblEifficiencyShift.Visible = false;
            lblPiecesLiveShift.Visible = false;

            // Σταμάτα το live update
            Update1ChartTimer.Enabled = false;

            // 1) Πάρε το range από τα date/time pickers
            DateTime from = datePickerFrom.Value.Date + timePickerFrom.Value.TimeOfDay;
            DateTime to = datePickerTo.Value.Date + timePickerTo.Value.TimeOfDay;

            if (to <= from)
            {
                MessageBox.Show("Η ώρα λήξης πρέπει να είναι μετά την ώρα έναρξης.",
                                "Λάθος εύρος χρόνου",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            Live_btn.Visible = true;
            btExport.Visible = true;

            // 2) Φέρε τις κοπές από τη βάση
            var data = _dbContext.TempOven1
                .Where(x => x.Cut == 1 &&
                            x.Date >= from &&
                            x.Date <= to)
                .OrderBy(x => x.Date)
                .ToList();

            int count = data.Count;

            // 3) Γέμισε τη σειρά του chart
            series.Points.Clear();

            foreach (var item in data)
            {
                series.Points.AddXY(item.Date, item.Cut);
            }

            // 4) Ρύθμιση άξονα Χ στο εύρος της αναζήτησης
            var area = chart.ChartAreas["MainArea"];

            // Reset τυχόν παλιό zoom
            area.AxisX.ScaleView.ZoomReset();

            area.AxisX.Minimum = from.ToOADate();
            area.AxisX.Maximum = to.ToOADate();
            area.AxisX.LabelStyle.Format = "HH:mm";

            double totalMinutes = (to - from).TotalMinutes;

            if (totalMinutes <= 60)
            {
                area.AxisX.IntervalType = DateTimeIntervalType.Minutes;
                area.AxisX.Interval = 5;
            }
            else if (totalMinutes <= 6 * 60)
            {
                area.AxisX.IntervalType = DateTimeIntervalType.Minutes;
                area.AxisX.Interval = 30;
            }
            else
            {
                area.AxisX.IntervalType = DateTimeIntervalType.Hours;
                area.AxisX.Interval = 1;
            }

            // 5) Ζωγράφισε τα stop events ως rectangles στο ίδιο εύρος
            AddStopEventsToChart(from, to);

            // 6) Υπολογισμός απόδοσης για το εύρος αναζήτησης
            double hours = (to - from).TotalHours;

            if (hours <= 0)
            {
                lblEfficiency.Text = "Απόδοση: N/A";
                return;
            }

            var plan = _dbContext.MachinePlan
                .SingleOrDefault(x => x.Machine == "Cutting - Machine 01");

            if (plan == null || plan.PlanHour <= 0)
            {
                lblEfficiency.Text = "Απόδοση: N/A";
                return;
            }

            double expectedCuts = plan.PlanHour * hours;

            double efficiency = (expectedCuts > 0)
                ? (count / expectedCuts) * 100.0
                : 0.0;

            lblEfficiency.Text =
                $"Απόδοση (αναζήτησης): {efficiency:F1}%   |   {count}/{expectedCuts:F0}";

            // 👉 Αποθήκευση για το PDF export
            _lastFrom = from;
            _lastTo = to;
            _lastCount = count;
            _lastExpectedCuts = expectedCuts;
            _lastEfficiency = efficiency;
            _lastMachineName = "Cutting - Machine 01"; // ή από combo κλπ στο μέλλον
            _hasSearchResult = true;



            // 7) Χρωματισμός label απόδοσης
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

        private double GetOverlapMinutes(DateTime start, DateTime end, DateTime from, DateTime to)
        {
            if (end <= from || start >= to)
                return 0;

            var s = start < from ? from : start;
            var e = end > to ? to : end;

            return (e - s).TotalMinutes;
        }


        private void Live_btn_Click(object sender, EventArgs e)
        {
            Live_btn.Visible = false;
            btExport.Visible = false;
            Update1ChartTimer.Enabled = true;
            lblEifficiencyShift.Visible = true;
            lblPiecesLiveShift.Visible = true;
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

        private void btExport_Click(object sender, EventArgs e)
        {
            if (!_hasSearchResult)
            {
                MessageBox.Show("Πρώτα κάνε μια αναζήτηση για να υπάρχουν δεδομένα.",
                                "Export",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF files (*.pdf)|*.pdf";
                sfd.FileName = $"Report_{_lastFrom:yyyyMMdd_HHmm}-{_lastTo:HHmm}.pdf";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                // 1) Στάσεις για το ίδιο διάστημα
                var stopEvents = _dbContext.MachineStopEvents
                    .Include(e => e.StopReason)
                    .Where(e => e.Machine == _lastMachineName &&
                                e.StartTime < _lastTo &&
                                e.EndTime > _lastFrom)
                    .ToList();

                double totalStopMinutes = stopEvents
                    .Sum(evt => GetOverlapMinutes(evt.StartTime, evt.EndTime, _lastFrom, _lastTo));

                int delayedThresholdMinutes = 5;
                int delayedCount = stopEvents
                    .Count(evt => GetOverlapMinutes(evt.StartTime, evt.EndTime, _lastFrom, _lastTo)
                                  >= delayedThresholdMinutes);

                // 2) Δημιουργία PDF
                PdfDocument doc = new PdfDocument();
                doc.Info.Title = "Αναφορά Παραγωγής";

                PdfPage page = doc.AddPage();
                page.Size = PdfSharp.PageSize.A4;

                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Αν χρειαστεί, άλλαξέ τα σε XFontStyle
                XFont titleFont = new XFont("Arial", 16, XFontStyleEx.Bold);
                XFont headerFont = new XFont("Arial", 12, XFontStyleEx.Bold);
                XFont normalFont = new XFont("Arial", 10, XFontStyleEx.Regular);
                XFont smallFont = new XFont("Arial", 8, XFontStyleEx.Regular);

                double marginLeft = 40;
                double yTop = 30;
                double y;

                // 3) LOGOs – EPG αριστερά, Alumil στο κέντρο
                double maxLogoHeight = 0;

                try
                {
                    string alumilLogoPath = @"C:\WORKS ALL\C#\Alumil 1.png";
                    string epgLogoPath = @"C:\WORKS ALL\C#\MainOldLogo.png";

                    // EPG αριστερά
                    if (File.Exists(epgLogoPath))
                    {
                        XImage epgLogo = XImage.FromFile(epgLogoPath);

                        double epgWidth = 80;
                        double epgRatio = epgWidth / epgLogo.PixelWidth;
                        double epgHeight = epgLogo.PixelHeight * epgRatio;

                        double epgX = marginLeft;
                        double epgY = yTop;

                        gfx.DrawImage(epgLogo, epgX, epgY, epgWidth, epgHeight);

                        if (epgHeight > maxLogoHeight)
                            maxLogoHeight = epgHeight;
                    }

                    // Alumil στο κέντρο – μεγαλύτερο
                    if (File.Exists(alumilLogoPath))
                    {
                        XImage alumilLogo = XImage.FromFile(alumilLogoPath);

                        double alumilWidth = 130; // ό,τι μέγεθος θες
                        double alumilRatio = alumilWidth / alumilLogo.PixelWidth;
                        double alumilHeight = alumilLogo.PixelHeight * alumilRatio;

                        double alumilX = (page.Width - alumilWidth) / 2;
                        double alumilY = yTop;

                        gfx.DrawImage(alumilLogo, alumilX, alumilY, alumilWidth, alumilHeight);

                        if (alumilHeight > maxLogoHeight)
                            maxLogoHeight = alumilHeight;
                    }
                }
                catch
                {
                    // αγνόησε προβλήματα με logos
                }

                // y κάτω από το πιο ψηλό logo
                y = yTop + maxLogoHeight + 20;

                // 4) Τίτλος
                gfx.DrawString("Αναφορά Παραγωγής", titleFont, XBrushes.Black,
                    new XRect(0, y, page.Width, 30), XStringFormats.TopCenter);
                y += 40;

                // 5) Στοιχεία περιόδου / απόδοσης
                string machineDisplayName = string.IsNullOrEmpty(_lastMachineName)
                    ? "Κοπτικό 01"
                    : _lastMachineName;

                double summaryTopY = y; // αρχή summary (για να κεντράρουμε το pie)

                gfx.DrawString($"Μηχάνημα: {machineDisplayName}", headerFont, XBrushes.Black,
                    new XPoint(marginLeft, y));
                y += 20;

                gfx.DrawString($"Περίοδος: {_lastFrom:dd/MM/yyyy HH:mm}  -  {_lastTo:dd/MM/yyyy HH:mm}",
                    normalFont, XBrushes.Black, new XPoint(marginLeft, y));
                y += 15;

                gfx.DrawString($"Συνολικά κομμένα: {_lastCount}",
                    normalFont, XBrushes.Black, new XPoint(marginLeft, y));
                y += 15;

                gfx.DrawString($"Εκτιμώμενα (σχέδιο): {_lastExpectedCuts:F0}",
                    normalFont, XBrushes.Black, new XPoint(marginLeft, y));
                y += 15;

                gfx.DrawString($"Απόδοση αναζήτησης: {_lastEfficiency:F1} %",
                    normalFont, XBrushes.Black, new XPoint(marginLeft, y));
                y += 20;

                gfx.DrawString($"Συνολικός χρόνος στάσεων: {totalStopMinutes:F1} λεπτά",
                    normalFont, XBrushes.Black, new XPoint(marginLeft, y));
                y += 15;

                gfx.DrawString($"Αργοπορημένες στάσεις (≥ {delayedThresholdMinutes}'): {delayedCount}",
                    normalFont, XBrushes.Black, new XPoint(marginLeft, y));
                y += 25;

                double summaryBottomY = y; // τέλος summary

                // 6) PIE CHART: Κομμένα vs Μη κομμένα (με στρογγυλοποίηση)
                double rawDone = _lastCount;
                double rawExpected = _lastExpectedCuts;

                // Στρογγυλεύουμε τις εκτιμώμενες κοπές στο κοντινότερο ακέραιο
                int expectedInt = (int)Math.Round(rawExpected, MidpointRounding.AwayFromZero);
                int doneInt = (int)rawDone;
                int missingInt = Math.Max(0, expectedInt - doneInt);

                if (expectedInt > 0)
                {
                    var pieChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
                    pieChart.Width = 450;
                    pieChart.Height = 300;

                    var areaPie = new System.Windows.Forms.DataVisualization.Charting.ChartArea("PieArea");
                    pieChart.ChartAreas.Add(areaPie);

                    var seriesPie = new System.Windows.Forms.DataVisualization.Charting.Series("Series1");
                    seriesPie.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                    seriesPie.IsValueShownAsLabel = true;

                    // Πιο μεγάλο font για ετικέτες
                    seriesPie.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);

                    // Ετικέτα με όνομα + τιμή (χωρίς δεκαδικά)
                    seriesPie.Label = "#VALX: #VAL (#PERCENT{P0})";
                    seriesPie.LabelFormat = "N0"; // ακέραιοι

                    seriesPie.Points.AddXY("Κομμένα", doneInt);
                    seriesPie.Points.AddXY("Μη κομμένα", missingInt);

                    pieChart.Series.Add(seriesPie);

                    using (var msPie = new MemoryStream())
                    {
                        pieChart.SaveImage(msPie,
                            System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
                        msPie.Position = 0;

                        XImage pieImg = XImage.FromStream(msPie);

                        double pieWidth = 200;
                        double ratioPie = pieWidth / pieImg.PixelWidth;
                        double pieHeight = pieImg.PixelHeight * ratioPie;

                        double pieX = page.Width - pieWidth - marginLeft;
                        double pieY = summaryTopY + (summaryBottomY - summaryTopY - pieHeight) / 2;

                        gfx.DrawImage(pieImg, pieX, pieY, pieWidth, pieHeight);
                    }
                }


                // Μεταφέρουμε το y λίγο κάτω από το summary για το κύριο chart
                y = summaryBottomY + 20;

                // 7) Κύριο CHART σαν εικόνα
                using (var ms = new MemoryStream())
                {
                    chart.SaveImage(ms,
                        System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
                    ms.Position = 0;

                    XImage chartImg = XImage.FromStream(ms);

                    double availableWidth = page.Width - marginLeft * 2;
                    double ratio = availableWidth / chartImg.PixelWidth;
                    double chartHeight = chartImg.PixelHeight * ratio;

                    double maxHeight = page.Height - y - 40;
                    if (chartHeight > maxHeight)
                    {
                        ratio = maxHeight / chartImg.PixelHeight;
                        chartHeight = maxHeight;
                        availableWidth = chartImg.PixelWidth * ratio;
                    }

                    gfx.DrawImage(chartImg, marginLeft, y, availableWidth, chartHeight);
                    y += chartHeight + 10;
                }

                // 8) Footer
                gfx.DrawString($"Ημερομηνία δημιουργίας: {DateTime.Now:dd/MM/yyyy HH:mm}",
                    smallFont, XBrushes.Gray,
                    new XPoint(marginLeft, page.Height - 40));

                // 9) Αποθήκευση
                doc.Save(sfd.FileName);
                doc.Close();

                MessageBox.Show("Το PDF report δημιουργήθηκε επιτυχώς.",
                                "Export",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }




    }
}
