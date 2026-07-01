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
    public class BasketController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CartKingDbContext _context;
        private readonly ILogger<BasketController> _logger;

        public BasketController(SignInManager<IdentityUser> signInManager, CartKingDbContext context, UserManager<IdentityUser> userManager,ILogger<BasketController> logger)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger =logger;
        }

        // --- Dashboard Logic ---
        
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

            return RedirectToAction("Index","Home");    


         

        }



        
    }
        



    

}