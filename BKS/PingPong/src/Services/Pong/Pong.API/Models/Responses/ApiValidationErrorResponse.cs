namespace Pong.API.Models.Dtos.Responses
{
    public class ApiValidationErrorResponse : ApiBadResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {
        }

        public List<string> Errors { get; set; }

    }
}
