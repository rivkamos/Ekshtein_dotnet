using Cars.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "Dana",
                Order = new Order
                {
                    Id = 101,
                    ProductName = "Toyota Corolla",
                    Quantity = 1,
                    TotalPrice = 120000m,
                    OrderDate = new DateTime(2026, 10, 1)
                }
            },
            new User
            {
                Id = 2,
                Name = "Yossi",
                Order = new Order
                {
                    Id = 102,
                    ProductName = "Mazda 3",
                    Quantity = 2,
                    TotalPrice = 230000m,
                    OrderDate = new DateTime(2026, 10, 15)
                }
            },
            new User
            {
                Id = 3,
                Name = "Noa",
                Order = new Order
                {
                    Id = 103,
                    ProductName = "Hyundai i20",
                    Quantity = 1,
                    TotalPrice = 95000m,
                    OrderDate = new DateTime(2026, 10, 28)
                }
            }
        };
        private static int _nextId = 4;

        // GET: api/User
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_users);
        }

        // GET: api/User/5
        [HttpGet("{id}/order/{orderId}")]
        public ActionResult<User> Get(int id, int orderId)
        {
            var user = _users.FirstOrDefault(u => u.Id == id && u.Order.Id == orderId);
            
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // GET: api/User/search?name=Dana
        // GET: api/User/search?product=mazda&minPrice=100000&maxPrice=300000
        // GET: api/User/search?name=o&page=1&pageSize=2
        // פרמטרים אחרי סימן ה-? הם ה-Query String. כולם אופציונליים (nullable / ערך ברירת מחדל)
        [HttpGet("search")]
        public ActionResult<List<User>> Search(
            [FromQuery] string? name,
            [FromQuery] string? product,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            IEnumerable<User> result = _users;

            if (!string.IsNullOrWhiteSpace(name))
                result = result.Where(u => u.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(product))
                result = result.Where(u => u.Order.ProductName.Contains(product, StringComparison.OrdinalIgnoreCase));

            if (minPrice.HasValue)
                result = result.Where(u => u.Order.TotalPrice >= minPrice.Value);

            if (maxPrice.HasValue)
                result = result.Where(u => u.Order.TotalPrice <= maxPrice.Value);

            if (page < 1 || pageSize < 1)
                return BadRequest("page and pageSize must be greater than 0");

            result = result.Skip((page - 1) * pageSize).Take(pageSize);

            return Ok(result);
        }

        // POST: api/User
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user, [FromRoute] int id)
        {
            user.Id = _nextId++;
            _users.Add(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User updatedUser)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            user.Name = updatedUser.Name;
            return Ok(user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            _users.Remove(user);
            return NoContent();
        }

       
        [HttpGet("Sub")]
        public ActionResult<double> Sub(int a, int b) {

            if(b == 0)
            {
                return BadRequest();
            }

            return a / b;
        }
    }
}