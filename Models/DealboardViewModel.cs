using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart_King.Models
{
    public class DealboardViewModel
    {
    

     public int ProductId { get; set; }

     public string ProductName { get; set; } = null!;
     public string ImageUrl { get; set; } = null!;
     public string ShortDescription { get; set; } = null!;

     public decimal OriginalPrice { get; set; }
     public decimal SalePrice { get; set; }
    
    }

    }
