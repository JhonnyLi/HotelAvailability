using HotelAvailabilityApiService.Models.Hotels;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HotelAvailabilityApiService.Services.HotelService;

namespace HotelAvailabilityApiService.Services
{
    public interface IHotelService 
    {
        Task<Hotel> GetHotelByNameAsync(string name);

        Task<GetHotelsResponse> GetHotelsAsync();

        Task<GetHotelsResponse> GetHotelsWithFieldsAsync(List<HotelAttributeFields> attributes);
    }
}
