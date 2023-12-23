using FluentValidation.TestHelper;
using AutoRest.Api.ModelsRequest.LoyaltyCard;
using AutoRest.Api.Validators.LoyaltyCard;
using Xunit;

namespace AutoRest.Api.Tests.Validators
{
    /// <summary>
    /// Тесты <see cref="LoyaltyCardRequestValidator"/>
    /// </summary>
    public class LoyaltyCardRequestValidatorTest
    {
        private readonly CreateLoyaltyCardRequestValidator validatorCreateRequest;
        private readonly LoyaltyCardRequestValidator validatorRequest;

        /// <summary>
        /// ctor
        /// </summary>
        public LoyaltyCardRequestValidatorTest()
        {
            validatorRequest = new LoyaltyCardRequestValidator();
            validatorCreateRequest = new CreateLoyaltyCardRequestValidator();
        }

        /// <summary>
        /// Тест с ошибкой
        /// </summary>
        [Fact]
        public void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new LoyaltyCardRequest();

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
            var model = new LoyaltyCardRequest
            {
                Number = $"Number{Guid.NewGuid():N}",
            };

            // Act
            var result = validatorRequest.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Number);
            result.ShouldNotHaveValidationErrorFor(x => x.LoyaltyCardType);
        }

        /// <summary>
        /// Тест с ошибкой
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateLoyaltyCardRequest();

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
            var model = new CreateLoyaltyCardRequest
            {
                Number = $"Number{Guid.NewGuid():N}",
            };

            // Act
            var result = validatorCreateRequest.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Number);
            result.ShouldNotHaveValidationErrorFor(x => x.LoyaltyCardType);
        }
    }
}
