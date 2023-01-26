using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ping.Dtos
{
    /// <summary>
    /// Дто для создания понг сообщения
    /// </summary>
    public class CreatePongMessageRequestDto
    {
        public string Message { get; set; }

        public int User { get; set; }
    }
}
