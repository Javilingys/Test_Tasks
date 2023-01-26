namespace Pong.API.Models.Dtos.Messages
{
    /// <summary>
    /// Дто для отдачи клиенту сообщения
    /// </summary>
    public class PongMessageToReturnDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
