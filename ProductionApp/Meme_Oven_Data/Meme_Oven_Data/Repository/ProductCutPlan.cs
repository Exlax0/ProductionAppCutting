using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meme_Oven_Data.Repository
{
    public class ProductCutPlan
    {
        public int Id { get; set; }

        // π.χ. "NH432"
        public string ProductCode { get; set; } = null!;

        public string? Description { get; set; }

        // Πόσα τεμάχια βγάζει κάθε κοπή
        public int PiecesPerCut { get; set; }

        // Πόσες κοπές/ώρα στο στόχο
        public int CutsPerHour { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }

}
