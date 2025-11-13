using Meme_Oven_Data.Repository;
using Meme_Oven_Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;

namespace Meme_Oven_Data
{
    public partial class DesOven1 : UserControl
    {
        private readonly MicrOvenContext _dbContext;
        private Chart chart;
        Series series;
        Series onOffSeries;
        Button btMain;
        Button btData;
        Label lblRecipe;

        public DesOven1(MicrOvenContext dbContext)
        {
            _dbContext = dbContext;
            //_plc = plc;
            InitializeComponent();

            this.lblRecipe = new Label()
            {
                Text = "Recipe Name",
                ForeColor = Color.Green,
                BackColor = Color.Transparent,
                Font = new Font("Arial", 14, FontStyle.Bold | FontStyle.Italic),
                AutoSize = true,
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
                Visible = true
            };

            btMain.Click += BtMain_Click;
            btData.Click += BtData_Click;

            this.chart = new Chart
            {
                Size = new Size(1657, 858),
                Location = new Point(50, 58),
                BackColor = Color.White // Neutral background color
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
                            Title = "Temperature (°C)",
                            TitleFont = new Font("Arial", 18, FontStyle.Bold),
                            LabelStyle = { ForeColor = Color.Black },
                            MajorGrid = { LineColor = Color.LightGray }
                        },
                AxisY2 = new Axis
                {
                    Title = "On/Off Oven State",
                    TitleFont = new Font("Arial", 18, FontStyle.Bold),
                    Interval = 1,
                    Enabled = AxisEnabled.True,
                    LabelStyle = { ForeColor = Color.Black },
                    MajorGrid = { LineColor = Color.Transparent },
                    LineColor = Color.Black,
                    Minimum = 0, // Set minimum to 0
                    Maximum = 2  // Set maximum to 2 for better visualization
                }
            };

            chart.ChartAreas.Add(chartArea);

            // Set chart background color
            chart.BackColor = Color.White;

            // Add series
            this.series = new Series("DataSeries")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.Blue // Line color
            };

            // Configure the On/Off series
            this.onOffSeries = new Series("OnOffSeries")
            {
                ChartType = SeriesChartType.StepLine, // Binary data fits better with StepLine
                BorderWidth = 2,
                Color = Color.Red,
                YAxisType = AxisType.Secondary // Use AxisY2
            };


            chartArea.AxisY2 = new Axis
            {
                Title = "On/Off Oven State",
                TitleFont = new Font("Arial", 18, FontStyle.Bold),
                Interval = 1,
                Enabled = AxisEnabled.True,
                LabelStyle = { ForeColor = Color.Black },
                MajorGrid = { LineColor = Color.Transparent },
                LineColor = Color.Black,
                Maximum = 2,
                Minimum = 0,
            };


            this.Controls.Add(lblRecipe);
            this.Controls.Add(btData);
            this.Controls.Add(btMain);
            this.chart.Series.Add(series);
            this.chart.Series.Add(onOffSeries);
            //UpdateChart();
            this.Controls.Add(chart);
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
            picMain.Visible = true;
            lblRecipe.BringToFront();
            this.BackColor = Color.FromArgb(20, 50, 100);
            //tmrReadColor.Enabled = true;


            CoolerConveyor0.Visible = true;
            CoolerConveyor1.Visible = true;
            CoolerConveyor2.Visible = true;
            CoolerConveyor3.Visible = true;
            CoolerConveyor4.Visible = true;

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
            picMain.Visible = false;
            this.BackColor = Color.FromArgb(32, 48, 64);
            tmrReadColor.Enabled = false;

            tmrReadColor.Enabled = false;

            CoolerConveyor0.Visible = false;
            CoolerConveyor1.Visible = false;
            CoolerConveyor2.Visible = false;
            CoolerConveyor3.Visible = false;
            CoolerConveyor4.Visible = false;

