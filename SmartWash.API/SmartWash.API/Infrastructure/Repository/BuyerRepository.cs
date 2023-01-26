using SmartWash.API.Domain.Contracts;
using SmartWash.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartWash.API.Infrastructure.Repository
{
    public class BuyerRepository : BaseRepository<Buyer>, IBuyerRepository
    {
        private readonly AppDbContext _context;

        public BuyerRepository(AppDbContext context) 
            : base(context)
        {
            _context = context;
        }

        public async Task<Buyer> GetBuyerById(int id)
        {
            return await _context.Buyers
                .Include(x => x.BuyerSales)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Buyer>> GetBuyers()
        {
            return await _context.Buyers
                .Include(x => x.BuyerSales)
                .ToListAsync();
        }
    }
}
