using Pong.API.Models.Dtos.Messages;
using Pong.API.Models.Dtos.Responses;

namespace Pong.API.Models.Responses.Messages
{
    /// <summary>
    /// Ответ сервиса при запросе списка сообщений
    /// </summary>
    public class ListResponse : BaseResponse
    {
        public IEnumerable<PongMessageToReturnDto> Messages { get; set; }

        public static ListResponse Success(List<PongMessageToReturnDto> messages)
        {
            return new ListResponse
            {
                Messages = messages,
                Status = Enumerations.Enums.ResponseStatus.Success
            };
        }
    }
}
