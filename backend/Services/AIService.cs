using backend.Dtos;
using backend.Models;
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
            var resultContent = await getResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();

            var sentimentData = ((JsonElement)resultContent["data"]).Deserialize<List<Dictionary<string, object>>>();

            string? label = sentimentData[0]["label"].ToString();
            float score = float.Parse(sentimentData[0]["score"].ToString());

            SentimentData data = new SentimentData()
            {
                Label = label,
                Score = score
            };

            return data;
        }
    }
}
