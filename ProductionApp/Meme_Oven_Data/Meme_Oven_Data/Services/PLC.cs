using Meme_Oven_Data.Services.Interfaces;
using Sharp7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meme_Oven_Data.Services
{
    public class PLC : IPLC
    {
        private S7Client client;
        public PLC() 
        {
            client = new S7Client();
        }

        public bool ConnectToPLC(string ipAddress, int rack, int slot)
        {
            try
            {
                int result = client.ConnectTo(ipAddress, rack, slot);

                if (result == 0)
                {
                    Console.WriteLine($"Connected to PLC at {ipAddress} (Rack: {rack}, Slot: {slot}).");
                    //MessageBox.Show("PLC connection successful.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to connect to PLC: {client.ErrorText(result)}");
                    MessageBox.Show($"Connection failed: {client.ErrorText(result)}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during PLC connection: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }

        public byte[] ReadDataBlock(int dbNumber, int startByte, int size)
        {
            try
            {
                byte[] buffer = new byte[size];
                int result = client.DBRead(dbNumber, startByte, size, buffer);

                if (result == 0)
                {
                    Console.WriteLine($"Successfully read {size} bytes from DB{dbNumber} starting at byte {startByte}.");
                    return buffer;
                }
                else
                {
                    Console.WriteLine($"Failed to read from DB{dbNumber}: {client.ErrorText(result)}");
                    //MessageBox.Show($"Read failed: {client.ErrorText(result)}");

                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during DB read: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}");
                return null;
            }
        }


        public void DisconnectFromPLC()
        {
            try
            {
                client.Disconnect();
                Console.WriteLine("Disconnected from PLC.");
                MessageBox.Show("Disconnected from PLC.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during PLC disconnection: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
