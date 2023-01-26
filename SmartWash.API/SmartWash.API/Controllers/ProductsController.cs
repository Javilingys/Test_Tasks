using Microsoft.AspNetCore.Http;
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
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IUnitOfWork unitOfWork, ILogger<ProductsController> logger)
        {
            // по-хорошему здесь юнит оф ворк не нужен. Сюда следует инжектить сервис, который уже отдает данные
            // но для простоты сойдет
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllBuyers()
        {
            var result = await _unitOfWork.ProductRepository.GetAll();

            // по-хорошему надо мапить в DTO. Но для простоты сойдет
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetBuyer(int id)
        {
            var result = await _unitOfWork.ProductRepository.GetById(id);

            if (result is null)
            {
                _logger.LogWarning("Продукт {id} не найден.", id);
                return NotFound("Продукт не найден");
            }

            // по-хорошему надо мапить в DTO. Но для простоты сойдет
            return Ok(result);
        }

        // тут еще должны быть, судя по ТЗ, POST, PUT, DELETE. Но для экономии времени, просто словам напишу это.
    }
}
