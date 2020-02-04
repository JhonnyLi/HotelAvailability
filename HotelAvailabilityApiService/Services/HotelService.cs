using HotelAvailabilityApiService.Models.Hotels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Services
{
    using Newtonsoft.Json;

    public class HotelService : IHotelService
    {
        private readonly IHttpService _httpClient;
        private readonly string _baseUrl;
        private static Dictionary<string, string> _hotelsDictionary;
        
        public HotelService(IHttpService httpClient, ISecretsService secretsService)
        {
            _httpClient = httpClient;
            _baseUrl = secretsService.GetSecret("ApiBaseUrl");
            _hotelsDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(secretsService.GetSecret("HotelsDictionaryJson"));
        }

        public async Task<Hotel> GetHotelByNameAsync(string name)
        {
            var hotels = await GetHotelsAsync().ConfigureAwait(false);
            
            return hotels.Data.FirstOrDefault(hn => hn.Attributes.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<GetHotelsResponse> GetHotelsAsync()
        {
            var uri = _baseUrl + "/hotel/hotels?language=sv&Fields[hotels]=Name,Address";
            var result = await _httpClient.GetAsync<GetHotelsResponse>(uri).ConfigureAwait(false);
            
            return result;
        }

        public static string GetHotelGuidFromDictionary(string name)
        {
            return _hotelsDictionary.SingleOrDefault(h => h.Key.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Value;
        }

        public async Task<GetHotelsResponse> GetHotelsWithFieldsAsync(List<HotelAttributeFields> attributes = default)
        {
            //TODO: Add logic for getting attributes.
            var fields = "Name,Keywords";
            if (attributes != null || attributes.Any())
            {
                fields = string.Empty;
                foreach(var field in attributes)
                {
                    fields += $"{nameof(field)},";
                }
                fields.TrimEnd(',');
            } 
            var uri = $"{_baseUrl}/hotel/hotels?language=sv&Fields[hotels]={fields}";
            var result = await _httpClient.GetAsync<GetHotelsResponse>(uri).ConfigureAwait(false);

            return result;
        }

        //TODO: Complete with all available attributes...
        public enum HotelAttributeFields
        {
            All = 0,
            Name = 1,
            Address = 2,
            OperaId = 3,
            Keywords = 4
        }
    }
}
