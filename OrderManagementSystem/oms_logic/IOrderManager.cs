namespace OrderManagementSystem.Logic
{
    public interface IOrderManager
    {
        Order CreateOrder(Order order);
        void AcceptOrder(Guid orderId);
        void DeclineOrder(Guid orderId);
        void ForgetUser(Guid userId);
        List<Order> GetOrdersFromUser(Guid userId);
    }
}