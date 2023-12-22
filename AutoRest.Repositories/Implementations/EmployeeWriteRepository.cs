using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Context.Contracts.Models;
using AutoRest.Repositories.Contracts;

namespace AutoRest.Repositories.Implementations
{
    /// <inheritdoc cref="IEmployeeWriteRepository"/>
    public class EmployeeWriteRepository : BaseWriteRepository<Employee>,
        IEmployeeWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EmployeeWriteRepository"/>
        /// </summary>
        public EmployeeWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
