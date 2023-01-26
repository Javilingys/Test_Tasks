using SmartWash.API.Domain.Contracts;
using System.Threading.Tasks;

namespace SmartWash.API.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private ISalesPointRepository _salesPointRepository;
        private IProductRepository _productRepository;
        private IBuyerRepository _buyerRepository;
        private IBuyerSaleRepository _buyerSaleRepository;
        private ISaleRepository _saleRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        
        public ISalesPointRepository SalesPointRepository => _salesPointRepository ??= new SalesPointRepository(_context);

        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);

        public IBuyerRepository BuyerRepository => _buyerRepository ??= new BuyerRepository(_context);
        public IBuyerSaleRepository BuyerSaleRepository => _buyerSaleRepository ??= new BuyerSaleRepository(_context);

        public ISaleRepository SaleRepository => _saleRepository ??= new SaleRepository(_context);

        public async Task StartTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransaction()
        {
            await _context.Database.RollbackTransactionAsync();
        }
        
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
