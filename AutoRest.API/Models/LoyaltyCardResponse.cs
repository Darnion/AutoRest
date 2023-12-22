using AutoRest.Api.Models.Enums;

namespace AutoRest.Api.Models
{
    /// <summary>
    /// Модель ответа сущности карты лояльности
    /// </summary>
    public class LoyaltyCardResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <inheritdoc cref="LoyaltyCardTypesResponse"/>
        public string LoyaltyCardType { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; } = string.Empty;
    }
}
