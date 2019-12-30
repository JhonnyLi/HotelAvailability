using HotelAvailabilityApiService.Models.Hotels;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Services
{
    public interface IHotelService 
    {
        Task<Hotel> GetHotelByNameAsync(string name);
    }
}
