using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cart_King.Models.ViewModels
{
    public class WishlistViewModel
  {
     [Required] // UI Validation
     [StringLength(100)]
     public string Name { get; set; } = string.Empty;

     [Range(0.01, 10000.00)] // UI Validation
     public decimal Price { get; set; }

     public int ProductId { get; set; }
     public int WishlistItemId { get; set; } // for removal from db to make "remove" button work 
     public string CategoryName { get; set; } = string.Empty;

     // for the views 
     public string ImageUrl { get; set; } = string.Empty;
     public string ShortDescription { get; set; } = string.Empty;
     public int StockQuantity { get; set;}
  }
}                