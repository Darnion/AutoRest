namespace AutoRest.Context.Contracts.Models
{
    /// <summary>
    /// Позиции
    /// </summary>
    public class MenuItem : BaseAuditEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal Cost { get; set; } = 0;

        /// <summary>
        /// Нужна для связи один ко многим по вторичному ключу <see cref="OrderItem"/>
        /// </summary>
        public ICollection<OrderItem>? OrderItem { get; set; }

    }
}