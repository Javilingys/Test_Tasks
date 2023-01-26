using System.Threading.Tasks;

namespace SmartWash.API.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task StartTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();

        ISalesPointRepository SalesPointRepository { get; }
        IProductRepository ProductRepository { get; }
        IBuyerRepository BuyerRepository { get; }
        IBuyerSaleRepository BuyerSaleRepository { get; }
        ISaleRepository SaleRepository { get; }

        Task SaveAsync();
    }
}
