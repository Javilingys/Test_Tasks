using Pong.API.Models.Dtos.Responses;

namespace Pong.API.Models.Responses.Messages
{
    /// <summary>
    /// Ответ сервиса при добавление сообщения
    /// </summary>
    public class AddResponse : BaseResponse
    {
        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        public Guid Id { get; set; }

        public static AddResponse Success(Guid id)
        {
            return new AddResponse
            {
                Id = id,
                Status = Enumerations.Enums.ResponseStatus.Success
            };
        }
    }
}
