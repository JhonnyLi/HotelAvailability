using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Services
{
    public class SecretsService : ISecretsService
    {
        private readonly SecretClient _secretClient;
        private readonly IConfiguration Configuration;
        public SecretsService(IConfiguration config)
        {
            Configuration = config;
            var (uri, clientCredentials) = InitializeKeyVault();
            _secretClient = new SecretClient(uri, clientCredentials);
        }
        public async Task<string> GetSecretAsync(string keyName)
        {
            var secret = await _secretClient.GetSecretAsync(keyName).ConfigureAwait(false);
            return secret.Value.ToString();
        }

        public string GetSecret(string keyName)
        {
            var secret = _secretClient.GetSecret(keyName);
            return secret.Value.Value.ToString();
        }

        private (Uri uri, ClientCertificateCredential clientCredentials) InitializeKeyVault()
        {
            var keyvaultUri = new Uri(Configuration["KeyVault:KeyVaultUrl"]);
            var clientCredentials = new ClientCertificateCredential(Configuration["KeyVault:TenantId"], Configuration["KeyVault:ClientId"], GetCertificate(Configuration["KeyVault:CertThumbPrint"]));
            return (keyvaultUri, clientCredentials);
        }

        private X509Certificate2 GetCertificate(string thumbPrint)
        {
            X509Certificate2 certificate;
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certCollection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbPrint, false);
                try
                {
                    certificate = certCollection[0];
                }
                catch
                {
                    throw new Exception("Certificate not found");
                }
            }
            return certificate;
        }
    }
}
