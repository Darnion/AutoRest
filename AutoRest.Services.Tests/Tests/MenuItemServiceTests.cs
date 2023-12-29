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
    public class MenuItemServiceTests : AutoRestContextInMemory
    {
        private readonly IMenuItemService menuItemService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="MenuItemServiceTests"/>
        /// </summary>

        public MenuItemServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            menuItemService = new MenuItemService(
                new MenuItemReadRepository(Reader),
                new MenuItemWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение всех позиций возвращает empty
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            //Arrange

            // Act
            var result = await menuItemService.GetAllAsync(CancellationToken);

            // Assert
            result.Should().BeEmpty();
        }

        /// <summary>
        /// Получение всех позиций возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.MenuItem();
            var deletedTarget = TestDataGenerator.MenuItem();

            deletedTarget.DeletedAt = DateTimeOffset.UtcNow;

            await Context.MenuItems.AddRangeAsync(target, deletedTarget);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await menuItemService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение позиции по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await menuItemService.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение позиции по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.MenuItem();
            await Context.MenuItems.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await menuItemService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Title,
                    target.Cost,
                });
        }

        // <summary>
        /// Добавление позиции, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.MenuItemRequestModel();

            //Act
            var act = await menuItemService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.MenuItems.Single(x =>
                x.Id == act.Id &&
                x.Title == target.Title
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Изменение позиции, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.MenuItem();
            await Context.MenuItems.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.MenuItemRequestModel();
            targetModel.Id = target.Id;
            targetModel.Title = "Vegetables";
            //Act
            var act = await menuItemService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.MenuItems.Single(x =>
                x.Id == act.Id &&
                x.Cost == targetModel.Cost &&
                x.Title == "Vegetables"
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Удаление позиции, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.MenuItem();
            await Context.MenuItems.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => menuItemService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.MenuItems.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

    }
}

