using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetById(int id);
        Task<User> GetByUserName(string userName);
        Task AddUser(UserRegisterRequest userRequest);
        Task UpdateUser(int id, UserRequest userRequest);
        Task DeleteUser(int id);
    }
}
