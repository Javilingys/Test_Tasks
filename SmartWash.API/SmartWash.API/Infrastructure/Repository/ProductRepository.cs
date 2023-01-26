using SmartWash.API.Domain.Contracts;
using SmartWash.API.Domain.Entities;

namespace SmartWash.API.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) 
            : base(context)
        {
        }
    }
}
