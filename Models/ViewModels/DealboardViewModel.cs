using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cart_King.Models.ViewModels
{
    public class DealboardViewModel
    {
     
     public int ProductId { get; set; }

     public string ProductName { get; set; } = null!;
     public string ImageUrl { get; set; } = null!;
     public string ShortDescription { get; set; } = null!;

     public decimal OriginalPrice { get; set; }
     public decimal SalePrice { get; set; }
     public int SalesDealId { get; set; }   

   
      

    }

    }
