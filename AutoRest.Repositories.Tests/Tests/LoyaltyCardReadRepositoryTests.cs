using AutoRest.Repositories.Contracts;
using AutoRest.Repositories.Implementations;
using FluentAssertions;
using AutoRest.Context.Tests;
using Xunit;

namespace AutoRest.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="ILoyaltyCardReadRepository"/>
    /// </summary>
    public class LoyaltyCardReadRepositoryTests : AutoRestContextInMemory
    {
        private readonly ILoyaltyCardReadRepository loyaltyCardReadRepository;

        public LoyaltyCardReadRepositoryTests()
        {
            loyaltyCardReadRepository = new LoyaltyCardReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список карт лояльности
        /// </summary>
        [Fact]
        public async Task GetAllLoyaltyCardEmpty()
        {
            // Act
            var result = await loyaltyCardReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список карт лояльности
        /// </summary>
        [Fact]
        public async Task GetAllLoyaltyCardsValue()
        {
            //Arrange
            var target = TestDataGenerator.LoyaltyCard();
            await Context.LoyaltyCards.AddRangeAsync(target,
                TestDataGenerator.LoyaltyCard(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await loyaltyCardReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение карт лояльности по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdLoyaltyCardNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await loyaltyCardReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение списка карт лояльности по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdLoyaltyCardValue()
        {
            //Arrange
            var target = TestDataGenerator.LoyaltyCard();
            await Context.LoyaltyCards.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await loyaltyCardReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка карт лояльности по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsLoyaltyCardEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await loyaltyCardReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка карт лояльности по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsLoyaltyCardsValue()
        {
            //Arrange
            var target1 = TestDataGenerator.LoyaltyCard();
            var target2 = TestDataGenerator.LoyaltyCard(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.LoyaltyCard();
            var target4 = TestDataGenerator.LoyaltyCard();
            await Context.LoyaltyCards.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await loyaltyCardReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }
    }
}
