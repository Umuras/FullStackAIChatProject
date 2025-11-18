using backend.Data;
using backend.Dtos;
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
        private readonly IUserContextService userContextService;

        public MessageService(IMessageRepository messageRepository, AppDbContext context, IUserContextService userContextService)
        {
            this.messageRepository = messageRepository;
            this.context = context;
            this.userContextService = userContextService;
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

        public async Task AddMessageAsync(MessageRequest messageRequest)
        {
            if(!String.IsNullOrEmpty(messageRequest.MessageText))
            {
                throw new ArgumentNullException("Message cannot be null");
            }

            Message message = new Message()
            {
                MessageText = messageRequest.MessageText
            };

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

        public async Task UpdateMessageAsync(int id, MessageRequest messageRequest)
        {
            if(!String.IsNullOrEmpty(messageRequest.MessageText))
            {
                throw new ArgumentNullException("Message cannot be null");
            }

            Message dbMessage = await GetMessageByIdAsync(id);
            int currentUserId = userContextService.GetUserId();

            if(dbMessage.UserId != currentUserId)
            {
                throw new Exception("This message is not yours, so you can't change this message");
            }

            if(messageRequest.MessageText != null)
            {
                dbMessage.MessageText = messageRequest.MessageText;
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

            int currentUserId = userContextService.GetUserId();

            if (message.UserId != currentUserId)
            {
                throw new Exception("This message is not yours, so you can't delete this message");
            }

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
