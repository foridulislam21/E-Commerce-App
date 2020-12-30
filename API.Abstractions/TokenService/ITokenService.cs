using API.Models.Identity;

namespace API.Abstractions.TokenService
{
    public interface ITokenService
    {
        string CreateToken (AppUser user);
    }
}