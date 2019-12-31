using System;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Services
{
    public interface IAvailabilityService
    {
        Task GetAvailabilityForHotelByIdAndStartDateAsync(string id, DateTime startDate = default);
    }
}
