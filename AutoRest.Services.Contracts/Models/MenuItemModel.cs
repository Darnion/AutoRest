namespace AutoRest.Services.Contracts.Models
{
    /// <summary>
    /// Модель "Позиции"
    /// </summary>
    public class MenuItemModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal Cost { get; set; } = 0;
    }
}