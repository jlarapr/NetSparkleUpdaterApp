# NetSparkleUpdaterApp


`netsparkle-generate-appcast  --single-file NetSparkleUpdaterApp-v1.0.2.zip --file-version 1.0.2  --key-path C:\Users\jlara\AppData\Local\netsparkle\. --use-ed25519-signature-attribute --appcast-output-directory .`

`netsparkle-generate-appcast --generate-keys`

`Get-Item .\NetSparkleUpdaterApp-v1.0.2.zip).Length`
`netsparkle-generate-appcast --generate-signature NetSparkleUpdaterApp-v1.0.2.zip --key-path .`

`
$base64 = Get-Content .\NetSparkle_Ed25519.pub -Raw
[IO.File]::WriteAllBytes("NetSparkle_Ed25519_FIXED.pub", [Convert]::FromBase64String($base64))
`

`netsparkle-generate-appcast --generate-signature appcast.xml --key-path .`
