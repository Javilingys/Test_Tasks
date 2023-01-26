using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pong.API.Models.Dtos.Messages
{
    public class DeletePongMessageRequestDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        [JsonPropertyName("user")]
        public int UserId { get; set; }

        [Required]
        public Guid Id { get; set; }
    }
}
