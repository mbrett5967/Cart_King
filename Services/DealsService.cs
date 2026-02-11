
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cart_King.Models;
using Cart_King.Connected_Services;
using Microsoft.EntityFrameworkCore.Internal;
using Cart_King.Controllers;

namespace Cart_King.Services
{
    public class DealsService
    {
        private readonly CartKingDbContext _context;

        public DealsService(CartKingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetActiveDealsAsync()
        {
            // Placeholder deal logic: return first 4 products.
            return await _context.Products
                .Take(3)
                .ToListAsync();
        }

        public async Task<List<DealboardViewModel>> GetFeaturedDealsAsync()
        {
            var now = DateTime.UtcNow;

            return await _context.SalesDeals
                .Include(d => d.Product)
                .Where(d =>
                    d.IsActive &&
                    d.IsFeatured &&
                    d.StartDate <= now &&
                    d.EndDate >= now)
                .OrderByDescending(d => d.Priority)
                .Take(3)
                .Select(d => new DealboardViewModel
                {
                    ProductId = d.ProductId,
                    ProductName = d.Product.Name,
                    ImageUrl = d.Product.ImageUrl,
                    ShortDescription = d.Product.ShortDescription,
                    OriginalPrice = d.Product.Price,
                    SalePrice = d.SalePrice
                })
                .ToListAsync();
        }
    }
}


