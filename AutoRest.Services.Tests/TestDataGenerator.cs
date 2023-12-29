using AutoRest.Context.Contracts.Enums;
using AutoRest.Context.Contracts.Models;
using AutoRest.Services.Contracts.ModelsRequest;

namespace AutoRest.Services.Tests
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

        static internal MenuItemRequestModel MenuItemRequestModel(Action<MenuItemRequestModel>? settings = null)
        {
            var result = new MenuItemRequestModel
            {
                Id = Guid.NewGuid(),
                Title = $"Title{Guid.NewGuid():N}",
                Cost = (short)Random.Shared.Next(0, 100),
            };

            settings?.Invoke(result);
            return result;
        }


        static internal LoyaltyCard LoyaltyCard(Action<LoyaltyCard>? action = null)
        {
            var item = new LoyaltyCard
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

        static internal LoyaltyCardRequestModel LoyaltyCardRequestModel(Action<LoyaltyCardRequestModel>? action = null)
        {
            var item = new LoyaltyCardRequestModel
            {
                Id = Guid.NewGuid(),
                Number = $"Number{Guid.NewGuid():N}",
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
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal PersonRequestModel PersonRequestModel(Action<PersonRequestModel>? action = null)
        {
            var item = new PersonRequestModel
            {
                Id = Guid.NewGuid(),
                LastName = $"LastName{Guid.NewGuid():N}",
                FirstName = $"FirstName{Guid.NewGuid():N}",
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

        static internal EmployeeRequestModel EmployeeRequestModel(Action<EmployeeRequestModel>? action = null)
        {
            var item = new EmployeeRequestModel
            {
                Id = Guid.NewGuid(),
                EmployeeType = EmployeeTypes.Waiter,
            };

            action?.Invoke(item);
            return item;
        }

        static internal Table Table(Action<Table>? action = null)
        {
            var item = new Table
            {
                Id = Guid.NewGuid(),
                Number = Random.Shared.Next(0, 100).ToString(),
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal TableRequestModel TableRequestModel(Action<TableRequestModel>? action = null)
        {
            var item = new TableRequestModel
            {
                Id = Guid.NewGuid(),
                Number = Random.Shared.Next(0, 100).ToString(),
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

        static internal OrderItemRequestModel OrderItemRequestModel(Action<OrderItemRequestModel>? action = null)
        {
            var item = new OrderItemRequestModel
            {
                Id = Guid.NewGuid(),
                OrderStatus = false,
            };

            action?.Invoke(item);
            return item;
        }
    }
}
