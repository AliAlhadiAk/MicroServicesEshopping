using IdentityService.DTOs;
using IdentityService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(dto);
            if (result)
            {
                return Ok("Registration successful. Please check your email to confirm your account.");
            }

            return BadRequest("Registration failed. The email might already be in use.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(dto.UserName, dto.Password);
            if (result)
            {
                return Ok("Login successful.");
            }

            return Unauthorized("Invalid username or password.");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.ForgotPass(dto.Email);
            if (result)
            {
                return Ok("Password reset email sent.");
            }

            return BadRequest("Failed to send password reset email.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.ResetPass(dto);
            if (result)
            {
                return Ok("Password has been reset.");
            }

            return BadRequest("Failed to reset password. Please check the token and try again.");
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.ConfirmEmailAsync(dto.UserId, dto.Code);
            if (result)
            {
                return Ok("Email confirmed successfully.");
            }

            return BadRequest("Failed to confirm email. The link might be invalid or expired.");
        }

        [HttpPost("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
            var result = await _authService.SeedRoles();
            if (result)
            {
                return Ok("Roles have been seeded.");
            }

            return BadRequest("Failed to seed roles.");
        }
    }
}
