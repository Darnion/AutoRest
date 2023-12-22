namespace TimeTable203.Api.ModelsRequest.Person
{
    public class PersonRequest : CreatePersonRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
