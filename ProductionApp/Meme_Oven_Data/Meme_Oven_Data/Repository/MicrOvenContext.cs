using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meme_Oven_Data.Repository
{
    public class MicrOvenContext : DbContext
    {
        public MicrOvenContext(DbContextOptions<MicrOvenContext> options) : base(options)
        {
        }
        public DbSet<ShiftPlan> MachineShiftPlan { get; set; }

        public DbSet<TempOven1> TempOven1 { get; set; }
        public DbSet<TempOven2> TempOven2 { get; set; }

        public DbSet<MachinePlan> MachinePlan { get; set; }

    }
}
