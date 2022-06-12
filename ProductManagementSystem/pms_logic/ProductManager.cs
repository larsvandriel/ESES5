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
        public IMessageBusClient MessageBusClient { get; set; }

        public ProductManager(IProductRepository repository, IMessageBusClient messageBusClient)
        {
            Repository = repository;
            MessageBusClient = messageBusClient;
        }

        public Product CreateProduct(Product product)
        {
            product = Repository.SaveProduct(product);
            MessageBusClient.SendProductCreatedEvent(product);
            return product;
        }

        public void DeleteProduct(Product product)
        {
            Repository.DeleteProduct(product);
            MessageBusClient.SendProductDeletedEvent(product);
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
            MessageBusClient.SendProductUpdatedEvent(product);
        }

        public void UpdateStock(Guid productId, int newStockAmount)
        {
            Product product = GetProductById(productId);
            product.AmountInStorage = newStockAmount;
            Repository.UpdateProduct(product);
        }
    }
}
