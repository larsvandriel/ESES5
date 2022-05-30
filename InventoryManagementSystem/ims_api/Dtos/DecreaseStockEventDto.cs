namespace InventoryManagementSystem.API.Dtos
{
    public class DecreaseStockEventDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
    }
}
