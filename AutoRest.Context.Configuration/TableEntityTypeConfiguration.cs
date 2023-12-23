using AutoRest.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRest.Context.Configuration
{
    public class TableEntityTypeConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("Tables");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Number).IsRequired().HasMaxLength(20);

            builder
               .HasMany(x => x.OrderItem)
               .WithOne(x => x.Table)
               .HasForeignKey(x => x.TableId);

            builder.HasIndex(x => x.Number)
                .IsUnique()
                .HasFilter($"{nameof(Table.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Table)}_{nameof(Table.Number)}");
        }
    }
}