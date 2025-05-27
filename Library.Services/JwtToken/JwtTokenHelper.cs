using Library.Models.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Services.JwtToken
{
    public class JwtTokenHelper : IJwtTokenHelper
    {
        private readonly IConfiguration _configuration;

        public JwtTokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates a JWT token for the specified user using application settings.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <returns>A signed JWT token string.</returns>
        public string GenerateToken(Users user)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var expiry = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiryInMinutes"]));

            // Define user-specific claims to embed in the token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleName),
            };

            // Create the signing credentials using the secret key and algorithm
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define the token descriptor including claims, expiry, and signing informations
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiry,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            // Create and return the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}



