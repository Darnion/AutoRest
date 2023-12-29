using AutoMapper;
using AutoRest.Repositories.Implementations;
using AutoRest.Services.Automappers;
using AutoRest.Services.Contracts.Interfaces;
using AutoRest.Services.Implementations;
using FluentAssertions;
using AutoRest.Context.Tests;
using Xunit;
using AutoRest.Context.Contracts.Enums;

namespace AutoRest.Services.Tests.Tests
{
    public class OrderItemServiceTests : AutoRestContextInMemory
    {
        private readonly IOrderItemService orderItemService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="OrderItemServiceTests"/>
        /// </summary>

        public OrderItemServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            orderItemService = new OrderItemService(
                new OrderItemReadRepository(Reader),
                new OrderItemWriteRepository(WriterContext),
                new EmployeeReadRepository(Reader),
                new TableReadRepository(Reader),
                new MenuItemReadRepository(Reader),
                new LoyaltyCardReadRepository(Reader),
                new PersonReadRepository(Reader),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение всех заказов возвращает empty
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            //Arrange

            // Act
            var result = await orderItemService.GetAllAsync(CancellationToken);

            // Assert
            result.Should().BeEmpty();
        }

        /// <summary>
        /// Получение всех заказов возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.OrderItem();
            var deletedTarget = TestDataGenerator.OrderItem();

            var menuItem = TestDataGenerator.MenuItem();
            await Context.MenuItems.AddAsync(menuItem);
            var table = TestDataGenerator.Table();
            await Context.Tables.AddAsync(table);
            var person = TestDataGenerator.Person();
            await Context.Persons.AddAsync(person);
            var employeeWaiter = TestDataGenerator.Employee();
            employeeWaiter.PersonId = person.Id;
            await Context.Employees.AddAsync(employeeWaiter);

            target.MenuItemId = menuItem.Id;
            target.TableId = table.Id;
            target.EmployeeWaiterId = employeeWaiter.Id;

            deletedTarget.MenuItem = menuItem;
            deletedTarget.TableId = table.Id;
            deletedTarget.EmployeeWaiter = employeeWaiter;
            deletedTarget.DeletedAt = DateTimeOffset.UtcNow;

            await Context.OrderItems.AddRangeAsync(target, deletedTarget);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await orderItemService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение заказа по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await orderItemService.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение заказа по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.OrderItem();
            await Context.OrderItems.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await orderItemService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.OrderStatus,
                    target.EmployeeWaiter,
                    target.Table,
                    target.MenuItem,
                });
        }

        // <summary>
        /// Добавление заказа, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.OrderItemRequestModel();
            var menuItem = TestDataGenerator.MenuItem();
            await Context.MenuItems.AddAsync(menuItem);
            var table = TestDataGenerator.Table();
            await Context.Tables.AddAsync(table);
            var person = TestDataGenerator.Person();
            await Context.Persons.AddAsync(person);
            var employeeWaiter = TestDataGenerator.Employee();
            employeeWaiter.PersonId = person.Id;
            await Context.Employees.AddAsync(employeeWaiter);
            target.MenuItemId = menuItem.Id;
            target.TableId = table.Id;
            target.EmployeeWaiterId = employeeWaiter.Id;

            //Act
            var act = await orderItemService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.OrderItems.Single(x =>
                x.Id == act.Id &&
                x.EmployeeWaiterId == target.EmployeeWaiterId &&
                x.TableId == target.TableId &&
                x.MenuItemId == target.MenuItemId
            );
            entity.Should().NotBeNull();

            employeeWaiter.EmployeeType.Should().Be(EmployeeTypes.Waiter);
        }

        /// <summary>
        /// Изменение заказа, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.OrderItem();
            var menuItem = TestDataGenerator.MenuItem();
            var menuItemModel = TestDataGenerator.MenuItem();
            await Context.MenuItems.AddRangeAsync(menuItem, menuItemModel);
            var table = TestDataGenerator.Table();
            var tableModel = TestDataGenerator.Table();
            await Context.Tables.AddRangeAsync(table, tableModel);
            var person = TestDataGenerator.Person();
            var personModel = TestDataGenerator.Person();
            await Context.Persons.AddRangeAsync(person, personModel);
            var employeeWaiter = TestDataGenerator.Employee();
            employeeWaiter.PersonId = person.Id;
            employeeWaiter.EmployeeType = EmployeeTypes.Waiter;
            var employeeWaiterModel = TestDataGenerator.Employee();
            employeeWaiterModel.PersonId = personModel.Id;
            employeeWaiterModel.EmployeeType = EmployeeTypes.Waiter;
            await Context.Employees.AddRangeAsync(employeeWaiter, employeeWaiterModel);
            target.MenuItemId = menuItem.Id;
            target.TableId = table.Id;
            target.EmployeeWaiterId = employeeWaiter.Id;
            await Context.OrderItems.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.OrderItemRequestModel();
            targetModel.Id = target.Id;
            targetModel.MenuItemId = menuItemModel.Id;
            targetModel.TableId = tableModel.Id;
            targetModel.EmployeeWaiterId = employeeWaiterModel.Id;

            //Act
            var act = await orderItemService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.OrderItems.Single(x =>
               x.Id == act.Id &&
                x.MenuItemId == targetModel.MenuItemId &&
                x.TableId == targetModel.TableId &&
                x.EmployeeWaiterId == targetModel.EmployeeWaiterId
            );
            entity.Should().NotBeNull();
            employeeWaiterModel.EmployeeType.Should().Be(EmployeeTypes.Waiter);
        }

        /// <summary>
        /// Удаление заказа, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.OrderItem();
            await Context.OrderItems.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => orderItemService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.OrderItems.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

    }
}

