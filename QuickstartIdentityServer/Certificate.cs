using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using System;
using System.Security.Cryptography.X509Certificates;

namespace QuickstartIdentityServer
{
    public class Certificate
    {
		public static X509Certificate2 GenerateSelfSignedCertificate(string subjectName, string issuerName, AsymmetricKeyParameter issuerPrivKey)
		{
			const int keyStrength = 2048;

			// Generating Random Numbers
			CryptoApiRandomGenerator randomGenerator = new CryptoApiRandomGenerator();
			SecureRandom random = new SecureRandom(randomGenerator);
			ISignatureFactory signatureFactory = new Asn1SignatureFactory("SHA512WITHRSA", issuerPrivKey, random);
			// The Certificate Generator
			X509V3CertificateGenerator certificateGenerator = new X509V3CertificateGenerator();
			certificateGenerator.AddExtension(X509Extensions.ExtendedKeyUsage.Id, true, new ExtendedKeyUsage(KeyPurposeID.IdKPServerAuth));
			// Serial Number
			BigInteger serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(Int64.MaxValue), random);
			certificateGenerator.SetSerialNumber(serialNumber);

			// Signature Algorithm
			//const string signatureAlgorithm = "SHA512WITHRSA";
			//certificateGenerator.SetSignatureAlgorithm(signatureAlgorithm);

			// Issuer and Subject Name
			X509Name subjectDN = new X509Name(subjectName);
			X509Name issuerDN = new X509Name(issuerName);
			certificateGenerator.SetIssuerDN(issuerDN);
			certificateGenerator.SetSubjectDN(subjectDN);

			// Valid For
			DateTime notBefore = DateTime.UtcNow.Date;
			DateTime notAfter = notBefore.AddYears(2);

			certificateGenerator.SetNotBefore(notBefore);
			certificateGenerator.SetNotAfter(notAfter);

			// Subject Public Key
			AsymmetricCipherKeyPair subjectKeyPair;
			var keyGenerationParameters = new KeyGenerationParameters(random, keyStrength);
			var keyPairGenerator = new RsaKeyPairGenerator();
			keyPairGenerator.Init(keyGenerationParameters);
			subjectKeyPair = keyPairGenerator.GenerateKeyPair();

			certificateGenerator.SetPublicKey(subjectKeyPair.Public);

			// Generating the Certificate
			AsymmetricCipherKeyPair issuerKeyPair = subjectKeyPair;

			// selfsign certificate
			var certificate = certificateGenerator.Generate(signatureFactory);

			// correcponding private key
			PrivateKeyInfo info = PrivateKeyInfoFactory.CreatePrivateKeyInfo(subjectKeyPair.Private);


			// merge into X509Certificate2
			X509Certificate2 x509 = new X509Certificate2(certificate.GetEncoded());

			Asn1Sequence seq = (Asn1Sequence)Asn1Object.FromByteArray(info.ParsePrivateKey().GetDerEncoded());
			if (seq.Count != 9)
			{
				//throw new PemException("malformed sequence in RSA private key");
			}

			RsaPrivateKeyStructure rsa = RsaPrivateKeyStructure.GetInstance(seq); //new RsaPrivateKeyStructure(seq);
			RsaPrivateCrtKeyParameters rsaparams = new RsaPrivateCrtKeyParameters(
				rsa.Modulus, rsa.PublicExponent, rsa.PrivateExponent, rsa.Prime1, rsa.Prime2, rsa.Exponent1, rsa.Exponent2, rsa.Coefficient);

			return x509.CopyWithPrivateKey(DotNetUtilities.ToRSA(rsaparams));


		}
		public static X509Certificate2 GenerateCACertificate(string subjectName, ref AsymmetricKeyParameter CaPrivateKey)
		{
			const int keyStrength = 2048;

			// Generating Random Numbers
			CryptoApiRandomGenerator randomGenerator = new CryptoApiRandomGenerator();
			SecureRandom random = new SecureRandom(randomGenerator);

			// The Certificate Generator
			X509V3CertificateGenerator certificateGenerator = new X509V3CertificateGenerator();

			// Serial Number
			BigInteger serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(Int64.MaxValue), random);
			certificateGenerator.SetSerialNumber(serialNumber);

			// Signature Algorithm
			//const string signatureAlgorithm = "SHA256WithRSA";
			//certificateGenerator.SetSignatureAlgorithm(signatureAlgorithm);

			// Issuer and Subject Name
			X509Name subjectDN = new X509Name(subjectName);
			X509Name issuerDN = subjectDN;
			certificateGenerator.SetIssuerDN(issuerDN);
			certificateGenerator.SetSubjectDN(subjectDN);

			// Valid For
			DateTime notBefore = DateTime.UtcNow.Date;
			DateTime notAfter = notBefore.AddYears(2);

			certificateGenerator.SetNotBefore(notBefore);
			certificateGenerator.SetNotAfter(notAfter);

			// Subject Public Key
			AsymmetricCipherKeyPair subjectKeyPair;
			KeyGenerationParameters keyGenerationParameters = new KeyGenerationParameters(random, keyStrength);
			RsaKeyPairGenerator keyPairGenerator = new RsaKeyPairGenerator();
			keyPairGenerator.Init(keyGenerationParameters);
			subjectKeyPair = keyPairGenerator.GenerateKeyPair();

			certificateGenerator.SetPublicKey(subjectKeyPair.Public);

			// Generating the Certificate
			AsymmetricCipherKeyPair issuerKeyPair = subjectKeyPair;
			ISignatureFactory signatureFactory = new Asn1SignatureFactory("SHA512WITHRSA", issuerKeyPair.Private, random);
			// selfsign certificate
			var certificate = certificateGenerator.Generate(signatureFactory);
			X509Certificate2 x509 = new X509Certificate2(certificate.GetEncoded());

			CaPrivateKey = issuerKeyPair.Private;

			return x509;
			//return issuerKeyPair.Private;
		}

		private static bool addCertToStore(System.Security.Cryptography.X509Certificates.X509Certificate2 cert, System.Security.Cryptography.X509Certificates.StoreName st, System.Security.Cryptography.X509Certificates.StoreLocation sl)
		{
			bool bRet = false;

			try
			{
				X509Store store = new X509Store(st, sl);
				store.Open(OpenFlags.ReadWrite);
				store.Add(cert);

				store.Close();
			}
			catch
			{

			}

			return bRet;
		}


		private static X509Certificate2 FindCertificate()
		{
			using (var store = new X509Store(StoreName.Root, StoreLocation.LocalMachine))
			{
				store.Open(OpenFlags.OpenExistingOnly);
				var certs = store.Certificates.Find(X509FindType.FindBySubjectName, "buyingagentidentitycert", false);
				return certs.Count > 0 ? certs[0] : null;
			}
		}

		public static X509Certificate2 GetCertificate()
		{
			var cert = FindCertificate();
			if (cert != null)
				return cert;

			AsymmetricKeyParameter myCAprivateKey = null;
			//generate a root CA cert and obtain the privateKey
			X509Certificate2 MyRootCAcert = GenerateCACertificate("CN=MYCA", ref myCAprivateKey);
			//add CA cert to store
			addCertToStore(MyRootCAcert, StoreName.Root, StoreLocation.LocalMachine);

			//generate cert based on the CA cert privateKey
			X509Certificate2 MyCert = GenerateSelfSignedCertificate("CN=IdentityServerAuth", "CN=MYCA", myCAprivateKey);
			//add cert to store
			addCertToStore(MyCert, StoreName.Root, StoreLocation.LocalMachine);

			return MyCert;
		}

	}
}
