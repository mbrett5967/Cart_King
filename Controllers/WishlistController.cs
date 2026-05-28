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

        public WishlistController(SignInManager<IdentityUser> signInManager, CartKingDbContext context,UserManager<IdentityUser> userManager)
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
            var itemCheck = await _context.WishlistItems
            .AnyAsync(w => w.IdentityUserId == userId && w.ProductId == productId);
          
            if (itemCheck)
            {
                //add redirct to action for state changes to icon etc
               TempData[$"IsInWishlist_{productId}"] = true; 
               return LocalRedirect(returnUrl);
            }

            // if item check is false
            var newItem = new WishlistItem 
            { 
                IdentityUserId = userId, 
                ProductId = productId 
            };

           //Save changes 
            _context.WishlistItems.Add(newItem);
            await _context.SaveChangesAsync();
            TempData[$"IsInWishlist_{productId}"] = true; 

            //safety fallback to prevent redirect vulnerability 
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Index","Home");    

        }





    }
}