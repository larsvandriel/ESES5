using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Logic
{
    public class InventoryManager : IInventoryManager
    {
        public IInventoryRepository Repository { get; set; }
        public IMessageBusClient EventSender { get; set; }

        public InventoryManager(IInventoryRepository repository, IMessageBusClient eventSender)
        {
            Repository = repository;
            EventSender = eventSender;
        }

        public void AddProduct(Product product)
        {
            Repository.CreateProduct(product);
        }

        public void DecreaseStorage(Guid productId, int amountToDecrease)
        {
            Product product = Repository.GetProductById(productId);
            if(product.AmountInStorage < amountToDecrease)
            {
                throw new InvalidOperationException("Not enough items in stock!");
            }
            product.AmountInStorage -= amountToDecrease;
            Repository.UpdateProduct(product);
        }

        public void DecreaseStorage(Guid orderId, Guid productId, int amountToDecrease)
        {
            Product product = Repository.GetProductById(productId);
            if (product.AmountInStorage < amountToDecrease)
            {
                EventSender.SendOrderDeniedEvent(orderId);
                return;
            }
            product.AmountInStorage -= amountToDecrease;
            Repository.UpdateProduct(product);
            EventSender.SendOrderApprovedEvent(orderId);
        }

        public void IncreaseStorage(Guid productId, int amountToIncrease)
        {
            Product product = Repository.GetProductById(productId);
            product.AmountInStorage += amountToIncrease;
            Repository.UpdateProduct(product);
        }

        public void RemoveProduct(Product product)
        {
            Repository.DeleteProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            Repository.UpdateProduct(product);
        }
    }
}
