using SmartWash.API.Domain.Contracts;
using SmartWash.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartWash.API.Infrastructure.Repository
{
    public class BuyerSaleRepository : BaseRepository<BuyerSale>, IBuyerSaleRepository
    {
        public BuyerSaleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
