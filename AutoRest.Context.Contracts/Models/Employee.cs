using AutoRest.Context.Contracts.Enums;

namespace AutoRest.Context.Contracts.Models
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Employee : BaseAuditEntity
    {
        /// <inheritdoc cref="EmployeeTypes"/>
        public EmployeeTypes EmployeeType { get; set; } = EmployeeTypes.Waiter;

        /// <summary>
        /// Идентификатор <inheritdoc cref="Person"/>
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Нужна для связи один ко многим по вторичному ключу <see cref="OrderItem"/>
        /// </summary>
        public ICollection<OrderItem>? OrderItem { get; set; }
    }
}