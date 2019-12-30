using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _client;
        private readonly ISecretsService _secrets;
        public HttpService(ISecretsService secrets)
        {
            _secrets = secrets;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            _client = new HttpClient();
            SetDefaultHeaders();
        }
        public async Task<T> GetAsync<T>(string url)
        {
            var result = await _client.GetAsync(url).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }

        public Task<T> PostAsync<T>(string url, string content)
            => throw new NotImplementedException();

        public void SetBaseUrl(string url)
        {
            _client.BaseAddress = new Uri(url);
        }

        public void SetHeader(string key, string value)
        {
            _client.DefaultRequestHeaders.Add(key, value);
        }

        private void SetDefaultHeaders()
        {
            _client.BaseAddress = new Uri(_secrets.GetSecret("ApiBaseUrl"));
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add(_secrets.GetSecret("AuthHeader"), _secrets.GetSecret("ApiPrimaryKey"));
        }
    }
}
