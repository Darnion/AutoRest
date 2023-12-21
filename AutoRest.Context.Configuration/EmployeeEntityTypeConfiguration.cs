﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AutoRest.Context.Contracts.Models;

namespace AutoRest.Context.Configuration
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.PersonId).IsRequired();

            builder
               .HasMany(x => x.OrderItem)
               .WithOne(x => x.Employee)
               .HasForeignKey(x => x.EmployeeWaiterId)
               .HasForeignKey(x => x.EmployeeCashierId);

            builder.HasIndex(x => x.EmployeeType)
                .HasDatabaseName($"IX_{nameof(Employee)}_{nameof(Employee.EmployeeType)}");
        }
    }
}