using InstaClone.Commons.Models;
using InstaClone.Commons.Responses;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Newtonsoft.Json;

namespace InstaClone.Commons.Helpers
{
    public static class GenerateTokenHelper
    {
        public static LoginResponse GenerateToken(User user, IConfiguration configuration, IMapper mapper)
        {

            Claim[] _Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim("userId", user.Id.ToString()),
            };
            var dateNow = DateTime.Now;
            DateTime expiresIn = dateNow.AddMinutes(double.Parse(configuration["JWT:Expiration"]!));
            SecurityTokenDescriptor tokenDescriptor = GetTokenDescriptor(_Claims, configuration, expiresIn, dateNow);
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return new LoginResponse
            {
                Token = tokenHandler.WriteToken(securityToken),
                ExpiresIn = expiresIn,
            };
        }

        private static SecurityTokenDescriptor GetTokenDescriptor(Claim[] claims, IConfiguration configuration, DateTime expiresIn, DateTime dateNow)
        {
            
            var certificate = new CertificateGeneratorHelper();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresIn,
                NotBefore = dateNow,
                Issuer = configuration["JWT:Issuer"],
                Audience = configuration["JWT:Audience"],
                SigningCredentials = certificate.GetPrivateSigningKey()
            };

            return tokenDescriptor;
        }
    }
}
