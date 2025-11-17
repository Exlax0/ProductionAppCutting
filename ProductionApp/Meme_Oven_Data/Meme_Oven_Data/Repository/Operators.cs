using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meme_Oven_Data.Repository
{
    [Table("Operators")]
    public class Operators
    {
        [Key]
        [Column("Id")]
        public Int32 Id { get; set; }

        [Column("FullName")]
        public String FullName { get; set; }

    }
}
