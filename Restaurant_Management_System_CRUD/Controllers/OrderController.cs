using Microsoft.AspNetCore.Mvc;
using Restaurant_Management_System_CRUD.Context;
using Restaurant_Management_System_CRUD.Models;

namespace Restaurant_Management_System_CRUD.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            var data = _context.Menu.ToList();
            return View(data);
        }

        public ActionResult PlaceOrder(string menuId, string Quantity)
        {
            var order = new Order()
            {
                MenuId = Convert.ToInt32(menuId),
                Quantity = Convert.ToInt32(Quantity),
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            return View();
        }
    }
}
