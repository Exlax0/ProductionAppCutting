using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meme_Oven_Data.Services.Interfaces
{
    public interface IPLC
    {
        bool ConnectToPLC(string ipAddress, int rack, int slot);

        byte[] ReadDataBlock(int dbNumber, int startByte, int size);

        void DisconnectFromPLC();
    }
}
