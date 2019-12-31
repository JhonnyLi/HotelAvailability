using System;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IHttpService _httpClient;
        private readonly string _baseUrl;
        public AvailabilityService(IHttpService httpService, ISecretsService secrets)
        {
            _httpClient = httpService;
            _baseUrl = secrets.GetSecret("AvailabilityApiBaseUrl");
        }

        public Task GetAvailabilityForHotelByIdAndStartDateAsync(string id, DateTime startDate)
        {
            throw new NotImplementedException();
        }
    }
}
