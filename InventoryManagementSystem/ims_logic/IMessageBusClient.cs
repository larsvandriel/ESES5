using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Logic
{
    public interface IInventoryEventSender
    {
        void SendUpdateStockEvent(Product product);
        void SendOrderApprovedEvent(Guid orderId);
        void SendOrderDeniedEvent(Guid orderId);
    }
}
