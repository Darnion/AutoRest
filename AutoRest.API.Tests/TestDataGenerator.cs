using AutoRest.Context.Contracts.Enums;
using AutoRest.Context.Contracts.Models;
using AutoRest.Services.Contracts.Models;
using AutoRest.Services.Contracts.Models.Enums;

namespace AutoRest.Api.Tests
{
    public static class TestDataGenerator
    {
        public static void SetBaseParams<TEntity>(this TEntity entity) where TEntity : BaseAuditEntity
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.CreatedBy = $"CreatedBy{Guid.NewGuid():N}";
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}";
        }

        static internal Employee Employee(Action<Employee>? action = null)
        {
            var item = new Employee
            {
                EmployeeType = EmployeeTypes.Waiter,
            };

            item.SetBaseParams();

            action?.Invoke(item);
            return item;
        }

        static internal EmployeeModel EmployeeModel(Action<EmployeeModel>? action = null)
        {
            var item = new EmployeeModel
            {
                Id = Guid.NewGuid(),
                EmployeeType = EmployeeTypesModel.Waiter,
            };

            action?.Invoke(item);
            return item;
        }

        static internal Person Person(Action<Person>? action = null)
        {
            var item = new Person
            {
                LastName = $"LastName{Guid.NewGuid():N}",
                FirstName = $"FirstName{Guid.NewGuid():N}",
            };

            item.SetBaseParams();

            action?.Invoke(item);
            return item;
        }

        static internal PersonModel PersonModel(Action<PersonModel>? action = null)
        {
            var item = new PersonModel
            {
                Id = Guid.NewGuid(),
                LastName = $"LastName{Guid.NewGuid():N}",
                FirstName = $"FirstName{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal MenuItem MenuItem(Action<MenuItem>? action = null)
        {
            var item = new MenuItem
            {
                Title = $"Title{Guid.NewGuid():N}",
                Cost = (short)Random.Shared.Next(0, 100),
            };

            item.SetBaseParams();

            action?.Invoke(item);
            return item;
        }

        static internal MenuItemModel MenuItemModel(Action<MenuItemModel>? action = null)
        {
            var item = new MenuItemModel
            {
                Id = Guid.NewGuid(),
                Title = $"Title{Guid.NewGuid():N}",
                Cost = (short)Random.Shared.Next(0, 100),
            };

            action?.Invoke(item);
            return item;
        }

        static internal Table Table(Action<Table>? action = null)
        {
            var item = new Table
            {
                Number = Random.Shared.Next(0, 100).ToString(),
            };

            item.SetBaseParams();

            action?.Invoke(item);
            return item;
        }

        static internal TableModel TableModel(Action<TableModel>? action = null)
        {
            var item = new TableModel
            {
                Id = Guid.NewGuid(),
                Number = Random.Shared.Next(0, 100).ToString(),
            };

            action?.Invoke(item);
            return item;
        }

        static internal LoyaltyCard LoyaltyCard(Action<LoyaltyCard>? action = null)
        {
            var item = new LoyaltyCard
            {
                Number = $"Number{Guid.NewGuid():N}",
            };

            item.SetBaseParams();

            action?.Invoke(item);
            return item;
        }

        static internal LoyaltyCardModel LoyaltyCardModel(Action<LoyaltyCardModel>? action = null)
        {
            var item = new LoyaltyCardModel
            {
                Id = Guid.NewGuid(),
                Number = $"Number{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal OrderItem OrderItem(Action<OrderItem>? action = null)
        {
            var item = new OrderItem
            {
                LoyaltyCardId = null,
                OrderStatus = false,
                EmployeeCashierId = null,
            };

            item.SetBaseParams();

            action?.Invoke(item);
            return item;
        }

        static internal OrderItemModel OrderItemModel(Action<OrderItemModel>? action = null)
        {
            var item = new OrderItemModel
            {
                Id = Guid.NewGuid(),
                LoyaltyCard = null,
                OrderStatus = false,
                EmployeeCashier = null,
            };

            action?.Invoke(item);
            return item;
        }
    }
}
