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

        /*GET: /browse/catName*/
        public IActionResult Browse(string category)
        {
            // store the selected category name in the ViewBag so we can display in the view heading
            ViewBag.Category = category;

            // get the list of products for the selected category and pass the list to the view
            var products = _context.Product.Where(p => p.Category.Name == category).OrderBy(p => p.Name).ToList();
            return View(products);
        }

        /*GET: /ProdcutDetails/prodName*/
        public IActionResult ProductDetails(string product)
        {
            // use SingleOrDefault to find either 1 exact match or a null object
            var selectedProduct = _context.Product.SingleOrDefault(p => p.Name == product);
            return View(selectedProduct);
        }

    }
}