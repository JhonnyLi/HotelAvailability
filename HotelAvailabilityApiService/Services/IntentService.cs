using HotelAvailabilityApiService.Models.Hotels;
using HotelAvailabilityApiService.Models.Request;
using HotelAvailabilityApiService.Models.Response;
using System;
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
            var hotel = await _hotelService.GetHotelByNameAsync(request.QueryResult.Parameters.Hotel);
            return CreateResponse(hotel, request.QueryResult.Parameters.Date, request.QueryResult.Parameters.LeavingDate);
        }

        private static IntentResponse CreateResponse(Hotel hotel, DateTime checkinDate, DateTime checkoutDate)
        {
            var responseMessage = $"{hotel.Attributes.Name} has got available rooms during the period you specified.";
            var response = new IntentResponse
            {
                FulfillmentText = responseMessage,
                FulfillmentMessages = new Models.Response.Fulfillmentmessage[]
                {
                    new Models.Response.Fulfillmentmessage
                    {
                        Card = new Card
                        {
                            Title = responseMessage,
                            Subtitle = $"Hotel adress: {hotel.Attributes.Address.StreetAddress}"
                        }
                    }
                },
                Payload = new Models.Response.Payload
                {
                    Google = new Models.Response.Google
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
                                        TextToSpeech = responseMessage
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
