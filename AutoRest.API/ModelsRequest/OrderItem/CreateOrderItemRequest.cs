using AutoRest.Services.Contracts.Models;

namespace AutoRest.Api.ModelsRequest.OrderItem
{
    public class CreateOrderItemRequest
    {
        /// <summary>
        /// <inheritdoc cref="EmployeeModel"/>
        /// </summary>
        public Guid EmployeeWaiterId { get; set; }

        /// <summary>
        /// <inheritdoc cref="TableModel"/>
        /// </summary>
        public Guid TableId { get; set; }

        /// <summary>
        /// <inheritdoc cref="MenuItemModel"/>
        /// </summary>
        public Guid MenuItemId { get; set; }

        /// <summary>
        /// <inheritdoc cref="LoyaltyCardModel"/>
        /// </summary>
        public Guid? LoyaltyCardId { get; set; } = Guid.Empty;

        /// <summary>
        /// Статус заказа
        /// </summary>
        public bool OrderStatus { get; set; } = false;

        /// <summary>
        /// <inheritdoc cref="EmployeeModel"/>
        /// </summary>
        public Guid? EmployeeCashierId { get; set; } = Guid.Empty;
    }
}
