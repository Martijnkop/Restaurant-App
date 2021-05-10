using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Controllers
{
    public class KitchenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
