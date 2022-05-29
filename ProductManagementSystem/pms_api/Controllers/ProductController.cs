using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IProductManager ProductManager { get; set; }

        public ProductController(IProductManager productManager)
        {
            ProductManager = productManager;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public List<Product> Get()
        {
            return ProductManager.GetAll();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public Product Get(Guid id)
        {
            return ProductManager.GetProductById(id);
        }

        // POST api/<ProductController>
        [HttpPost]
        public Product Post([FromBody] Product product)
        {
            return ProductManager.CreateProduct(product);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] Product product)
        {
            ProductManager.UpdateProduct(product);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            ProductManager.DeleteProduct(ProductManager.GetProductById(id));
        }
    }
}
