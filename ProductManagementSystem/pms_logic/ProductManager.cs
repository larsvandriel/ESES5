using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Logic
{
    public class ProductManager : IProductManager
    {
        public IProductRepository Repository { get; set; }
        public IProductEventSender EventSender { get; set; }

        public ProductManager(IProductRepository repository, IProductEventSender eventSender)
        {
            Repository = repository;
            EventSender = eventSender;
        }

        public Product CreateProduct(Product product)
        {
            product = Repository.SaveProduct(product);
            EventSender.SendProductCreatedEvent(product);
            return product;
        }

        public void DeleteProduct(Product product)
        {
            Repository.DeleteProduct(product);
            EventSender.SendProductDeletedEvent(product);
        }

        public List<Product> GetAll()
        {
            return Repository.GetAll();
        }

        public Product GetProductById(Guid id)
        {
            return Repository.GetProduct(id);
        }

        public void UpdateProduct(Product product)
        {
            Repository.UpdateProduct(product);
            EventSender.SendProductUpdatedEvent(product);
        }

        public void UpdateStock(Guid productId, int newStockAmount)
        {
            Product product = GetProductById(productId);
            product.AmountInStorage = newStockAmount;
            Repository.UpdateProduct(product);
        }
    }
}
