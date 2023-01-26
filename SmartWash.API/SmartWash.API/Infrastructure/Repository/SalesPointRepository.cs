using SmartWash.API.Domain.Contracts;
using SmartWash.API.Domain.Entities;

namespace SmartWash.API.Infrastructure.Repository
{
    public class SalesPointRepository : BaseRepository<SalesPoint>, ISalesPointRepository
    {
        public SalesPointRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
