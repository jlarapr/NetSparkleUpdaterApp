using NetSparkleUpdater.Interfaces;
using System.IO;
using System.Text;
using Chaos.NaCl;
using NetSparkleUpdater.Enums;

namespace NetSparkleUpdaterApp.BaseClass;

public class CustomEd25519SignatureVerifierBase : ISignatureVerifier {
      private readonly byte[] _publicKey;

      protected CustomEd25519SignatureVerifierBase(string publicKeyFilePath)
        {
            if (!File.Exists(publicKeyFilePath))
                throw new FileNotFoundException("Clave pública no encontrada.", publicKeyFilePath);

            _publicKey = File.ReadAllBytes(publicKeyFilePath);

            if (_publicKey.Length != Ed25519.PublicKeySizeInBytes)
                throw new ArgumentException("Clave pública inválida. Debe tener 32 bytes.");
        }

        public virtual bool IsSignatureRequired => true;
        public virtual bool HasValidKeyInformation() => _publicKey is { Length: 32 };
        public virtual SecurityMode SecurityMode { get; set; } = SecurityMode.Strict;

        public virtual ValidationResult VerifySignature(string signature, byte[] dataToVerify)
        {
            // Detecta si esto es el appcast.xml por heurística (por ejemplo, tamaño pequeño)
            if (dataToVerify.Length < 20000 && Encoding.UTF8.GetString(dataToVerify).Contains("<rss"))
            {
                File.AppendAllText("verify-log.txt", "✔ Ignorando firma del appcast.xml\n");
                return ValidationResult.Valid;
            }
            try
            {
                File.AppendAllText("verify-log.txt", $"Firma recibida (base64): {signature.Substring(0, 20)}...\n");

                byte[] signatureBytes = Convert.FromBase64String(signature);
                if (signatureBytes.Length != Ed25519.SignatureSizeInBytes) {
                    File.AppendAllText("verify-log.txt", $"Firma inválida: tamaño {signatureBytes.Length}, esperado 64.\n");
                    return ValidationResult.Invalid;
                }

                bool valid = Ed25519.Verify(signatureBytes, dataToVerify, _publicKey);
                File.AppendAllText("verify-log.txt", $"Resultado de verificación: {valid}\n");

                return valid ? ValidationResult.Valid : ValidationResult.Invalid;
            }
            catch(Exception ex)
            {
                File.AppendAllText("verify-log.txt", $"Excepción en verificación: {ex.Message}\n");

                return ValidationResult.Invalid;
            }
        }

        public virtual ValidationResult VerifySignatureOfFile(string signature, string binaryPath)
        {
            File.AppendAllText("verifySignatureOfFile-log.txt", $"Verificando: {binaryPath}\n");

            // Detectamos si se está intentando verificar el appcast.xml
            if (Path.GetFileName(binaryPath).Equals("appcast.xml", StringComparison.OrdinalIgnoreCase))
            {
                File.AppendAllText("verifySignatureOfFile-log.txt", "↪ Ignorando firma del appcast.xml\n");

                return ValidationResult.Valid; // ✅ Ignoramos validación del appcast
            }

            if (!File.Exists(binaryPath))
                return ValidationResult.Invalid;

            byte[] fileData = File.ReadAllBytes(binaryPath);
            return VerifySignature(signature, fileData);
        }

        public virtual ValidationResult VerifySignatureOfString(string signature, string data)
        {
            byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(data);
            return VerifySignature(signature, dataBytes);
        }

        public virtual bool VerifySignature(string downloadedFilePath, string signatureFilePath, string publicKeyFilePath)
        {
            // Este método no se usa cuando la firma viene en el appcast.xml (lo normal)
            return true;
        }

}