using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Logic
{
    public interface IProductRepository
    {
        Product SaveProduct(Product product);
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);
        Product GetProduct(Guid id);
        List<Product> GetAll();
    }
}
