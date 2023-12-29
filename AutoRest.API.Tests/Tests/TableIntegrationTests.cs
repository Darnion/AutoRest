using AutoRest.Api.Tests.Infrastructures;
using FluentAssertions;
using Newtonsoft.Json;
using AutoRest.Api.Models;
using Xunit;
using System.Text;
using AutoRest.API.Tests;
using AutoRest.Api.ModelsRequest.Table;

namespace AutoRest.Api.Tests.Tests
{
    [Collection(nameof(AutoRestApiTestCollection))]
    public class TableIntegrationTests : BaseIntegrTest
    {
        public TableIntegrationTests(AutoRestApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Получение столика по id
        /// </summary>
        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var table1 = TestDataGenerator.Table();
            var table2 = TestDataGenerator.Table();

            await context.Tables.AddRangeAsync(table1, table2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Table/{table1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TableResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    table1.Id,
                });
        }

        /// <summary>
        /// Получение всех столиков по id
        /// </summary>
        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var table1 = TestDataGenerator.Table();
            var table2 = TestDataGenerator.Table();

            table2.DeletedAt = DateTimeOffset.UtcNow;

            await context.Tables.AddRangeAsync(table1, table2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Table");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<TableResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.Contain(x => x.Id == table1.Id)
                .And.NotContain(x => x.Id == table2.Id);
        }

        /// <summary>
        /// Добавление столика
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var target = TestDataGenerator.TableModel();

            var table = mapper.Map<CreateTableRequest>(target);

            // Act
            string data = JsonConvert.SerializeObject(table);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Table", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TableResponse>(resultString);

            var tableAdded = context.Tables.Single(x => x.Id == result!.Id);

            // Assert          
            tableAdded.Should()
                .NotBeNull();
        }

        /// <summary>
        /// Изменение столика
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var table = TestDataGenerator.Table();
            await context.Tables.AddAsync(table);
            await unitOfWork.SaveChangesAsync();

            var tableRequest = mapper.Map<TableRequest>(TestDataGenerator.TableModel(x => x.Id = table.Id));
            tableRequest.Number = "2FSAXX";

            // Act
            string data = JsonConvert.SerializeObject(tableRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/Table", contextdata);

            var tableEdited = context.Tables.Single(x => x.Id == table.Id
                                                        && x.Number == tableRequest.Number);

            // Assert          
            tableEdited.Should()
                .NotBeNull();
        }

        /// <summary>
        /// Удаление столика
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var table = TestDataGenerator.Table();
            await context.Tables.AddAsync(table);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.DeleteAsync(($"/Table/{table.Id}"));

            var tableDeleted = context.Tables.Single(x => x.Id == table.Id);

            // Assert
            tableDeleted.DeletedAt.Should().NotBeNull();
        }
    }
}
