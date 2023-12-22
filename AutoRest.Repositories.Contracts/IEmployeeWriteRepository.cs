using AutoRest.Context.Contracts.Models;

namespace AutoRest.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Employee"/>
    /// </summary>

    public interface IEmployeeWriteRepository : IRepositoryWriter<Employee>
    {
    }
}
