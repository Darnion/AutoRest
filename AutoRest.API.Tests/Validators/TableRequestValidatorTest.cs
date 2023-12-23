using FluentValidation.TestHelper;
using AutoRest.Api.ModelsRequest.Table;
using AutoRest.Api.Validators.Table;
using Xunit;

namespace AutoRest.Api.Tests.Validators
{
    /// <summary>
    /// Тесты <see cref="TableRequestValidator"/>
    /// </summary>
    public class TableRequestValidatorTest
    {
        private readonly CreateTableRequestValidator validatorCreateRequest;
        private readonly TableRequestValidator validatorRequest;

        /// <summary>
        /// ctor
        /// </summary>
        public TableRequestValidatorTest()
        {
            validatorRequest = new TableRequestValidator();
            validatorCreateRequest = new CreateTableRequestValidator();
        }

        /// <summary>
        /// Тест с ошибкой
        /// </summary>
        [Fact]
        public void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new TableRequest();

            // Act
            var result = validatorRequest.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Number);
        }

        /// <summary>
        /// Тест без ошибок
        /// </summary>
        [Fact]
        public void ValidatorRequestShouldSuccess()
        {
            //Arrange
            var model = new TableRequest
            {
                Number = $"Number{Guid.NewGuid():N}",
            };

            // Act
            var result = validatorRequest.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Number);
        }

        /// <summary>
        /// Тест с ошибкой
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateTableRequest();

            // Act
            var result = validatorCreateRequest.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Number);
        }

        /// <summary>
        /// Тест без ошибок
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = new CreateTableRequest
            {
                Number = $"Number{Guid.NewGuid():N}",
            };

            // Act
            var result = validatorCreateRequest.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Number);
        }
    }
}
