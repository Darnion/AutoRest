using AutoRest.Repositories.Contracts;
using AutoRest.Repositories.Implementations;
using FluentAssertions;
using AutoRest.Context.Tests;
using Xunit;

namespace AutoRest.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IMenuItemReadRepository"/>
    /// </summary>
    public class MenuItemReadRepositoryTests : AutoRestContextInMemory
    {
        private readonly IMenuItemReadRepository menuItemReadRepository;

        public MenuItemReadRepositoryTests()
        {
            menuItemReadRepository = new MenuItemReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список позиций
        /// </summary>
        [Fact]
        public async Task GetAllMenuItemEmpty()
        {
            // Act
            var result = await menuItemReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список позиций
        /// </summary>
        [Fact]
        public async Task GetAllMenuItemsValue()
        {
            //Arrange
            var target = TestDataGenerator.MenuItem();
            await Context.MenuItems.AddRangeAsync(target,
                TestDataGenerator.MenuItem(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await menuItemReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение позиций по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdMenuItemNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await menuItemReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение списка позиций по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdMenuItemValue()
        {
            //Arrange
            var target = TestDataGenerator.MenuItem();
            await Context.MenuItems.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await menuItemReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка позиций по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsMenuItemEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await menuItemReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка позиций по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsMenuItemsValue()
        {
            //Arrange
            var target1 = TestDataGenerator.MenuItem();
            var target2 = TestDataGenerator.MenuItem(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.MenuItem();
            var target4 = TestDataGenerator.MenuItem();
            await Context.MenuItems.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await menuItemReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }
    }
}
