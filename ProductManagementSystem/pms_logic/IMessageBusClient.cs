namespace ProductManagementSystem.Logic
{
    public interface IMessageBusClient
    {
        void SendProductCreatedEvent(Product product);
        void SendProductDeletedEvent(Product product);
        void SendProductUpdatedEvent(Product product);
    }
}