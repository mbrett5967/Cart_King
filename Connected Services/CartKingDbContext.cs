using System;
using Microsoft.EntityFrameworkCore; 
using Cart_King.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace Cart_King.Connected_Services
{
    public class CartKingDbContext : IdentityDbContext<IdentityUser>
    {
        public CartKingDbContext(DbContextOptions<CartKingDbContext> options)
            : base(options) { }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Deals> SalesDeals { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Computer Accessories", DisplayOrder = 1 },
                new Category { CategoryId = 2, Name = "Graphics Cards", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "CPU Processors", DisplayOrder = 3 },
                new Category { CategoryId = 4, Name = "Custom Built PC's", DisplayOrder = 4 },
                new Category { CategoryId = 5, Name = "VR Headsets", DisplayOrder = 5 },
                new Category { CategoryId = 6, Name = "Consoles", DisplayOrder = 6 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Keyboard", Price = 19.99M, ImageUrl = "https://media.currys.biz/i/currysprod/10235883?$l-large$&fmt=auto", StockQuantity = 100, CategoryId = 1, ShortDescription = "Premium mechanical keyboard with RGB lighting" },
                new Product { ProductId = 2, Name = "ASUS GeForce RTX 5070 Ti TUF OC 16GB GDDR7", Price = 989.99M, ImageUrl = "https://media.currys.biz/i/currysprod/10275017?$l-large$&fmt=auto", StockQuantity = 40, CategoryId = 2, ShortDescription = "Ultimate gaming graphics card with ray tracing" },
                new Product { ProductId = 3, Name = "AMD Ryzen 7 9850X3D 8 Core/16 Thread AM5 CPU", Price = 449.99M, ImageUrl = "https://media.currys.biz/i/currysprod/10297321?$l-large$&fmt=auto", StockQuantity = 10, CategoryId = 3, ShortDescription = "High-performance processor for gaming and workstations" },
                new Product { ProductId = 4, Name = "MSI MPG Infinite Z3 AI Gaming PC - AMD Ryzen 9, RTX 5080, 2 TB SSD", Price = 2699.99M, ImageUrl = "https://media.currys.biz/i/currysprod/10282274?$l-large$&fmt=auto", StockQuantity = 10, CategoryId = 4, ShortDescription = "This MSI Infinite Z3 packs a punch with its AMD Ryzen 9 Processor." },
                new Product { ProductId = 5, Name = "META Quest 3S Mixed Reality Headset", Price = 339.99M, ImageUrl = "https://media.currys.biz/i/currysprod/10270714?$l-large$&fmt=auto", StockQuantity = 30, CategoryId = 5, ShortDescription = "Dive into amazing experiences with the Meta Quest 3S" },
                new Product { ProductId = 6, Name = "NINTENDO Switch 2", Price = 395.00M, ImageUrl = "https://media.currys.biz/i/currysprod/10281815?$l-large$&fmt=auto", StockQuantity = 100, CategoryId = 6, ShortDescription = "Game anywhere with Nintendo Switch 2." }
            );
        }
    }
}