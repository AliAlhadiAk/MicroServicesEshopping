using System.Security.Claims;

namespace IdentityService.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> LoginAsync();
        public Task<bool> RegisterAsync(DTO_s.RegisterDTO dto);
        public Task<bool> ConfirmEmailAsync(string? email, string? code);
        public Task<bool> SeedRoles();
        public Task<bool> RefreshTokenrequest(string Refresh_token);
        public Task<bool> ForgotPass(string Email);
        public Task<bool> ResetPass (DTO_s.ResetPassDTO dto);
    }
}
