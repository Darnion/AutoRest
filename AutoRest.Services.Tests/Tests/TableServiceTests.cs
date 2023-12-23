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
    public class TableServiceTests : AutoRestContextInMemory
    {
        private readonly ITableService tableService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TableServiceTests"/>
        /// </summary>

        public TableServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            tableService = new TableService(
                new TableReadRepository(Reader),
                new TableWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение столика по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await tableService.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение столика по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Table();
            await Context.Tables.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await tableService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Number,
                });
        }

        // <summary>
        /// Добавление столика, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.TableRequestModel();

            //Act
            var act = await tableService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.Tables.Single(x =>
                x.Id == act.Id &&
                x.Number == target.Number
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Изменение столика, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Table();
            await Context.Tables.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.TableRequestModel();
            targetModel.Id = target.Id;
            targetModel.Number = "12A";
            //Act
            var act = await tableService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Tables.Single(x =>
                x.Id == act.Id &&
                x.Number == "12A"
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Удаление столика, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Table();
            await Context.Tables.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => tableService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Tables.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

    }
}

