using System.Text.Json.Serialization;

namespace backend.Dtos
{
    public record SentimentData
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }
        [JsonPropertyName("score")]
        public float Score { get; set; }
    }
}
