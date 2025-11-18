using backend.Models;

namespace backend.Repositories
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetAllMessagesAsync();
        Task<Message> GetMessageByIdAsync(int id);
        Task AddMessageAsync(Message message);
        Task UpdateMessageAsync(Message message);
        Task DeleteMessageAsync(Message message);
    }
}
