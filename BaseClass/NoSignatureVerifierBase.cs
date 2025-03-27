using NetSparkleUpdater.Interfaces;
using NetSparkleUpdater.Enums;
namespace NetSparkleUpdaterApp.BaseClass;

public abstract class NoSignatureVerifierBase : ISignatureVerifier {
    public bool IsSignatureRequired => false;

    public SecurityMode SecurityMode { get; set; } = SecurityMode.Unsafe;

    public virtual bool HasValidKeyInformation()
    {
         return true;
    }

    public virtual bool VerifySignature(string downloadedFilePath, string signatureFilePath, string publicKeyFilePath)
    {
        return true;
    }

    public virtual ValidationResult VerifySignature(string signature, byte[] dataToVerify)
    {
        return ValidationResult.Valid;
    }

    public virtual ValidationResult VerifySignatureOfFile(string signature, string binaryPath)
    {
        return ValidationResult.Valid;
    }

    public virtual ValidationResult VerifySignatureOfString(string signature, string data)
    {
        return ValidationResult.Valid;
    }
}