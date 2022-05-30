using InventoryManagementSystem.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DataAccessLayer
{
    public class InventoryRepository : IInventoryRepository
    {
        public RepositoryContext RepositoryContext { get; set; }

        public InventoryRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public void CreateProduct(Product product)
        {
            RepositoryContext.Set<Product>().Add(product);
            RepositoryContext.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            RepositoryContext.Set<Product>().Remove(product);
            RepositoryContext.SaveChanges();
        }

        public Product GetProductById(Guid productId)
        {
            return RepositoryContext.Set<Product>().First(p => p.Id.Equals(productId));
        }

        public void UpdateProduct(Product product)
        {
            RepositoryContext.Set<Product>().Update(product);
            RepositoryContext.SaveChanges();
        }
    }
}
