using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AssignmentAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TOS.API.Services;

namespace AssignmentAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserServices _userServices;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUserServices userServices, IConfiguration configuration)
        {
            _userServices = userServices;
            _configuration = configuration;
        }
        public Response<string> Login(User item)
        {
            var user = _userServices.GetAll().FirstOrDefault(s => s.Email == item.Email && s.Password == item.Password);
            var res = new Response<string>
            {
                Payload = user != null ? user.Id +";" + GenToken(user) : ";",
                Susscess = user != null,
            };
            return res;
        }

        private string GenToken(User user)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                Issuer = _configuration.GetSection("AppSettings:Issuer").Value,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}