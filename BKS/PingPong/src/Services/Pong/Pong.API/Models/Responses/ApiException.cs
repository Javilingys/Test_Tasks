namespace Pong.API.Models.Dtos.Responses
{
    /// <summary>
    /// класс API ошибки, предназначен для одинаковости 500-й ошибок в API
    /// </summary>
    public class ApiException : ApiBadResponse
    {
        public ApiException(int statusCode, string message = null, string details = null) 
            : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}
