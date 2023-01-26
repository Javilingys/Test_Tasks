using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pong.API.Infrastructure;
using Pong.API.Models.Dtos.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pong.API.Controllers
{
    // Контроллер чисто тестовый, проверить, какой респонс получается с ошибок, возможно удалю его перед отправкой
    // Но если он тут, то не удалил, забыл))
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        public BuggyController()
        {
        }

        [HttpGet("bad-request")]
        public async Task<IActionResult> Get400Error()
        {
            return BadRequest(new ApiBadResponse(400, "Bad Request was happened"));
        }

        // Передасть строку
        [HttpGet("validation/{id}")]
        public async Task<IActionResult> Get400ValidationError(int id)
        {
            return Ok(id);
        }

        [Authorize]
        [HttpGet("authorize")]
        public async Task<IActionResult> Get401Error()
        {
            return Ok("Secret info");
        }

        [HttpGet("not-found")]
        public async Task<IActionResult> Get404Error()
        {

            return NotFound(new ApiBadResponse(404, "Not found was happaned"));
        }

        [HttpGet("server-error")]
        public async Task<IActionResult> Get500Error()
        {
            object nullObj = null;

            string name = nullObj.ToString();

            return Ok();
        }
    }
}
