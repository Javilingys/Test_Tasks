namespace Pong.API.Models.Dtos.Responses
{
    /// <summary>
    /// класс API ответа, предназначен для одинаковости 400х ошибок в API
    /// </summary>
    public class ApiBadResponse
    {
        public ApiBadResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
