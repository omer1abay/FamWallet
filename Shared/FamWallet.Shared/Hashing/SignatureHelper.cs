using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Parameters;

namespace FamWallet.Shared.Hashing
{
    public static class SignatureHelper
    {
        public static string CreateSignature(string data, string privateKey)
        {
            try
            {
                byte[] signedData;
                ASCIIEncoding ByteConverter = new ASCIIEncoding();
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                var privateKeyService = GetPrivateKey(privateKey);
                var Key = privateKeyService.ExportParameters(true);

                byte[] originalData = ByteConverter.GetBytes(data);

                signedData = HashAndSignBytes(originalData, Key);

                if (VerifySignedData(originalData,signedData,Key))
                {
                    Console.WriteLine("Signed data is verified");
                }
                else
                {
                    Console.WriteLine("Signed data is not verified");
                }

                return Convert.ToBase64String(signedData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private static bool VerifySignedData(byte[] originalData, byte[] signedData, RSAParameters key)
        {
            try
            {
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

                RSAalg.ImportParameters(key);

                return RSAalg.VerifyData(originalData,SHA256.Create(),signedData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private static byte[] HashAndSignBytes(byte[] originalData, RSAParameters key)
        {
            try
            {
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                
                RSAalg.ImportParameters(key);

                var signedData = RSAalg.SignData(originalData, SHA256.Create());

                return signedData;

            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        private static RSACryptoServiceProvider GetPrivateKey(string privateKey)
        {

            using (TextReader reader = new StringReader(privateKey))
            {
                var readKeyPair = (AsymmetricCipherKeyPair)new PemReader(reader).ReadObject();

                var privateKeyParameters = ((RsaPrivateCrtKeyParameters) readKeyPair.Private);

                var cryptoServiceProvider = new RSACryptoServiceProvider();

                var parameters = new RSAParameters
                {
                    Modulus = privateKeyParameters.Modulus.ToByteArrayUnsigned(),
                    D = privateKeyParameters.Exponent.ToByteArrayUnsigned(),
                    DP = privateKeyParameters.DP.ToByteArrayUnsigned(),
                    DQ = privateKeyParameters.DQ.ToByteArrayUnsigned(),
                    P = privateKeyParameters.P.ToByteArrayUnsigned(),
                    Q = privateKeyParameters.Q.ToByteArrayUnsigned(),
                    InverseQ = privateKeyParameters.QInv.ToByteArrayUnsigned(),
                    Exponent = privateKeyParameters.PublicExponent.ToByteArrayUnsigned()
                };

                cryptoServiceProvider.ImportParameters(parameters);

                return cryptoServiceProvider;
            }    


        }

    }
}
