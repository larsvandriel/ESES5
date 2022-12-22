using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrderManager OrderManager { get; set; }

        public OrderController(IOrderManager orderManager)
        {
            OrderManager = orderManager;
        }

        // POST api/<OrderController>
        [HttpPost]
        public Order Post([FromBody] Order order)
        {
            return OrderManager.CreateOrder(order);
        }

        [HttpGet]
        public List<Order> GetOrdersByUser(Guid userId)
        {
            return OrderManager.GetOrdersFromUser(userId);
        }
    }
}
