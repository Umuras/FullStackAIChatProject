using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext context;

        public MessageRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Message>> GetAllMessagesAsync()
        {
            return await context.Messages.Include(m => m.User).ToListAsync();
        }

        public async Task<Message> GetMessageByIdAsync(int id)
        {
            Message? message = await context.Messages.Include(m => m.User).FirstOrDefaultAsync(m => m.Id == id);
            return message;
        }

        public async Task AddMessageAsync(Message message)
        {
            await context.Messages.AddAsync(message);
        }

        public Task UpdateMessageAsync(Message message)
        {
            context.Messages.Update(message);
            return Task.CompletedTask;
        }

        public Task DeleteMessageAsync(Message message)
        {
            context.Messages.Remove(message);
            return Task.CompletedTask;
        }
    }
}
