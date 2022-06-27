using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Logic;
using System.Security.Claims;

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
            order.TimeOrderCreated = DateTime.Now;
            order.UserId = Guid.Parse(this.User.FindFirst("id")?.Value);
            return OrderManager.CreateOrder(order);
        }
    }
}
