using backend.Models;

namespace backend.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetById(int id);
        Task AddUser(User user);
        Task UpdateUser(int id, User user);
        Task DeleteUser(int id);
    }
}
