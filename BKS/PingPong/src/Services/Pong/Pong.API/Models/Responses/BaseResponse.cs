
using static Pong.API.Enumerations.Enums;

namespace Pong.API.Models.Dtos.Responses
{
    /// <summary>
    /// Базовый класс для всех респонсов, содержит в себе только поле статуса,  <br></br>
    /// </summary>
    public abstract class BaseResponse
    {
        public ResponseStatus Status { get; set; }
    }
}
