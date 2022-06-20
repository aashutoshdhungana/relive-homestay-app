using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Relive.Server.Core.UserAggregate;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Relive.Server.API.Services
{
    public class UserAuthenticationService
    {
        private readonly IConfiguration _iConfiguration;

        public UserAuthenticationService(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
        }

        public string GenerateToken(User user, UserTypes userType)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string tokenKey = _iConfiguration["JWT:AppSecret"];
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Expires = DateTime.UtcNow.AddMinutes(10),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userid", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, userType.ToString())
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenKey)), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string HashPassword(string rawPassword)
        {
            byte[] salt = Encoding.Unicode.GetBytes("NZsP6NnmfBuYeJrrAKNuVQ==");
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: rawPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
