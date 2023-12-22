using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Context.Contracts.Models;
using AutoRest.Repositories.Contracts;

namespace AutoRest.Repositories.Implementations
{
    /// <inheritdoc cref="IOrderItemWriteRepository"/>
    public class OrderItemWriteRepository : BaseWriteRepository<OrderItem>,
        IOrderItemWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="OrderItemWriteRepository"/>
        /// </summary>
        public OrderItemWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
