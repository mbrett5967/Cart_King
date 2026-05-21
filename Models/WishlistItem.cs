using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Cart_King.Models
{
    public class WishlistItem
    {
        [Key]
        public int WishlistItemId { get; set; }

        // Relationship for product
        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;

        // Relationship for user
        [Required]
        public string IdentityUserId { get; set; } = string.Empty;

        [ForeignKey("IdentityUserId")]
        public IdentityUser? User { get; set; }
    }
}
