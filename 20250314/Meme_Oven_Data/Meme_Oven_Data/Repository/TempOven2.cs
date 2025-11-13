using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meme_Oven_Data.Repository
{
    [Table("TempeOven2")]
    public class TempOven2
    {
        [Key]
        [Column("Id")]
        public Int64 Id { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("Temperature")]
        public double Temperature { get; set; }

        [Column("OnOffOven")]
        public bool? OnOffOven { get; set; }
    }
}
