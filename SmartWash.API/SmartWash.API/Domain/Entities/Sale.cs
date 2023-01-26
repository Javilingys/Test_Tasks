using System;
using System.Collections.Generic;

namespace SmartWash.API.Domain.Entities
{
    public sealed class Sale : Entity
    {
        public DateTimeOffset SaleDate { get; set; }
        public int SalesPointId { get; set; }
        public int? BuyerId { get; set; }
        public List<SaleData> SalesData { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
