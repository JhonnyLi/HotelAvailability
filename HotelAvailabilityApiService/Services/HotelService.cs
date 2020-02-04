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
        private static List<(string Name, string Id)> _hotels;
        
        public HotelService(IHttpService httpClient, ISecretsService secretsService)
        {
            _httpClient = httpClient;
            _baseUrl = secretsService.GetSecret("ApiBaseUrl");
            var test = "[{\"Item1\":\"Scandic Infra City\",\"Item2\":\"5b3b1f4d-1620-43f9-be51-b5f3989e9634\"},{\"Item1\":\"Haymarket by Scandic\",\"Item2\":\"4149be92-7d32-4f46-9520-d5fdf419fe4e\"},{\"Item1\":\"Scandic Järva Krog\",\"Item2\":\"15923b22-cd6b-44f1-b74a-068f34ae0d46\"},{\"Item1\":\"Scandic Victoria Tower\",\"Item2\":\"b7baca4b-8b73-4eb4-af71-c93e745982c4\"},{\"Item1\":\"Scandic Talk\",\"Item2\":\"b32158ee-3a9f-4eff-b0c6-a335ff3c837c\"},{\"Item1\":\"Scandic Gamla Stan\",\"Item2\":\"2544ee32-8b6b-4645-aa53-ec4d6132ff62\"},{\"Item1\":\"Scandic Continental\",\"Item2\":\"4ba7f654-a670-47f2-af1f-24408f2ed31f\"},{\"Item1\":\"Scandic Sjöfartshotellet\",\"Item2\":\"d2339250-a8e8-41aa-814a-b0b68899de39\"},{\"Item1\":\"Scandic Täby\",\"Item2\":\"714735fd-307d-4e7c-aa1f-9ac6a2db370b\"},{\"Item1\":\"Scandic Kungens Kurva\",\"Item2\":\"c4a719a3-8b8a-4b18-9b3e-11d1d3c08b8d\"},{\"Item1\":\"Scandic Klara\",\"Item2\":\"809a59a3-5347-4fe4-ae4a-7f34d3186ee6\"},{\"Item1\":\"Scandic Star Sollentuna\",\"Item2\":\"36109bad-97ea-4a41-8cd6-563a6a2b2f1e\"},{\"Item1\":\"Scandic No. 53\",\"Item2\":\"88f3b46a-97f8-4a8c-88dd-3646c9091759\"},{\"Item1\":\"Scandic Foresta\",\"Item2\":\"c1dbfe23-d82c-416d-a8b4-9973a4f3ea23\"},{\"Item1\":\"Scandic Malmen\",\"Item2\":\"8a877595-560f-4f66-94b6-125d614f3468\"}]";
            _hotels = JsonConvert.DeserializeObject<List<(string, string)>>(test);
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
            return _hotels.SingleOrDefault(s => s.Item1.Equals("Scandic Crown", StringComparison.InvariantCultureIgnoreCase)).Item2;
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
