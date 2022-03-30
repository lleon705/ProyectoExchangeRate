using ExchangeRate.Models;

namespace ExchangeRate.Service
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(LoginRequestDTO request, out string token);
    }
}
