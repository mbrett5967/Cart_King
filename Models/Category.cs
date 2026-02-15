using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cart_King.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        // This links the category back to the list of products
        public virtual ICollection<Product>? Products { get; set; }
    }
}