namespace SmartWash.API.Domain.Entities
{
    public sealed class SaleData
    {
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductIdAmount { get; set; }
    }
}