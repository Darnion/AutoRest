using AutoRest.Api.ModelsRequest.OrderItem;
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
        public OrderItemRequestValidator()
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.EmployeeWaiterFIO)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя официанта не должно быть пустым или null");

            RuleFor(x => x.TableNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер столика не должен быть пустым или null");

            RuleFor(x => x.MenuItem)
                .NotNull()
                .NotEmpty()
                .WithMessage("Заказанная позиция не должна быть пустой или null");
        }
    }
}