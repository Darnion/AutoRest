using AutoRest.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRest.Context.Configuration
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.EmployeeWaiterId).IsRequired();
            builder.Property(x => x.TableId).IsRequired();
            builder.Property(x => x.MenuItemId).IsRequired();
            builder.Property(x => x.OrderStatus).IsRequired();

            builder.HasIndex(x => x.CreatedAt)
                .HasDatabaseName($"IX_{nameof(OrderItem)}_{nameof(OrderItem.CreatedAt)}");
        }
    }
}