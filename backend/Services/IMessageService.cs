using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IMessageService
    {
        Task<List<Message>> GetAllMessagesAsync();
        Task<Message> GetMessageByIdAsync(int id);
        Task AddMessageAsync(MessageRequest messageRequest);
        Task UpdateMessageAsync(int id, MessageRequest messageRequest);
        Task DeleteMessageAsync(int id);
    }
}
