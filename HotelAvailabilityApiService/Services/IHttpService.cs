using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Services
{
    public interface IHttpService
    {
        void SetBaseUrl(string url);
        void SetHeader(string key, string value);

        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, string content);
    }
}