            Anevatori0.Visible = false;
            Anevatori1.Visible = false;
            Anevatori2.Visible = false;
            Anevatori3.Visible = false;
            Anevatori4.Visible = false;

            Turbine1Cooler0.Visible = false;
            Turbine1Cooler1.Visible = false;
            Turbine1Cooler2.Visible = false;
            Turbine1Cooler3.Visible = false;
            Turbine1Cooler4.Visible = false;

            Turbine2Cooler0.Visible = false;
            Turbine2Cooler1.Visible = false;
            Turbine2Cooler2.Visible = false;
            Turbine2Cooler3.Visible = false;
            Turbine2Cooler4.Visible = false;

            FurnaceConvayor0.Visible = false;
            FurnaceConvayor1.Visible = false;
            FurnaceConvayor2.Visible = false;
            FurnaceConvayor3.Visible = false;
            FurnaceConvayor4.Visible = false;

            Turbine1Smoke0.Visible = false;
            Turbine1Smoke1.Visible = false;
            Turbine1Smoke2.Visible = false;
            Turbine1Smoke3.Visible = false;
            Turbine1Smoke4.Visible = false;

            Turbine2Smoke0.Visible = false;
            Turbine2Smoke1.Visible = false;
            Turbine2Smoke2.Visible = false;
            Turbine2Smoke3.Visible = false;
            Turbine2Smoke4.Visible = false;

            TurbineChimney0.Visible = false;
            TurbineChimney1.Visible = false;
            TurbineChimney2.Visible = false;
            TurbineChimney3.Visible = false;
            TurbineChimney4.Visible = false;

            Burner0.Visible = false;
            Burner1.Visible = false;
            Burner2.Visible = false;
            Burner3.Visible = false;
            Burner4.Visible = false;

            Sesoula0.Visible = false;
            Sesoula1.Visible = false;
            Sesoula2.Visible = false;
            Sesoula3.Visible = false;
            Sesoula4.Visible = false;

            Donisi0.Visible = false;
            Donisi1.Visible = false;
            Donisi2.Visible = false;
            Donisi3.Visible = false;
            Donisi4.Visible = false;

