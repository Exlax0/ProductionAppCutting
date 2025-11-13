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
                            LabelStyle = { ForeColor = Color.Black },
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


            this.chart.Series.Add(series);
            this.chart.Series.Add(onOffSeries);
            UpdateChart();
            this.Controls.Add(chart);
        }

        private void UpdateChart()
        {
            try
            {
                // Retrieve the last 100 records, ordered by Date descending
                var data = _dbContext.TempOven2
                    .OrderByDescending(x => x.Date) // Get the most recent records
                    .Take(100)                     // Limit to 100 records
                    .OrderBy(x => x.Date)          // Reorder to ascending by Date for proper charting
                     .Select(x => new
                     {
                         x.Id,
                         x.Date,
                         x.Temperature,
                         x.OnOffOven
                     })
                    .ToList();

                this.series.Points.Clear();
                this.onOffSeries.Points.Clear();

                // Add data points to the chart
                for (int i = 1; i < data.Count; i++)
                {
                    this.series.Points.AddXY(data.ElementAt(i).Date, data.ElementAt(i).Temperature);
                    this.onOffSeries.Points.AddXY(data.ElementAt(i).Date, data.ElementAt(i).OnOffOven);
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

            var data = _dbContext.TempOven2
                    .Where(x => x.Date >= dateTimePickerFrom && x.Date <= dateTimePickerTo)
                    .OrderByDescending(x => x.Date) // Get the most recent records
                                                    //.Take(1000)                     // Limit to 100 records
                    .OrderBy(x => x.Date)          // Reorder to ascending by Date for proper charting
                    .ToList();

            this.series.Points.Clear();
            this.onOffSeries.Points.Clear();

            // Add data points to the chart
            for (int i = 1; i < data.Count; i++)
            {
                this.series.Points.AddXY(data.ElementAt(i).Date, data.ElementAt(i).Temperature);
                this.onOffSeries.Points.AddXY(data.ElementAt(i).Date, data.ElementAt(i).OnOffOven);
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
