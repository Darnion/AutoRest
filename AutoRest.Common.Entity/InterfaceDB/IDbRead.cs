using AutoRest.Common.Entity.EntityInterface;

namespace AutoRest.Common.Entity.InterfaceDB
{
    /// <summary>
    /// Интерфейс получения записей из бд
    /// </summary>
    public interface IDbRead
    {
        /// <summary>
        /// Предоставляет функциональные возможности для выполнения запросов
        /// </summary> 
        IQueryable<TEntity> Read<TEntity>() where TEntity : class, IEntity;
    }
}