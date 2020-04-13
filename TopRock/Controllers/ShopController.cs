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


        /*Get: /shop*/
        public IActionResult Index()
        {
            // return list of categories for the user to browse
            var categories = _context.Category.OrderBy(c => c.Name).ToList();
            return View(categories);
        }



    }
}