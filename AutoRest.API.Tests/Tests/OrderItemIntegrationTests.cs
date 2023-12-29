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
        private readonly Employee employeeWaiter;
        private readonly Person person;
        private readonly Table table;
        private readonly MenuItem menuItem;

        public OrderItemIntegrationTests(AutoRestApiFixture fixture) : base(fixture)
        {
            employeeWaiter = TestDataGenerator.Employee();
            person = TestDataGenerator.Person();
            table = TestDataGenerator.Table();
            menuItem = TestDataGenerator.MenuItem();

            employeeWaiter.PersonId = person.Id;

            context.Persons.AddAsync(person);
            context.Employees.AddAsync(employeeWaiter);
            context.Tables.AddAsync(table);
            context.MenuItems.AddAsync(menuItem);
            unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Получение заказа по id
        /// </summary>
        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var orderitem1 = TestDataGenerator.OrderItem();
            var orderitem2 = TestDataGenerator.OrderItem();

            orderitem1.EmployeeWaiterId = employeeWaiter.Id;
            orderitem1.TableId = table.Id;
            orderitem1.MenuItemId = menuItem.Id;

            var employeeWaiter2 = TestDataGenerator.Employee();
            var person2 = TestDataGenerator.Person();
            var table2 = TestDataGenerator.Table();
            var menuItem2 = TestDataGenerator.MenuItem();

            employeeWaiter2.PersonId = person2.Id;

            await context.Persons.AddAsync(person2);
            await context.Employees.AddAsync(employeeWaiter2);
            await context.Tables.AddAsync(table2);
            await context.MenuItems.AddAsync(menuItem2);

            orderitem2.EmployeeWaiterId = employeeWaiter2.Id;
            orderitem2.TableId = table2.Id;
            orderitem2.MenuItemId = menuItem2.Id;

            await context.OrderItems.AddRangeAsync(orderitem1, orderitem2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/OrderItem/{orderitem1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OrderItemResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    orderitem1.Id,
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

            orderitem1.EmployeeWaiterId = employeeWaiter.Id;
            orderitem1.TableId = table.Id;
            orderitem1.MenuItemId = menuItem.Id;

            var employeeWaiter2 = TestDataGenerator.Employee();
            var person2 = TestDataGenerator.Person();
            var table2 = TestDataGenerator.Table();
            var menuItem2 = TestDataGenerator.MenuItem();

            employeeWaiter2.PersonId = person2.Id;

            await context.Persons.AddAsync(person2);
            await context.Employees.AddAsync(employeeWaiter2);
            await context.Tables.AddAsync(table2);
            await context.MenuItems.AddAsync(menuItem2);

            orderitem2.EmployeeWaiterId = employeeWaiter2.Id;
            orderitem2.TableId = table2.Id;
            orderitem2.MenuItemId = menuItem2.Id;

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
