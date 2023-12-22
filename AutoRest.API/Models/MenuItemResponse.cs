namespace AutoRest.Api.Models
{
    /// <summary>
    /// Модель ответа сущности позиции меню
    /// </summary>
    public class MenuItemResponse
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
