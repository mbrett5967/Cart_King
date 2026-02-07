using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cart_King.Models
{   
    public class Product
    {
        [Key]
        [Required]
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }



        public string? ImageUrl { get; set; } = string.Empty;

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
    }
}