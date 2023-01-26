using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SmartWash.API.Application.Contracts;
using SmartWash.API.Domain.Contracts;
using SmartWash.API.Domain.Entities;
using SmartWash.API.DTOs;

namespace SmartWash.API.Application.Service
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SaleService> _logger;

        public SaleService(IUnitOfWork unitOfWork, ILogger<SaleService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<int>> SaleProduct(SaleForCreateDto saleDto)
        {
            //await _unitOfWork.StartTransaction();

            try
            {
                // получаем точку продажи
                SalesPoint salesPoint = await _unitOfWork.SalesPointRepository.GetById(saleDto.SalesPointId);

                if (salesPoint is null)
                {
                    // Если точка продажи не найдена, возвращаем not found респонс
                    return ApiResponse<int>.CreateFailureNotFound("Точка продажи не найдена");
                }

                Product product = await _unitOfWork.ProductRepository.GetById(saleDto.SaledProductId);

                if (product is null)
                {
                    return ApiResponse<int>.CreateFailureNotFound("Продаваемый продукт отсуствует");
                }

                // вытягиваем из точки продажи этот продукт для продажи
                ProvidedProduct productForSale =
                    salesPoint.ProvidedProducts.FirstOrDefault(x => x.ProductId == saleDto.SaledProductId);

                if (productForSale is null)
                {
                    // Если продукта для продажи нет или его количество меньше запрашиевомого, то вовзвращаем not found респонс
                    return ApiResponse<int>.CreateFailureNotFound("Продукт не найден");
                }

                if (productForSale.ProductQuantity < saleDto.Quantity)
                {
                    return ApiResponse<int>.CreateFailure("Запрашиваемое количество продукта больше, чем находится в продаже");
                }

                // Изменяется количество доступных товаров в точке продажи, согласно количеству проданных товаров
                productForSale.ProductQuantity -= saleDto.Quantity;

                // Формируется экземпляр сущности Sale, и записывается в базу данных
                Sale sale = new()
                {
                    BuyerId = saleDto.BuyerId,
                    SaleDate = DateTimeOffset.UtcNow,
                    SalesPointId = saleDto.SalesPointId,
                    TotalAmount = saleDto.Quantity * product.Price,
                    SalesData = new()
                    {
                        new SaleData()
                        {
                            ProductId = product.Id,
                            ProductQuantity = saleDto.Quantity,
                            ProductIdAmount = saleDto.Quantity * product.Price
                        }
                    }
                };

                _unitOfWork.SaleRepository.Add(sale);
                await _unitOfWork.SaveAsync();

                if (saleDto.BuyerId.HasValue)
                {
                    Buyer buyer = await _unitOfWork.BuyerRepository.GetById(saleDto.BuyerId.Value);

                    BuyerSale buyerSale = new()
                    {
                        BuyerId = buyer.Id,
                        SaleId = sale.Id
                    };
                    _unitOfWork.BuyerSaleRepository.Add(buyerSale);
                }

                await _unitOfWork.SaveAsync();

                //await _unitOfWork.CommitTransaction();

                return ApiResponse<int>.CreateSuccess(sale.Id);
            }
            catch (Exception ex)
            {
                //await _unitOfWork.RollbackTransaction();
                _logger.LogError(ex, "Ошибка при продаже продукта {saledProductId} их точки продаж {salesPointId}",
                    saleDto.SaledProductId, saleDto.SalesPointId);

                return ApiResponse<int>.CreateFailure("Незивестная ошибка.");
            }
        }
    }
}
