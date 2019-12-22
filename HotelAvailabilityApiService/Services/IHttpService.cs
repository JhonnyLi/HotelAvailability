using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Services
{
    public interface IHttpService
    {
        void SetBaseUrl(string url);
        void SetHeader(string key, string value);

        Task<HttpResponse> GetAsync<T>(string url);
        Task<HttpResponse> PostAsync<T>(string url, string content);
    }
}
