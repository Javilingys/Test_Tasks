using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pong.API.Entities;
using Pong.API.Infrastructure;
using Pong.API.Models.Dtos.Messages;
using Pong.API.Models.Dtos.Responses;
using Pong.API.Models.Responses.Messages;
using System.Net;

namespace Pong.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly PongDbContext _dbContext;
        private readonly ILogger<MessagesController> _logger;
        private readonly IMapper _mapper;

        // В целях тестового проекта, решил исопльзовать контекст на прямую
        // Чтобы не создавать юнит ов форк с репозиторием
        public MessagesController(PongDbContext dbContext, 
                                  ILogger<MessagesController> logger, 
                                  IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }


        // Опять же в целях простоты, так как и так замудрил слегка), вся логика будет здесь, в методах контроллера


        // POST: api/messages/list
        // Получить сообщения для юзера по его идентификатору
        [HttpPost]
        [Route("list")]
        [ProducesResponseType(typeof(ListResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ListResponse>> GetMessages(
            [FromBody] ListPongMessagesRequestDto requestDto
        )
        {
            // Получить список сообщений, которые отправлены определнным юзером и
            // если в запросе есть айди сообщения, то еще и айди этого сообщения
            var resultMessages = await _dbContext.PongMessages
                .AsNoTracking()
                .Where(x => x.UserId == requestDto.UserId
                    && (requestDto.Id == null || x.Id.Equals(requestDto.Id)))
                .ProjectTo<PongMessageToReturnDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var response = ListResponse.Success(
                _mapper.Map<List<PongMessageToReturnDto>>(resultMessages)
            );

            return Ok(response);
        }


        // POST: api/messages/add
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(AddResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiBadResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AddResponse>> CreateMessage(
            [FromBody] CreatePongMessageDto createPongMessageDto
        )
        {
            _logger.LogDebug("--> Айди пользователя в реквесте: {userId}", createPongMessageDto.UserId);

            var pongMessage = _mapper.Map<PongMessage>(createPongMessageDto);

            _dbContext.PongMessages.Add(pongMessage);
            bool saveResult = await _dbContext.SaveChangesAsync() > 0;
            
            if (saveResult == false)
            {
                _logger.LogWarning("--> Сообщение {message} для пользователя {userId} не было создано",
                    createPongMessageDto.Message, createPongMessageDto.UserId);
                return BadRequest(new ApiBadResponse((int)HttpStatusCode.BadRequest, "Ошибка создания сообщения"));
            }

            return Ok(AddResponse.Success(pongMessage.Id));
        }

        // POST: api/messages/delete
        /// <param name="id">GUID индетфикатор сообщения</param>
        [HttpPost]
        [Route("delete")]
        [ProducesResponseType(typeof(DeleteResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiBadResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<DeleteResponse>> DeleteMessage(
            [FromBody] DeletePongMessageRequestDto requestDto
        )
        {
            // Сообщение для удаления
            var messageForDelete = await _dbContext.PongMessages
                .FirstOrDefaultAsync(x => x.UserId == requestDto.UserId
                    && x.Id.Equals(requestDto.Id));

            if (messageForDelete == null)
            {
                _logger.LogWarning("--> Сообщение {messageId} для пользователя {userId} не было найдено, чтобы его удалить..",
                    requestDto.Id, requestDto.UserId);
                return NotFound(new ApiBadResponse((int)HttpStatusCode.NotFound, "Сообщение для удаления не найдено"));
            }

            _dbContext.Entry(messageForDelete).State = EntityState.Deleted;
            bool saveResult = await _dbContext.SaveChangesAsync() > 0;

            if (saveResult == false)
            {
                _logger.LogWarning("--> Ошибка при сохранении сообщения {messageId} для пользователя {userId}",
                    requestDto.Id, requestDto.UserId);
                return BadRequest(new ApiBadResponse((int)HttpStatusCode.BadRequest, "Ошибка удаления сообщения"));
            }

            return Ok(DeleteResponse.Success());
        }
    }
}
