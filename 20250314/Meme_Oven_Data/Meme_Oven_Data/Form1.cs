using Meme_Oven_Data.Pages;
using Meme_Oven_Data.Repository;
using Meme_Oven_Data.Services;
using Meme_Oven_Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sharp7;
using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Meme_Oven_Data
{
    public partial class Form1 : Form
    {

        public static int ColorTurbineFurnace { get; set; } 
        public static int ColorTainiaFurnace { get; set; }
        public static int ColorTurbineSmokeNo1 { get; set; }
        public static int ColorTurbineSmokeNo2 { get; set; }
        public static int ColorTurbineChimney { get; set; }
        public static int ColorTainiaCooler { get; set; }
        public static int ColorSesoula { get; set; }
        public static int ColorAnevatori { get; set; }
        public static int ColorTurbineCoolerNo1 { get; set; }
        public static int ColorTurbineCoolerNo2 { get; set; }
        public static int ColorWaterPump { get; set; }
        public static int ColorBurner { get; set; }
        public static int ColorDonisi { get; set; }
        public static int Recipe { get; set; }

        public static string OnomaSintagis { get; set; }



        private readonly MicrOvenContext _dbContext;
        private readonly IPLC _plc;
        private readonly IConfiguration _configuration;
        private readonly DesOven1 _desOven1;
        private readonly DesOven2 _desOven2;
        private Button Testbutton;

        public Form1(MicrOvenContext dbContext, IPLC plc, IConfiguration configuration, DesOven1 desOven1, DesOven2 desOven2)
        {
            _desOven1 = desOven1;
            _desOven2 = desOven2;
            _dbContext = dbContext;
            _plc = plc;
            _configuration = configuration;


            //var aaaa = _dbContext.TempOven2.Where(test => test.Id == 5).FirstOrDefault().Date;
            var select = _dbContext.TempOven1.Select(x => x.Temperature).Take(10);
            var aa = _dbContext.TempOven1.OrderByDescending(x => x.Date);
            var aaa = select.FirstOrDefault();
            var data = _dbContext.TempOven1
                    .OrderByDescending(x => x.Date) // Get the most recent records
                    .Take(100)                     // Limit to 100 records
                    .OrderBy(x => x.Date).Select(x => new
                    {
                        x.Id,
                        x.Date,
                        x.Temperature
                    })
                    .ToList();
            InitializeComponent();
            AddDesOven1Page();
            AddDesOven2Page();

            this.Testbutton = new Button
            {
                Size = new Size(50, 50),
                Location = new Point(0, 0)
            };
            this.Testbutton.Show();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            desOven11.Hide();
            desOven21.Hide();


            string plcIpAddress = _configuration["plc:IpAddress1"]; // PLC's IP address
            int rack = int.Parse(_configuration["plc:rack"]); // Rack number
            int slot = int.Parse(_configuration["plc:slot"]); // Slot number (usually 1 for S7-1200)

            var client1 = _plc.ConnectToPLC(plcIpAddress, rack, slot);

            string plcIpAddress2 = _configuration["plc:IpAddress2"]; // PLC's IP address
            int rack2 = int.Parse(_configuration["plc:rack"]); // Rack number
            int slot2 = int.Parse(_configuration["plc:slot"]); // Slot number (usually 1 for S7-1200)

            //var client2 = _plc.ConnectToPLC(plcIpAddress2, rack2, slot2);

            if (client1 == true)
            {
                ReadData1.Enabled = true;
                WriteToSQL.Enabled = true;
                reconnect_btn.Visible= false;
            }
            else
            {
                ReadData1.Enabled = false;
                WriteToSQL.Enabled = false;
                reconnect_btn.Visible = true;
            }

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btOven1_Click(object sender, EventArgs e)
        {
            desOven11.Show();
            desOven11.BringToFront();
            desOven11.Location = new Point(0, 0);
            desOven11.Size = new Size(1720, 980);
            desOven21.Hide();
        }

        private void btOven2_Click(object sender, EventArgs e)
        {
            desOven21.Show();
            desOven21.BringToFront();
            desOven21.Location = new Point(0, 0);
            desOven21.Size = new Size(1720, 980);
            desOven11.Hide();
        }

        private void ReadData1_Tick(object sender, EventArgs e)
        {
            int dbNumber = 31; // Data Block number
            int startByte = 0; // Start at byte 0
            byte[] buffer = new byte[10]; // Buffer for 8 bytes (2 REAL variables)
            int size = 10;

            var aaaaa = _plc.ReadDataBlock(dbNumber, startByte, size);

            if(aaaaa== null)
            {
             
                ReadData1.Enabled = false;
                WriteToSQL.Enabled = false;
                reconnect_btn.Visible = true;
            }

            //====Read The Color====
            Form1.ColorTurbineFurnace = GetColorMotor(3, 0, 30, 0);
            Form1.ColorTainiaFurnace = GetColorMotor(3, 0, 30, 2);
            Form1.ColorTurbineSmokeNo2 = GetColorMotor(3, 0, 30, 4);
            Form1.ColorTurbineChimney = GetColorMotor(3, 0, 30, 6);
            Form1.ColorTainiaCooler = GetColorMotor(3, 0, 30, 8);
            Form1.ColorSesoula = GetColorMotor(3, 0, 30, 10);
            Form1.ColorAnevatori = GetColorMotor(3, 0, 30, 14);
            Form1.ColorTurbineSmokeNo2 = GetColorMotor(3, 0, 30, 14);
            Form1.ColorTurbineCoolerNo1 = GetColorMotor(3, 0, 30, 16);
            Form1.ColorTurbineCoolerNo2 = GetColorMotor(3, 0, 30, 18);
            Form1.ColorTurbineSmokeNo1 = GetColorMotor(3, 0, 30, 20);
            Form1.ColorWaterPump = GetColorMotor(3, 0, 30, 22);
            Form1.ColorBurner = GetColorMotor(3, 0, 30, 28);
            Form1.ColorDonisi = GetColorMotor(3, 0, 32, 30);
            Form1.Recipe = GetColorMotor(20, 0, 142, 140);

            if (Form1.Recipe == 1)
            {
                //lblRecipe.Text = "Live Recipe: Kelyfwta";
                Form1.OnomaSintagis = "Kelyfwta";
            }
            else if (Form1.Recipe == 2)
            {
                //lblRecipe.Text = "Live Recipe: Fistikia";
                Form1.OnomaSintagis = "Fistikia";
            }
            else if (Form1.Recipe == 3)
            {
                // lblRecipe.Text = "Live Recipe: USA keli";
                Form1.OnomaSintagis = "USA keli";
            }


        }
        public int GetColorMotor(int dbnumber, int startByte, int size, int offset)
        {
            byte[] buffer = _plc.ReadDataBlock(dbnumber, startByte, size);

            if (buffer != null)
            {
                return S7.GetIntAt(buffer, offset); // Extract the REAL value from the buffer
            }
            else
            {
                Console.WriteLine("Failed to read tag value for Oven2.");
                return 0; // Return NaN on failure
            }
        }
        private float GetTagValueForOven1()
        {
            int dbNumber = 31; // Example: Data block number for Oven1
            int startByte = 0; // Address of the tag in the PLC
            int size = 4; // Size of a REAL (4 bytes)

            byte[] buffer = _plc.ReadDataBlock(dbNumber, startByte, size);

            if (buffer != null)
            {
                return S7.GetRealAt(buffer, 0); // Extract the REAL value from the buffer
            }
            else
            {
                Console.WriteLine("Failed to read tag value for Oven1.");
                return float.NaN; // Return NaN on failure
            }
        }

        private bool ReadEnable1()
        {
            int dbNumber = 34; // Example: Data block number for Oven1
            int startByte = 0; // Address of the tag in the PLC
            int size = 2; // Size of a REAL (4 bytes)

            byte[] buffer = _plc.ReadDataBlock(dbNumber, startByte, size);

            if (buffer != null)
            {
                return S7.GetBitAt(buffer, 1,0); // Extract the REAL value from the buffer
            }
            else
            {
                Console.WriteLine("Failed to read tag value for Oven1.");
                return false; // Return NaN on failure
            }
        }
        private bool ReadEnable2()
        {
            int dbNumber = 1; // Example: Data block number for Oven1
            int startByte = 0; // Address of the tag in the PLC
            int size = 10; // Size of a REAL (4 bytes)

            byte[] buffer = _plc.ReadDataBlock(dbNumber, startByte, size);

            if (buffer != null)
            {
                return S7.GetBitAt(buffer, 8, 1); // Extract the REAL value from the buffer
            }
            else
            {
                Console.WriteLine("Failed to read tag value for Oven1.");
                return false; // Return NaN on failure
            }
        }

        private float GetTagValueForOven2()
        {
            int dbNumber = 1; // Example: Data block number for Oven2
            int startByte = 4; // Address of the tag in the PLC (offset for Oven2)
            int size = 4; // Size of a REAL (4 bytes)

            byte[] buffer = _plc.ReadDataBlock(dbNumber, startByte, size);

            if (buffer != null)
            {
                return S7.GetRealAt(buffer, 0); // Extract the REAL value from the buffer
            }
            else
            {
                Console.WriteLine("Failed to read tag value for Oven2.");
                return float.NaN; // Return NaN on failure
            }
        }


        private void WriteToSQL_Tick(object sender, EventArgs e)
        {
           
            try
            {
                string RecipeName = Form1.OnomaSintagis;
                float tagValueOven1 = GetTagValueForOven1(); // Replace with your method to fetch the tag value
                //float tagValueOven2 = GetTagValueForOven2(); // Replace with your method to fetch the tag value
                var EnableOven1 = ReadEnable1();
                //var  EnableOven2 = ReadEnable2();
                // Create a new TempOven1 entity
                var tempOven1 = new TempOven1
                {
                    Temperature = tagValueOven1, // Manual value
                    Date = DateTime.Now, // Add a timestamp
                    OnOffOven = EnableOven1,
                    Recipe = Form1.OnomaSintagis 
                };

                //var tempOven2 = new TempOven2
                //{
                //    //Temperature = tagValueOven2, // Manual value for TempOven2
                //    Date = DateTime.Now, // Current timestamp
                //    //OnOffOven = EnableOven2
                //};

                // Add the entity to the context
                _dbContext.TempOven1.Add(tempOven1);
                //_dbContext.TempOven2.Add(tempOven2);

                // Save changes to the database
                _dbContext.SaveChanges();

                Console.WriteLine("Temperature value written to SQL table.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to SQL table: {ex.Message}");
            }
        }

        private void btOven1_MouseUp(object sender, MouseEventArgs e)
        {
            btOven1.BackColor = Color.FromArgb(42, 54, 65);
        }

        private void reconnect_btn_Click(object sender, EventArgs e)
        {
            string plcIpAddress = _configuration["plc:IpAddress1"]; // PLC's IP address
            int rack = int.Parse(_configuration["plc:rack"]); // Rack number
            int slot = int.Parse(_configuration["plc:slot"]); // Slot number (usually 1 for S7-1200)

            var client1 = _plc.ConnectToPLC(plcIpAddress, rack, slot);

            if (client1 == true)
            {
                ReadData1.Enabled = true;
                WriteToSQL.Enabled = true;
                reconnect_btn.Visible = false;
            }
            else
            {
                ReadData1.Enabled = false;
                WriteToSQL.Enabled = false;
                reconnect_btn.Visible = true;
            }
        }
    }
}
