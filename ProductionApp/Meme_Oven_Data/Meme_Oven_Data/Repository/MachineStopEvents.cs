using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meme_Oven_Data.Repository
{
    [Table("MachineStopEvents")]
    public class MachineStopEvent
    {
        [Key]
        public int Id { get; set; }

        public string Machine { get; set; }
        public int StopReasonId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string OperatorName { get; set; }
        public string Comment { get; set; }

        [ForeignKey(nameof(StopReasonId))]
        public StopReason StopReason { get; set; }
    }
}
