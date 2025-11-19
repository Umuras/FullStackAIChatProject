using backend.Dtos;
using backend.JwtUtil;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;

        public AuthService(IUserService userService, ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request)
        {
            UserLoginResponse response = new UserLoginResponse();

            if (string.IsNullOrEmpty(request.Username))
            {
                throw new ArgumentNullException(nameof(request));
            }
            User user = await userService.GetByUserName(request.Username);
            GenerateTokenResponse generatedTokenInformation = await tokenService.GenerateToken(new JwtUtil.GenerateTokenRequest { UserId=user.Id, Username = request.Username });
            response.AuthToken = generatedTokenInformation.Token;
            response.AuthenticateResult = true;
            response.AccessTokenExpireDate = generatedTokenInformation.TokenExpireDate;

            return response;
        }

        public async Task<UserRegisterResponse> RegisterUserAsync(UserRegisterRequest request)
        {
            User dbUser = await userService.GetByUserName(request.Username);
            if(dbUser != null)
            {
                throw new ArgumentException("This username already exist, please try another name");
            }

            UserRegisterResponse userRegisterResponse = new UserRegisterResponse();

            await userService.AddUser(request);
            userRegisterResponse.RegisterMessage = "New user added";

            return userRegisterResponse;
        }
    }
}
