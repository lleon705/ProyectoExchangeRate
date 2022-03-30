using ExchangeRate.Models;

namespace ExchangeRate.Service
{
    public class UserService : IUserService
    {
        // Prueba de simulación, el valor predeterminado es verificación artificial efectiva
        public bool IsValid(LoginRequestDTO req)
        {
            if(req.Username=="admin" && req.Password=="123456")
                return true;
            else
                return false;
        }
    }
}
