namespace AutoRest.Services.Contracts.Exceptions
{
    /// <summary>
    /// Ошибка выполнения операции
    /// </summary>
    public class AutoRestInvalidOperationException : AutoRestException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AutoRestInvalidOperationException"/>
        /// с указанием сообщения об ошибке
        /// </summary>
        public AutoRestInvalidOperationException(string message)
            : base(message)
        {

        }
    }
}
