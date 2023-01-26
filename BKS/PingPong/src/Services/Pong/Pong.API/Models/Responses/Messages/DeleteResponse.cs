using Pong.API.Models.Dtos.Responses;

namespace Pong.API.Models.Responses.Messages
{
    /// <summary>
    /// Ответ сервиса при удалиние сообщения
    /// </summary>
    public class DeleteResponse : BaseResponse
    {
        public static DeleteResponse Success()
        {
            return new DeleteResponse
            {
                Status = Enumerations.Enums.ResponseStatus.Success
            };
        }
    }
}
