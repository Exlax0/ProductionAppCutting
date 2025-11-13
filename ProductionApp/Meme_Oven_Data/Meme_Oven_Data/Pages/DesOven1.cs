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



        }

        

        private void DesOven1_Load(object sender, EventArgs e)
        {
            //chart.BringToFront();
            
        }
               

    }
}
