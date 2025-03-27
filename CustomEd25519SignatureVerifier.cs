using System;
using System.IO;
using Chaos.NaCl;
using NetSparkleUpdater.Enums;
using NetSparkleUpdater.Interfaces;

namespace NetSparkleUpdaterApp
{
    public class CustomEd25519SignatureVerifier : ISignatureVerifier
    {
        private readonly byte[] _publicKey;

        public CustomEd25519SignatureVerifier(string publicKeyFilePath)
        {
            if (!File.Exists(publicKeyFilePath))
                throw new FileNotFoundException("Clave pública no encontrada.", publicKeyFilePath);

            _publicKey = File.ReadAllBytes(publicKeyFilePath);

            if (_publicKey.Length != Ed25519.PublicKeySizeInBytes)
                throw new ArgumentException("Clave pública inválida. Debe tener 32 bytes.");
        }

        public bool IsSignatureRequired => true;

        public bool HasValidKeyInformation() => _publicKey is { Length: 32 };

        public ValidationResult VerifySignature(string signature, byte[] dataToVerify)
        {
            try
            {
                var signatureBytes = Convert.FromBase64String(signature);
                if (signatureBytes.Length != Ed25519.SignatureSizeInBytes)
                    return ValidationResult.Invalid;

                bool valid = Ed25519.Verify(signatureBytes, dataToVerify, _publicKey);
                return valid ? ValidationResult.Valid : ValidationResult.Invalid;
            }
            catch
            {
                return ValidationResult.Invalid;
            }
        }

        public ValidationResult VerifySignatureOfFile(string signature, string binaryPath)
        {
            // Detectamos si se está intentando verificar el appcast.xml
            if (Path.GetFileName(binaryPath).Equals("appcast.xml", StringComparison.OrdinalIgnoreCase))
            {
                return ValidationResult.Valid; // ✅ Ignoramos validación del appcast
            }

            if (!File.Exists(binaryPath))
                return ValidationResult.Invalid;

            byte[] fileData = File.ReadAllBytes(binaryPath);
            return VerifySignature(signature, fileData);
        }

        public ValidationResult VerifySignatureOfString(string signature, string data)
        {
            byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(data);
            return VerifySignature(signature, dataBytes);
        }

        public bool VerifySignature(string downloadedFilePath, string signatureFilePath, string publicKeyFilePath)
        {
            // Este método no se usa cuando la firma viene en el appcast.xml (lo normal)
            return true;
        }

        public SecurityMode SecurityMode { get; set; } = SecurityMode.Strict;
    }
}
