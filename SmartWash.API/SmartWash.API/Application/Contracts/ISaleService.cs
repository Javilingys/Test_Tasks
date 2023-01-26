using System.Threading.Tasks;
using SmartWash.API.DTOs;

namespace SmartWash.API.Application.Contracts
{
    public interface ISaleService
    {
        /// <summary>
        /// Продать продукт
        /// </summary>
        /// <returns>Илентификатор продажи</returns>
        Task<ApiResponse<int>> SaleProduct(SaleForCreateDto saleDto);
    }
}
