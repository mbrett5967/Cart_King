using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cart_King.Models;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using System.Data.Common;

namespace Cart_King.Connected_Services
{
    public class CartKingDbContext : DbContext
    {
        public CartKingDbContext(DbContextOptions<CartKingDbContext> options)
            : base(options) { }

        // Type of products on sale - electronics, clothing etc
        public DbSet<Category> Categories { get; set; } = null!;
        
        // Name, Price , image and stock quantity
        public DbSet<Product> Products { get; set; } = null!;

        // Who purchased and what is the price
        public DbSet<Order> Orders { get; set; } = null!;

        //Price at time of purchase - must not link to product
        public DbSet<OrderItem> OrderItems { get ; set; } = null!;
     
     // Data seeding
     protected override void OnModelCreating  (ModelBuilder modelBuilder)
        {
            // Seeding Parent - other entities wont work otherwise    
            modelBuilder.Entity<Category>().HasData(
         
            new Category { CategoryId = 1, Name = "Computer Accessories", DisplayOrder = 1 },
            new Category { CategoryId = 2, Name = "Graphics Cards" , DisplayOrder = 2 },            
            new Category { CategoryId = 3, Name = "CPU Processors" , DisplayOrder = 3 },
            new Category { CategoryId = 4, Name = "Custom Built PC's" , DisplayOrder = 4 },
            new Category { CategoryId = 5, Name = "VR Headsets" , DisplayOrder = 5 },
            new Category { CategoryId = 6, Name = "Consoles" , DisplayOrder = 6 }

            );
            
            // Seeding Child

            modelBuilder.Entity<Product>().HasData(
            new Product {ProductId = 1, Name = "Keyboard", Price = 19.99M, ImageUrl = "https://via.placeholder.com/150", StockQuantity = 100, CategoryId = 1 },
            new Product {ProductId = 2, Name = "ASUS GeForce RTX 5070 Ti TUF OC 16GB GDDR7", Price = 989.99M, ImageUrl = null!, StockQuantity = 40, CategoryId = 2 },
            new Product {ProductId = 3, Name = "AMD Ryzen 7 9850X3D 8 Core/16 Thread AM5 CPU", Price = 449.99M, ImageUrl = null!, StockQuantity = 40, CategoryId = 3 }    
            );
        }

    }
}


