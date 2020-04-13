using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TopRock.Models;

namespace TopRock.Controllers
{
    public class ShopController : Controller
    {
        private readonly toprockContext _context;

        public ShopController(toprockContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}