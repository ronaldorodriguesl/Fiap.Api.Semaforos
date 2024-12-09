using Fiap.Api.Semaforos.Models;
using Fiap.Api.Semaforos.Data.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Fiap.Api.Semaforos.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _userRepository;

        public AuthService(IUsuarioRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UsuarioModel Autenticar(string email, string senha)
        {
            var user = _userRepository.GetByEmail(email);

            if (user == null || user.Senha != senha)
            {
                return null;
            }

            return user;
        }

        public string GenerateJwtToken(UsuarioModel user)
        {
            var secretKey = "f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = "fiap",
                Audience = "fiap",
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
