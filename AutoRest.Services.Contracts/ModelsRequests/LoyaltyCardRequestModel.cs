using AutoRest.Services.Contracts.Enums;

namespace AutoRest.Services.Contracts.ModelsRequest
{
    public class LoyaltyCardRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <inheritdoc cref="LoyaltyCardTypesModel"/>
        public LoyaltyCardTypesModel LoyaltyCardType { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; } = string.Empty;
    }
}
