using backend.Dtos;
using backend.Models;
using System.Diagnostics;
using System.Text.Json;

namespace backend.Services
{
    public class AIService : IAIService
    {
        private readonly HttpClient httpClient;
        private string spaceUrl = "https://auk97-sentiment-service.hf.space/gradio_api/call/analyze";

        public AIService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<SentimentData> SendMessageAIAsync(Message message)
        {
            HttpResponseMessage postResponse = await httpClient.PostAsJsonAsync(spaceUrl,
                new { data = new string[] { message.MessageText } });
            Dictionary<string, object>? postContent = await postResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
            string? eventId = postContent["event_id"].ToString();

            var getResponse = await httpClient.GetAsync($"{spaceUrl}/{eventId}");
            var body = await getResponse.Content.ReadAsStringAsync();
            var lines = body.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            // data: ile başlayan satırı bul
            var dataLine = lines.FirstOrDefault(l => l.StartsWith("data:"));

            //string? label = sentimentData[0]["label"].ToString();
            //float score = float.Parse(sentimentData[0]["score"].ToString());
            SentimentData sentiment = new SentimentData();

            if (dataLine != null)
            {
                var jsonPart = dataLine.Substring("data:".Length).Trim();

                // artık JSON string
                var sentimentList = JsonSerializer.Deserialize<List<SentimentData>>(jsonPart);

                sentiment = sentimentList[0];
                Console.WriteLine($"Label: {sentiment.Label}, Score: {sentiment.Score}");
            }

            SentimentData data = new SentimentData()
            {
                Label = sentiment.Label,
                Score = sentiment.Score
            };

            return data;
        }
    }
}
