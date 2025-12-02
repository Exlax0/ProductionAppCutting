using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meme_Oven_Data.Repository
{
    public class DataTags
    {
        public static int StartTimeMachine1 { get; set; }
        public static int EndTimeMachine1 { get; set; }
        public static string ShiftCode { get; set; }
        public static string CurrentOperator1 { get; set; } 
        public static string CurrentOperator2 { get; set; }
        public static string CurrentCode1 { get; set; }

        public static int ShiftCuts1 { get; set; }
        public static int HourCuts1 { get; set; }
        public static int CutPieces1 { get; set; }
        public static string CurrentCode2 { get; set; }

        public static int Machine1PiecesPerShift { get; set; }
    }
}
