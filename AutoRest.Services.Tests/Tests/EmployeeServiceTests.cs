using AutoMapper;
using AutoRest.Repositories.Implementations;
using AutoRest.Services.Automappers;
using AutoRest.Services.Contracts.Interfaces;
using AutoRest.Services.Implementations;
using FluentAssertions;
using AutoRest.Context.Tests;
using Xunit;

namespace AutoRest.Services.Tests.Tests
{
    public class EmployeeServiceTests : AutoRestContextInMemory
    {
        private readonly IEmployeeService employeeService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EmployeeServiceTests"/>
        /// </summary>

        public EmployeeServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            employeeService = new EmployeeService(
                new EmployeeReadRepository(Reader),
                new EmployeeWriteRepository(WriterContext),
                UnitOfWork,
                new PersonReadRepository(Reader),
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение работника по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await employeeService.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение работника по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Employee();
            await Context.Employees.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await employeeService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.EmployeeType
                });
        }

        /// <summary>
        /// Добавление работника, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.EmployeeRequestModel();
            var person = TestDataGenerator.Person();
            await Context.Persons.AddAsync(person);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            target.PersonId = person.Id;
            var act = await employeeService.AddAsync(target, CancellationToken);

            //Assert

            var entity = Context.Employees.Single(x =>
                x.Id == act.Id &&
                x.PersonId == target.PersonId
            );
            entity.Should().NotBeNull();
        }

        /// <summary>
        /// Изменение работника, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var person = TestDataGenerator.Person();
            var personEdit = TestDataGenerator.Person();
            await Context.Persons.AddRangeAsync(person, personEdit);

            var target = TestDataGenerator.Employee();
            target.PersonId = person.Id;
            await Context.Employees.AddAsync(target);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.EmployeeRequestModel();
            targetModel.Id = target.Id;
            targetModel.PersonId = personEdit.Id;

            //Act
            var act = await employeeService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Employees.Single(x =>
                x.Id == act.Id &&
                x.PersonId == targetModel.PersonId
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Удаление работника, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Employee();
            await Context.Employees.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => employeeService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Employees.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

    }
}

