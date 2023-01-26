namespace SmartWash.API.Domain.Entities
{
    public sealed class Product : Entity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// стоимость
        /// </summary>
        public decimal Price { get; set; }
    }
}
