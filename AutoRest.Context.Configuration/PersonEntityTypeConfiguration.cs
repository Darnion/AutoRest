using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AutoRest.Context.Contracts.Models;

namespace AutoRest.Context.Configuration
{
    public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(80);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(80);
            builder.Property(x => x.Patronymic).HasMaxLength(80);

            builder
               .HasMany(x => x.Employee)
               .WithOne(x => x.Person)
               .HasForeignKey(x => x.PersonId);

            builder.HasIndex(x => x.LastName)
                .HasDatabaseName($"IX_{nameof(Person)}_{nameof(Person.LastName)}");
        }
    }
}
