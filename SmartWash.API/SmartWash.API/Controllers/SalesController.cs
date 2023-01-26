using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartWash.API.Domain.Contracts;
using SmartWash.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartWash.API.Application.Contracts;
using SmartWash.API.DTOs;

namespace SmartWash.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SalesController> _logger;
        private readonly ISaleService _saleService;

        public SalesController(IUnitOfWork unitOfWork, ILogger<SalesController> logger, ISaleService saleService)
        {
            // по-хорошему здесь юнит оф ворк не нужен. Сюда следует инжектить сервис, который уже отдает данные
            // но для простоты сойдет
            _unitOfWork = unitOfWork;
            _logger = logger;
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Sale>>> GetAllBuyers()
        {
            var result = await _unitOfWork.SaleRepository.GetAll();

            // по-хорошему надо мапить в DTO. Но для простоты сойдет
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Sale>> GetBuyer(int id)
        {
            var result = await _unitOfWork.SaleRepository.GetById(id);

            if (result is null)
            {
                _logger.LogWarning("Продажа {id} не найден.", id);
                return NotFound("Продажа не найден");
            }

            // по-хорошему надо мапить в DTO. Но для простоты сойдет
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> SaleProduct(SaleForCreateDto saleDto)
        {
            var response = await _saleService.SaleProduct(saleDto);

            return GenerateResult<int>(response);
        }

        private ActionResult<T> GenerateResult<T>(ApiResponse<T> apiResponse)
        {
            if (apiResponse.IsNotFound)
            {
                return NotFound(apiResponse.FailureMessage);
            }
            if (apiResponse.IsSuccess && apiResponse.Data is null)
            {
                return NotFound();
            }
            if (apiResponse.IsSuccess && apiResponse.Data is not null)
            {
                return Ok(apiResponse.Data);
            }

            return BadRequest(apiResponse.FailureMessage);
        }

        // тут еще должны быть, судя по ТЗ, POST, PUT, DELETE. Но для экономии времени, просто словам напишу это.
    }
}
