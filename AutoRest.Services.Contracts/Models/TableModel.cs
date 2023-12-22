namespace AutoRest.Context.Contracts.Models
{
    /// <summary>
    /// Модель столика
    /// </summary>
    public class TableModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; } = string.Empty;
    }
}