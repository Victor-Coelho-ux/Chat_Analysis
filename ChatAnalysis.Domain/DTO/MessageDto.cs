using System.Text.Json.Serialization;

namespace ChatAnalysis.Domain.DTO
{
    public class MessageDto
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
