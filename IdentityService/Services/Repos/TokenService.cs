using IdentityService.Model;
using IdentityService.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Services.Repos
{
    public class TokenService : ITokenService
    {
        private readonly JwtAuthentication _jwt;
        public TokenService(JwtAuthentication jwt)
        {
            _jwt = jwt;
        }
        public string GenerateToken(List<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwt.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenCreate = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(tokenCreate);
            return token.ToString();
        }

    }
}
