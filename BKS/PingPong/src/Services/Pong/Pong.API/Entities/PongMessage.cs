namespace Pong.API.Entities
{
    /// <summary>
    /// Сообщение
    /// </summary>
    public class PongMessage
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Идентификатор отправителя
        /// </summary>
        public int UserId { get; set; }
    }
}
