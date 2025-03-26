using NetSparkleUpdater.Enums;
using NetSparkleUpdater.Interfaces;

namespace NetSparkleUpdaterApp
{
    /// <summary>
    /// Verificador de firma que no realiza validación. Útil para pruebas o entornos internos.
    /// </summary>
    public class NoSignatureVerifier : ISignatureVerifier
    {
        public bool IsSignatureRequired => false;

        public SecurityMode SecurityMode { get; set; } = SecurityMode.Unsafe;

        public bool HasValidKeyInformation()
        {
            return true;
        }

        public bool VerifySignature(string downloadedFilePath, string signatureFilePath, string publicKeyFilePath)
        {
            return true;
        }

        public ValidationResult VerifySignature(string signature, byte[] dataToVerify)
        {
            return ValidationResult.Valid;
        }

        public ValidationResult VerifySignatureOfFile(string signature, string binaryPath)
        {
            return ValidationResult.Valid;
        }

        public ValidationResult VerifySignatureOfString(string signature, string data)
        {
            return ValidationResult.Valid;
        }
    }
}