            WaterPump0.Visible = false;
            WaterPump1.Visible = false;
            WaterPump2.Visible = false;
            WaterPump3.Visible = false;
            WaterPump4.Visible = false;

        }


        private void UpdateChart()
        {
            try
            {
                // Retrieve the last 100 records, ordered by Date descending
                var data = _dbContext.TempOven1
                    .OrderByDescending(x => x.Date) // Get the most recent records
                    .Take(200)                     // Limit to 100 records
                    .OrderBy(x => x.Date)          // Reorder to ascending by Date for proper 
                    .Select(x => new
                    {
                        x.Id,
                        x.Date
                        //x.Temperature,
                        //x.OnOffOven
                    })
                    .ToList();
                this.series.Points.Clear();
                this.onOffSeries.Points.Clear();

                // Add data points to the chart
                for (int i = 1; i < data.Count; i++)
                {
                    //this.series.Points.AddXY(data.ElementAt(i).Date, data.ElementAt(i).Temperature);
                   // this.onOffSeries.Points.AddXY(data.ElementAt(i).Date, data.ElementAt(i).OnOffOven);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating chart: {ex.Message}");
            }
        }

        private void Update1ChartTimer_Tick(object sender, EventArgs e)
        {
           // UpdateChart();

        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            Update1ChartTimer.Enabled = false;
            var dateTimePickerFrom = datePickerFrom.Value.Date + timePickerFrom.Value.TimeOfDay;
            var dateTimePickerTo = datePickerTo.Value.Date + timePickerTo.Value.TimeOfDay;

            var data = _dbContext.TempOven1
                    .Where(x => x.Date >= dateTimePickerFrom && x.Date <= dateTimePickerTo)
                    .OrderByDescending(x => x.Date) // Get the most recent records
                                                    //.Take(1000)                     // Limit to 100 records
                    .OrderBy(x => x.Date)          // Reorder to ascending by Date for proper charting
                    .ToList();

            this.series.Points.Clear();

            // Add data points to the chart
            for (int i = 1; i < data.Count; i++)
            {
                //this.series.Points.AddXY(data.ElementAt(i).Date, data.ElementAt(i).Temperature);
                //this.onOffSeries.Points.AddXY(data.ElementAt(i).Date, data.ElementAt(i).OnOffOven);
            }

        }

        private void Live_btn_Click(object sender, EventArgs e)
        {
            Update1ChartTimer.Enabled = true;
        }

        private void tmrReadColor_Tick(object sender, EventArgs e)
        {
            //Label Of the Recipe
            Dictionary<int, string> recipeNames = new Dictionary<int, string>
                {
                    { 1, "Live Recipe: Kelyfwta" },
                    { 2, "Live Recipe: Fistikia" },
                    { 3, "Live Recipe: USA keli" },
                    { 4, "" },
                    { 5, "" },
                    { 6, "" },
                    { 7, "" },
                    { 8, "" }
                };

            // Assign the label text using dictionary lookup
            if (!recipeNames.TryGetValue(Form1.Recipe, out string recipeText))
            {
                recipeText = "Adeio"; // Default text if the recipe is not found
            }


            //Recipe Label
            lblRecipe.Text = recipeText;


            //Katagrafi sintagis stin SQL
            Form1.OnomaSintagis = lblRecipe.Text;

            //Check the Motor CoolerConveyor
            if (Form1.ColorTainiaCooler == 0)
            {
                CoolerConveyor0.Visible = true;
                CoolerConveyor1.Visible = false;
                CoolerConveyor2.Visible = false;
                CoolerConveyor3.Visible = false;
                CoolerConveyor4.Visible = false;
                CoolerConveyor0.BringToFront();
            }
            else if (Form1.ColorTainiaCooler == 1)
            {
                CoolerConveyor0.Visible = false;
                CoolerConveyor1.Visible = true;
                CoolerConveyor2.Visible = false;
                CoolerConveyor3.Visible = false;
                CoolerConveyor4.Visible = false;
                CoolerConveyor1.BringToFront();
            }
            else if (Form1.ColorTainiaCooler == 2)
            {
                CoolerConveyor0.Visible = false;
                CoolerConveyor1.Visible = false;
                CoolerConveyor2.Visible = true;
                CoolerConveyor3.Visible = false;
                CoolerConveyor4.Visible = false;
                CoolerConveyor2.BringToFront();
            }
            else if (Form1.ColorTainiaCooler == 3)
            {
                CoolerConveyor0.Visible = false;
                CoolerConveyor1.Visible = false;
                CoolerConveyor2.Visible = false;
                CoolerConveyor3.Visible = true;
                CoolerConveyor4.Visible = false;
                CoolerConveyor3.BringToFront();
            }
            else if (Form1.ColorTainiaCooler == 4)
            {
                CoolerConveyor0.Visible = false;
                CoolerConveyor1.Visible = false;
                CoolerConveyor2.Visible = false;
                CoolerConveyor3.Visible = false;
                CoolerConveyor4.Visible = true;
                CoolerConveyor4.BringToFront();
            }

            //Color Anevatori
            if (Form1.ColorAnevatori == 0)
            {
                Anevatori0.Visible = true;
                Anevatori1.Visible = false;
                Anevatori2.Visible = false;
                Anevatori3.Visible = false;
                Anevatori4.Visible = false;
            }
            else if (Form1.ColorAnevatori == 1)
            {
                Anevatori0.Visible = false;
                Anevatori1.Visible = true;
                Anevatori2.Visible = false;
                Anevatori3.Visible = false;
                Anevatori4.Visible = false;
            }
            else if (Form1.ColorAnevatori == 2)
            {
                Anevatori0.Visible = false;
                Anevatori1.Visible = false;
                Anevatori2.Visible = true;
                Anevatori3.Visible = false;
                Anevatori4.Visible = false;
            }
            else if (Form1.ColorAnevatori == 3)
            {
                Anevatori0.Visible = false;
                Anevatori1.Visible = false;
                Anevatori2.Visible = false;
                Anevatori3.Visible = true;
                Anevatori4.Visible = false;
            }
            else if (Form1.ColorAnevatori == 4)
            {
                Anevatori0.Visible = false;
                Anevatori1.Visible = false;
                Anevatori2.Visible = false;
                Anevatori3.Visible = false;
                Anevatori4.Visible = true;
            }


            //Color Cooler Turbine 1
            if (Form1.ColorTurbineCoolerNo1 == 0)
            {
                Turbine1Cooler0.Visible = true;
                Turbine1Cooler1.Visible = false;
                Turbine1Cooler2.Visible = false;
                Turbine1Cooler3.Visible = false;
                Turbine1Cooler4.Visible = false;
            }
            else if (Form1.ColorTurbineCoolerNo1 == 1)
            {
                Turbine1Cooler0.Visible = false;
                Turbine1Cooler1.Visible = true;
                Turbine1Cooler2.Visible = false;
                Turbine1Cooler3.Visible = false;
                Turbine1Cooler4.Visible = false;
            }
            else if (Form1.ColorTurbineCoolerNo1 == 2)
            {
                Turbine1Cooler0.Visible = false;
                Turbine1Cooler1.Visible = false;
                Turbine1Cooler2.Visible = true;
                Turbine1Cooler3.Visible = false;
                Turbine1Cooler4.Visible = false;
            }
            else if (Form1.ColorTurbineCoolerNo1 == 3)
            {
                Turbine1Cooler0.Visible = false;
                Turbine1Cooler1.Visible = false;
                Turbine1Cooler2.Visible = false;
                Turbine1Cooler3.Visible = true;
                Turbine1Cooler4.Visible = false;
            }
            else if (Form1.ColorTurbineCoolerNo1 == 4)
            {
                Turbine1Cooler0.Visible = false;
                Turbine1Cooler1.Visible = false;
                Turbine1Cooler2.Visible = false;
                Turbine1Cooler3.Visible = false;
                Turbine1Cooler4.Visible = true;
            }

            //Color Cooler Turbine 2
            if (Form1.ColorTurbineCoolerNo2 == 0)
            {
                Turbine2Cooler0.Visible = true;
                Turbine2Cooler1.Visible = false;
                Turbine2Cooler2.Visible = false;
                Turbine2Cooler3.Visible = false;
                Turbine2Cooler4.Visible = false;
            }
            else if (Form1.ColorTurbineCoolerNo2 == 1)
            {
                Turbine2Cooler0.Visible = false;
                Turbine2Cooler1.Visible = true;
                Turbine2Cooler2.Visible = false;
                Turbine2Cooler3.Visible = false;
                Turbine2Cooler4.Visible = false;
            }
            else if (Form1.ColorTurbineCoolerNo2 == 2)
            {
                Turbine2Cooler0.Visible = false;
                Turbine2Cooler1.Visible = false;
                Turbine2Cooler2.Visible = true;
                Turbine2Cooler3.Visible = false;
                Turbine2Cooler4.Visible = false;
            }
            else if (Form1.ColorTurbineCoolerNo2 == 3)
            {
                Turbine2Cooler0.Visible = false;
                Turbine2Cooler1.Visible = false;
                Turbine2Cooler2.Visible = false;
                Turbine2Cooler3.Visible = true;
                Turbine2Cooler4.Visible = false;
            }
            else if (Form1.ColorTurbineCoolerNo2 == 4)
            {
                Turbine2Cooler0.Visible = false;
                Turbine2Cooler1.Visible = false;
                Turbine2Cooler2.Visible = false;
                Turbine2Cooler3.Visible = false;
                Turbine2Cooler4.Visible = true;
            }

            //Color Furnace Convayor
            if (Form1.ColorTainiaFurnace == 0)
            {
                FurnaceConvayor0.Visible = true;
                FurnaceConvayor1.Visible = false;
                FurnaceConvayor2.Visible = false;
                FurnaceConvayor3.Visible = false;
                FurnaceConvayor4.Visible = false;

            }
            else if (Form1.ColorTainiaFurnace == 1)
            {
                FurnaceConvayor0.Visible = false;
                FurnaceConvayor1.Visible = true;
                FurnaceConvayor2.Visible = false;
                FurnaceConvayor3.Visible = false;
                FurnaceConvayor4.Visible = false;
            }
            else if (Form1.ColorTainiaFurnace == 2)
            {
                FurnaceConvayor0.Visible = false;
                FurnaceConvayor1.Visible = false;
                FurnaceConvayor2.Visible = true;
                FurnaceConvayor3.Visible = false;
                FurnaceConvayor4.Visible = false;
            }
            else if (Form1.ColorTainiaFurnace == 3)
            {
                FurnaceConvayor0.Visible = false;
                FurnaceConvayor1.Visible = false;
                FurnaceConvayor2.Visible = false;
                FurnaceConvayor3.Visible = true;
                FurnaceConvayor4.Visible = false;
            }
            else if (Form1.ColorTainiaFurnace == 4)
            {
                FurnaceConvayor0.Visible = false;
                FurnaceConvayor1.Visible = false;
                FurnaceConvayor2.Visible = false;
                FurnaceConvayor3.Visible = false;
                FurnaceConvayor4.Visible = true;
            }

            // Color Turbine Smoke 1
            if (Form1.ColorTurbineSmokeNo1 == 0)
            {
                Turbine1Smoke0.Visible = true;
                Turbine1Smoke1.Visible = false;
                Turbine1Smoke2.Visible = false;
                Turbine1Smoke3.Visible = false;
                Turbine1Smoke4.Visible = false;
            }
            else if (Form1.ColorTurbineSmokeNo1 == 1)
            {
                Turbine1Smoke0.Visible = false;
                Turbine1Smoke1.Visible = true;
                Turbine1Smoke2.Visible = false;
                Turbine1Smoke3.Visible = false;
                Turbine1Smoke4.Visible = false;
            }
            else if (Form1.ColorTurbineSmokeNo1 == 2)
            {
                Turbine1Smoke0.Visible = false;
                Turbine1Smoke1.Visible = false;
                Turbine1Smoke2.Visible = true;
                Turbine1Smoke3.Visible = false;
                Turbine1Smoke4.Visible = false;
            }
            else if (Form1.ColorTurbineSmokeNo1 == 3)
            {
                Turbine1Smoke0.Visible = false;
                Turbine1Smoke1.Visible = false;
                Turbine1Smoke2.Visible = false;
                Turbine1Smoke3.Visible = true;
                Turbine1Smoke4.Visible = false;
            }
            else if (Form1.ColorTurbineSmokeNo1 == 4)
            {
                Turbine1Smoke0.Visible = false;
                Turbine1Smoke1.Visible = false;
                Turbine1Smoke2.Visible = false;
                Turbine1Smoke3.Visible = false;
                Turbine1Smoke4.Visible = true;
            }

            // Color Turbine Smoke 2
            if (Form1.ColorTurbineSmokeNo2 == 0)
            {
                Turbine2Smoke0.Visible = true;
                Turbine2Smoke1.Visible = false;
                Turbine2Smoke2.Visible = false;
                Turbine2Smoke3.Visible = false;
                Turbine2Smoke4.Visible = false;
            }
            else if (Form1.ColorTurbineSmokeNo2 == 1)
            {
                Turbine2Smoke0.Visible = false;
                Turbine2Smoke1.Visible = true;
                Turbine2Smoke2.Visible = false;
                Turbine2Smoke3.Visible = false;
                Turbine2Smoke4.Visible = false;
            }
            else if (Form1.ColorTurbineSmokeNo2 == 2)
            {
                Turbine2Smoke0.Visible = false;
                Turbine2Smoke1.Visible = false;
                Turbine2Smoke2.Visible = true;
                Turbine2Smoke3.Visible = false;
                Turbine2Smoke4.Visible = false;
            }
            else if (Form1.ColorTurbineSmokeNo2 == 3)
            {
                Turbine2Smoke0.Visible = false;
                Turbine2Smoke1.Visible = false;
                Turbine2Smoke2.Visible = false;
                Turbine2Smoke3.Visible = true;
                Turbine2Smoke4.Visible = false;
            }
            else if (Form1.ColorTurbineSmokeNo2 == 4)
            {
                Turbine2Smoke0.Visible = false;
                Turbine2Smoke1.Visible = false;
                Turbine2Smoke2.Visible = false;
                Turbine2Smoke3.Visible = false;
                Turbine2Smoke4.Visible = true;
            }

            // Color Turbine Chimney
            if (Form1.ColorTurbineChimney == 0)
            {
                TurbineChimney0.Visible = true;
                TurbineChimney1.Visible = false;
                TurbineChimney2.Visible = false;
                TurbineChimney3.Visible = false;
                TurbineChimney4.Visible = false;
            }
            else if (Form1.ColorTurbineChimney == 1)
            {
                TurbineChimney0.Visible = false;
                TurbineChimney1.Visible = true;
                TurbineChimney2.Visible = false;
                TurbineChimney3.Visible = false;
                TurbineChimney4.Visible = false;
            }
            else if (Form1.ColorTurbineChimney == 2)
            {
                TurbineChimney0.Visible = false;
                TurbineChimney1.Visible = false;
                TurbineChimney2.Visible = true;
                TurbineChimney3.Visible = false;
                TurbineChimney4.Visible = false;
            }
            else if (Form1.ColorTurbineChimney == 3)
            {
                TurbineChimney0.Visible = false;
                TurbineChimney1.Visible = false;
                TurbineChimney2.Visible = false;
                TurbineChimney3.Visible = true;
                TurbineChimney4.Visible = false;
            }
            else if (Form1.ColorTurbineChimney == 4)
            {
                TurbineChimney0.Visible = false;
                TurbineChimney1.Visible = false;
                TurbineChimney2.Visible = false;
                TurbineChimney3.Visible = false;
                TurbineChimney4.Visible = true;
            }

            //Color Burner
            if (Form1.ColorBurner == 0)
            {
                Burner0.Visible = true;
                Burner1.Visible = false;
                Burner2.Visible = false;
                Burner3.Visible = false;
                Burner4.Visible = false;
            }
            else if (Form1.ColorBurner == 1)
            {
                Burner0.Visible = false;
                Burner1.Visible = true;
                Burner2.Visible = false;
                Burner3.Visible = false;
                Burner4.Visible = false;
            }
            else if (Form1.ColorBurner == 2)
            {
                Burner0.Visible = false;
                Burner1.Visible = false;
                Burner2.Visible = true;
                Burner3.Visible = false;
                Burner4.Visible = false;
            }
            else if (Form1.ColorBurner == 3)
            {
                Burner0.Visible = false;
                Burner1.Visible = false;
                Burner2.Visible = false;
                Burner3.Visible = true;
                Burner4.Visible = false;
            }
            else if (Form1.ColorBurner == 4)
            {
                Burner0.Visible = false;
                Burner1.Visible = false;
                Burner2.Visible = false;
                Burner3.Visible = false;
                Burner4.Visible = true;
            }

            //Color Sesoula

            if (Form1.ColorSesoula == 0)
            {
                Sesoula0.Visible = true;
                Sesoula1.Visible = false;
                Sesoula2.Visible = false;
                Sesoula3.Visible = false;
                Sesoula4.Visible = false;
            }
            else if (Form1.ColorSesoula == 1)
            {
                Sesoula0.Visible = false;
                Sesoula1.Visible = true;
                Sesoula2.Visible = false;
                Sesoula3.Visible = false;
                Sesoula4.Visible = false;
            }
            else if (Form1.ColorSesoula == 2)
            {
                Sesoula0.Visible = false;
                Sesoula1.Visible = false;
                Sesoula2.Visible = true;
                Sesoula3.Visible = false;
                Sesoula4.Visible = false;
            }
            else if (Form1.ColorSesoula == 3)
            {
                Sesoula0.Visible = false;
                Sesoula1.Visible = false;
                Sesoula2.Visible = false;
                Sesoula3.Visible = true;
                Sesoula4.Visible = false;
            }
            else if (Form1.ColorSesoula == 4)
            {
                Sesoula0.Visible = false;
                Sesoula1.Visible = false;
                Sesoula2.Visible = false;
                Sesoula3.Visible = false;
                Sesoula4.Visible = true;
            }

            //Color Donisi
            if (Form1.ColorDonisi == 0)
            {
                Donisi0.Visible = true;
                Donisi1.Visible = false;
                Donisi2.Visible = false;
                Donisi3.Visible = false;
                Donisi4.Visible = false;
            }
            else if (Form1.ColorDonisi == 1)
            {
                Donisi0.Visible = false;
                Donisi1.Visible = true;
                Donisi2.Visible = false;
                Donisi3.Visible = false;
                Donisi4.Visible = false;
            }
            else if (Form1.ColorDonisi == 2)
            {
                Donisi0.Visible = false;
                Donisi1.Visible = false;
                Donisi2.Visible = true;
                Donisi3.Visible = false;
                Donisi4.Visible = false;
            }
            else if (Form1.ColorDonisi == 3)
            {
                Donisi0.Visible = false;
                Donisi1.Visible = false;
                Donisi2.Visible = false;
                Donisi3.Visible = true;
                Donisi4.Visible = false;
            }
            else if (Form1.ColorDonisi == 4)
            {
                Donisi0.Visible = false;
                Donisi1.Visible = false;
                Donisi2.Visible = false;
                Donisi3.Visible = false;
                Donisi4.Visible = true;
            }

            //Color Water Pump
            if(Form1.ColorWaterPump == 0)
            {
                WaterPump0.Visible = true;
                WaterPump1.Visible = false;
                WaterPump2.Visible = false;
                WaterPump3.Visible = false;
                WaterPump4.Visible = false;
            }
            else if (Form1.ColorWaterPump == 1)
            {
                WaterPump0.Visible = false;
                WaterPump1.Visible = true;
                WaterPump2.Visible = false;
                WaterPump3.Visible = false;
                WaterPump4.Visible = false;
            }
            else if (Form1.ColorWaterPump == 2)
            {
                WaterPump0.Visible = false;
                WaterPump1.Visible = false;
                WaterPump2.Visible = true;
                WaterPump3.Visible = false;
                WaterPump4.Visible = false;
            }
            else if (Form1.ColorWaterPump == 3)
            {
                WaterPump0.Visible = false;
                WaterPump1.Visible = false;
                WaterPump2.Visible = false;
                WaterPump3.Visible = true;
                WaterPump4.Visible = false;
            }
            else if (Form1.ColorWaterPump == 4)
            {
                WaterPump0.Visible = false;
                WaterPump1.Visible = false;
                WaterPump2.Visible = false;
                WaterPump3.Visible = false;
                WaterPump4.Visible = true;
            }
        }






        private void CoolerConveyor4_Click(object sender, EventArgs e)
        {

        }

        private void DesOven1_Load(object sender, EventArgs e)
        {
            //chart.BringToFront();
            picMain.SendToBack();
        }

        private void Anevatori4_Click(object sender, EventArgs e)
        {

        }

        private void Donisi0_Click(object sender, EventArgs e)
        {

        }
    }
}
