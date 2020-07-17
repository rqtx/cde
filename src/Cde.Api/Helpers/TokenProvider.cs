using Cde.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cde.Api.Helpers
{
	public static class TokenProvider
	{
        static public string GenerateToken(UserModel user) {
            //Authentication successful, Issue Token with user credentials 
            //Provide the security key which is given in 
            //Startup.cs ConfigureServices() method 
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"));
            //Generate Token for user 
            var JWToken = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
                audience: Environment.GetEnvironmentVariable("JWT_ISSUER"),
                claims: GetUserClaims(user),
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                //Using HS256 Algorithm to encrypt Token  
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(JWToken);
        }

        static private IEnumerable<Claim> GetUserClaims(UserModel user) {
            IEnumerable<Claim> claims = new Claim[] {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };
            return claims;
        }
    }
}
