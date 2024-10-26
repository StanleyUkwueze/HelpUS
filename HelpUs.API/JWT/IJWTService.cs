

using HelpUs.API.Models;

namespace HelpUs.API.JWT
{
    public interface IJWTService
    {
        Task<string> GenerateJwtToken(User user);
    }
}
