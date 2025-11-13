using Meme_Oven_Data.Pages;
using Meme_Oven_Data.Repository;
using Meme_Oven_Data.Services;
using Meme_Oven_Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sharp7;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Meme_Oven_Data
{
    public partial class Form1 : Form
    {

        public static Boolean CuttingPulse1 { get; set; }
        private bool _prevCuttingPulse1=false;
        public static Boolean CuttingPulse2 { get; set; }
        private bool _prevCuttingPulse2 = false;
        public static Boolean CuttingPulse3 { get; set; }
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


            
            //var select = _dbContext.TempOven1.Select(x => x.Temperature).Take(10);
            var aa = _dbContext.TempOven1.OrderByDescending(x => x.Date);
            //var aaa = select.FirstOrDefault();
            //var data = _dbContext.TempOven1
            //        .OrderByDescending(x => x.Date) // Get the most recent records
            //        //.Take(100)                     // Limit to 100 records
            //        .OrderBy(x => x.Date).Select(x => new
            //        {
            //            x.Id,
            //            x.Date
                        
            //        })
                    //.ToList();
            InitializeComponent();
            AddDesOven1Page();
            

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
                reconnect_btn.Visible= false;
            }
            else
            {
                ReadData1.Enabled = false;
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
            //desOven21.Hide();
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
            int dbNumber = 7; // Data Block number
            int startByte = 0; // Start at byte 0
            byte[] buffer = new byte[4]; // Buffer for 8 bytes (2 REAL variables)
            int size = 4;

            var aaaaa = _plc.ReadDataBlock(dbNumber, startByte, size);

            if(aaaaa== null)
            {
             
                ReadData1.Enabled = false;
                WriteToSQL.Enabled = false;
                reconnect_btn.Visible = true;
            }

            CuttingPulse1 = GetBitValue(9, 0, 0);
            CuttingPulse2 = GetBitValue(9, 0, 1);

            try
            {

                bool risingEdge1 = CuttingPulse1 && !_prevCuttingPulse1;
                bool risingEdge2 = CuttingPulse2 && !_prevCuttingPulse2;

                if (risingEdge1)
                {
                    var tempOven1 = new TempOven1
                    {
                        Date = DateTime.Now,
                        Machine = "Cutting - Machine 01",
                        Name = "gpapianos",
                        Cut = 1
                    };

                    _dbContext.TempOven1.Add(tempOven1);
                    _dbContext.SaveChanges();
                }
                _prevCuttingPulse1 = CuttingPulse1;
                
                if (risingEdge2)
                {
                    var tempOven1 = new TempOven1
                    {
                        Date = DateTime.Now,
                        Machine = "Cutting - Machine 02",
                        Name = "gpapianos",
                        Cut = 2
                    };

                    _dbContext.TempOven1.Add(tempOven1);
                    _dbContext.SaveChanges();
                }
                _prevCuttingPulse2 = CuttingPulse2;


            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("INNER: " + ex.InnerException.Message);
                Console.WriteLine($"Error writing to SQL table: {ex.Message}");
            }
            


        }

        public bool GetBitValue(int dbNumber, int byteIndex, int bitIndex)
        {
            var buffer = _plc.ReadDataBlock(dbNumber, byteIndex, 1);
            return buffer != null && S7.GetBitAt(buffer, 0, bitIndex);
        }
        public int GetIntValue(int dbnumber, int startByte, int size, int offset)
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
        public float GetRealValue(int dbNumber, int startByte, int size)
        {
             
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

       

        private void WriteToSQL_Tick(object sender, EventArgs e)
        {
           
            try
            {
                string RecipeName = Form1.OnomaSintagis;
                float Level = GetRealValue(7,0,4); // Replace with your method to fetch the tag value
                bool risingEdge = CuttingPulse1 && !_prevCuttingPulse1;

                if (risingEdge)
                {
                    // Create a new TempOven1 entity
                    //var tempOven1 = new TempOven1
                    //{
                    //    Date = DateTime.Now, // Add a timestamp
                    //    Machine = "Cutting - Machine 01",
                    //    Name = "gpapianos",
                    //    Cut = 1
                    //};

                    //// Add the entity to the context
                    //_dbContext.TempOven1.Add(tempOven1);
                    ////_dbContext.TempOven2.Add(tempOven2);

                    //// Save changes to the database
                    //_dbContext.SaveChanges();

                    //_prevCuttingPulse1 = CuttingPulse1;
                Console.WriteLine("Temperature value written to SQL table.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to SQL table: {ex.Message}");
            }
            _prevCuttingPulse1 = CuttingPulse1;
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
