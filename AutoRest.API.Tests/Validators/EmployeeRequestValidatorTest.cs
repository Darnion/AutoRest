//using FluentValidation.TestHelper;
//using AutoRest.Api.ModelsRequest.Employee;
//using AutoRest.Api.Validators.Employee;
//using Xunit;
//using AutoRest.Repositories.Contracts;
//using AutoRest.Repositories.Implementations;
//using System.Reflection.PortableExecutable;

//namespace AutoRest.Api.Tests.Validators
//{
//    /// <summary>
//    /// Тесты <see cref="EmployeeRequestValidator"/>
//    /// </summary>
//    public class EmployeeRequestValidatorTest
//    {
//        private readonly CreateEmployeeRequestValidator validatorCreateRequest;
//        private readonly EmployeeRequestValidator validatorRequest;

//        /// <summary>
//        /// ctor
//        /// </summary>
//        public EmployeeRequestValidatorTest()
//        {
//            validatorRequest = new EmployeeRequestValidator(new PersonReadRepository(Reader));
//            validatorCreateRequest = new CreateEmployeeRequestValidator();
//        }

//        /// <summary>
//        /// Тест на наличие ошибок
//        /// </summary>
//        [Fact]
//        public void ValidatorRequestShouldError()
//        {
//            //Arrange
//            var model = new EmployeeRequest();

//            // Act
//            var result = validatorRequest.TestValidate(model);

//            // Assert
//            result.ShouldHaveValidationErrorFor(x => x.Number);
//        }

//        /// <summary>
//        /// Тест на отсутствие ошибок
//        /// </summary>
//        [Fact]
//        public void ValidatorRequestShouldSuccess()
//        {
//            //Arrange
//            var model = new EmployeeRequest
//            {
//                Number = $"Name{Guid.NewGuid():N}",
//            };

//            // Act
//            var result = validatorRequest.TestValidate(model);

//            // Assert
//            result.ShouldNotHaveValidationErrorFor(x => x.Number);
//        }

//        /// <summary>
//        /// Тест на наличие ошибок
//        /// </summary>
//        [Fact]
//        public void ValidatorCreateRequestShouldError()
//        {
//            //Arrange
//            var model = new CreateEmployeeRequest();

//            // Act
//            var result = validatorCreateRequest.TestValidate(model);

//            // Assert
//            result.ShouldHaveValidationErrorFor(x => x.Number);
//        }

//        /// <summary>
//        /// Тест на отсутствие ошибок
//        /// </summary>
//        [Fact]
//        public void ValidatorCreateRequestShouldSuccess()
//        {
//            //Arrange
//            var model = new CreateEmployeeRequest
//            {
//                Number = $"Name{Guid.NewGuid():N}",
//            };

//            // Act
//            var result = validatorCreateRequest.TestValidate(model);

//            // Assert
//            result.ShouldNotHaveValidationErrorFor(x => x.Number);
//        }
//    }
//}
