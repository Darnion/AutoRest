using AutoRest.Api.Tests.Infrastructures;
using FluentAssertions;
using Newtonsoft.Json;
using AutoRest.Api.Models;
using Xunit;
using System.Text;
using AutoRest.API.Tests;
using AutoRest.Api.ModelsRequest.Employee;
using AutoRest.Context.Contracts.Models;

namespace AutoRest.Api.Tests.Tests
{
    [Collection(nameof(AutoRestApiTestCollection))]
    public class EmployeeIntegrationTests : BaseIntegrTest
    {
        public EmployeeIntegrationTests(AutoRestApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Получение работника по id
        /// </summary>
        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var person1 = TestDataGenerator.Person();
            await context.Persons.AddAsync(person1);
            var person2 = TestDataGenerator.Person();
            await context.Persons.AddAsync(person2);

            var employee1 = TestDataGenerator.Employee();
            var employee2 = TestDataGenerator.Employee();
            employee1.PersonId = person1.Id;
            employee2.PersonId = person2.Id;

            await context.Employees.AddRangeAsync(employee1, employee2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Employee/{employee1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<EmployeeResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    employee1.Id,
                });
        }

        /// <summary>
        /// Получение всех работников по id
        /// </summary>
        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var person1 = TestDataGenerator.Person();
            await context.Persons.AddAsync(person1);
            var person2 = TestDataGenerator.Person();
            await context.Persons.AddAsync(person2);

            var employee1 = TestDataGenerator.Employee();
            var employee2 = TestDataGenerator.Employee();
            employee1.PersonId = person1.Id;
            employee2.PersonId = person2.Id;
            employee2.DeletedAt = DateTimeOffset.UtcNow;

            await context.Employees.AddRangeAsync(employee1, employee2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Employee");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<EmployeeResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And.Contain(x => x.Id == employee1.Id)
                .And.NotContain(x => x.Id == employee2.Id);
        }

        /// <summary>
        /// Добавление работника
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var personModel = TestDataGenerator.PersonModel();

            var target = TestDataGenerator.EmployeeModel();
            target.Person = personModel;

            var person = mapper.Map<Person>(personModel);
            await context.Persons.AddAsync(person);
            await unitOfWork.SaveChangesAsync();

            var employee = mapper.Map<CreateEmployeeRequest>(target);

            // Act
            string data = JsonConvert.SerializeObject(employee);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Employee", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<EmployeeResponse>(resultString);

            var employeeAdded = context.Employees.Single(x => x.Id == result!.Id);

            // Assert          
            employeeAdded.Should()
                .NotBeNull();
        }

        /// <summary>
        /// Изменение работника
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var person = TestDataGenerator.Person();
            await context.Persons.AddAsync(person);
            var person2 = TestDataGenerator.Person();
            await context.Persons.AddAsync(person2);

            var employee = TestDataGenerator.Employee();
            employee.PersonId = person.Id;

            await context.Employees.AddAsync(employee);
            await unitOfWork.SaveChangesAsync();

            var employeeRequest = mapper.Map<EmployeeRequest>(TestDataGenerator.EmployeeModel(x => x.Id = employee.Id));
            employeeRequest.PersonId = person2.Id;

            // Act
            string data = JsonConvert.SerializeObject(employeeRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/Employee", contextdata);

            var employeeEdited = context.Employees.Single(x => x.Id == employee.Id
                                                        && x.PersonId == employeeRequest.PersonId);

            // Assert          
            employeeEdited.Should()
                .NotBeNull();
        }

        /// <summary>
        /// Удаление работника
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var person = TestDataGenerator.Person();
            await context.Persons.AddAsync(person);

            var employee = TestDataGenerator.Employee();
            employee.PersonId = person.Id;

            await context.Employees.AddAsync(employee);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.DeleteAsync(($"/Employee/{employee.Id}"));

            var employeeDeleted = context.Employees.Single(x => x.Id == employee.Id);

            // Assert
            employeeDeleted.DeletedAt.Should().NotBeNull();
        }
    }
}
