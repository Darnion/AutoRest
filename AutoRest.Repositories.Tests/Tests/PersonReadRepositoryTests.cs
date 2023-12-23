using AutoRest.Repositories.Contracts;
using AutoRest.Repositories.Implementations;
using FluentAssertions;
using AutoRest.Context.Tests;
using Xunit;

namespace AutoRest.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IPersonReadRepository"/>
    /// </summary>
    public class PersonReadRepositoryTests : AutoRestContextInMemory
    {
        private readonly IPersonReadRepository personReadRepository;

        public PersonReadRepositoryTests()
        {
            personReadRepository = new PersonReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список персон
        /// </summary>
        [Fact]
        public async Task GetAllPersonEmpty()
        {
            // Act
            var result = await personReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список персон
        /// </summary>
        [Fact]
        public async Task GetAllPersonsValue()
        {
            //Arrange
            var target = TestDataGenerator.Person();
            await Context.Persons.AddRangeAsync(target,
                TestDataGenerator.Person(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await personReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение персоны по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdPersonNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await personReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение документа по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdPersonValue()
        {
            //Arrange
            var target = TestDataGenerator.Person();
            await Context.Persons.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await personReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка персон по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsSPersonEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await personReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка персон по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsPersonsValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Person();
            var target2 = TestDataGenerator.Person(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Person();
            var target4 = TestDataGenerator.Person();
            await Context.Persons.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await personReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }
    }
}
