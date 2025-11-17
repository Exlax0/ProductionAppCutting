using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meme_Oven_Data.Repository
{
    [Table("MachineData")]
    public class TempOven1
    {
        [Key]
        [Column("Id")]
        public Int32 Id { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("Machine")]
        public String Machine { get; set; }

        [Column("Name")]
        public String Name { get; set; }

        [Column("Cut")]
        public Int32 Cut { get; set; }

    }
}
