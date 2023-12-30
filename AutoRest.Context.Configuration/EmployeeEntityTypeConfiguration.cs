using AutoRest.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
               .HasMany(x => x.OrderWaiter)
               .WithOne(x => x.EmployeeWaiter)
               .HasForeignKey(x => x.EmployeeWaiterId);

            builder
               .HasMany(x => x.OrderCashier)
               .WithOne(x => x.EmployeeCashier)
               .HasForeignKey(x => x.EmployeeCashierId);

            builder.HasIndex(x => x.EmployeeType)
                .HasDatabaseName($"IX_{nameof(Employee)}_{nameof(Employee.EmployeeType)}");

            builder.HasIndex(x => x.PersonId)
                .IsUnique()
                .HasFilter($"{nameof(Employee.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Employee)}_{nameof(Employee.PersonId)}");
        }
    }
}
