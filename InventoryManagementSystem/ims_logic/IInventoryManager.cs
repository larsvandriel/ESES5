using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Logic
{
    public interface IInventoryManager
    {
        void DecreaseStorage(Guid productId, int amountToDecrease);
        void DecreaseStorage(Guid orderId, Guid productId, int amountToDecrease);
        void IncreaseStorage(Guid productId, int amountToIncrease);
        void AddProduct(Product product);
        void RemoveProduct(Product product);
        void UpdateProduct(Product product);
    }
}
