using System;
using System.IO;
using System.Text;
using Chaos.NaCl;
using NetSparkleUpdater.Enums;
using NetSparkleUpdater.Interfaces;
using NetSparkleUpdaterApp.BaseClass;

namespace NetSparkleUpdaterApp
{
    public class CustomEd25519SignatureVerifier : CustomEd25519SignatureVerifierBases
    {
        public CustomEd25519SignatureVerifier(string publicKeyFilePath) : base(publicKeyFilePath) { }
    }
}
