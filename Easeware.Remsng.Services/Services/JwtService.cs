using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Cryptography;

namespace Easeware.Remsng.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private SecurityKey _issuerSigningKey;
        private SigningCredentials _signingCredentials;
        private JwtHeader _jwtHeader;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        public TokenValidationParameters Parameters { get; private set; }
        private IEncryptionService _encryptionService;
        private JwtConfiguration _jwtConfiguration;
        public JwtService(JwtConfiguration jwtConfiguration,
            IEncryptionService encryptionService)
        {
            _jwtConfiguration = jwtConfiguration;
            _encryptionService = encryptionService;
            InitializeRsa();
            InitializeJwtParameters();
        }

        public string Get(UserModel userModel)
        {
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddSeconds(_jwtConfiguration.TokenLifespan);

            var centuryBegin = new DateTime(1970, 1, 1);
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
            var now = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);
            var issuer = _jwtConfiguration.Issuer ?? string.Empty;
            var payload = new JwtPayload
        {
            {"sub", userModel.id},
            {"unique_name", $"{userModel.lastname} {userModel.otherNames}"},
            {"iss", issuer},
            {"iat", now},
            {"nbf", now},
            {"exp", exp},
            {"jti", Guid.NewGuid().ToString("N")},
            {"email", userModel.email }
        };
            var jwt = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            return token;
        }

        private void InitializeRsa()
        {
            using (RSA publicRsa = RSA.Create())
            {
                var publicKeyXml = File.ReadAllText(_jwtConfiguration.RsaPublicKeyXml);
                _encryptionService.FromXmlStr(publicRsa, publicKeyXml);
                _issuerSigningKey = new RsaSecurityKey(publicRsa);
            }
            if (string.IsNullOrWhiteSpace(_jwtConfiguration.RsaPrivateKeyXml))
            {
                return;
            }
            using (RSA privateRsa = RSA.Create())
            {
                var privateKeyXml = File.ReadAllText(_jwtConfiguration.RsaPrivateKeyXml);
                _encryptionService.FromXmlStr(privateRsa, privateKeyXml);
                var privateKey = new RsaSecurityKey(privateRsa);

                _signingCredentials = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256);
            }
        }

        private void InitializeJwtParameters()
        {
            _jwtHeader = new JwtHeader(_signingCredentials);
            Parameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = _jwtConfiguration.Issuer,
                IssuerSigningKey = _issuerSigningKey
            };
        }

        public object ValidatorParameters()
        {
            return Parameters;
        }
    }
}
