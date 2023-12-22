using AutoRest.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRest.Context.Configuration
{
    public class LoyaltyCardEntityTypeConfiguration : IEntityTypeConfiguration<LoyaltyCard>
    {
        public void Configure(EntityTypeBuilder<LoyaltyCard> builder)
        {
            builder.ToTable("LoyaltyCards");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Number).IsRequired();

            builder
               .HasMany(x => x.OrderItem)
               .WithOne(x => x.LoyaltyCard)
               .HasForeignKey(x => x.LoyaltyCardId);

            builder.HasIndex(x => x.LoyaltyCardType)
                .HasDatabaseName($"IX_{nameof(LoyaltyCard)}_{nameof(LoyaltyCard.LoyaltyCardType)}");
        }
    }
}
