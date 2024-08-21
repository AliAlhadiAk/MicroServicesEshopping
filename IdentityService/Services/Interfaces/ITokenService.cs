using System.Security.Claims;

namespace IdentityService.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(List<Claim> claims);

    }
}
