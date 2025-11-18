using backend.Data;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;
        private readonly AppDbContext context;

        public MessageService(IMessageRepository messageRepository, AppDbContext context)
        {
            this.messageRepository = messageRepository;
            this.context = context;
        }

        public async Task<List<Message>> GetAllMessagesAsync()
        {
            List<Message> messages = await messageRepository.GetAllMessagesAsync();
            return messages;
        }

        public async Task<Message> GetMessageByIdAsync(int id)
        {
            Message message = await messageRepository.GetMessageByIdAsync(id);
            if (message == null)
            {
                throw new KeyNotFoundException($"There isn't message belong with this id:{id}");
            }
            return message;
        }

        public async Task AddMessageAsync(Message message)
        {
            if(message  == null)
            {
                throw new ArgumentNullException("Message cannot be null");
            }

            using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await messageRepository.AddMessageAsync(message);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateMessageAsync(int id, Message message)
        {
            if(message == null)
            {
                throw new ArgumentNullException("Message cannot be null");
            }

            Message dbMessage = await GetMessageByIdAsync(id);

            if(message.MessageText != null)
            {
                dbMessage.MessageText = message.MessageText;
            }

            using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await messageRepository.UpdateMessageAsync(dbMessage);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteMessageAsync(int id)
        {
            Message message = await GetMessageByIdAsync(id);

            using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await messageRepository.DeleteMessageAsync(message);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
