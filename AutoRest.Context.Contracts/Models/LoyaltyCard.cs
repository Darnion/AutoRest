﻿using AutoRest.Context.Contracts.Enums;

namespace AutoRest.Context.Contracts.Models
{
    /// <summary>
    /// Карта лояльности
    /// </summary>
    public class LoyaltyCard : BaseAuditEntity
    {
        /// <inheritdoc cref="LoyaltyCardTypes"/>
        public LoyaltyCardTypes LoyaltyCardType { get; set; } = LoyaltyCardTypes.Bronze;

        /// <summary>
        /// Номер карты лояльности
        /// </summary>
        public string LoyaltyCardNumber { get; set; } = string.Empty;

        /// <summary>
        /// Нужна для связи один ко многим по вторичному ключу <see cref="OrderItem"/>
        /// </summary>
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}