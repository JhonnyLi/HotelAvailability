using HotelAvailabilityApiService.Models.Hotels;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHttpService _httpClient;
        private readonly string _baseUrl;
        public HotelService(IHttpService httpClient, ISecretsService secretsService)
        {
            _httpClient = httpClient;
            _baseUrl = secretsService.GetSecret("ApiBaseUrl");
        }

        public async Task<Hotel> GetHotelByNameAsync(string name)
        {
            var hotels = await GetHotelsAsync();
            return hotels.Data.FirstOrDefault(hn => hn.Attributes.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        private async Task<GetHotelsResponse> GetHotelsAsync()
        {
            var uri = _baseUrl + "/hotel/hotels?language=sv&Fields[hotels]=Name,Address";
            var result = await _httpClient.GetAsync<GetHotelsResponse>(uri).ConfigureAwait(false);
            return result;
        }
    }
}
