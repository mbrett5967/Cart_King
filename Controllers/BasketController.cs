using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Cart_King.Connected_Services;
using Cart_King.Models;
using Cart_King.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;


namespace Cart_King.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CartKingDbContext _context;
        private readonly ILogger<BasketController> _logger;

        public BasketController(SignInManager<IdentityUser> signInManager, CartKingDbContext context, UserManager<IdentityUser> userManager, ILogger<BasketController> logger)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {

            // Get Id of current Logged-in User
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Unauthorized();
            }

            // Search for the users basket on DB
            var findBasket = await _context.BasketItems //Loads basketItems table
            .Where(w => w.IdentityUserId == userId) // Look for IDentityID in table that matches current users ID
            .Include(w => w.Product) //Include the product table
            .ToListAsync();

            var viewModels = findBasket.Select(w => new BasketItemViewModel
            {
                Name = w.Product.Name,
                Price = w.Product.Price,
                Quantity = w.Quantity,
                ProductId = w.Product.ProductId,
                ImageUrl = w.Product.ImageUrl,
                ShortDescription = w.Product.ShortDescription,
                StockQuantity = w.Product.StockQuantity
            }).ToList();

            return View("Index", viewModels);

        }



        [HttpPost]
        public async Task<IActionResult> AddToBasket(int Id, string returnUrl)
        {

            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Unauthorized();
            }

            // checks to see if basket item exists for user in Db
            var basket = await _context.BasketItems.FirstOrDefaultAsync(w => w.IdentityUserId == userId && w.ProductId == Id);

            //Adds the new item if no
            if (basket == null)
            {
                var newItem = new BasketItem
                {
                    IdentityUserId = userId,
                    ProductId = Id,
                    Quantity = 1

                };
                _context.BasketItems.Add(newItem);


            }

            else
            {
                //adds to the quantity if yes
                basket.Quantity += 1;

            }

            await _context.SaveChangesAsync();



            if (!string.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");




        }

        public async Task<IActionResult> RemoveBasketItem(int productId, string returnUrl)
        {

            var userId = _userManager.GetUserId(User);

            if (userId == null) //sends unregistered users to login
            {
                return Unauthorized();
            }

            var currentBasket = await _context.BasketItems
                .FirstOrDefaultAsync(w => w.IdentityUserId == userId && w.ProductId == productId);

            if (currentBasket != null)
            {
                _context.BasketItems.Remove(currentBasket);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction("Index");
            }
            else
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        public async Task<IActionResult> AdjustQuantity(int Id, int adjustment)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Json(new { success = false, message = "Unauthorized" });
            }

            var basketItem = await _context.BasketItems
                .Include(b => b.Product) // Include Product to access price/stock if needed
                .FirstOrDefaultAsync(w => w.IdentityUserId == userId && w.ProductId == Id);

            if (basketItem == null)
            {
                return Json(new { success = false, message = "Item not found in basket." });
            }

            // adjustment (+1 or -1)
            basketItem.Quantity += adjustment;

            // If the quantity drops to 0 or less, completely remove the item
            if (basketItem.Quantity <= 0)
            {
                _context.BasketItems.Remove(basketItem);
                await _context.SaveChangesAsync();

                return Json(new { success = true, newQty = 0, itemRemoved = true });
            }

            // Optional Check: Prevent going over available stock
            if (basketItem.Quantity > basketItem.Product.StockQuantity)
            {
                return Json(new { success = false, message = "Cannot add more than available stock." });
            }

            await _context.SaveChangesAsync();

            // Calculate item total and overall basket total to send back to the UI
            var itemTotal = basketItem.Quantity * basketItem.Product.Price;

            // Grab the new grand total for the whole basket
            var totalBasketPrice = await _context.BasketItems
                .Where(w => w.IdentityUserId == userId)
                .SumAsync(w => w.Quantity * w.Product.Price);

            return Json(new
            {
                success = true,
                newQty = basketItem.Quantity,
                itemRemoved = false,
                itemTotal = itemTotal.ToString("C"), // Formatted currency string
                basketTotal = totalBasketPrice.ToString("C")
            });
        }


        /*public async Task<IActionResult> AdjustQuantity(int Id)
        {

            var userId = _userManager.GetUserId(User);

            if (userId == null) //sends unregistered users to login
            {
                return Unauthorized();
            }

            var basket = await _context.BasketItems.FirstOrDefaultAsync(w => w.IdentityUserId == userId && w.ProductId == Id);                                  

            //Adds the new item if no
            if (basket == null)
            {
            return RedirectToAction("Index");
            }

            else
            {  
                basket.Quantity -= 1;
                await _context.SaveChangesAsync();

            RedirectToAction("Index");
            }

            return View("Index");*/

    }

}
        



    

