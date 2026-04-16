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

namespace Cart_King.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CartKingDbContext _context;

        public AccountController(SignInManager<IdentityUser> signInManager, CartKingDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // The ViewModel to be used by other actions
        private async Task<DeliveryAddressViewModel?> BuildDeliveryViewModel()
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
                return null;

            // Look for an existing profile in the database
            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.IdentityUserId == userId);


            // Builds the ViewModel 
            return new DeliveryAddressViewModel
            {
                IdentityUserId = userId, 
                FirstName = profile?.FirstName,
                LastName = profile?.LastName,
                AddressLine1 = profile?.AddressLine1,
                City = profile?.City,
                Postcode = profile?.Postcode
            };
        }


        // Opens the profile Index view
        public async Task<IActionResult> OpenDashboard(string? activeTab = null)
        {
            ViewBag.ActiveTab = activeTab;

            var userId = _userManager.GetUserId(User);

            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.IdentityUserId == userId);

            if (userId == null)
                return Unauthorized();

            var model = await BuildDeliveryViewModel();

            if (model == null)
                return Unauthorized();


            return View("Index", model);
        }


        // Get delivery address 
        public async Task<IActionResult> GetDeliveryAddress()
        {
            var model = await BuildDeliveryViewModel();

            if (model == null)
                return Unauthorized();

            return PartialView("_GetDeliveryAddressPartial", model);
        }

        // Receives the data from the user and saves it
        [HttpPost]
        public async Task<IActionResult> SaveDeliveryAddress(DeliveryAddressViewModel model)
        {

            if (!ModelState.IsValid) return RedirectToAction("OpenDashboard");

            // Check if they already have a profile row
            var profile = _context.UserProfiles.FirstOrDefault(p => p.IdentityUserId == model.IdentityUserId);

            if (profile == null)
            {
                // If NO profile exists, create a brand new one
                profile = new UserProfile { IdentityUserId = model.IdentityUserId };
                _context.UserProfiles.Add(profile);
            }

            // Copy the data from the "Envelope" (ViewModel) into the "Filing Cabinet" (Model)
            profile.FirstName = model.FirstName;
            profile.LastName = model.LastName;
            profile.AddressLine1 = model.AddressLine1;
            profile.City = model.City;
            profile.Postcode = model.Postcode;

            // if data present in rows, display success message
            int dataInRows = await _context.SaveChangesAsync();

            if (dataInRows > 0)
            {
                TempData["SuccessMessage"] = "Saved Successfully!";
            }
            else TempData["ErrorMessage"] = "Oops, something went";

            return RedirectToAction("OpenDashboard", new { activeTab = "delivery" });
        }


        //Get contact details action

        //Get card info

        //Get wishlist?


        // Signout Button
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}