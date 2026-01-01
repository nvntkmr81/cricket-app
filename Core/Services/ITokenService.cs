using Contracts.Models;

namespace Core.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
