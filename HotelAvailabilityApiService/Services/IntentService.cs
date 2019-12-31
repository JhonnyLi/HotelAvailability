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
                FulfillmentText = "Det finns lediga rum på " + hotel.Attributes.Name,
                FulfillmentMessages = new Models.Response.Fulfillmentmessage[]
                {
                    new Models.Response.Fulfillmentmessage
                    {
                        Card = new Card
                        {
                            Title = hotel.Attributes.Name,
                            Subtitle = "Rum tillgängliga"
                        }
                    }
                },
                Payload = new Models.Response.Payload
                {
                    Google = new Google
                    {
                        ExpectUserResponse = false,
                        RichResponse = new Richresponse
                        {
                            Items = new Item[]
                            {
                                new Item
                                {
                                    SimpleResponse = new Simpleresponse
                                    {
                                        TextToSpeech = "Det finns lediga rum på " + hotel.Attributes.Name
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
