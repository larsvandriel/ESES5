using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Logic
{
    public class OrderManager : IOrderManager
    {
        public IOrderRepository Repository { get; set; }
        public IMessageBusClient EventSender { get; set; }

        public OrderManager(IOrderRepository repository, IMessageBusClient eventSender)
        {
            Repository = repository;
            EventSender = eventSender;
        }

        public void AcceptOrder(Guid orderId)
        {
            Order order = Repository.GetOrder(orderId);
            order.Status = OrderStatus.ACCEPTED;
            Repository.UpdateOrder(order);
        }

        public Order CreateOrder(Order order)
        {
            Order newOrder = Repository.CreateOrder(order);
            EventSender.SendDecreaseStockEvent(order.Id, order.ProductId, 1);
            while(newOrder.Status == OrderStatus.PENDING)
            {
                Thread.Sleep(2000);
                newOrder = Repository.GetOrder(newOrder.Id);
                Console.WriteLine(newOrder.Status);
            }
            Console.WriteLine("Order Creation Finished");
            return newOrder;
        }

        public void DeclineOrder(Guid orderId)
        {
            Order order = Repository.GetOrder(orderId);
            order.Status = OrderStatus.REJECTED;
            Repository.UpdateOrder(order);
        }

        public void ForgetUser(Guid userId)
        {
            foreach (Order order in Repository.GetOrdersFromUser(userId))
            {
                order.UserId = Guid.Empty;
                Repository.UpdateOrder(order);
            }
        }

        public List<Order> GetOrdersFromUser(Guid userId)
        {
            return Repository.GetOrdersFromUser(userId);
        }
    }
}
