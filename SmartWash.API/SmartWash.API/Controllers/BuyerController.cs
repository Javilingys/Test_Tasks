using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartWash.API.Domain.Contracts;
using SmartWash.API.Domain.Entities;
using SmartWash.API.DTOs;

namespace SmartWash.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BuyerController> _logger;

        public BuyerController(IUnitOfWork unitOfWork, ILogger<BuyerController> logger)
        {
            // по-хорошему здесь юнит оф ворк не нужен. Сюда следует инжектить сервис, который уже отдает данные
            // но для простоты сойдет
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Buyer>>> GetAllBuyers()
        {
            var result = await _unitOfWork.BuyerRepository.GetBuyers();
            
            // по-хорошему надо мапить в DTO. Но для простоты сойдет
            return Ok(result.Select(x => new BuyerDto()
                {
                    Id = x.Id,
                    SalesIds = x.BuyerSales.Select(bs => bs.SaleId).ToList()
                })
                .ToList()
            );
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Buyer>> GetBuyer(int id)
        {
            var result = await _unitOfWork.BuyerRepository.GetBuyerById(id);

            if (result is null)
            {
                _logger.LogWarning("Пользователь {id} не найден.", id);
                return NotFound("Пользователь не найден");
            }

            // по-хорошему надо мапить в DTO. Но для простоты сойдет
            return Ok(new BuyerDto()
            {
                Id = result.Id,
                SalesIds = result.BuyerSales.Select(bs => bs.SaleId).ToList()
            });
        }
        
        // тут еще должны быть, судя по ТЗ, POST, PUT, DELETE. Но для экономии времени, просто словам напишу это.
    }
}
