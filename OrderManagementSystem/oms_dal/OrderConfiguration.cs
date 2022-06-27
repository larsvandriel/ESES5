using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.DataAccessLayer
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.ProductId).IsRequired();
            builder.Property(o => o.UserId).IsRequired();
            builder.Property(o => o.Status).IsRequired();
            builder.Property(o => o.TimeOrderCreated).IsRequired();
            builder.Property(o => o.TimeOrderHandled);
        }
    }
}
