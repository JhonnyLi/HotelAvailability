using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HotelAvailabilityApiService.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _client;
        public HttpService(ISecretsService secrets)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(secrets.GetSecret("ApiBaseUrl"));
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add(secrets.GetSecret("AuthHeader"), secrets.GetSecret("ApiPrimaryKey"));
        }
        public Task<HttpResponse> GetAsync<T>(string url)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponse> PostAsync<T>(string url, string content)
        {
            throw new NotImplementedException();
        }

        public void SetBaseUrl(string url)
        {
            _client.BaseAddress = new Uri(url);
        }

        public void SetHeader(string key, string value)
        {
            _client.DefaultRequestHeaders.Add(key,value);
        }
    }
}
