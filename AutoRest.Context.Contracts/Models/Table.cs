namespace AutoRest.Context.Contracts.Models
{
    /// <summary>
    /// Сущность столика
    /// </summary>
    public class Table : BaseAuditEntity
    {
        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Заказы связь один ко многим
        /// </summary>
        public ICollection<OrderItem>? Orders { get; set; }
    }
}