using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cart_King.Connected_Services;
using Microsoft.Extensions.Logging;

namespace Cart_King.Controllers
{
    
    public class DealsController : Controller
    {
        private readonly CartKingDbContext _context;

        public DealsController(CartKingDbContext context)
        {
            _context = context;
        }
      
        public IActionResult Index()
        {
            return View();
        }

        // View deal details of product
     public async Task<IActionResult> DealsService(int id)
     { 
        var product = await _context.SalesDeals
            .Include(v => v.IsActive)
            .FirstOrDefaultAsync(m => m.ProductId == id);

        if (product == null) return NotFound();
        return View("SalesDealDetails");
     }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}