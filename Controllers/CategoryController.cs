using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cart_King.Connected_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cart_King.Controllers
{
    
    public class CategoryController : Controller

    // Reads from db
    {
     private readonly CartKingDbContext _db;

     // preserving lifetime in a private variable 
     public CategoryController (CartKingDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}