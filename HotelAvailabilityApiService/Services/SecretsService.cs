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
#if DEBUG
            var (uri, clientCredentials) = InitializeKeyVaultLocally();
                _secretClient = new SecretClient(uri, clientCredentials);
#else
            var uri = InitializeKeyVault();
                _secretClient = new SecretClient(uri, new DefaultAzureCredential());
#endif
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
#if DEBUG
        private (Uri uri, ClientCertificateCredential clientCredentials) InitializeKeyVaultLocally()
        {
            var keyvaultUri = new Uri(Configuration["KeyVaultUrl"]);
            var clientCredentials = new ClientCertificateCredential(Configuration["KeyVaultTenantId"], Configuration["KeyVaultClientId"], GetCertificate(Configuration["CertThumbPrint"]));
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
#else
        private Uri InitializeKeyVault()
        {
            var keyvaultUri = new Uri(Configuration["KeyVaultUrl"]);
            return keyvaultUri;
        }
#endif
    }
}
