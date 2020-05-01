using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TopRock.Controllers;
using TopRock.Models;

namespace TopRockTest
{
    [TestClass]
    public class ProductsControllerTest
    {
        ProductsController productsController;

        List<Product> products;

        // mock db object
        private toprockContext _context;
        

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<toprockContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new toprockContext(options);
            products = new List<Product>();

            // 2. create mock data and add to in-memory database
            Category mockCategory = new Category
            {
                CategoryId = 100,
                Name = "A Fake Category"
            };

            products.Add(new Product
            {
                ProductId = 1,
                Name = "Product A",
                Price = 10,
                Category = mockCategory
            });

            products.Add(new Product
            {
                ProductId = 2,
                Name = "Product B",
                Price = 5,
                Category = mockCategory
            });

            products.Add(new Product
            {
                ProductId = 3,
                Name = "Product C",
                Price = 15,
                Category = mockCategory
            });

            foreach (var p in products)
            {
                // add each product to in-memory db
                _context.Product.Add(p);
            }
            _context.SaveChanges();


            productsController = new ProductsController(_context);
        }

        [TestMethod]
        public void IndexReturnCorrectView()
        {
            var result = productsController.Index().Result;
            var viewResult = (ViewResult)result;
            Assert.AreEqual("Index", viewResult.ViewName);
        }


        [TestMethod]
        public void IndexReturnsProducts()
        {
            var result = productsController.Index().Result;
            var viewResult = (ViewResult)result;
            CollectionAssert.AreEqual(products.OrderBy(p => p.Name).ToList(), (List<Product>)viewResult.Model);
        }
    }
}
