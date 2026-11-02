using Cars.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private List<Order> orders = new List<Order>
{
    new Order
    {
        Id = 1,
        ProductName = "Laptop",
        Quantity = 2,
        TotalPrice = 5000m,
        OrderDate = new DateTime(2026, 1, 10)
    },
    new Order
    {
        Id = 2,
        ProductName = "Mouse",
        Quantity = 5,
        TotalPrice = 250m,
        OrderDate = new DateTime(2026, 2, 15)
    },
    new Order
    {
        Id = 3,
        ProductName = "Keyboard",
        Quantity = 3,
        TotalPrice = 450m,
        OrderDate = new DateTime(2026, 3, 20)
    },
    new Order
    {
        Id = 4,
        ProductName = "Monitor",
        Quantity = 1,
        TotalPrice = 1200m,
        OrderDate = new DateTime(2026, 4, 5)
    },
    new Order
    {
        Id = 5,
        ProductName = "Headphones",
        Quantity = 4,
        TotalPrice = 800m,
        OrderDate = new DateTime(2026, 5, 12)
    }
};


        [HttpGet]
        public ActionResult<Order> GetOrderBtId(int id)
        {
            return Ok(orders.FirstOrDefault(orders => orders.Id == id));
        }
    }
}
