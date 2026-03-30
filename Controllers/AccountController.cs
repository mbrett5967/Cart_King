using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace Cart_King.Controllers
{
    
    public class AccountController : Controller
    {
       
        public IActionResult OpenDashboard()
        {
            return View("Index");
        }
    }
       
}