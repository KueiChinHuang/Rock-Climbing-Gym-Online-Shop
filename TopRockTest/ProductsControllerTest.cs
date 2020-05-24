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

        /**
         * Test Index Method
         */

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

        /**
         * Test Details Method
         */

        [TestMethod]
        public void DetailsMissingId()
        {
            var result = productsController.Details(null).Result;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DetailsInvalidId()
        {
            var result = productsController.Details(200).Result;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DetailsValidIdLoadsProducts()
        {
            var result = productsController.Details(1).Result;
            var viewResult = (ViewResult)result;
            Assert.AreEqual(products[0], viewResult.Model);
        }

        /**
         * Test Create Method
         */

        [TestMethod]
        public void CreatePostInvalidData()
        {
            // arrange -> create product object
            var product = new Product
            {
                ProductId = 99,
                Price = 9,
                Category = new Category { CategoryId = 400, Name = "New Cate" }
            };

            productsController.ModelState.AddModelError("Error", "Fake model error");

            // act
            var result = productsController.Create(product);
            var viewResult = (ViewResult)result.Result;

            // assert
            Assert.AreEqual("Create", viewResult.ViewName);
        }


        [TestMethod]
        public void CreatePostInvalidDataPopulatesCategories()
        {
            // arrange -> create product object
            var product = new Product
            {
                ProductId = 99,
                Price = 9,
                Category = new Category { CategoryId = 400, Name = "New Cate" }
            };

            productsController.ModelState.AddModelError("Error", "Fake model error");

            // act
            var result = productsController.Create(product);
            var viewResult = (ViewResult)result.Result;

            // assert
            Assert.IsNotNull(viewResult.ViewData["CategoryId"]);
        }


        [TestMethod]
        public void CreatePostAddsProduct()
        {
            // arrange -> create product object
            var product = new Product
            {
                ProductId = 99,
                Name = "New Product",
                Price = 9,
                Category = new Category { CategoryId = 400, Name = "New Cate" }
            };

            // act
            var result = productsController.Create(product);

            // assert
            Assert.AreEqual(_context.Product.LastOrDefault(), product);
        }


        [TestMethod]
        public void CreatePostRedirectsToIndex()
        {
            // arrange -> create product object
            var product = new Product
            {
                ProductId = 99,
                Name = "New Product",
                Price = 9,
                Category = new Category { CategoryId = 400, Name = "New Cate" }
            };

            // act
            var result = productsController.Create(product);
            var redirectResult = (RedirectToActionResult)result.Result;

            // assert
            Assert.AreEqual("Index", redirectResult.ActionName);
        }
    }
}
