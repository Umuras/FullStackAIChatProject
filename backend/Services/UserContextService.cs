namespace backend.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var userIdClaim = httpContextAccessor.HttpContext?
                .User
                .Claims
                .FirstOrDefault(c => c.Type == "userId");

            if (userIdClaim == null)
                throw new Exception("Unauthorized: userId not found in token.");

            return int.Parse(userIdClaim.Value);
        }

        public string GetUserName()
        {
            return httpContextAccessor.HttpContext?
                .User
                .Claims
                .FirstOrDefault(c => c.Type == "userName")
                ?.Value ?? string.Empty;
        }
    }
}
