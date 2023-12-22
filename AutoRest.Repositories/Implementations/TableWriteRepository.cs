using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Context.Contracts.Models;
using AutoRest.Repositories.Contracts;

namespace AutoRest.Repositories.Implementations
{
    /// <inheritdoc cref="ITableWriteRepository"/>
    public class TableWriteRepository : BaseWriteRepository<Table>,
        ITableWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TableWriteRepository"/>
        /// </summary>
        public TableWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
