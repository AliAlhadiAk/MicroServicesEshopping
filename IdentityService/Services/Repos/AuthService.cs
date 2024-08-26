using IdentityService.DatabaseContext;
using IdentityService.DTOs;
using IdentityService.Model;
using IdentityService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityService.Services.Repos
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly JwtAuthentication _jwt;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AuthService> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;

        public AuthService(
            AppDbContext appDbContext,
            IEmailService emailService,
            JwtAuthentication jwt,
            UserManager<IdentityUser> userManager,
            ILogger<AuthService> logger,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ITokenService tokenService)
        {
            _appDbContext = appDbContext;
            _jwt = jwt;
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
            _tokenService = tokenService;
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            return result.Succeeded;
        }

        public async Task<bool> ForgotPass(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false; 
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"https://example.com/reset-password?token={token}"; // Update URL as needed

            var emailSent = await _emailService.SendEmailAsync(email, "Password Reset", $"Click the following link to reset your password: {resetLink}");
            return emailSent;
        }

        public string GenerateToken(List<Claim> claims)
        {
            return _tokenService.GenerateToken(claims); 
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return false; 
            }

            var result = await _userManager.CheckPasswordAsync(user, password);
            if (result)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var token = GenerateToken(userClaims);
                // Additional logic to return the token if needed
                return true;
            }

            return false; // Password incorrect
        }

        public async Task<bool> RefreshTokenRequest(string refreshToken)
        {
            var isValid = _tokenService.ValidateRefreshToken(refreshToken); // Validate refresh token
            if (isValid)
            {
                var newToken = _tokenService.GenerateNewToken(refreshToken); // Generate new token
                return true;
            }

            return false; 
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
                _logger.LogError($"User creation failed: {errors}");
                return response;
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(account);

            var emailSent = await _emailService.SendEmailAsync(dto.Email, "Email Confirmation", code);
            if (!emailSent)
            {
                _logger.LogError("Failed to send confirmation email.");
                return response;
            }

            var roleResult = dto.Email.Equals("alialhadiabokhalil@gmail.com", StringComparison.OrdinalIgnoreCase)
                ? await _userManager.AddToRoleAsync(account, UserROLES.Admin)
                : await _userManager.AddToRoleAsync(account, UserROLES.User);

            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                _logger.LogError($"Role assignment failed: {errors}");
                return response;
            }

            response = true;
            return response;
        }

        public async Task<bool> ResetPass(ResetPassDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return false; // User does not exist
            }

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            return result.Succeeded;
        }

        public async Task<bool> SeedRoles()
        {
            var roles = new[] { UserROLES.Admin, UserROLES.User };

            foreach (var role in roles)
            {
                var exists = await _roleManager.RoleExistsAsync(role);
                if (!exists)
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(role));
                    if (!result.Succeeded)
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        _logger.LogError($"Role creation failed: {errors}");
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
