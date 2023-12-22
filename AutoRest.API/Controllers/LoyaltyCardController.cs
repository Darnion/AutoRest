using AutoMapper;
using AutoRest.Api.Attribute;
using AutoRest.Api.Infrastructures.Validator;
using AutoRest.Api.Models;
using AutoRest.Api.ModelsRequest.LoyaltyCard;
using AutoRest.Services.Contracts.Interfaces;
using AutoRest.Services.Contracts.ModelsRequest;
using Microsoft.AspNetCore.Mvc;

namespace AutoRest.Api.Controllers
{

    /// <summary>
    /// CRUD контроллер по работе с картами лояльности
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "LoyaltyCard")]
    public class LoyaltyCardController : ControllerBase
    {
        private readonly ILoyaltyCardService LoyaltyCardService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="LoyaltyCardController"/>
        /// </summary>
        public LoyaltyCardController(ILoyaltyCardService LoyaltyCardService,
            IMapper mapper,
            IApiValidatorService validatorService)
        {
            this.LoyaltyCardService = LoyaltyCardService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        /// <summary>
        /// Получить список карт лояльности
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<LoyaltyCardResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await LoyaltyCardService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<LoyaltyCardResponse>>(result));
        }

        /// <summary>
        /// Получает карту лояльности по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(LoyaltyCardResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await LoyaltyCardService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(mapper.Map<LoyaltyCardResponse>(item));
        }

        /// <summary>
        /// Создаёт новую карту лояльности
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(LoyaltyCardResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateLoyaltyCardRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var LoyaltyCardRequestModel = mapper.Map<LoyaltyCardRequestModel>(request);
            var result = await LoyaltyCardService.AddAsync(LoyaltyCardRequestModel, cancellationToken);
            return Ok(mapper.Map<LoyaltyCardResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющуюся карту лояльности
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(LoyaltyCardResponse))]
        [ApiConflict]
        public async Task<IActionResult> Edit(LoyaltyCardRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<LoyaltyCardRequestModel>(request);
            var result = await LoyaltyCardService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<LoyaltyCardResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющуюся карту лояльности по id
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await LoyaltyCardService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
