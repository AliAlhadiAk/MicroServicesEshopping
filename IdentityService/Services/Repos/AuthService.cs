using IdentityService.DatabaseContext;
using IdentityService.DTO_s;
using IdentityService.Model;
using IdentityService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityService.Services.Repos
{
    public class AuthService : IAuthService
    {

        private readonly AppDbContext _appDbContext;
        private readonly JwtAuthentication _jwt;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AuthService> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        public readonly IConfiguration Configuration;
        private readonly IEmailService _emailService;

        public AuthService(AppDbContext appDbContext, IEmailService emailservice, JwtAuthentication jwt, UserManager<IdentityUser> userManager, ILogger<AuthService> logger, RoleManager<IdentityRole> roleManager, IConfiguration configurationManager)
        {
            _appDbContext = appDbContext;
            _jwt = jwt;
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
            configurationManager = Configuration;
            emailservice = _emailService;

        }
        public Task<bool> ConfirmEmailAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ForgotPass(string Email)
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(List<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoginAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RefreshTokenrequest(string Refresh_token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterAsync(RegisterDTO dto)
        {

            bool response = false; 

            var checkEmail = await _userManager.FindByEmailAsync(dto.Email);
            if (checkEmail != null)
            {
                return response;
            }

            var account = new IdentityUser
            {
                Email = dto.Email,
                UserName = dto.UserName,
            };

            var createResult = await _userManager.CreateAsync(account, dto.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                Console.WriteLine($"User creation failed: {errors}");
                return response;
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(account);

            var emailSent = await _emailService.SendEmailAsync(dto.Email, "Email Confirmation", code);
            if (!emailSent)
            {
                Console.WriteLine("Failed to send confirmation email.");
                return response;
            }

            var roleResult = dto.Email.Equals("alialhadiabokhalil@gmail.com", StringComparison.OrdinalIgnoreCase)
                ? await _userManager.AddToRoleAsync(account, UserROLES.Admin)
                : await _userManager.AddToRoleAsync(account, UserROLES.User);

            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                Console.WriteLine($"Role assignment failed: {errors}");
                return response;
            }
            response = true;
            return response;

        }

        public Task<bool> ResetPass(ResetPassDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SeedRoles()
        {
            throw new NotImplementedException();
        }
    }
}
