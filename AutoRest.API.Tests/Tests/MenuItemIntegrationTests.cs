using AutoRest.Api.Tests.Infrastructures;
using FluentAssertions;
using Newtonsoft.Json;
using AutoRest.Api.Models;
using Xunit;
using System.Text;
using AutoRest.API.Tests;
using AutoRest.Api.ModelsRequest.MenuItem;

namespace AutoRest.Api.Tests.Tests
{
    [Collection(nameof(AutoRestApiTestCollection))]
    public class MenuItemIntegrationTests : BaseIntegrTest
    {
        public MenuItemIntegrationTests(AutoRestApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Получение позиции по id
        /// </summary>
        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var menuitem1 = TestDataGenerator.MenuItem();
            var menuitem2 = TestDataGenerator.MenuItem();

            await context.MenuItems.AddRangeAsync(menuitem1, menuitem2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/MenuItem/{menuitem1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MenuItemResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    menuitem1.Id,
                });
        }

        /// <summary>
        /// Получение всех позиций по id
        /// </summary>
        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var menuitem1 = TestDataGenerator.MenuItem();
            var menuitem2 = TestDataGenerator.MenuItem();

            menuitem2.DeletedAt = DateTimeOffset.UtcNow;

            await context.MenuItems.AddRangeAsync(menuitem1, menuitem2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/MenuItem");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<MenuItemResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And.Contain(x => x.Id == menuitem1.Id)
                .And.NotContain(x => x.Id == menuitem2.Id);
        }

        /// <summary>
        /// Добавление позиции
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var target = TestDataGenerator.MenuItemModel();

            var menuitem = mapper.Map<CreateMenuItemRequest>(target);

            // Act
            string data = JsonConvert.SerializeObject(menuitem);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/MenuItem", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MenuItemResponse>(resultString);

            var menuitemAdded = context.MenuItems.Single(x => x.Id == result!.Id);

            // Assert          
            menuitemAdded.Should()
                .NotBeNull();
        }

        /// <summary>
        /// Изменение позиции
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var menuitem = TestDataGenerator.MenuItem();
            await context.MenuItems.AddAsync(menuitem);
            await unitOfWork.SaveChangesAsync();

            var menuitemRequest = mapper.Map<MenuItemRequest>(TestDataGenerator.MenuItemModel(x => x.Id = menuitem.Id));
            menuitemRequest.Title = Guid.NewGuid().ToString();

            // Act
            string data = JsonConvert.SerializeObject(menuitemRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/MenuItem", contextdata);

            var menuitemEdited = context.MenuItems.Single(x => x.Id == menuitem.Id
                                                        && x.Title == menuitemRequest.Title);

            // Assert          
            menuitemEdited.Should()
                .NotBeNull();
        }

        /// <summary>
        /// Удаление позиции
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var menuitem = TestDataGenerator.MenuItem();
            await context.MenuItems.AddAsync(menuitem);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.DeleteAsync(($"/MenuItem/{menuitem.Id}"));

            var menuitemDeleted = context.MenuItems.Single(x => x.Id == menuitem.Id);

            // Assert
            menuitemDeleted.DeletedAt.Should().NotBeNull();
        }
    }
}
