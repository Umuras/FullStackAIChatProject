using backend.Dtos;

namespace backend.Services
{
    public interface IAuthService
    {
        public Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request);
        public Task<UserRegisterResponse> RegisterUserAsync(UserRegisterRequest request);
    }
}
