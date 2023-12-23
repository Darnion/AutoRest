using FluentValidation.TestHelper;
using AutoRest.Api.ModelsRequest.MenuItem;
using AutoRest.Api.Validators.MenuItem;
using Xunit;

namespace AutoRest.Api.Tests.Validators
{
    /// <summary>
    /// Тесты <see cref="MenuItemRequestValidator"/>
    /// </summary>
    public class MenuItemRequestValidatorTest
    {
        private readonly CreateMenuItemRequestValidator validatorCreateRequest;
        private readonly MenuItemRequestValidator validatorRequest;

        /// <summary>
        /// ctor
        /// </summary>
        public MenuItemRequestValidatorTest()
        {
            validatorRequest = new MenuItemRequestValidator();
            validatorCreateRequest = new CreateMenuItemRequestValidator();
        }

        /// <summary>
        /// Тест с ошибкой
        /// </summary>
        [Fact]
        public void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new MenuItemRequest();

            // Act
            var result = validatorRequest.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        /// <summary>
        /// Тест без ошибок
        /// </summary>
        [Fact]
        public void ValidatorRequestShouldSuccess()
        {
            //Arrange
            var model = new MenuItemRequest
            {
                Title = $"Title{Guid.NewGuid():N}",
            };

            // Act
            var result = validatorRequest.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Title);
            result.ShouldNotHaveValidationErrorFor(x => x.Cost);
        }

        /// <summary>
        /// Тест с ошибкой
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateMenuItemRequest();

            // Act
            var result = validatorCreateRequest.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        /// <summary>
        /// Тест без ошибок
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = new CreateMenuItemRequest
            {
                Title = $"Title{Guid.NewGuid():N}",
            };

            // Act
            var result = validatorCreateRequest.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Title);
            result.ShouldNotHaveValidationErrorFor(x => x.Cost);
        }
    }
}
