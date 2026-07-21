using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cart_King.Connected_Services;
using Cart_King.Models;
using Cart_King.Models.ViewModels;
using Microsoft.AspNetCore.Identity;


namespace Cart_King.Controllers
{
    public class ProductController : Controller
    {
        private readonly CartKingDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductController(CartKingDbContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
   
         // Gets details view of specific product via productID
        public async Task<IActionResult> Details(int id)
        {
            var productId = id; // can only use one param for single entity, fixed bugged wishlist behaviour. so combined the two. 
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null) 
            {
              
              return NotFound();
            }
            
            var viewModel = new DashboardViewModel
            {
                ProductDetails = new ProductDetailsViewModel
                {
                    Name = product.Name,
                    Price = product.Price,
                    CategoryName = product.Category?.Name ?? "None",
                    ImageUrl = product.ImageUrl,
                    ShortDescription = product.ShortDescription,
                    StockQuantity = product.StockQuantity
                },
                BasketItems = new BasketItemViewModel
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    CategoryName = product.Category?.Name ?? "None",
                    ImageUrl = product.ImageUrl,
                    ShortDescription = product.ShortDescription,
                    StockQuantity = product.StockQuantity
                }
            };

            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return View(viewModel);
            }

            var basketItem = await _context.BasketItems
                .FirstOrDefaultAsync(w => w.IdentityUserId == userId && w.ProductId == productId);

            bool wishlistCheck = await _context.WishlistItems.AnyAsync(w => w.IdentityUserId == userId && w.ProductId == productId);
            viewModel.ProductDetails.IsInWishlist = wishlistCheck;
            viewModel.ProductDetails.IsInBasket = basketItem != null;
            viewModel.BasketItems.Quantity = basketItem?.Quantity ?? 0;
            viewModel.BasketItems.BasketItemId = basketItem?.BasketItemId ?? 0;

            return View(viewModel);

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