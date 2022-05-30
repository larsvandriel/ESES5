namespace OrderManagementSystem.Logic
{
    public interface IOrderManager
    {
        Order CreateOrder(Order order);
        void AcceptOrder(Guid orderId);
        void DeclineOrder(Guid orderId);
    }
}