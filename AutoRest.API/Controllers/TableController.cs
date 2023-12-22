using AutoMapper;
using AutoRest.Api.Attribute;
using AutoRest.Api.Infrastructures.Validator;
using AutoRest.Api.Models;
using AutoRest.Api.ModelsRequest.Table;
using AutoRest.Services.Contracts.Interfaces;
using AutoRest.Services.Contracts.ModelsRequest;
using Microsoft.AspNetCore.Mvc;

namespace AutoRest.Api.Controllers
{

    /// <summary>
    /// CRUD контроллер по работе со столиками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Table")]
    public class TableController : ControllerBase
    {
        private readonly ITableService TableService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TableController"/>
        /// </summary>
        public TableController(ITableService TableService,
            IMapper mapper,
            IApiValidatorService validatorService)
        {
            this.TableService = TableService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        /// <summary>
        /// Получить список всех столиков
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<TableResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await TableService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<TableResponse>>(result));
        }

        /// <summary>
        /// Получает столик по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(TableResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await TableService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(mapper.Map<TableResponse>(item));
        }

        /// <summary>
        /// Создаёт новый столик
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(TableResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateTableRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var TableRequestModel = mapper.Map<TableRequestModel>(request);
            var result = await TableService.AddAsync(TableRequestModel, cancellationToken);
            return Ok(mapper.Map<TableResponse>(result));
        }

        /// <summary>
        /// Редактирует существующий столик
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(TableResponse))]
        [ApiConflict]
        public async Task<IActionResult> Edit(TableRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<TableRequestModel>(request);
            var result = await TableService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<TableResponse>(result));
        }

        /// <summary>
        /// Удаляет существующий столик
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await TableService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
