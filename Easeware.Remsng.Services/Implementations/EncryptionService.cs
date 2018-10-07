using Easeware.Remsng.Common.Interfaces.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Easeware.Remsng.Services.Implementations
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IConfiguration _configuration;
        public EncryptionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Decrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            using (RSA rsa = RSA.Create())
            {
                string values = File.ReadAllText(_configuration["keys:private"]);
                FromXmlStr(rsa, values);
                byte[] encryptValue = rsa.Decrypt(Convert.FromBase64String(value), RSAEncryptionPadding.OaepSHA1);
                return Encoding.UTF8.GetString(encryptValue);
            }
        }

        public string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            try
            {
                using (RSA rsa = RSA.Create())
                {
                    string values = File.ReadAllText(_configuration["keys:public"]);
                    FromXmlStr(rsa, values);

                    byte[] byteValue = Encoding.UTF8.GetBytes(value);
                    byte[] encryptValue = rsa.Encrypt(byteValue, RSAEncryptionPadding.OaepSHA1);
                    
                    
                    return Convert.ToBase64String(encryptValue, Base64FormattingOptions.None);
                }
            }
            catch (Exception x)
            {
                Log.Error(x, "Error encrypting data");
                return string.Empty;
            }
        }

        public void FromXmlStr(RSA rsa, string xmlString)
        {
            var parameters = new RSAParameters();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus": parameters.Modulus = Convert.FromBase64String(node.InnerText); break;
                        case "Exponent": parameters.Exponent = Convert.FromBase64String(node.InnerText); break;
                        case "P": parameters.P = Convert.FromBase64String(node.InnerText); break;
                        case "Q": parameters.Q = Convert.FromBase64String(node.InnerText); break;
                        case "DP": parameters.DP = Convert.FromBase64String(node.InnerText); break;
                        case "DQ": parameters.DQ = Convert.FromBase64String(node.InnerText); break;
                        case "InverseQ": parameters.InverseQ = Convert.FromBase64String(node.InnerText); break;
                        case "D": parameters.D = Convert.FromBase64String(node.InnerText); break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
        }
    }
}
