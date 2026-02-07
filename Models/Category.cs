using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cart_King.Models
{
    public class Category
    {
     
         [ForeignKey("CategoryId")]
         
        public int CategoryId { get; set; }

        
        public string Name { get; set; } = string.Empty;
        
        public int DisplayOrder { get; set; }



    }
}