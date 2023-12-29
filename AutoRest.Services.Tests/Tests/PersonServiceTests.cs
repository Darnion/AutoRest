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
    public class PersonServiceTests : AutoRestContextInMemory
    {
        private readonly IPersonService personService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PersonServiceTests"/>
        /// </summary>

        public PersonServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            personService = new PersonService(
                new PersonReadRepository(Reader),
                new PersonWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение всех личностей возвращает empty
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            //Arrange

            // Act
            var result = await personService.GetAllAsync(CancellationToken);

            // Assert
            result.Should().BeEmpty();
        }

        /// <summary>
        /// Получение всех личностей возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Person();
            var deletedTarget = TestDataGenerator.Person();

            deletedTarget.DeletedAt = DateTimeOffset.UtcNow;

            await Context.Persons.AddRangeAsync(target, deletedTarget);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await personService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение личности по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await personService.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение личности по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Person();
            await Context.Persons.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await personService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.LastName,
                    target.FirstName,
                    target.Patronymic,
                });
        }

        // <summary>
        /// Добавление личности, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.PersonRequestModel();

            //Act
            var act = await personService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.Persons.Single(x =>
                x.Id == act.Id &&
                x.LastName == target.LastName
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Изменение личности, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Person();
            await Context.Persons.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.PersonRequestModel();
            targetModel.Id = target.Id;
            targetModel.Patronymic = null;
            //Act
            var act = await personService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Persons.Single(x =>
                x.Id == act.Id &&
                x.LastName == targetModel.LastName &&
                x.Patronymic == null
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Удаление личности, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Person();
            await Context.Persons.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => personService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Persons.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

    }
}

