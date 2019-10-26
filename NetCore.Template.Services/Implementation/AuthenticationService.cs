using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NetCore.Template.Configuration;
using NetCore.Template.DTOs.Requests;
using NetCore.Template.DTOs.Responses;
using NetCore.Template.Services.Helper;

namespace NetCore.Template.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ConfigurationAccessor configurationAccessor;

        public AuthenticationService(ConfigurationAccessor configurationAccessor)
        {
            this.configurationAccessor = configurationAccessor;
        }

        public bool TryAuthenticate(LoginCredentials loginCredentials, out TokenResponse tokenResponse)
        {
            if (loginCredentials.Username.Equals("Admin", StringComparison.InvariantCultureIgnoreCase))
                tokenResponse = GenerateToken("Admin");
            else
                tokenResponse = GenerateToken("User");

            return true;
        }

        private TokenResponse GenerateToken(string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "User"),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: configurationAccessor.SecurityConfiguration.Issuer,
                audience: configurationAccessor.SecurityConfiguration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(configurationAccessor.SecurityConfiguration.MinutesToExpiration),
                signingCredentials: SecurityHelper.GetSigningCredentials(configurationAccessor.SecurityConfiguration.SigningKey)
            );

            return new TokenResponse
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
                expires_in = configurationAccessor.SecurityConfiguration.MinutesToExpiration.ToString()
            };
        }


    }
}
