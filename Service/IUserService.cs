using ExchangeRate.Models;

namespace ExchangeRate.Service
{
    public interface IUserService
    {
        bool IsValid(LoginRequestDTO req);
    }
}
