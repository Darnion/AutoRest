using AutoRest.Api.Models.Enums;

namespace AutoRest.Api.ModelsRequest.LoyaltyCard
{
    public class CreateLoyaltyCardRequest
    {
        /// <inheritdoc cref="LoyaltyCardTypesResponse"/>
        public LoyaltyCardTypesResponse LoyaltyCardType { get; set; } = LoyaltyCardTypesResponse.Bronze;

        /// <summary>
        /// Номер карты лояльности
        /// </summary>
        public string Number { get; set; }
    }
}
