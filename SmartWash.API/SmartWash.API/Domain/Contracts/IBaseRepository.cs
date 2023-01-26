using SmartWash.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartWash.API.Domain.Contracts
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
