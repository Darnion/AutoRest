using FluentValidation.TestHelper;
using AutoRest.Api.ModelsRequest.Person;
using AutoRest.Api.Validators.Person;
using Xunit;

namespace AutoRest.Api.Tests.Validators
{
    /// <summary>
    /// Тесты <see cref="PersonRequestValidator"/>
    /// </summary>
    public class PersonRequestValidatorTest
    {
        private readonly CreatePersonRequestValidator validatorCreateRequest;
        private readonly PersonRequestValidator validatorRequest;

        /// <summary>
        /// ctor
        /// </summary>
        public PersonRequestValidatorTest()
        {
            validatorRequest = new PersonRequestValidator();
            validatorCreateRequest = new CreatePersonRequestValidator();
        }

        /// <summary>
        /// Тест с ошибкой
        /// </summary>
        [Fact]
        public void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new PersonRequest()
            {
                Patronymic = "dfghgfhsfdgfdgdfshgfdshgdfshfgshgfhsfdg32453hdbfkgj32hnfsgdngfdbxdfxcfgbxgfchngnfgn23",
            };

            // Act
            var result = validatorRequest.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.LastName);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
            result.ShouldHaveValidationErrorFor(x => x.Patronymic);
        }

        /// <summary>
        /// Тест без ошибок
        /// </summary>
        [Fact]
        public void ValidatorRequestShouldSuccess()
        {
            //Arrange
            var model = new PersonRequest
            {
                LastName = $"LastName{Guid.NewGuid():N}",
                FirstName = $"FirstName{Guid.NewGuid():N}",
            };

            // Act
            var result = validatorRequest.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.LastName);
            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
            result.ShouldNotHaveValidationErrorFor(x => x.Patronymic);
        }

        /// <summary>
        /// Тест с ошибкой
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreatePersonRequest();

            // Act
            var result = validatorCreateRequest.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.LastName);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        /// <summary>
        /// Тест без ошибок
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = new CreatePersonRequest
            {
                LastName = $"LastName{Guid.NewGuid():N}",
                FirstName = $"FirstName{Guid.NewGuid():N}",
            };

            // Act
            var result = validatorCreateRequest.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.LastName);
            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
            result.ShouldNotHaveValidationErrorFor(x => x.Patronymic);
        }
    }
}
