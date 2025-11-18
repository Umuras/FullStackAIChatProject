using backend.Models;

namespace backend.Services
{
    public interface IMessageService
    {
        Task<List<Message>> GetAllMessagesAsync();
        Task<Message> GetMessageByIdAsync(int id);
        Task AddMessageAsync(Message message);
        Task UpdateMessageAsync(int id, Message message);
        Task DeleteMessageAsync(int id);
    }
}
