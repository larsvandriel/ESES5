using OrderManagementSystem.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.DataAccessLayer
{
    public class OrderRepository : IOrderRepository
    {
        public RepositoryContext RepositoryContext { get; set; }

        public OrderRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public Order CreateOrder(Order order)
        {
            order = RepositoryContext.Set<Order>().Add(order).Entity;
            RepositoryContext.SaveChanges();
            return order;
        }

        public void DeleteOrder(Order order)
        {
            RepositoryContext.Set<Order>().Remove(order);
            RepositoryContext.SaveChanges();
        }

        public Order GetOrder(Guid id)
        {
            return RepositoryContext.Set<Order>().First(o => o.Id == id);
        }

        public void UpdateOrder(Order order)
        {
            RepositoryContext.Orders.Update(order);
            RepositoryContext.SaveChanges();
        }
    }
}
