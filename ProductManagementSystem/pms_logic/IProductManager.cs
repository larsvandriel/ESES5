using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Logic
{
    public interface IProductManager
    {
        Product CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Product GetProductById(Guid id);
        List<Product> GetAll();
        void UpdateStock(Guid productId, int newStockAmount);
    }
}
