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
        public string MenuItemTitle { get; set; } = string.Empty;

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal MenuItemCost { get; set; } = 100;

    }
}