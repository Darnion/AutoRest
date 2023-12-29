using AutoRest.Repositories.Contracts;
using AutoRest.Repositories.Implementations;
using FluentAssertions;
using AutoRest.Context.Tests;
using Xunit;
using AutoRest.Context.Contracts.Enums;

namespace AutoRest.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IEmployeeReadRepository"/>
    /// </summary>
    public class EmployeeReadRepositoryTests : AutoRestContextInMemory
    {
        private readonly IEmployeeReadRepository employeeReadRepository;

        public EmployeeReadRepositoryTests()
        {
            employeeReadRepository = new EmployeeReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список работников
        /// </summary>
        [Fact]
        public async Task GetAllEmployeeEmpty()
        {
            // Act
            var result = await employeeReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список работников
        /// </summary>
        [Fact]
        public async Task GetAllEmployeeValue()
        {
            //Arrange
            var target = TestDataGenerator.Employee();
            await Context.Employees.AddRangeAsync(target,
                TestDataGenerator.Employee(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await employeeReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение работника по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdEmployeeNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await employeeReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение работника по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdEmployeeValue()
        {
            //Arrange
            var target = TestDataGenerator.Employee();
            await Context.Employees.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await employeeReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка работников по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsEmployeesEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await employeeReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка работников по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsEmployeesValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Employee();
            var target2 = TestDataGenerator.Employee(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Employee();
            var target4 = TestDataGenerator.Employee();
            await Context.Employees.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await employeeReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Проверка должности по идентификатору работника (true)
        /// </summary>
        [Fact]
        public async Task IsTypeNotAllowedReturnTrue()
        {
            //Arrange
            var target = TestDataGenerator.Employee();
            await Context.Employees.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await employeeReadRepository.IsTypeNotAllowedAsync(target.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Проверка должности по идентификатору работника (false)
        /// </summary>
        [Fact]
        public async Task IsTypeNotAllowedReturnFalse()
        {
            //Arrange
            var target = TestDataGenerator.Employee();
            target.EmployeeType = EmployeeTypes.Сashier;
            await Context.Employees.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await employeeReadRepository.IsTypeNotAllowedAsync(target.Id, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }
    }
}
