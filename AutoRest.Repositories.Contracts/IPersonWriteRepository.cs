using AutoRest.Context.Contracts.Models;

namespace AutoRest.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Person"/>
    /// </summary>
    public interface IPersonWriteRepository : IRepositoryWriter<Person>
    {
    }
}
