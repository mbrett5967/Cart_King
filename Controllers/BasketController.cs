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

            return View ("Index",viewModels);

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

            return RedirectToAction("Index","Home");    


         

        }



        
    }
        



    

}