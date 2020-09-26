using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesBoard.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace SalesBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _session;
        public HomeController(IHttpContextAccessor session) 
        {
            _session = session;
        }
        public IActionResult Index()
        {
            //MS: If Admin -> All Items , 
            if (_session.HttpContext.Session.IsAvailable && _session.HttpContext.Session.Keys.Contains("isAdmin")) 
            {
                if (_session.HttpContext.Session.GetString("isAdmin").ToUpper() == "ADMIN")
                {
                    return RedirectToAction("Index", "Items");
                }
                else
                {
                    return RedirectToAction("MyItems", "Items");
                }
            }
            else
            {
                return RedirectToAction("MyItems", "Items");
                //return View();
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact Us";

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
