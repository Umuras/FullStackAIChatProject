using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IAIService
    {
        Task<SentimentData> SendMessageAIAsync(Message message);
    }
}
