using AutoRest.Repositories.Contracts;
using FluentValidation.TestHelper;
using Moq;
using AutoRest.Api.ModelsRequest.Employee;
using AutoRest.Api.Validators.Employee;
using Xunit;

namespace AutoRest.Api.Tests.Validators
{
    public class EmployeeRequestValidatorTest
    {
        private readonly CreateEmployeeRequestValidator validationCreateRequest;
        private readonly EmployeeRequestValidator validationRequest;

        private readonly Mock<IPersonReadRepository> personReadRepositoryMock;
        public EmployeeRequestValidatorTest()
        {
            personReadRepositoryMock = new Mock<IPersonReadRepository>();
            validationCreateRequest = new CreateEmployeeRequestValidator(personReadRepositoryMock.Object);
            validationRequest = new EmployeeRequestValidator(personReadRepositoryMock.Object);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new EmployeeRequest();

            //Act
            var validation = await validationRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveValidationErrorFor(x => x.Id);
            validation.ShouldHaveValidationErrorFor(x => x.PersonId);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldSuccess()
        {
            //Arrange
            var model = new EmployeeRequest()
            {
                Id = Guid.NewGuid(),
                PersonId = Guid.NewGuid()
            };

            personReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.PersonId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Act
            var validation = await validationRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.Id);
            validation.ShouldNotHaveValidationErrorFor(x => x.EmployeeType);
            validation.ShouldNotHaveValidationErrorFor(x => x.PersonId);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateEmployeeRequest();

            //Act
            var validation = await validationCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveValidationErrorFor(x => x.PersonId);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = new CreateEmployeeRequest()
            {
                PersonId = Guid.NewGuid(),
            };

            personReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.PersonId, It.IsAny<CancellationToken>()))
               .ReturnsAsync(true);

            //Act
            var validation = await validationCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.PersonId);
        }
    }
}