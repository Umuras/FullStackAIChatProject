using System.Text.Json.Serialization;

namespace backend.Dtos
{
    public record AiResponse
    {
        [JsonPropertyName("event")]
        public string EventName { get; set; }
        [JsonPropertyName("data")]
        public List<SentimentData> Data { get; set; }
    }
}
