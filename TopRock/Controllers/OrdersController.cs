using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TopRock.Models;

namespace TopRock.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly toprockContext _context;

        public OrdersController(toprockContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var username = User.Identity.Name;
            return View(await _context.Order.Where(c => c.UserId == username).ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var username = User.Identity.Name;

            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Where(c=>c.UserId == username)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
