using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Context.Contracts.Models;
using AutoRest.Repositories.Contracts;

namespace AutoRest.Repositories.Implementations
{
    /// <inheritdoc cref="IPersonWriteRepository"/>
    public class PersonWriteRepository : BaseWriteRepository<Person>,
        IPersonWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PersonWriteRepository"/>
        /// </summary>
        public PersonWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
