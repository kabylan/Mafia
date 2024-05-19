using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mafia.Application.Utils
{
    public class AuthOptions
    {
        public const string ISSUER = "Ambulation"; // издатель токена
        public const string AUDIENCE = "Doctors"; // потребитель токена
        public const int LIFETIME = 180000;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            var key = Encoding.UTF8.GetBytes(DotNetEnv.Env.GetString("JWT_KEY", "Variable not found"));
            return new SymmetricSecurityKey(key);
        }
    }
}
