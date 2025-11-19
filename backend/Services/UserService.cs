using backend.Data;
using backend.Dtos;
using backend.Models;
using backend.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography.X509Certificates;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        private AppDbContext context;
        private readonly IUserContextService userContextService;


        public UserService(IUserRepository userRepository, AppDbContext context, IUserContextService userContextService)
        {
            this.userRepository = userRepository;
            this.context = context;
            this.userContextService = userContextService;
        }

        public async Task<List<User>> GetAllUsers()
        {
            List<User> users = await userRepository.GetAllUsers();
            return users;
        }

        public async Task<User> GetById(int id)
        {
            User? user = await userRepository.GetById(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"There isn't user belong with this id:{id}");
            }
            return user;
        }

        public async Task AddUser(UserRegisterRequest userRequest)
        {
            if(String.IsNullOrEmpty(userRequest.Username))
            {
                throw new ArgumentNullException("Username cannot null");
            }

            User user = new User()
            {
                Username = userRequest.Username
            };

            using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await userRepository.AddUser(user);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateUser(int id, UserRequest userRequest)
        {
            if(userRequest == null)
            {
                throw new ArgumentNullException("User cannot be null");
            }

            User dbUser = await GetById(id);
            int currentUserId = userContextService.GetUserId();

            if(dbUser.Id != currentUserId)
            {
                throw new Exception("This user is not yours, so you cannot update it.");
            }

            if(userRequest.Username != null)
            {
                dbUser.Username = userRequest.Username;
            }

            using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await userRepository.UpdateUser(dbUser);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteUser(int id)
        {
            User user = await GetById(id);
            int currentUserId = userContextService.GetUserId();

            if (user.Id != currentUserId)
            {
                throw new Exception("This user is not yours, so you cannot delete it.");
            }

            using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await userRepository.DeleteUser(user);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<User> GetByUserName(string userName)
        {
            User user = await userRepository.GetByUserName(userName);
            return user;
        }
    }
}
