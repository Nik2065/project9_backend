using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Project9Api
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123123123123009909";   // ключ для шифрации
        public const int LIFETIME = 5; // время жизни токена - в минутах
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
