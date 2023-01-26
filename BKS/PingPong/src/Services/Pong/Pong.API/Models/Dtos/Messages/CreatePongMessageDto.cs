using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pong.API.Models.Dtos.Messages
{
    /// <summary>
    /// Дто для создания понг сообщения
    /// </summary>
    public class CreatePongMessageDto
    {
        [Required]
        [MinLength(1)]
        public string Message { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [JsonPropertyName("user")]
        public int UserId { get; set; }
    }
}
