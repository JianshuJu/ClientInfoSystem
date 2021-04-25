using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IJwtService
    {
        string GenerateToken(LoginResponseModel model);
    }
}