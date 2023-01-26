using System.Collections.Generic;
using System.Threading.Tasks;
using SmartWash.API.Domain.Entities;

namespace SmartWash.API.Domain.Contracts
{
    public interface IBuyerRepository : IBaseRepository<Buyer>
    {
        Task<Buyer> GetBuyerById(int id);
        Task<List<Buyer>> GetBuyers();
    }
}
