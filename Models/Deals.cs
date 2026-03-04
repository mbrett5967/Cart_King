using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Cart_King.Models
{
    public class Deals
    {
        [Key]
        public int SalesDealId { get; set; } 

        // Relationship
        public int ProductId { get; set; }
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