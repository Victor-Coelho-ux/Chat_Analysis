using System.Text.Json.Serialization;
using ChatAnalysis.Domain.DTO;

namespace ChatAnalysis.Domain.Serialization 
{
    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, WriteIndented = true)]
    [JsonSerializable(typeof(MessageDto))]
    public partial class AppJsonSerializerContext : JsonSerializerContext
    {
    }
}


