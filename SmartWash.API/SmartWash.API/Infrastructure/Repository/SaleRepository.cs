using SmartWash.API.Domain.Contracts;
using SmartWash.API.Domain.Entities;

namespace SmartWash.API.Infrastructure.Repository
{
    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        public SaleRepository(AppDbContext context) 
            : base(context)
        {
        }
    }
}
