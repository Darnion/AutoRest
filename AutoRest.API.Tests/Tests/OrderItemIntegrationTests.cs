using AutoRest.Api.Tests.Infrastructures;
using FluentAssertions;
using Newtonsoft.Json;
using AutoRest.Api.Models;
using Xunit;
using System.Text;
using AutoRest.API.Tests;
using AutoRest.Api.ModelsRequest.OrderItem;
using AutoRest.Context.Contracts.Models;
using System.Data;

namespace AutoRest.Api.Tests.Tests
{
    [Collection(nameof(AutoRestApiTestCollection))]
    public class OrderItemIntegrationTests : BaseIntegrTest
    {
        private readonly Employee employee1, employee2;
        private readonly Person person1, person2;
        private readonly Table table;
        private readonly MenuItem menuItem;

        public OrderItemIntegrationTests(AutoRestApiFixture fixture) : base(fixture)
        {
            person1 = TestDataGenerator.Person();
            person2 = TestDataGenerator.Person();
            context.Persons.AddRangeAsync(person1, person2);

            employee1 = TestDataGenerator.Employee();
            employee2 = TestDataGenerator.Employee();
            employee1.PersonId = person1.Id;
            employee2.PersonId = person2.Id;
            context.Employees.AddRangeAsync(employee1, employee2);

            table = TestDataGenerator.Table();
            context.Tables.AddAsync(table);

            menuItem = TestDataGenerator.MenuItem();
            context.MenuItems.AddAsync(menuItem);

            unitOfWork.SaveChangesAsync().Wait();
        }

        /// <summary>
        /// Получение заказа по id
        /// </summary>
        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var orderItem1 = TestDataGenerator.OrderItem();
            var orderItem2 = TestDataGenerator.OrderItem();

            orderItem1.EmployeeWaiterId = employee1.Id;
            orderItem1.TableId = table.Id;
            orderItem1.MenuItemId = menuItem.Id;

            orderItem2.EmployeeWaiterId = employee2.Id;
            orderItem2.TableId = table.Id;
            orderItem2.MenuItemId = menuItem.Id;

            await context.OrderItems.AddRangeAsync(orderItem1, orderItem2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/OrderItem/{orderItem1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OrderItemResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    orderItem1.Id,
                });
        }

        /// <summary>
        /// Получение всех заказов по id
        /// </summary>
        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var orderitem1 = TestDataGenerator.OrderItem();
            var orderitem2 = TestDataGenerator.OrderItem();

            orderitem1.EmployeeWaiterId = employee1.Id;
            orderitem1.TableId = table.Id;
            orderitem1.MenuItemId = menuItem.Id;

            orderitem2.EmployeeWaiterId = employee2.Id;
            orderitem2.TableId = table.Id;
            orderitem2.MenuItemId = menuItem.Id;

            orderitem2.DeletedAt = DateTimeOffset.UtcNow;

            await context.OrderItems.AddRangeAsync(orderitem1, orderitem2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/OrderItem");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<OrderItemResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And.Contain(x => x.Id == orderitem1.Id)
                .And.NotContain(x => x.Id == orderitem2.Id);
        }

        ///// <summary>
        ///// Добавление заказа
        ///// </summary>
        //[Fact]
        //public async Task AddShouldWork()
        //{
        //    // Arrange
        //    var client = factory.CreateClient();

        //    var menuItemModel = TestDataGenerator.MenuItemModel();
        //    var tableModel = TestDataGenerator.TableModel();
        //    var personModel = TestDataGenerator.PersonModel();
        //    var employeeModel = TestDataGenerator.EmployeeModel();

        //    employeeModel.Person = personModel;

        //    var person1 = mapper.Map<Person>(personModel);
        //    var table1 = mapper.Map<Table>(tableModel);
        //    var menuItem1 = mapper.Map<MenuItem>(menuItemModel);
        //    var employee1 = mapper.Map<Employee>(employeeModel);

        //    employee1.PersonId = person1.Id;

        //    await context.Persons.AddAsync(person1);
        //    await context.Employees.AddAsync(employee1);
        //    await context.Tables.AddAsync(table1);
        //    await context.MenuItems.AddAsync(menuItem1);
        //    await unitOfWork.SaveChangesAsync();

        //    var target = TestDataGenerator.OrderItemModel();

        //    target.EmployeeWaiter = personModel;
        //    target.Table = tableModel;
        //    target.MenuItem = menuItemModel;

        //    var orderitem = mapper.Map<CreateOrderItemRequest>(target);

        //    // Act
        //    string data = JsonConvert.SerializeObject(orderitem);
        //    var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
        //    var response = await client.PostAsync("/OrderItem", contextdata);
        //    var resultString = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<OrderItemResponse>(resultString);

        //    var orderitemAdded = context.OrderItems.Single(x => x.Id == result!.Id);

        //    // Assert          
        //    orderitemAdded.Should()
        //        .NotBeNull();
        //}

        ///// <summary>
        ///// Изменение заказа
        ///// </summary>
        //[Fact]
        //public async Task EditShouldWork()
        //{
        //    // Arrange
        //    var client = factory.CreateClient();

        //    var orderitem = TestDataGenerator.OrderItem();
        //    await context.OrderItems.AddAsync(orderitem);
        //    await unitOfWork.SaveChangesAsync();

        //    var orderitemRequest = mapper.Map<OrderItemRequest>(TestDataGenerator.OrderItemModel(x => x.Id = orderitem.Id));
        //    orderitemRequest.OrderStatus = false;

        //    // Act
        //    string data = JsonConvert.SerializeObject(orderitemRequest);
        //    var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
        //    var response = await client.PutAsync("/OrderItem", contextdata);

        //    var orderitemEdited = context.OrderItems.Single(x => x.Id == orderitem.Id
        //                                                && x.OrderStatus == orderitemRequest.OrderStatus);

        //    // Assert          
        //    orderitemEdited.Should()
        //        .NotBeNull();
        //}

        ///// <summary>
        ///// Удаление заказа
        ///// </summary>
        //[Fact]
        //public async Task DeleteShouldWork()
        //{
        //    // Arrange
        //    var client = factory.CreateClient();

        //    var orderitem = TestDataGenerator.OrderItem();
        //    await context.OrderItems.AddAsync(orderitem);
        //    await unitOfWork.SaveChangesAsync();

        //    // Act
        //    var response = await client.DeleteAsync(($"/OrderItem/{orderitem.Id}"));

        //    var orderitemDeleted = context.OrderItems.Single(x => x.Id == orderitem.Id);

        //    // Assert
        //    orderitemDeleted.DeletedAt.Should().NotBeNull();
        //}
    }
}
