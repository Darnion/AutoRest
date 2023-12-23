using AutoRest.Context.Contracts.Enums;
using AutoRest.Context.Contracts.Models;

namespace AutoRest.Repositories.Tests
{
    static internal class TestDataGenerator
    {
        static internal MenuItem MenuItem(Action<MenuItem>? settings = null)
        {
            var result = new MenuItem
            {
                Id = Guid.NewGuid(),
                Title = $"Title{Guid.NewGuid():N}",
                Cost = (short)Random.Shared.Next(0, 100),
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            settings?.Invoke(result);
            return result;
        }


        static internal LoyaltyCard LoyaltyCard(Action<LoyaltyCard>? action = null)
        {
            var item = new LoyaltyCard
            {
                Id = Guid.NewGuid(),
                LoyaltyCardType = LoyaltyCardTypes.StuffCard,
                Number = $"Number{Guid.NewGuid():N}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }
        static internal Person Person(Action<Person>? action = null)
        {
            var item = new Person
            {
                Id = Guid.NewGuid(),
                LastName = $"LastName{Guid.NewGuid():N}",
                FirstName = $"FirstName{Guid.NewGuid():N}",
                Patronymic = $"Patronymic{Guid.NewGuid():N}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal Employee Employee(Action<Employee>? action = null)
        {
            var item = new Employee
            {
                Id = Guid.NewGuid(),
                EmployeeType = EmployeeTypes.Waiter,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal Table Table(Action<Table>? action = null)
        {
            var item = new Table
            {
                Id = Guid.NewGuid(),
                Number = $"Number{Guid.NewGuid():N}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal OrderItem OrderItem(Action<OrderItem>? action = null)
        {
            var item = new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderStatus = false,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }
    }
}
