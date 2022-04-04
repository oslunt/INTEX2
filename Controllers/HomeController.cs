//using INTEX2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using INTEX2.Models;


namespace INTEX2.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private ICrashRepository  repo {get;set;}

        public HomeController(ICrashRepository temp)
        {
            repo = temp;
            
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Table()
        {
            var c = repo.Crashes
            .Take(10)
            .ToList();
            return View(c);
        }
    }
}
