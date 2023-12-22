using FluentValidation;
using AutoRest.Api.Validators.Discipline;
using AutoRest.Api.Validators.Document;
using AutoRest.Api.Validators.Employee;
using AutoRest.Api.Validators.Group;
using AutoRest.Api.Validators.Person;
using AutoRest.Api.Validators.TimeTableItem;
using AutoRest.Repositories.Contracts;
using AutoRest.Services.Contracts.Exceptions;
using AutoRest.Shared;

namespace AutoRest.Api.Infrastructures.Validator
{
    internal sealed class ApiValidatorService : IApiValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ApiValidatorService(IPersonReadRepository personReadRepository,
            IEmployeeReadRepository employeeReadRepository,
            IDisciplineReadRepository disciplineReadRepository,
            IGroupReadRepository groupReadRepository)
        {
            Register<CreateDisciplineRequestValidator>();
            Register<DisciplineRequestValidator>();

            Register<CreateDocumentRequestValidator>(personReadRepository);
            Register<DocumentRequestValidator>(personReadRepository);

            Register<CreateEmployeeRequestValidator>(personReadRepository);
            Register<EmployeeRequestValidator>(personReadRepository);

            Register<CreateGroupRequestValidator>(employeeReadRepository);
            Register<GroupRequestValidator>(employeeReadRepository);

            Register<CreatePersonRequestValidator>();
            Register<PersonRequestValidator>();

            Register<CreateTimeTableItemRequestValidator>(employeeReadRepository, disciplineReadRepository, groupReadRepository);
            Register<TimeTableItemRequestValidator>(employeeReadRepository, disciplineReadRepository, groupReadRepository);
        }

        ///<summary>
        /// Регистрирует валидатор в словаре
        /// </summary>
        public void Register<TValidator>(params object[] constructorParams)
            where TValidator : IValidator
        {
            var validatorType = typeof(TValidator);
            var innerType = validatorType.BaseType?.GetGenericArguments()[0];
            if (innerType == null)
            {
                throw new ArgumentNullException($"Указанный валидатор {validatorType} должен быть generic от типа IValidator");
            }

            if (constructorParams?.Any() == true)
            {
                var validatorObject = Activator.CreateInstance(validatorType, constructorParams);
                if (validatorObject is IValidator validator)
                {
                    validators.TryAdd(innerType, validator);
                }
            }
            else
            {
                validators.TryAdd(innerType, Activator.CreateInstance<TValidator>());
            }
        }

        public async Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            var modelType = model.GetType();
            if (!validators.TryGetValue(modelType, out var validator))
            {
                throw new InvalidOperationException($"Не найден валидатор для модели {modelType}");
            }

            var context = new ValidationContext<TModel>(model);
            var result = await validator.ValidateAsync(context, cancellationToken);

            if (!result.IsValid)
            {
                throw new TimeTableValidationException(result.Errors.Select(x =>
                InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
