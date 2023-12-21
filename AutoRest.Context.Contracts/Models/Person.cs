namespace AutoRest.Context.Contracts.Models
{
    /// <summary>
    /// Сущность личности
    /// </summary>
    public class Person : BaseAuditEntity
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string PersonLastName { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string PersonFirstName { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string? PersonPatronymic { get; set; }

        /// <summary>
        /// Нужна для связи один ко многим по вторичному ключу <see cref="Employee"/>
        /// </summary>
        public ICollection<Employee> Employee { get; set; }
    }
}