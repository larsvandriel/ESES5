using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Logic
{
    public interface IOrderRepository
    {
        Order CreateOrder(Order order);
        void DeleteOrder(Order order);
        void UpdateOrder(Order order);
        Order GetOrder(Guid id);
    }
}
