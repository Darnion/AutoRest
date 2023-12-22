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
    public class TableService : ITableService, IServiceAnchor
    {
        private readonly ITableReadRepository tableReadRepository;
        private readonly ITableWriteRepository tableWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TableService(ITableReadRepository tableReadRepository,
            ITableWriteRepository tableWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.tableReadRepository = tableReadRepository;
            this.tableWriteRepository = tableWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<TableModel>> ITableService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await tableReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<TableModel>>(result);
        }

        async Task<TableModel?> ITableService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await tableReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<TableModel>(item);
        }

        async Task<TableModel> ITableService.AddAsync(TableRequestModel table, CancellationToken cancellationToken)
        {
            var item = new Table
            {
                Id = Guid.NewGuid(),
                Number = table.Number,
            };
            tableWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<TableModel>(item);
        }

        async Task<TableModel> ITableService.EditAsync(TableRequestModel source, CancellationToken cancellationToken)
        {
            var targetTable = await tableReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetTable == null)
            {
                throw new AutoRestEntityNotFoundException<Table>(source.Id);
            }

            targetTable.Number = source.Number;

            tableWriteRepository.Update(targetTable);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<TableModel>(targetTable);
        }

        async Task ITableService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetTable = await tableReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetTable == null)
            {
                throw new AutoRestEntityNotFoundException<Table>(id);
            }
            if (targetTable.DeletedAt.HasValue)
            {
                throw new AutoRestInvalidOperationException($"Столик с идентификатором {id} уже удален");
            }

            tableWriteRepository.Delete(targetTable);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
