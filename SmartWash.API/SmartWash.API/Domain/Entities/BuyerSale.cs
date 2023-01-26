namespace SmartWash.API.Domain.Entities
{
    public class BuyerSale : Entity
    {
        public int BuyerId { get; set; }
        public int SaleId { get; set; }

        public Buyer Buyer { get; set; }
        public Sale Sale { get; set; }
    }
}
