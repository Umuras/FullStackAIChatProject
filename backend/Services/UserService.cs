using backend.Data;
using backend.Models;
using backend.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography.X509Certificates;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        private AppDbContext context;


        public UserService(IUserRepository userRepository, AppDbContext context)
        {
            this.userRepository = userRepository;
            this.context = context;
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

        public async Task AddUser(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("User doesn't null");
            }

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

        public async Task UpdateUser(int id, User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("User cannot be null");
            }

            User dbUser = await GetById(id);

            if(user.UserName != null)
            {
                dbUser.UserName = user.UserName;
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
    }
}
