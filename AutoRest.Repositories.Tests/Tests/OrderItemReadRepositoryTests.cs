using AutoRest.Repositories.Contracts;
using AutoRest.Repositories.Implementations;
using FluentAssertions;
using AutoRest.Context.Tests;
using Xunit;

namespace AutoRest.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IOrderItemReadRepository"/>
    /// </summary>
    public class OrderItemReadRepositoryTests : AutoRestContextInMemory
    {
        private readonly IOrderItemReadRepository orderItemReadRepository;

        public OrderItemReadRepositoryTests()
        {
            orderItemReadRepository = new OrderItemReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список заказов
        /// </summary>
        [Fact]
        public async Task GetAllOrderItemEmpty()
        {
            // Act
            var result = await orderItemReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список заказов
        /// </summary>
        [Fact]
        public async Task GetAllOrderItemsValue()
        {
            //Arrange
            var target = TestDataGenerator.OrderItem();
            await Context.OrderItems.AddRangeAsync(target,
                TestDataGenerator.OrderItem(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await orderItemReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение заказов по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdOrderItemNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await orderItemReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение списка заказов по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdOrderItemValue()
        {
            //Arrange
            var target = TestDataGenerator.OrderItem();
            await Context.OrderItems.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await orderItemReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }
    }
}
