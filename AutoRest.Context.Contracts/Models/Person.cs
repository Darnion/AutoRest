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
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronymic { get; set; }

        /// <summary>
        /// Нужна для связи один ко многим по вторичному ключу <see cref="Employee"/>
        /// </summary>
        public ICollection<Employee> Employee { get; set; }
    }
}