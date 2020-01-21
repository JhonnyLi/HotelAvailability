using HotelAvailabilityApiService.Models.Availability;
using System;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Services
{
    public interface IAvailabilityService
    {
        Task<GetAvailabilityResponse> GetAvailabilityForHotelByIdAndStartDateAsync(string id, DateTime checkinDate, DateTime checkoutDate, double adults);
    }
}
