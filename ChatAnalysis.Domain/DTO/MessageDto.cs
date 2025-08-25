using System.Text.Json.Serialization;

namespace ChatAnalysis.Domain.DTO
{
    public class MessageDto
    {
        
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
