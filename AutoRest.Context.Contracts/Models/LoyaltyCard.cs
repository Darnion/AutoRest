using AutoRest.Context.Contracts.Enums;

namespace AutoRest.Context.Contracts.Models
{
    /// <summary>
    /// Карта лояльности
    /// </summary>
    public class LoyaltyCard : BaseAuditEntity
    {
        /// <inheritdoc cref="Enums.LoyaltyCardType"/>
        public LoyaltyCardType LoyaltyCardType { get; set; } = LoyaltyCardType.Bronze;

        /// <summary>
        /// Номер карты лояльности
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Нужна для связи один ко многим по вторичному ключу <see cref="OrderItem"/>
        /// </summary>
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}