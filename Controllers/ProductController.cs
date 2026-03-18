using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cart_King.Connected_Services;
using Cart_King.Models;
using Cart_King.Models.ViewModels;

namespace Cart_King.Controllers
{
    public class ProductController : Controller
    {
        private readonly CartKingDbContext _context;

        public ProductController(CartKingDbContext context)
        {
            _context = context;
        }
        
   

         // Gets details view of specific product via productID
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null) 
            {
              
              return NotFound();
            }
            
            // sends data to viewmodel
            var ViewModel = new ProductDetailsViewModel
            {
               Name = product.Name,
               Price = product.Price,
               CategoryName = product.Category?.Name ?? "None",
               ImageUrl = product.ImageUrl,             // Transferring the data
               ShortDescription = product.ShortDescription, 
               StockQuantity = product.StockQuantity
            };
          
          return View(ViewModel);
        }



            // search bar
              public async Task<IActionResult> Index(string searchString)
        {
            var products = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {                                   
                products = products.Where(p => p.Name.ToLower().Contains(searchString) // searches product name
                                            || p.ShortDescription.ToLower().Contains (searchString) // allows description to be considered 
                                            || p.Category !=null && p.Category.Name.ToLower().Contains (searchString)); // this fixes my dereference/reference null error
            }

            return View(await products.ToListAsync());
        }

        //shows all products using categoryID
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