using InventoryManagementSystem.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        public IInventoryManager InventoryManager { get; set; }

        public InventoryController(IInventoryManager inventoryManager)
        {
            InventoryManager = inventoryManager;
        }

        [HttpPatch("decrease/{productId}/{amount}")]
        public void DecreaseStorage(Guid productId, int amount)
        {
            InventoryManager.DecreaseStorage(productId, amount);
        }

        [HttpPatch("increase/{productId}/{amount}")]
        public void IncreaseStorage(Guid productId, int amount)
        {
            InventoryManager.IncreaseStorage(productId, amount);
        }
    }
}
