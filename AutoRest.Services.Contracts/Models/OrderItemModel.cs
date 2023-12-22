namespace AutoRest.Services.Contracts.Models
{
    /// <summary>
    /// Модель "Заказ"
    /// </summary>
    public class OrderItemModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public PersonModel EmployeeWaiter { get; set; }

        /// <summary>
        /// <inheritdoc cref="TableModel"/>
        /// </summary>
        public TableModel Table { get; set; }

        /// <summary>
        /// <inheritdoc cref="MenuItemModel"/>
        /// </summary>
        public MenuItemModel MenuItem { get; set; }

        /// <summary>
        /// <inheritdoc cref="LoyaltyCardModel"/>
        /// </summary>
        public LoyaltyCardModel? LoyaltyCard { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public bool OrderStatus { get; set; } = false;

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public PersonModel? EmployeeCashier { get; set; }
    }
}