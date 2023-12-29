namespace AutoRest.Context.Contracts.Models
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class OrderItem : BaseAuditEntity
    {
        /// <summary>
        /// Идентификатор официанта
        /// </summary>
        public Guid EmployeeWaiterId { get; set; }

        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public Employee EmployeeWaiter { get; set; }

        /// <summary>
        /// Идентификатор столика
        /// </summary>
        public Guid TableId { get; set; }

        /// <summary>
        /// Связь один ко многим
        /// </summary>
        public Table Table { get; set; }

        /// <summary>
        /// Идентификатор заказа
        /// </summary>
        public Guid MenuItemId { get; set; }

        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public MenuItem MenuItem { get; set; }

        /// <summary>
        /// Идентификатор карты лояльности
        /// </summary>
        public Guid? LoyaltyCardId { get; set; }

        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public LoyaltyCard? LoyaltyCard { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public bool OrderStatus { get; set; } = false;

        /// <summary>
        /// Идентификатор кассира
        /// </summary>
        public Guid? EmployeeCashierId { get; set; }

        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public Employee? EmployeeCashier { get; set; }

    }
}