using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WaterMonitoringIOT
{
    public static class GlobalFunctions
    {
        public static string GenerateToken(int Id, string Username,string Type)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Environment.GetEnvironmentVariable("JWT_SECRET");
            var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Convert.ToString (Id)),
                    new Claim ("dateIssued",DateTime.Now.ToString()),
                    new Claim("Username",Username),
                    new Claim("Type",Type)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string HashPassword(string password)
        {
            password = BCrypt.Net.BCrypt.HashPassword(password);
            return password;
        }

        public static bool VerifyPassword(string savedpassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, savedpassword);
        }
    }
}
