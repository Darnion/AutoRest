namespace AutoRest.Api.ModelsRequest.OrderItem
{
    public class CreateOrderItemRequest
    {
        /// <summary>
        /// Официант
        /// </summary>
        public string EmployeeWaiterFIO { get; set; }

        /// <summary>
        /// Номер столика
        /// </summary>
        public string TableNumber { get; set; }

        /// <summary>
        /// Позиция в меню
        /// </summary>
        public string MenuItem { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public bool OrderStatus { get; set; }

        /// <summary>
        /// Кассир
        /// </summary>
        public string EmployeeCashierFIO { get; set; }
    }
}
