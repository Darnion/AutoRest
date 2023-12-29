using AutoRest.Api.Tests.Infrastructures;
using FluentAssertions;
using Newtonsoft.Json;
using AutoRest.Api.Models;
using Xunit;
using System.Text;
using AutoRest.API.Tests;
using AutoRest.Api.ModelsRequest.Person;

namespace AutoRest.Api.Tests.Tests
{
    [Collection(nameof(AutoRestApiTestCollection))]
    public class PersonIntegrationTests : BaseIntegrTest
    {
        public PersonIntegrationTests(AutoRestApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Получение личности по id
        /// </summary>
        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var person1 = TestDataGenerator.Person();
            var person2 = TestDataGenerator.Person();

            await context.Persons.AddRangeAsync(person1, person2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Person/{person1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PersonResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    person1.Id,
                });
        }

        /// <summary>
        /// Получение всех личностей по id
        /// </summary>
        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var person1 = TestDataGenerator.Person();
            var person2 = TestDataGenerator.Person();

            person2.DeletedAt = DateTimeOffset.UtcNow;

            await context.Persons.AddRangeAsync(person1, person2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Person");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<PersonResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And.Contain(x => x.Id == person1.Id)
                .And.NotContain(x => x.Id == person2.Id);
        }

        /// <summary>
        /// Добавление личности
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var target = TestDataGenerator.PersonModel();

            var person = mapper.Map<CreatePersonRequest>(target);

            // Act
            string data = JsonConvert.SerializeObject(person);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Person", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PersonResponse>(resultString);

            var personAdded = context.Persons.Single(x => x.Id == result!.Id);

            // Assert          
            personAdded.Should()
                .NotBeNull();
        }

        /// <summary>
        /// Изменение личности
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var person = TestDataGenerator.Person();
            await context.Persons.AddAsync(person);
            await unitOfWork.SaveChangesAsync();

            var personRequest = mapper.Map<PersonRequest>(TestDataGenerator.PersonModel(x => x.Id = person.Id));
            personRequest.LastName = Guid.NewGuid().ToString();

            // Act
            string data = JsonConvert.SerializeObject(personRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/Person", contextdata);

            var personEdited = context.Persons.Single(x => x.Id == person.Id
                                                        && x.LastName == personRequest.LastName);

            // Assert          
            personEdited.Should()
                .NotBeNull();
        }

        /// <summary>
        /// Удаление личности
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var person = TestDataGenerator.Person();
            await context.Persons.AddAsync(person);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.DeleteAsync(($"/Person/{person.Id}"));

            var personDeleted = context.Persons.Single(x => x.Id == person.Id);

            // Assert
            personDeleted.DeletedAt.Should().NotBeNull();
        }
    }
}
