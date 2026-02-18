using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cart_King.Models;
using Cart_King.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cart_King.Connected_Services;

namespace Cart_King.Controllers;

public class HomeController : Controller
{
      private readonly CartKingDbContext _context;

        public HomeController(CartKingDbContext context)
        {
            _context = context;
        }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
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
        return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
