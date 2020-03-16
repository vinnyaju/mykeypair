using Microsoft.AspNetCore.Mvc;
using mykeypair_api.Entities;
using mykeypair_api.Utils;
using System;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace mykeypair_api.Controllers
{
    [Route("/")]
    [ApiController]
    public class RSAKeyPairController : ControllerBase
    {
     
        private const string KEYTYPE = "RSA";
        [HttpGet]
        public IActionResult Get()
        {
            return Get(4096);
        }
        /// <summary>
        /// Método GET.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{keySizeInBits}")]
        public IActionResult Get(int keySizeInBits)
        {

            byte[] privateKeyBytes;
            byte[] publicKeyBytes;

            byte[] rsaPublicPKCS1Bytes;


            var keygen = new SshKeyGenerator.SshKeyGenerator(keySizeInBits);

            string privateKey = keygen.ToPrivateKey();
            string publicKey = keygen.ToPublicKey();
            string sshPubKey = keygen.ToRfcPublicKey();

            privateKeyBytes = Encoding.Default.GetBytes(privateKey);
            publicKeyBytes = Encoding.Default.GetBytes(publicKey);
            rsaPublicPKCS1Bytes = Encoding.Default.GetBytes(sshPubKey);



            MemoryFile mfPriv = new MemoryFile($"{KEYTYPE}-{keySizeInBits}bits-private-key.pem", privateKeyBytes);
            MemoryFile mfPublic = new MemoryFile($"{KEYTYPE}-{keySizeInBits}bits-public-key.pem", publicKeyBytes);
            MemoryFile mfRSAPublicPKCS1Bytes = new MemoryFile($"{KEYTYPE}-{keySizeInBits}bits-public-ssh-key.pem", rsaPublicPKCS1Bytes);

            byte[] zipBytes = ZipMemoryFile.ZipItAllFromAndToMemory(CompressionLevel.Optimal, mfPriv, mfPublic, mfRSAPublicPKCS1Bytes);

            var result = new FileContentResult(zipBytes, "application/zip")
            {
                FileDownloadName = $"RSA-keypair-{keySizeInBits}bits.zip"
            };
            return result;
        }
    }
}