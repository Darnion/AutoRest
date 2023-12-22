using AutoMapper;
using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Context.Contracts.Models;
using AutoRest.Repositories.Contracts;
using AutoRest.Services.Contracts.Exceptions;
using AutoRest.Services.Contracts.Interfaces;
using AutoRest.Services.Contracts.Models;
using AutoRest.Services.Contracts.ModelsRequest;

namespace AutoRest.Services.Implementations
{
    public class LoyaltyCardService : ILoyaltyCardService, IServiceAnchor
    {
        private readonly ILoyaltyCardReadRepository loyaltyCardReadRepository;
        private readonly ILoyaltyCardWriteRepository loyaltyCardWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LoyaltyCardService(ILoyaltyCardReadRepository loyaltyCardReadRepository,
            ILoyaltyCardWriteRepository loyaltyCardWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.loyaltyCardReadRepository = loyaltyCardReadRepository;
            this.loyaltyCardWriteRepository = loyaltyCardWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<LoyaltyCardModel>> ILoyaltyCardService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await loyaltyCardReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<LoyaltyCardModel>>(result);
        }

        async Task<LoyaltyCardModel?> ILoyaltyCardService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await loyaltyCardReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<LoyaltyCardModel>(item);
        }

        async Task<LoyaltyCardModel> ILoyaltyCardService.AddAsync(LoyaltyCardRequestModel loyaltyCard, CancellationToken cancellationToken)
        {
            var item = new LoyaltyCard
            {
                Id = Guid.NewGuid(),
                LoyaltyCardType = loyaltyCard.LoyaltyCardType,
                Number = loyaltyCard.Number,
            };
            loyaltyCardWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<LoyaltyCardModel>(item);
        }

        async Task<LoyaltyCardModel> ILoyaltyCardService.EditAsync(LoyaltyCardRequestModel source, CancellationToken cancellationToken)
        {
            var targetLoyaltyCard = await loyaltyCardReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetLoyaltyCard == null)
            {
                throw new AutoRestEntityNotFoundException<LoyaltyCard>(source.Id);
            }

            targetLoyaltyCard.LoyaltyCardType = source.LoyaltyCardType;
            targetLoyaltyCard.Number = source.Number;

            loyaltyCardWriteRepository.Update(targetLoyaltyCard);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<LoyaltyCardModel>(targetLoyaltyCard);
        }

        async Task ILoyaltyCardService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetLoyaltyCard = await loyaltyCardReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetLoyaltyCard == null)
            {
                throw new AutoRestEntityNotFoundException<LoyaltyCard>(id);
            }
            if (targetLoyaltyCard.DeletedAt.HasValue)
            {
                throw new AutoRestInvalidOperationException($"Карта лояльности с идентификатором {id} уже удалена");
            }

            loyaltyCardWriteRepository.Delete(targetLoyaltyCard);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
