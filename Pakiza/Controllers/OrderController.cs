using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pakiza.Models;

namespace Pakiza.Controllers
{
    //public class OrderController : Controller
    //{
    //    private readonly AppDbContext _context;

    //    public OrderController(AppDbContext context)
    //    {
    //        _context = context;
    //    }
    //    private static List<Customer> customers = new List<Customer>
    //{
    //    new Customer { Id = 1, CustomerName = "Mr. Rahat Ahmed", Phone = "123456789" },
    //    new Customer { Id = 2, CustomerName = "Mr. Aowal Azad", Phone = "123456789" }
    //};

    //    private static List<Order> orders = new List<Order>
    //{
    //    new Order
    //    {
    //        Id = 1,
    //        CustomerId = 1,
    //        Customer = customers[0],
    //        Products = new List<Product>
    //        {
    //            new Product { Id = 1, ProductName = "Book", Quantity = 5, UnitPrice = 100 },
    //            new Product { Id = 2, ProductName = "Pen", Quantity = 10, UnitPrice = 20 },
    //            new Product { Id = 3, ProductName = "Calculator", Quantity = 3, UnitPrice = 1000 }
    //        }
    //    },
    //    new Order
    //    {
    //        Id = 2,
    //        CustomerId = 2,
    //        Customer = customers[1],
    //        Products = new List<Product>
    //        {
    //            new Product { Id = 1, ProductName = "Pen", Quantity = 10, UnitPrice = 20 }
    //        }
    //    }
    //};

    //    public IActionResult Index()
    //    {
    //        return View(orders);
    //    }

    //    public IActionResult Create()
    //    {
    //        ViewBag.Customers = new SelectList(_context.Customers, "Id", "CustomerName");
    //        return View(new Order { Products = new List<Product>() });
    //    }

    //    [HttpPost]
    //    public IActionResult Create(Order order)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            order.Customer = _context.Customers.Find(order.CustomerId);
    //            _context.Orders.Add(order);
    //            _context.SaveChanges();
    //            return RedirectToAction("Index");
    //        }

    //        ViewBag.Customers = new SelectList(_context.Customers, "Id", "CustomerName");
    //        return View(order);
    //    }

    //    public IActionResult Edit(int id)
    //    {
    //        var order = orders.FirstOrDefault(o => o.Id == id);
    //        if (order == null) return NotFound();

    //        ViewBag.Customers = new SelectList(customers, "Id", "CustomerName", order.CustomerId);
    //        return View(order);
    //    }

    //    [HttpPost]
    //    public IActionResult Edit(Order order)
    //    {
    //        var existingOrder = orders.FirstOrDefault(o => o.Id == order.Id);
    //        if (existingOrder == null) return NotFound();

    //        existingOrder.CustomerId = order.CustomerId;
    //        existingOrder.Customer = customers.First(c => c.Id == order.CustomerId);
    //        existingOrder.Products = order.Products;

    //        return RedirectToAction("Index");
    //    }

    //    public IActionResult Delete(int id)
    //    {
    //        var order = orders.FirstOrDefault(o => o.Id == id);
    //        if (order != null)
    //        {
    //            orders.Remove(order);
    //        }
    //        return RedirectToAction("Index");
    //    }
    //}
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders.Include(o => o.Customer).Include(o => o.Products).ToList();
            return View(orders);
        }

        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(_context.Customers, "Id", "CustomerName");
            return View(new Order { Products = new List<Product>() });
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                order.Customer = _context.Customers.Find(order.CustomerId);
                _context.Orders.Add(order);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customers = new SelectList(_context.Customers, "Id", "CustomerName");
            return View(order);
        }

        public IActionResult Edit(int id)
        {
            var order = _context.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            ViewBag.Customers = new SelectList(_context.Customers, "Id", "CustomerName", order.CustomerId);
            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Update(order);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customers = new SelectList(_context.Customers, "Id", "CustomerName", order.CustomerId);
            return View(order);
        }

        public IActionResult Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
