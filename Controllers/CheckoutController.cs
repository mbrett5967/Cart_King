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
using Stripe;
using Stripe.Checkout;


namespace Cart_King.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CartKingDbContext _context;
        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(SignInManager<IdentityUser> signInManager, CartKingDbContext context, UserManager<IdentityUser> userManager, ILogger<CheckoutController> logger)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCheckoutSession()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var cartItems = await _context.BasketItems
                .Where(item => item.IdentityUserId == userId)
                .Include(item => item.Product)
                .ToListAsync();

            if (cartItems.Count == 0)
            {
                return RedirectToAction("Index", "Basket");
            }

            // Builds the Stripe list directly from database items
            var lineItems = cartItems.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(item.Product.Price * 100),
                    Currency = "gbp",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Product.Name
                    }
                },
                Quantity = item.Quantity
            }).ToList();

            // Tells Stripe where to send the user when done
            var domain = $"{Request.Scheme}://{Request.Host}";
            var options = new SessionCreateOptions
            {
                LineItems = lineItems,
                Mode = "payment",
                ClientReferenceId = userId,
                SuccessUrl = $"{domain}/Checkout/Success",
                CancelUrl = $"{domain}/Checkout/Cancel"
            };

            var service = new SessionService();
            Session session = service.Create(options);

            // Send the user to Stripe's hosted form
            return Redirect(session.Url);
        }

        [HttpGet]
        public IActionResult Success()
        {
            


            return View();
        }

        [HttpGet]
        public IActionResult Cancel()
        {
            return View();
        }
    }

}