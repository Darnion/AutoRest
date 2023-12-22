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
    public class PersonService : IPersonService, IServiceAnchor
    {
        private readonly IPersonReadRepository personReadRepository;
        private readonly IPersonWriteRepository personWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PersonService(IPersonReadRepository personReadRepository,
            IPersonWriteRepository personWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.personReadRepository = personReadRepository;
            this.personWriteRepository = personWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<PersonModel>> IPersonService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await personReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<PersonModel>>(result);
        }

        async Task<PersonModel?> IPersonService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await personReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<PersonModel>(item);
        }

        async Task<PersonModel> IPersonService.AddAsync(PersonRequestModel person, CancellationToken cancellationToken)
        {
            var item = new Person
            {
                Id = Guid.NewGuid(),
                LastName = person.LastName,
                FirstName = person.FirstName,
                Patronymic = person.Patronymic,
            };
            personWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<PersonModel>(item);
        }

        async Task<PersonModel> IPersonService.EditAsync(PersonRequestModel source, CancellationToken cancellationToken)
        {
            var targetPerson = await personReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetPerson == null)
            {
                throw new AutoRestEntityNotFoundException<Person>(source.Id);
            }

            targetPerson.LastName = source.LastName;
            targetPerson.FirstName = source.FirstName;
            targetPerson.Patronymic = source.Patronymic;

            personWriteRepository.Update(targetPerson);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<PersonModel>(targetPerson);
        }

        async Task IPersonService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetPerson = await personReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetPerson == null)
            {
                throw new AutoRestEntityNotFoundException<Person>(id);
            }
            if (targetPerson.DeletedAt.HasValue)
            {
                throw new AutoRestInvalidOperationException($"Личность с идентификатором {id} уже удалена");
            }

            personWriteRepository.Delete(targetPerson);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
