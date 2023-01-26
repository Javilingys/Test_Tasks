
namespace Ping.Dtos
{
    /// <summary>
    /// Ответ на добавление сообщения
    /// </summary>
    public class AddResponseDto
    {
        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Статус ответа
        /// </summary>
        public int Status { get; set; }
    }
}
