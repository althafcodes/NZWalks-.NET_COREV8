using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration config;

        public TokenRepository(IConfiguration config)
        {
            this.config = config;
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            //create claims
            var claims = new List<Claim>();
            //for email
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            //for role
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            //creating token
            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires : DateTime.Now.AddMinutes(15),
                signingCredentials : credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
