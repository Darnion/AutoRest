namespace AutoRest.Api.Models
{
    /// <summary>
    /// Модель ответа сущности столика
    /// </summary>
    public class TableResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; }
    }
}
