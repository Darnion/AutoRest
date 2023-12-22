namespace AutoRest.Api.ModelsRequest.MenuItem
{
    public class CreateMenuItemRequest
    {
        /// <summary>
        /// Название позиции меню
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Стоимость позиции
        /// </summary>
        public decimal Cost { get; set; } = 0;
    }
}
