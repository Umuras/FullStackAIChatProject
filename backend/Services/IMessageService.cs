using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IMessageService
    {
        Task<List<Message>> GetAllMessagesAsync();
        Task<Message> GetMessageByIdAsync(int id);
        Task<Message> AddMessageAsync(MessageRequest messageRequest);
        Task<Message> UpdateMessageAsync(int id, MessageRequest messageRequest);
        Task DeleteMessageAsync(int id);
        List<MessageResponse> ChangeMessagesResponse(List<Message> messages);
        MessageResponse ChangeMessageResponse(Message message);
    }
}
