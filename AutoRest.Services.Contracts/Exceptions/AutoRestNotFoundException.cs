namespace AutoRest.Services.Contracts.Exceptions
{
    /// <summary>
    /// Запрашиваемый ресурс не найден
    /// </summary>
    public class AutoRestNotFoundException : AutoRestException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AutoRestNotFoundException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        public AutoRestNotFoundException(string message)
            : base(message)
        { }
    }
}
