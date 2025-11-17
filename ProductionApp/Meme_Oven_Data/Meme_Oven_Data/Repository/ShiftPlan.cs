using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meme_Oven_Data.Repository
{


        [Table("MachineShiftPlan")]
        public class ShiftPlan
        {
            [Key]
            [Column("Id")]
            public Int32 Id { get; set; }

            [Column("Machine")]
            public String Machine { get; set; }

            [Column("ShiftCode")]
            public String ShiftCode { get; set; }

            [Column("ShiftName")]
            public String ShiftName { get; set; }

            [Column("StartTime")]
            public TimeSpan StartTime { get; set; }


            [Column("EndTime")]
            public TimeSpan EndTime { get; set; }


            [Column("PlanPerShift")]
            public Int32 PlanPerShift { get; set; }

            [Column("LastUpdated")]
            public DateTime LastUpdated { get; set; }


        }
    
}
