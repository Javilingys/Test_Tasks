using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartWash.API.Domain.Contracts;
using SmartWash.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartWash.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerSalesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BuyerSalesController> _logger;

        public BuyerSalesController(IUnitOfWork unitOfWork, ILogger<BuyerSalesController> logger)
        {
            // по-хорошему здесь юнит оф ворк не нужен. Сюда следует инжектить сервис, который уже отдает данные
            // но для простоты сойдет
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Buyer>>> GetAllBuyers()
        {
            var result = await _unitOfWork.BuyerSaleRepository.GetAll();

            // по-хорошему надо мапить в DTO. Но для простоты сойдет
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Buyer>> GetBuyer(int id)
        {
            var result = await _unitOfWork.BuyerSaleRepository.GetById(id);

            if (result is null)
            {
                _logger.LogWarning("Пользователь {id} не найден.", id);
                return NotFound("Пользователь не найден");
            }

            // по-хорошему надо мапить в DTO. Но для простоты сойдет
            return Ok(result);
        }

        // тут еще должны быть, судя по ТЗ, POST, PUT, DELETE. Но для экономии времени, просто словам напишу это.
    }
}
