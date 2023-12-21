using AutoRest.Context.Configuration;
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
            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .HasMany(x => x.AutoRestItem)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId);

            builder.HasIndex(x => x.Name)
                .IsUnique()
                .HasFilter($"{nameof(Person.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Person)}_{nameof(Person.Name)}");
        }
    }
}
