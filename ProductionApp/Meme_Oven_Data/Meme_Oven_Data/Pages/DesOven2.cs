using Meme_Oven_Data.Repository;
using Meme_Oven_Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Meme_Oven_Data.Pages
{
    public partial class DesOven2 : UserControl
    {
        private readonly MicrOvenContext _dbContext;
        private Chart chart;
        Series series;
        Series onOffSeries;
       

        public DesOven2(MicrOvenContext dbContext)
        {
            _dbContext = dbContext;
            InitializeComponent();


            this.chart = new Chart
            {
                Size = new Size(1657, 550),
                Location = new Point(50, 400),
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
                     Format = "dd/MM/yyyy\nHH:mm:ss"},
                            MajorGrid = { LineColor = Color.LightGray },
                            
                        },

                AxisY = {
                            Title = "Cuts per 15 minutes",
                            TitleFont = new Font("Arial", 18, FontStyle.Bold),
                            LabelStyle = { ForeColor = Color.Black },
                            MajorGrid = { LineColor = Color.LightGray }
                        }               
            };

            chart.ChartAreas.Add(chartArea);

            // Set chart background color
            chart.BackColor = Color.White;

            // Add series
           this.series = new Series("CutsPer15Min")
            {
                ChartType = SeriesChartType.Column,
                BorderWidth = 2,
                Color = Color.Red
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
                Enabled = AxisEnabled.False,
                LabelStyle = { ForeColor = Color.Black },
                MajorGrid = { LineColor = Color.Transparent },
                LineColor = Color.Black,
                Maximum = 2,
                Minimum = 0,
            };


            this.chart.Series.Add(series);
            this.chart.Series.Add(onOffSeries);
            UpdateChart();
            this.Controls.Add(chart);
        }

        private void UpdateChart()
        {
            try
            {
                DateTime oneHourAgo = DateTime.Now.AddHours(-1);
                var data = _dbContext.TempOven1
                   .Where(x => x.Cut == 2 && x.Date >= oneHourAgo)
                   .OrderByDescending(x => x.Date) // Get the most recent records
                                                   //.Take(20)                               //.Take(1000)                     // Limit to 100 records
                   .OrderBy(x => x.Date)          // Reorder to ascending by Date for proper charting
                   .ToList();

                
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

        private void Search_Btn_Click(object sender, EventArgs e)
        {
            timerUpdateChart.Enabled = false;
            var dateTimePickerFrom = datePickerFrom.Value.Date + TimePickerFrom.Value.TimeOfDay;
            var dateTimePickerTo = datePickerTo.Value.Date + timePickerTo.Value.TimeOfDay;

            var data = _dbContext.TempOven1
                    .Where(x => x.Date >= dateTimePickerFrom && x.Date <= dateTimePickerTo && x.Cut == 1)
                    .OrderByDescending(x => x.Date) // Get the most recent records
                                                    //.Take(1000)                     // Limit to 100 records
                    .OrderBy(x => x.Date)          // Reorder to ascending by Date for proper charting
                    .ToList();

            var count = data.Count;

            this.series.Points.Clear();

            // Add data points to the chart
            for (int i = 1; i < data.Count; i++)
            {
                this.series.Points.AddXY(data.ElementAt(i).Date, data.ElementAt(i).Cut);

            }
        }

        private void timerUpdateChart_Tick(object sender, EventArgs e)
        {
            UpdateChart();

        }

        private void Live_btn_Click(object sender, EventArgs e)
        {
            timerUpdateChart.Enabled = true;
        }
    }
}
