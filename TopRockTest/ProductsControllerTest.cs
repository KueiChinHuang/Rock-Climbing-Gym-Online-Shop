using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TopRock.Controllers;
using TopRock.Models;

namespace TopRockTest
{
    public class ProductsControllerTest
    {
        ProductsController productsController;

        // mock db object
        private readonly toprockContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<toprockContext>()
                .UseInMemoryDatabase("SomerandomString")
                .Options;

            var _context = new toprockContext(options);

            productsController = new ProductsController(_context);
        }

    }
}
