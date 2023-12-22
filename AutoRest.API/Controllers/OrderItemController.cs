using AutoMapper;
using AutoRest.Api.Attribute;
using AutoRest.Api.Infrastructures.Validator;
using AutoRest.Api.Models;
using AutoRest.Api.ModelsRequest.OrderItem;
using AutoRest.Services.Contracts.Interfaces;
using AutoRest.Services.Contracts.ModelsRequest;
using Microsoft.AspNetCore.Mvc;

namespace AutoRest.Api.Controllers
{

    /// <summary>
    /// CRUD контроллер по работе с заказами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "OrderItem")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService OrderItemService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="OrderItemController"/>
        /// </summary>
        public OrderItemController(IOrderItemService OrderItemService,
            IMapper mapper,
            IApiValidatorService validatorService)
        {
            this.OrderItemService = OrderItemService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        /// <summary>
        /// Получить список всех заказов
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<OrderItemResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await OrderItemService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<OrderItemResponse>>(result));
        }

        /// <summary>
        /// Получает заказ по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(OrderItemResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await OrderItemService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(mapper.Map<OrderItemResponse>(item));
        }

        /// <summary>
        /// Создаёт новый заказ
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(OrderItemResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateOrderItemRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var OrderItemRequestModel = mapper.Map<OrderItemRequestModel>(request);
            var result = await OrderItemService.AddAsync(OrderItemRequestModel, cancellationToken);
            return Ok(mapper.Map<OrderItemResponse>(result));
        }

        /// <summary>
        /// Редактирует существующий заказ
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(OrderItemResponse))]
        [ApiConflict]
        public async Task<IActionResult> Edit(OrderItemRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<OrderItemRequestModel>(request);
            var result = await OrderItemService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<OrderItemResponse>(result));
        }

        /// <summary>
        /// Удаляет существующий заказ
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await OrderItemService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
