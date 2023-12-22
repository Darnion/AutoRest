using AutoRest.Api.Validators.Employee;
using AutoRest.Api.Validators.LoyaltyCard;
using AutoRest.Api.Validators.MenuItem;
using AutoRest.Api.Validators.OrderItem;
using AutoRest.Api.Validators.Person;
using AutoRest.Api.Validators.Table;
using AutoRest.Repositories.Contracts;
using AutoRest.Services.Contracts.Exceptions;
using AutoRest.Shared;
using FluentValidation;

namespace AutoRest.Api.Infrastructures.Validator
{
    internal sealed class ApiValidatorService : IApiValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ApiValidatorService(IPersonReadRepository personReadRepository,
            IEmployeeReadRepository employeeReadRepository,
            ILoyaltyCardReadRepository loyaltycardReadRepository,
            ITableReadRepository tableReadRepository)
        {
            Register<CreateLoyaltyCardRequestValidator>();
            Register<LoyaltyCardRequestValidator>();

            Register<CreateMenuItemRequestValidator>(personReadRepository);
            Register<MenuItemRequestValidator>(personReadRepository);

            Register<CreateEmployeeRequestValidator>(personReadRepository);
            Register<EmployeeRequestValidator>(personReadRepository);

            Register<CreateTableRequestValidator>(employeeReadRepository);
            Register<TableRequestValidator>(employeeReadRepository);

            Register<CreatePersonRequestValidator>();
            Register<PersonRequestValidator>();

            Register<CreateOrderItemRequestValidator>(employeeReadRepository, loyaltycardReadRepository, tableReadRepository);
            Register<OrderItemRequestValidator>(employeeReadRepository, loyaltycardReadRepository, tableReadRepository);
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
                throw new AutoRestValidationException(result.Errors.Select(x =>
                InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
