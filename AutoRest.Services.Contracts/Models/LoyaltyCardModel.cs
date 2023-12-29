using AutoRest.Services.Contracts.Models.Enums;

namespace AutoRest.Services.Contracts.Models
{
    /// <summary>
    /// Модель "Карта лояльности"
    /// </summary>
    public class LoyaltyCardModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <inheritdoc cref="LoyaltyCardTypesModel"/>
        public LoyaltyCardTypesModel LoyaltyCardType { get; set; } = LoyaltyCardTypesModel.Bronze;

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; } = string.Empty;
    }
}