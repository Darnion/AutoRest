namespace AutoRest.Api.Models
{
    /// <summary>
    /// Модель ответа сущности заказа
    /// </summary>
    public class OrderItemResponse
    {

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Время принятия заказа
        /// </summary>
        public DateTimeOffset OrderAcceptTime { get; set; }

        /// <summary>
        /// Официант
        /// </summary>
        public string EmployeeWaiterFIO { get; set; }

        /// <summary>
        /// Номер столика
        /// </summary>
        public string TableNumber { get; set; }

        /// <summary>
        /// Позиция
        /// </summary>
        public string MenuItem { get; set; }

        /// <summary>
        /// Карта лояльности
        /// </summary>
        public string LoyaltyCardNumber { get; set; } = string.Empty;

        /// <summary>
        /// Тип карты лояльности
        /// </summary>
        public string LoyaltyCardType { get; set; } = string.Empty;

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal OrderCost { get; set; }

        /// <summary>
        /// Статус заказа (оплачен/нет)
        /// </summary>
        public string OrderStatus { get; set; }

        /// <summary>
        /// Кассир
        /// </summary>
        public string EmployeeCashierFIO { get; set; }
    }
}
