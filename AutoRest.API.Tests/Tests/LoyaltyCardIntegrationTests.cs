using AutoRest.Api.Tests.Infrastructures;
using FluentAssertions;
using Newtonsoft.Json;
using AutoRest.Api.Models;
using Xunit;
using System.Text;
using AutoRest.API.Tests;
using AutoRest.Api.ModelsRequest.LoyaltyCard;

namespace AutoRest.Api.Tests.Tests
{
    [Collection(nameof(AutoRestApiTestCollection))]
    public class LoyaltyCardIntegrationTests : BaseIntegrTest
    {
        public LoyaltyCardIntegrationTests(AutoRestApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Получение карты лояльности по id
        /// </summary>
        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var loyaltycard1 = TestDataGenerator.LoyaltyCard();
            var loyaltycard2 = TestDataGenerator.LoyaltyCard();

            await context.LoyaltyCards.AddRangeAsync(loyaltycard1, loyaltycard2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/LoyaltyCard/{loyaltycard1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoyaltyCardResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    loyaltycard1.Id,
                });
        }

        /// <summary>
        /// Получение всех карт по id
        /// </summary>
        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var loyaltycard1 = TestDataGenerator.LoyaltyCard();
            var loyaltycard2 = TestDataGenerator.LoyaltyCard();

            loyaltycard2.DeletedAt = DateTimeOffset.UtcNow;

            await context.LoyaltyCards.AddRangeAsync(loyaltycard1, loyaltycard2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/LoyaltyCard");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<LoyaltyCardResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And.Contain(x => x.Id == loyaltycard1.Id)
                .And.NotContain(x => x.Id == loyaltycard2.Id);
        }

        /// <summary>
        /// Добавление карты лояльности
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var target = TestDataGenerator.LoyaltyCardModel();

            var loyaltycard = mapper.Map<CreateLoyaltyCardRequest>(target);

            // Act
            string data = JsonConvert.SerializeObject(loyaltycard);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/LoyaltyCard", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoyaltyCardResponse>(resultString);

            var loyaltycardAdded = context.LoyaltyCards.Single(x => x.Id == result!.Id);

            // Assert          
            loyaltycardAdded.Should()
                .NotBeNull();
        }

        /// <summary>
        /// Изменение карты лояльности
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var loyaltycard = TestDataGenerator.LoyaltyCard();
            await context.LoyaltyCards.AddAsync(loyaltycard);
            await unitOfWork.SaveChangesAsync();

            var loyaltycardRequest = mapper.Map<LoyaltyCardRequest>(TestDataGenerator.LoyaltyCardModel(x => x.Id = loyaltycard.Id));
            loyaltycardRequest.Number = Guid.NewGuid().ToString();

            // Act
            string data = JsonConvert.SerializeObject(loyaltycardRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/LoyaltyCard", contextdata);

            var loyaltycardEdited = context.LoyaltyCards.Single(x => x.Id == loyaltycard.Id
                                                        && x.Number == loyaltycardRequest.Number);

            // Assert          
            loyaltycardEdited.Should()
                .NotBeNull();
        }

        /// <summary>
        /// Удаление карты лояльности
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();

            var loyaltycard = TestDataGenerator.LoyaltyCard();
            await context.LoyaltyCards.AddAsync(loyaltycard);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.DeleteAsync(($"/LoyaltyCard/{loyaltycard.Id}"));

            var loyaltycardDeleted = context.LoyaltyCards.Single(x => x.Id == loyaltycard.Id);

            // Assert
            loyaltycardDeleted.DeletedAt.Should().NotBeNull();
        }
    }
}
