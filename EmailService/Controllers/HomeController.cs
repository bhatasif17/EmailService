/* ------------------------------------------------------------------
 * The GNU General Public License v3.0
 * Copyright (C) 2007 Free Software Foundation, Inc. <https://fsf.org/>
 * Asif Bhat
   ------------------------------------------------------------------ */
using EmailService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmailService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
