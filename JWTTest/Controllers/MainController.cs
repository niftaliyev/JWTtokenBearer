using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTTest.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(int id = 500)
        {
            ViewBag.ErrorId = id;
            return View();
        }
    }
}
