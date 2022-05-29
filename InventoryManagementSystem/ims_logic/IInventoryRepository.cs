using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Logic
{
    public interface IInventoryRepository
    {
        Product GetProductById(Guid productId);

        void UpdateProduct(Product product);

        void DeleteProduct(Product product);

        void CreateProduct(Product product);
    }
}
