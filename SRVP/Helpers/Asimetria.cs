using Microsoft.IdentityModel.Tokens;
using SRVP.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Xml;

namespace SRVP.Helpers
{
    public class Asimetria
    {
        public static RsaSecurityKey GenerarClaveDeFirmaRSA(string claveEnXML)
        {
            var parametros = ParsearClaveEnXML(claveEnXML);
            var proveedorRSA = new RSACryptoServiceProvider(2048);
            proveedorRSA.ImportParameters(parametros);
            var claveRSA = new RsaSecurityKey(proveedorRSA);
            return claveRSA;
        }
        public static string GenerarTokenJWT(string claveEnXML, bool vivo, string nombre, string apellido, long cuil, string email, bool estadoCrediticio, string rol, string autor, string audiencia, DateTime vencimiento)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var Identity = new ClaimsIdentity(new[]
            {
                new Claim("Nombre", nombre),
                new Claim("Rol", rol),
                new Claim("Apellido", apellido),
                new Claim("Email", email),
                new Claim("Cuil", cuil.ToString()),
                new Claim("Estado", vivo.ToString()),
                new Claim("EstadoCrediticio", estadoCrediticio.ToString())
                // Claims adicionales...
            });
            SecurityKey key = GenerarClaveDeFirmaRSA(claveEnXML);
            var Token = new JwtSecurityToken
            (
            issuer: autor,
            audience: audiencia,
            claims: Identity.Claims,
            notBefore: DateTime.UtcNow,
            expires: vencimiento,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.RsaSha256, SecurityAlgorithms.Sha256Digest)
            );
            var TokenString = tokenHandler.WriteToken(Token);
            return TokenString;
        }
        private static RSAParameters ParsearClaveEnXML(string claveEnXML)
        {
            RSAParameters parameterosRSA = new RSAParameters();
            XmlDocument documentoXML = new XmlDocument();
            documentoXML.LoadXml(claveEnXML);

            if (documentoXML.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in documentoXML.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus": parameterosRSA.Modulus = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "Exponent": parameterosRSA.Exponent = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "P": parameterosRSA.P = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "Q": parameterosRSA.Q = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "DP": parameterosRSA.DP = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "DQ": parameterosRSA.DQ = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "InverseQ": parameterosRSA.InverseQ = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "D": parameterosRSA.D = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                    }
                }
            }
            else
            {
                throw new Exception("Archivo XML con clave RSA inválido.");
            }
            return parameterosRSA;
        }
    }
}
