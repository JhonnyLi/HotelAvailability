using HotelAvailabilityApiService.Models.Request;
using HotelAvailabilityApiService.Models.Response;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Services
{
    public interface IIntentService
    {
        Task<IntentResponse> GetIntentResponse(IntentRequest request);
    }
}
