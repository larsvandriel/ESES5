using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Logic
{
    public interface IProductEventSender
    {
        void SendProductCreatedEvent(Product product);
        void SendProductDeletedEvent(Product product);
        void SendProductUpdatedEvent(Product product);
    }
}
