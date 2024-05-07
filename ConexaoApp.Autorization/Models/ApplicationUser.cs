using Microsoft.AspNetCore.Identity;

namespace ConexaoApp.Autorization.Models;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
