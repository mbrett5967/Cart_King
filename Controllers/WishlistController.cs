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
{   [Authorize]
    public class WishlistController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
       
        private readonly CartKingDbContext _context;

        public WishlistController( CartKingDbContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            
        }

        // Add to Wishlist
       [HttpPost]
       public async Task<IActionResult> AddToWishlist(int productId, string returnUrl)
        {
            var userId = _userManager.GetUserId(User);
            
            if (userId == null) //sends unregistered users to login
            {
                return Unauthorized();
            }

            //check for any duplicate items in wishlist
            bool itemCheck = await _context.WishlistItems.AnyAsync(w => w.IdentityUserId == userId && w.ProductId == productId);
            
          
            if (itemCheck == false)
            {


                var newItem = new WishlistItem
                {
                    IdentityUserId = userId, 
                    ProductId = productId 
                   
                };
                _context.WishlistItems.Add(newItem);
                TempData[$"IsInWishlist_{productId}"] = false;
                //add redirct to action for state changes to icon etc
               
            }

            
            else  
            {   
                TempData[$"IsInWishlist_{productId}"] = true; 
               return LocalRedirect(returnUrl);

            };

           //Save changes 
            
            await _context.SaveChangesAsync();
            

            //safety fallback to prevent redirect vulnerability 
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Index","Home");    

        }

        public async Task<IActionResult> GetWishlist()
        {
            var userId = _userManager.GetUserId(User);
            
            if (userId == null) 
            {
                return Unauthorized();
            }

            var findWishlist = await _context.WishlistItems
            .Where(w => w.IdentityUserId == userId)
            .Include(w => w.Product)
            .ThenInclude(p => p.Category) //loads the linked product table and category
            .ToListAsync();

            // Maps each WishlistItem to WishlistViewModel
            var viewModels = findWishlist.Select(w => new WishlistViewModel
            {
               Name = w.Product.Name,
               Price = w.Product.Price,
               ProductId = w.Product.ProductId,
               CategoryName = w.Product.Category?.Name ?? "None",
               ImageUrl = w.Product.ImageUrl,
               ShortDescription = w.Product.ShortDescription, 
               StockQuantity = w.Product.StockQuantity
            }).ToList();
          
            return PartialView("_GetWishlistPartial", viewModels);
        }

         [HttpPost]
       public async Task<IActionResult> RemoveItem(int productId, string returnUrl)
        {
            var userId = _userManager.GetUserId(User);
            
            if (userId == null) //sends unregistered users to login
            {
                return Unauthorized();
            }
            
            var existingItem = await _context.WishlistItems
                .FirstOrDefaultAsync(w => w.IdentityUserId == userId && w.ProductId == productId);
            
            if (existingItem != null)
            {
                _context.WishlistItems.Remove(existingItem);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction("Index", "Account", new { activeTab = "wishlist" });
            }
            else 
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction("Index", "Account", new { activeTab = "wishlist" });
            }






            
        }

    }
}