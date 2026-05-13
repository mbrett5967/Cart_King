using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cart_King.Models
{
    public class Deals
    {
        [Key]
        public int SalesDealId { get; set; } 

        // Relationship
        [Required]
        public int ProductId { get; set; }
        [ForeignKey ("ProductId")]
        public Product Product { get; set; } = null!;

        // Pricing
        public decimal SalePrice { get; set; }

        // Timing
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // State
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }

        // Display control
        public int Priority { get; set; }
       
    }
}