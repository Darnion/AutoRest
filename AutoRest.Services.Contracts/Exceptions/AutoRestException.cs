namespace AutoRest.Services.Contracts.Exceptions
{
    /// <summary>
    /// Базовый класс исключений
    /// </summary>
    public abstract class AutoRestException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AutoRestException"/> без параметров
        /// </summary>
        protected AutoRestException() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AutoRestException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        protected AutoRestException(string message)
            : base(message) { }
    }
}
