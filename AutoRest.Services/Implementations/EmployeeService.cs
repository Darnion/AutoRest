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
    public class EmployeeService : IEmployeeService, IServiceAnchor
    {
        private readonly IEmployeeReadRepository employeeReadRepository;
        private readonly IEmployeeWriteRepository employeeWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPersonReadRepository personReadRepository;
        private readonly IMapper mapper;

        public EmployeeService(IEmployeeReadRepository employeeReadRepository,
            IEmployeeWriteRepository employeeWriteRepository,
            IUnitOfWork unitOfWork,
            IPersonReadRepository personReadRepository,
            IMapper mapper)
        {
            this.employeeReadRepository = employeeReadRepository;
            this.employeeWriteRepository = employeeWriteRepository;
            this.unitOfWork = unitOfWork;
            this.personReadRepository = personReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<EmployeeModel>> IEmployeeService.GetAllAsync(CancellationToken cancellationToken)
        {
            var employees = await employeeReadRepository.GetAllAsync(cancellationToken);
            var persons = await personReadRepository.GetByIdsAsync(employees.Select(x => x.PersonId).Distinct(), cancellationToken);
            var result = new List<EmployeeModel>();
            foreach (var employee in employees)
            {
                if (!persons.TryGetValue(employee.PersonId, out var person))
                {
                    continue;
                }
                var empl = mapper.Map<EmployeeModel>(employee);
                empl.Person = mapper.Map<PersonModel>(person);
                result.Add(empl);
            }

            return result;
        }

        async Task<EmployeeModel?> IEmployeeService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await employeeReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            var person = await personReadRepository.GetByIdAsync(item.PersonId, cancellationToken);
            var employee = mapper.Map<EmployeeModel>(item);
            employee.Person = mapper.Map<PersonModel>(person);
            return employee;
        }

        async Task<EmployeeModel> IEmployeeService.AddAsync(EmployeeRequestModel employeeRequestModel, CancellationToken cancellationToken)
        {
            var item = new Employee
            {
                Id = Guid.NewGuid(),
                EmployeeType = employeeRequestModel.EmployeeType,
                PersonId = employeeRequestModel.PersonId,
            };

            employeeWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<EmployeeModel>(item);
        }
        async Task<EmployeeModel> IEmployeeService.EditAsync(EmployeeRequestModel source, CancellationToken cancellationToken)
        {
            var targetEmployee = await employeeReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetEmployee == null)
            {
                throw new AutoRestEntityNotFoundException<Employee>(source.Id);
            }

            targetEmployee.EmployeeType = source.EmployeeType;

            var person = await personReadRepository.GetByIdAsync(source.PersonId, cancellationToken);
            targetEmployee.PersonId = person!.Id;
            targetEmployee.Person = person;

            employeeWriteRepository.Update(targetEmployee);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<EmployeeModel>(targetEmployee);
        }
        async Task IEmployeeService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetEmployee = await employeeReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetEmployee == null)
            {
                throw new AutoRestEntityNotFoundException<Employee>(id);
            }
            if (targetEmployee.DeletedAt.HasValue)
            {
                throw new AutoRestInvalidOperationException($"Работник с идентификатором {id} уже удален");
            }

            employeeWriteRepository.Delete(targetEmployee);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
