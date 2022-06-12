using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.DataAccessLayer
{
    public class ProductRepository : IProductRepository
    {
        protected RepositoryContext RepositoryContext { get; set; }

        public ProductRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public void DeleteProduct(Product product)
        {
            RepositoryContext.Set<Product>().Remove(product);
            RepositoryContext.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return RepositoryContext.Set<Product>().AsNoTracking().ToList();
        }

        public Product GetProduct(Guid id)
        {
            return RepositoryContext.Set<Product>().First(p => p.Id.Equals(id));
        }

        public Product SaveProduct(Product product)
        {
            product = RepositoryContext.Set<Product>().Add(product).Entity;
            RepositoryContext.SaveChanges();
            return product;
        }

        public void UpdateProduct(Product product)
        {
            RepositoryContext.Set<Product>().Update(product);
            RepositoryContext.SaveChanges();
        }
    }
}
