using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Org.BouncyCastle.Crypto.Parameters;  // RsaKeyParameters
using Org.BouncyCastle.X509;               // X509CertificateParser
using Org.BouncyCastle.Crypto.Engines;
using System;


namespace hhh
{


#if __IOS__
using Foundation;

public byte[] LoadCertificate()
{
    var certPath = NSBundle.MainBundle.PathForResource("uidai_auth_preprod", "cer");
 if (string.IsNullOrEmpty(certPath))
        throw new FileNotFoundException("Certificate file not found in bundle.");

    return File.ReadAllBytes(certPath);
}
#endif
    public class EncryptWithUIDAICert
    {


        public (byte[] aesKey, byte[] iv, byte[] encryptedPid) EncryptPIDWithAES(string pidXml)
        {
             var aes = Aes.Create();
            aes.KeySize = 256;
            aes.GenerateKey();
            aes.GenerateIV();

            var encryptor = aes.CreateEncryptor();
            var pidBytes = Encoding.UTF8.GetBytes(pidXml);

            byte[] encryptedPid;
            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(pidBytes, 0, pidBytes.Length);
                cs.FlushFinalBlock();
                encryptedPid = ms.ToArray();
            }

            return (aes.Key, aes.IV, encryptedPid);
        }

        public byte[] EncryptAESKeyWithRSA(byte[] aesKey, byte[] certBytes)
        {
            var certParser = new X509CertificateParser();
            var cert = certParser.ReadCertificate(certBytes);
            var rsaKey = (RsaKeyParameters)cert.GetPublicKey();

            var rsa = new RsaEngine();
            rsa.Init(true, rsaKey);

            return rsa.ProcessBlock(aesKey, 0, aesKey.Length);  // AES key is only 32 bytes, so OK
        }


        public byte[] EncryptPIDXml(byte[] aesKey, byte[] certBytes)
        {
            var certParser = new X509CertificateParser();
            var cert = certParser.ReadCertificate(certBytes);
            var rsaKey = (RsaKeyParameters)cert.GetPublicKey();

            var rsa = new RsaEngine();
            rsa.Init(true, rsaKey);

            return rsa.ProcessBlock(aesKey, 0, aesKey.Length);
        }
    }
}

