using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Context.Contracts.Models;
using AutoRest.Repositories.Contracts;

namespace AutoRest.Repositories.Implementations
{
    /// <inheritdoc cref="ILoyaltyCardWriteRepository"/>
    public class LoyaltyCardWriteRepository : BaseWriteRepository<LoyaltyCard>,
        ILoyaltyCardWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="LoyaltyCardWriteRepository"/>
        /// </summary>
        public LoyaltyCardWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
