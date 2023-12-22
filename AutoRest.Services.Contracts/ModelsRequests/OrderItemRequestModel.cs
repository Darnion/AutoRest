using AutoRest.Services.Contracts.Models;

namespace AutoRest.Services.Contracts.ModelsRequest
{
    public class OrderItemRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
        public Guid? LoyaltyCardId { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public bool OrderStatus { get; set; } = false;

        /// <summary>
        /// <inheritdoc cref="EmployeeModel"/>
        /// </summary>
        public Guid? EmployeeCashierId { get; set; }
    }
}
