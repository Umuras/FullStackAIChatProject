namespace backend.Dtos
{
    public record SentimentData
    {
        public string Label { get; set; }
        public float Score { get; set; }
    }
}
