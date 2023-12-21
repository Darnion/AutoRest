﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AutoRest.Context.Contracts.Models;

namespace AutoRest.Context.Configuration
{
    public class MenuItemEntityTypeConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("MenuItems");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Title).IsRequired();

            builder
               .HasMany(x => x.OrderItem)
               .WithOne(x => x.MenuItem)
               .HasForeignKey(x => x.MenuItemId);

            builder.HasIndex(x => x.Title)
                .HasDatabaseName($"IX_{nameof(MenuItem)}_{nameof(MenuItem.Title)}");
        }
    }
}