using Microsoft.AspNetCore.Identity;
using static System.Reflection.Metadata.BlobBuilder;

namespace IdentityService.Model
{
    public class User : IdentityUser
    {


        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }
       
    }
}
