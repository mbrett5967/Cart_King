using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cart_King.Connected_Services;
using Cart_King.Models;

namespace Cart_King.Controllers
{
    public class ProductController : Controller
    {
        private readonly CartKingDbContext _context;

        public ProductController(CartKingDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var products = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            return View(await products.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null) return NotFound();
            return View(product);
        }

        public async Task<IActionResult> ByCategory(int id)
        {
            var products = await _context.Products
                .Where(p => p.CategoryId == id)
                .ToListAsync();

            var category = await _context.Categories.FindAsync(id);
            ViewBag.CategoryName = category?.Name;

            return View("Index", products);
        }
    }
}