namespace SmartWash.API.DTOs
{
    public class SaleForCreateDto
    {
        public int SalesPointId { get; set; }
        public int SaledProductId { get; set; }
        public int Quantity { get; set; }
        public int? BuyerId { get; set; }
    }
}
