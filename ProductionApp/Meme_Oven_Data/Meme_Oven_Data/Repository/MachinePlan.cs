using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meme_Oven_Data.Repository
{
    
        [Table("MachinePlan")]
        public class MachinePlan
        {
            [Key]
            [Column("Id")]
            public Int32 Id { get; set; }

            [Column("Date")]
            public DateTime Date { get; set; }

            [Column("Machine")]
            public String Machine { get; set; }

            [Column("PlanHour")]
            public Int32 PlanHour { get; set; }

            [Column("PlanShift")]
            public Int32 PlanShift { get; set; }

        }
    
}
