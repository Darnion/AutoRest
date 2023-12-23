using AutoRest.Repositories.Contracts;
using AutoRest.Repositories.Implementations;
using FluentAssertions;
using AutoRest.Context.Tests;
using Xunit;

namespace AutoRest.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="ITableReadRepository"/>
    /// </summary>
    public class TableReadRepositoryTests : AutoRestContextInMemory
    {
        private readonly ITableReadRepository tableReadRepository;

        public TableReadRepositoryTests()
        {
            tableReadRepository = new TableReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список столиков
        /// </summary>
        [Fact]
        public async Task GetAllTableEmpty()
        {
            // Act
            var result = await tableReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список столиков
        /// </summary>
        [Fact]
        public async Task GetAllTablesValue()
        {
            //Arrange
            var target = TestDataGenerator.Table();
            await Context.Tables.AddRangeAsync(target,
                TestDataGenerator.Table(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await tableReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение столиков по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdTableNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await tableReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение списка столиков по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdTableValue()
        {
            //Arrange
            var target = TestDataGenerator.Table();
            await Context.Tables.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await tableReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка столиков по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsSTableEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await tableReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка столиков по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsTablesValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Table();
            var target2 = TestDataGenerator.Table(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Table();
            var target4 = TestDataGenerator.Table();
            await Context.Tables.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await tableReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }
    }
}
