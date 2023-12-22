using AutoMapper;
using AutoRest.Services.Contracts.Interfaces;
using AutoRest.Services.Contracts.ModelsRequest;
using Microsoft.AspNetCore.Mvc;
using AutoRest.Api.Attribute;
using AutoRest.Api.Infrastructures.Validator;
using AutoRest.Api.Models;
using AutoRest.Api.ModelsRequest.Person;

namespace AutoRest.Api.Controllers
{

    /// <summary>
    /// CRUD контроллер по работе с личностями
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Person")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PersonController"/>
        /// </summary>
        public PersonController(IPersonService personService,
            IMapper mapper,
            IApiValidatorService validatorService)
        {
            this.personService = personService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        /// <summary>
        /// Получить список всех личностей
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<PersonResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await personService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<PersonResponse>>(result));
        }

        /// <summary>
        /// Получает личность по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(PersonResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await personService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти личность с идентификатором {id}");
            }

            return Ok(mapper.Map<PersonResponse>(item));
        }

        /// <summary>
        /// Создаёт новую личность
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(PersonResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreatePersonRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var personRequestModel = mapper.Map<PersonRequestModel>(request);
            var result = await personService.AddAsync(personRequestModel, cancellationToken);
            return Ok(mapper.Map<PersonResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющуюся личность
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(PersonResponse))]
        [ApiConflict]
        public async Task<IActionResult> Edit(PersonRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<PersonRequestModel>(request);
            var result = await personService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<PersonResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющуюся личность по id
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await personService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
