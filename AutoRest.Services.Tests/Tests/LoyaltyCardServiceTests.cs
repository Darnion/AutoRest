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
    public class LoyaltyCardServiceTests : AutoRestContextInMemory
    {
        private readonly ILoyaltyCardService loyaltyCardService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="LoyaltyCardServiceTests"/>
        /// </summary>

        public LoyaltyCardServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            loyaltyCardService = new LoyaltyCardService(
                new LoyaltyCardReadRepository(Reader),
                new LoyaltyCardWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение всех карт лояльности возвращает empty
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            //Arrange

            // Act
            var result = await loyaltyCardService.GetAllAsync(CancellationToken);

            // Assert
            result.Should().BeEmpty();
        }

        /// <summary>
        /// Получение всех карт лояльности возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.LoyaltyCard();
            var deletedTarget = TestDataGenerator.LoyaltyCard();

            deletedTarget.DeletedAt = DateTimeOffset.UtcNow;

            await Context.LoyaltyCards.AddRangeAsync(target, deletedTarget);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await loyaltyCardService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение карты лояльности по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await loyaltyCardService.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение карты лояльности по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.LoyaltyCard();
            await Context.LoyaltyCards.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await loyaltyCardService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.LoyaltyCardType,
                    target.Number,
                });
        }

        // <summary>
        /// Добавление карты лояльности, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.LoyaltyCardRequestModel();

            //Act
            var act = await loyaltyCardService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.LoyaltyCards.Single(x =>
                x.Id == act.Id &&
                x.Number == target.Number
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Изменение карты лояльности, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.LoyaltyCard();
            await Context.LoyaltyCards.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.LoyaltyCardRequestModel();
            targetModel.Id = target.Id;
            targetModel.Number = "2FE";
            //Act
            var act = await loyaltyCardService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.LoyaltyCards.Single(x =>
                x.Id == act.Id &&
                x.LoyaltyCardType == targetModel.LoyaltyCardType &&
                x.Number == "2FE"
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Удаление карты лояльности, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.LoyaltyCard();
            await Context.LoyaltyCards.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => loyaltyCardService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.LoyaltyCards.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

    }
}

