using AutoRest.Repositories.Contracts;
using FluentValidation.TestHelper;
using Moq;
using AutoRest.Api.ModelsRequest.OrderItem;
using AutoRest.Api.Validators.OrderItem;
using Xunit;

namespace AutoRest.Api.Tests.Validators
{
    public class OrderItemRequestValidatorTest
    {
        private readonly CreateOrderItemRequestValidator validationCreateRequest;
        private readonly OrderItemRequestValidator validationRequest;

        private readonly Mock<IEmployeeReadRepository> employeeReadRepositoryMock;
        private readonly Mock<ILoyaltyCardReadRepository> loyaltyCardReadRepositoryMock;
        private readonly Mock<IMenuItemReadRepository> menuItemReadRepositoryMock;
        private readonly Mock<ITableReadRepository> tableReadRepositoryMock;

        public OrderItemRequestValidatorTest()
        {
            employeeReadRepositoryMock = new Mock<IEmployeeReadRepository>();
            loyaltyCardReadRepositoryMock = new Mock<ILoyaltyCardReadRepository>();
            menuItemReadRepositoryMock = new Mock<IMenuItemReadRepository>();
            tableReadRepositoryMock = new Mock<ITableReadRepository>();

            validationCreateRequest = new CreateOrderItemRequestValidator(employeeReadRepositoryMock.Object,
                                                                        loyaltyCardReadRepositoryMock.Object,
                                                                        menuItemReadRepositoryMock.Object,
                                                                        tableReadRepositoryMock.Object);
            validationRequest = new OrderItemRequestValidator(employeeReadRepositoryMock.Object,
                                                            loyaltyCardReadRepositoryMock.Object,
                                                            menuItemReadRepositoryMock.Object,
                                                            tableReadRepositoryMock.Object);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new OrderItemRequest();

            //Act
            var validation = await validationRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveValidationErrorFor(x => x.Id);
            validation.ShouldHaveValidationErrorFor(x => x.EmployeeWaiterId);
            validation.ShouldHaveValidationErrorFor(x => x.TableId);
            validation.ShouldHaveValidationErrorFor(x => x.MenuItemId);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldSuccess()
        {
            //Arrange
            var model = new OrderItemRequest()
            {
                Id = Guid.NewGuid(),
                EmployeeWaiterId = Guid.NewGuid(),
                TableId = Guid.NewGuid(),
                MenuItemId = Guid.NewGuid(),
            };

            employeeReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.EmployeeWaiterId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            tableReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.TableId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            menuItemReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.MenuItemId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Act
            var validation = await validationRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.Id);
            validation.ShouldNotHaveValidationErrorFor(x => x.EmployeeWaiterId);
            validation.ShouldNotHaveValidationErrorFor(x => x.TableId);
            validation.ShouldNotHaveValidationErrorFor(x => x.MenuItemId);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateOrderItemRequest();

            //Act
            var validation = await validationCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveValidationErrorFor(x => x.EmployeeWaiterId);
            validation.ShouldHaveValidationErrorFor(x => x.TableId);
            validation.ShouldHaveValidationErrorFor(x => x.MenuItemId);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = new CreateOrderItemRequest()
            {
                EmployeeWaiterId = Guid.NewGuid(),
                TableId = Guid.NewGuid(),
                MenuItemId = Guid.NewGuid(),
            };

            employeeReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.EmployeeWaiterId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            tableReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.TableId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            menuItemReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.MenuItemId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Act
            var validation = await validationCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.EmployeeWaiterId);
            validation.ShouldNotHaveValidationErrorFor(x => x.TableId);
            validation.ShouldNotHaveValidationErrorFor(x => x.MenuItemId);
        }
    }
}