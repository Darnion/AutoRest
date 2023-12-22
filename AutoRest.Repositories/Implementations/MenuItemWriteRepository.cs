using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Context.Contracts.Models;
using AutoRest.Repositories.Contracts;

namespace AutoRest.Repositories.Implementations
{
    /// <inheritdoc cref="IMenuItemWriteRepository"/>
    public class MenuItemWriteRepository : BaseWriteRepository<MenuItem>,
        IMenuItemWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="MenuItemWriteRepository"/>
        /// </summary>
        public MenuItemWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
