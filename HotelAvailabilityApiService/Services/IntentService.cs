using HotelAvailabilityApiService.Models.Hotels;
using HotelAvailabilityApiService.Models.Request;
using HotelAvailabilityApiService.Models.Response;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Services
{
    public class IntentService : IIntentService
    {
        private readonly IHotelService _hotelService;
        public IntentService(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        public async Task<IntentResponse> GetIntentResponse(IntentRequest request)
        {
            var hotel = await _hotelService.GetHotelByNameAsync(request.QueryResult.Parameters.Hotels);
            return CreateResponse(hotel);
        }

        private static IntentResponse CreateResponse(Hotel hotel)
        {
            var response = new IntentResponse
            {
                fulfillmentText = "Det finns lediga rum på " + hotel.Attributes.Name,
                fulfillmentMessages = new Models.Response.Fulfillmentmessage[]
                {
                    new Models.Response.Fulfillmentmessage
                    {
                        card = new Card
                        {
                            title = hotel.Attributes.Name,
                            subtitle = "Rum tillgängliga"
                        }
                    }
                },
                payload = new Models.Response.Payload
                {
                    google = new Google
                    {
                        expectUserResponse = false,
                        richResponse = new Richresponse
                        {
                            items = new Item[]
                            {
                                new Item
                                {
                                    simpleResponse = new Simpleresponse
                                    {
                                        textToSpeech = "Det finns lediga rum på " + hotel.Attributes.Name
                                    }
                                }
                            }
                        }
                    }
                }
            };
            return response;
        }
    }
}
