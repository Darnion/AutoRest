using AutoMapper;
using AutoRest.Api.Attribute;
using AutoRest.Api.Infrastructures.Validator;
using AutoRest.Api.Models;
using AutoRest.Api.ModelsRequest.MenuItem;
using AutoRest.Services.Contracts.Interfaces;
using AutoRest.Services.Contracts.ModelsRequest;
using Microsoft.AspNetCore.Mvc;

namespace AutoRest.Api.Controllers
{

    /// <summary>
    /// CRUD контроллер по работе с позициями меню
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "MenuItem")]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService MenuItemService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="MenuItemController"/>
        /// </summary>
        public MenuItemController(IMenuItemService MenuItemService,
            IMapper mapper,
            IApiValidatorService validatorService)
        {
            this.MenuItemService = MenuItemService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        /// <summary>
        /// Получить список всех позиций меню
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<MenuItemResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await MenuItemService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<MenuItemResponse>>(result));
        }

        /// <summary>
        /// Получает позицию меню по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(MenuItemResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await MenuItemService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(mapper.Map<MenuItemResponse>(item));
        }

        /// <summary>
        /// Создаёт новую позицию меню
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(MenuItemResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateMenuItemRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var MenuItemRequestModel = mapper.Map<MenuItemRequestModel>(request);
            var result = await MenuItemService.AddAsync(MenuItemRequestModel, cancellationToken);
            return Ok(mapper.Map<MenuItemResponse>(result));
        }

        /// <summary>
        /// Редактирует существующую позицию меню
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(MenuItemResponse))]
        [ApiConflict]
        public async Task<IActionResult> Edit(MenuItemRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<MenuItemRequestModel>(request);
            var result = await MenuItemService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<MenuItemResponse>(result));
        }

        /// <summary>
        /// Удаляет существующую позицию меню
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await MenuItemService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
