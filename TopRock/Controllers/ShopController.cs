using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        /*POST: /AddToCart*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int Quantity, int ProductId)
        {
            // identity product price
            var product = _context.Product.SingleOrDefault(p => p.ProductId == ProductId);
            var price = product.Price;

            // determine the username;
            var cartUsername = GetCartUsername();

            // check if this user has this product is already in cart. If so, update quantity
            var cartItem = _context.Cart.SingleOrDefault(c => c.ProductId == ProductId && c.Username == cartUsername);

            if (cartItem == null)
            {
                // if product not already in cart, create and save a new Cart object
                var cart = new Cart
                {
                    ProductId = ProductId,
                    Quantity = Quantity,
                    Price = price,
                    Username = cartUsername
                };

                _context.Cart.Add(cart);
            }
            else
            {
                cartItem.Quantity += Quantity; // add the new quantity to the existing quantity
                _context.Update(cartItem);
            }

            _context.SaveChanges();

            //show the cart page
            return RedirectToAction("Cart");
        }

        // check / set Cart username
        private string GetCartUsername()
        {
            // 1. check if we already are storing the username in the user's session
            if (HttpContext.Session.GetString("CartUsername") == null)
            {
                // initialize and empty string variable that we'll later add to the session object
                var cartUsername = "";

                // 2. if no username in session, is user logged in?
                // if yes, use their email for the session variable
                // if no, use the GUID class to make a new ID and store that in the session
                if (User.Identity.IsAuthenticated)
                {
                    cartUsername = User.Identity.Name;
                }
                else
                {
                    cartUsername = Guid.NewGuid().ToString();
                }

                // now store the cartUSername in a session variable
                HttpContext.Session.SetString("CartUsername", cartUsername);

            }

            // send back the username
            return HttpContext.Session.GetString("CartUsername");

        }

    }
}