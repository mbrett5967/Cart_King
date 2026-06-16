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
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CartKingDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(SignInManager<IdentityUser> signInManager, CartKingDbContext context, UserManager<IdentityUser> userManager,ILogger<AccountController> logger)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger =logger;
        }

        // --- Dashboard Logic ---
        
        public async Task<IActionResult> Index(string? activeTab = "welcome")
        {
            // ID check for security - looks in my db 
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            // Fetch data from both sources
            var user = await _userManager.FindByIdAsync(userId);
            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.IdentityUserId == userId);

            var model = new DashboardViewModel
            {
                ActiveTab = activeTab ?? "delivery",
                DeliveryAddress = new DeliveryAddressViewModel
                {
                    IdentityUserId = userId,
                    FirstName = profile?.FirstName,
                    LastName = profile?.LastName,
                    AddressLine1 = profile?.AddressLine1,
                    City = profile?.City,
                    Postcode = profile?.Postcode
                },
                
                ContactDetails = new ContactDetailsViewModel
                {   
                    IdentityUserId = userId,
                    Email = user?.Email,
                    PhoneNumber = user?.PhoneNumber,
                    MobileNumber = profile?.MobileNumber
                }
            };

            // Populate wishlist for dashboard tab
            var wishlistItems = await _context.WishlistItems
                .Where(w => w.IdentityUserId == userId)
                .Include(w => w.Product)
                .ThenInclude(p => p.Category)
                .ToListAsync();

            model.Wishlist = wishlistItems.Select(w => new WishlistViewModel
            {
                Name = w.Product.Name,
                Price = w.Product.Price,
                ProductId = w.Product.ProductId,
                CategoryName = w.Product.Category?.Name ?? "None",
                ImageUrl = w.Product.ImageUrl,
                ShortDescription = w.Product.ShortDescription,
                StockQuantity = w.Product.StockQuantity
            }).ToList();

            return View(model);
        }

        // --- Delivery Address Actions ---
        
        [HttpPost]
        public async Task<IActionResult> SaveDeliveryAddress(DeliveryAddressViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", new { activeTab = "delivery" });

            // ID check from DB - best for security 
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            // Looks for profile data in db
            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.IdentityUserId == model.IdentityUserId);

            if (profile == null)
            {
                profile = new UserProfile { IdentityUserId = model.IdentityUserId };
                _context.UserProfiles.Add(profile);
            }

            profile.FirstName = model.FirstName;
            profile.LastName = model.LastName;
            profile.AddressLine1 = model.AddressLine1;
            profile.City = model.City;
            profile.Postcode = model.Postcode;

           // save button display message 
           int DeliveryDataInRows = await _context.SaveChangesAsync();

           if (DeliveryDataInRows > 0)
            {
                TempData["DeliverySuccess"] = "Delivery address saved!";
            }
          else TempData["DeliveryError"] = "Oops, something went"; 
            
            return RedirectToAction("Index", new { activeTab = "delivery" });
        }

        // --- Contact Details Actions ---
        
        [HttpPost]
        public async Task<IActionResult> SaveContactDetails(ContactDetailsViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", new { activeTab = "contact" });
            

            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            // Update Identity table Data in db
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.Email = model.Email;
                user.UserName = model.Email; // Keep sync
                user.PhoneNumber = model.PhoneNumber;
                
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    // Refreshes sign in cookie so the user isn't logged out
                    await _signInManager.RefreshSignInAsync(user);
                }
            }

            // Update Profile Data table in Db 
            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.IdentityUserId == userId);
            if (profile == null)
            {
                profile = new UserProfile { IdentityUserId = userId };
                _context.UserProfiles.Add(profile);
            }

            profile.MobileNumber = model.MobileNumber;
            
            try 
            {
                await _context.SaveChangesAsync();


             TempData["ContactSuccess"] = "Contact details updated!"; 
            
               return RedirectToAction("Index", new { activeTab = "contact" });
            }
            
           catch (DbUpdateException error) 
           {    
                _logger.LogError (error, "Database failed to save contact details");
                TempData["ContactError"] = "Error, please try again"; 

                return View(model);
           }            
            
            
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View("_ForgotPasswordPartial");
        }


        // --- Authentication ---

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}