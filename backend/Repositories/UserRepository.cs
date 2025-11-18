using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            User? user = await context.Users.FindAsync(id);
            return user;
        }

        public async Task AddUser(User user)
        {
            await context.Users.AddAsync(user);
        }

        public Task UpdateUser(User user)
        {
            context.Users.Update(user);
            return Task.CompletedTask;
        }

        public Task DeleteUser(User user)
        {
            context.Users.Remove(user);
            return Task.CompletedTask;
        }

        public async Task<User> GetByUserName(string username)
        {
            User? user = await context.Users.FirstOrDefaultAsync(u => u.Username.Equals(username));
            return user;
        }
    }
}
