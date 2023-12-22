namespace AutoRest.Services.Contracts.Exceptions
{
    /// <summary>
    /// Запрашиваемая сущность не найдена
    /// </summary>
    public class AutoRestEntityNotFoundException<TEntity> : AutoRestNotFoundException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AutoRestEntityNotFoundException{TEntity}"/>
        /// </summary>
        public AutoRestEntityNotFoundException(Guid id)
            : base($"Сущность {typeof(TEntity)} c id = {id} не найдена.")
        {
        }
    }
}
