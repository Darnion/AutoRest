using AutoRest.Context.Contracts.Models;

namespace AutoRest.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="OrderItem"/>
    /// </summary>
    public interface IOrderItemWriteRepository : IRepositoryWriter<OrderItem>
    {
    }
}
