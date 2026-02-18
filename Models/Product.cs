using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cart_King.Models
{
    public class Product
    
    {
       
     [Key]
     public int ProductId { get; set; }

     public string Name { get; set; } = string.Empty;

     [Column(TypeName = "decimal(18,2)")] // Database storage logic
     public decimal Price { get; set; }

     public string? ImageUrl { get; set; }

     public int StockQuantity { get; set; }

     public string? ShortDescription { get; set; }

     // Relationship Logic
     public int CategoryId { get; set; } 
    
     [ForeignKey("CategoryId")]
     public virtual Category? Category { get; set; }
    
    }
       

        
        


    



}