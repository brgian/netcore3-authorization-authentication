using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NetCore.Template.Services.Helper
{
    public static class SecurityHelper
    {
        public static SigningCredentials GetSigningCredentials(string signingKey)
        {
            RSAParameters RSAKeyInfo = GetRSAParameters(signingKey);

            var securityKey = new RsaSecurityKey(RSAKeyInfo);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256Signature);
            return credentials;
        }

        private static RSAParameters GetRSAParameters(string signingKey)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048);
            RSA.FromXmlString(signingKey);
            RSAParameters RSAKeyInfo = RSA.ExportParameters(true);
            return RSAKeyInfo;
        }

        public static SecurityKey GetSecurityKey(string signingKey)
        {
            RSAParameters RSAKeyInfo = GetRSAParameters(signingKey);

            return new RsaSecurityKey(RSAKeyInfo);
        }
    }
}
