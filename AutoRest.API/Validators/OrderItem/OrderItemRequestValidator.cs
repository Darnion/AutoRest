using AutoRest.Api.ModelsRequest.OrderItem;
using AutoRest.Repositories.Contracts;
using FluentValidation;

namespace AutoRest.Api.Validators.OrderItem
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderItemRequestValidator : AbstractValidator<OrderItemRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public OrderItemRequestValidator(IEmployeeReadRepository employeeReadRepository,
                                        ILoyaltyCardReadRepository loyaltyCardReadRepository,
                                        IMenuItemReadRepository menuItemReadRepository,
                                        ITableReadRepository tableReadRepository)
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.EmployeeWaiterId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя официанта не должно быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var waiterExists = await employeeReadRepository.AnyByIdAsync(id, CancellationToken);
                    return waiterExists;
                })
                .WithMessage("Такого официанта не существует!");

            RuleFor(x => x.TableId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Столик не должен быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var tableExists = await tableReadRepository.AnyByIdAsync(id, CancellationToken);
                    return tableExists;
                })
                .WithMessage("Такого столика не существует!");

            RuleFor(x => x.MenuItemId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Заказанная позиция не должна быть пустой или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var menuItemExists = await menuItemReadRepository.AnyByIdAsync(id, CancellationToken);
                    return menuItemExists;
                })
                .WithMessage("Такой позиции не существует!");

            RuleFor(x => x.EmployeeCashierId)
                .MustAsync(async (id, CancellationToken) =>
                {
                    var employeeAllowed = await employeeReadRepository.IsTypeNotAllowedAsync(id!.Value, CancellationToken);
                    return employeeAllowed;
                })
                 .WithMessage("Работник не соответствует уровню допуска!");
        }
    }
}