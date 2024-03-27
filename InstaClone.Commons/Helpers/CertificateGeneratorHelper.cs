using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace InstaClone.Commons.Helpers
{
    public class CertificateGeneratorHelper : IDisposable
    {
        private readonly RSA rsa;
        public CertificateGeneratorHelper()
        {
            rsa = RSA.Create();
        }

        public RsaSecurityKey GetPublicSigningKey()
        {
            string publicXmlKey = File.ReadAllText("./public_key.xml");
            rsa.FromXmlString(publicXmlKey);

            return new RsaSecurityKey(rsa);
        }

        public SigningCredentials GetPrivateSigningKey()
        {
            string privateXmlKey = File.ReadAllText("./private_key.xml");
            rsa.FromXmlString(privateXmlKey);

            return new SigningCredentials(
                key: new RsaSecurityKey(rsa),
                algorithm: SecurityAlgorithms.RsaSha256);
        }

        public void Dispose()
        {
            rsa?.Dispose();
        }
    }


}
