using HotelAvailabilityApiService.Models.Availability;
using HotelAvailabilityApiService.Models.AvailabilityMessageModel;
using HotelAvailabilityApiService.Models.Request;
using HotelAvailabilityApiService.Models.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HotelAvailabilityApiService.Models.AvailabilityMessageModel.AvailabilityMessageModel;

namespace HotelAvailabilityApiService.Services
{
    public class IntentService : IIntentService
    {
        private readonly IHotelService _hotelService;
        private readonly IAvailabilityService _availabilityService;
        public IntentService(IHotelService hotelService, IAvailabilityService availabilityService)
        {
            _hotelService = hotelService;
            _availabilityService = availabilityService;
        }
        public async Task<IntentResponse> GetIntentResponse(IntentRequest request)
        {
            var hotel = await _hotelService.GetHotelByNameAsync(request.QueryResult.Parameters.Hotel);
            var availability = await _availabilityService.GetAvailabilityForHotelByIdAndStartDateAsync(hotel.Id, request.QueryResult.Parameters.Date, request.QueryResult.Parameters.LeavingDate);
            var messages = CreateResponseMessages(request, availability);
            return CreateResponse(messages);
        }

        private static AvailabilityMessageModel CreateResponseMessages(IntentRequest request, GetAvailabilityResponse availability)
        {
            var roomsAvailable = availability.Data.Any();
            var model = new AvailabilityMessageModel(request, availability);

            if (roomsAvailable)
            {
                var roomString = availability.Data.Count == 1 ? "room" : "rooms";
                model.FulFillmentMessage = $"{model.HotelName} has {availability.Data.Count} {roomString} available between {model.CheckInDate} and {model.CheckoutDate}.";
                var cardMessage = new CardMessage
                {
                    Title = $"{model.HotelName} room availability",
                    SubTitle = model.FulFillmentMessage
                };
                model.CardMessages.Add(cardMessage);


                var assistantSimpleResponse = new Simpleresponse
                {
                    TextToSpeech = model.FulFillmentMessage
                };
                model.SimpleResponses.Add(assistantSimpleResponse);

            }
            else
            {
                model.FulFillmentMessage = $"{model.HotelName} has no available rooms between {model.CheckInDate} and {model.CheckoutDate}.";
                var cardMessage = new CardMessage
                {
                    Title = $"{model.HotelName} room availability",
                    SubTitle = model.FulFillmentMessage
                };
                model.CardMessages.Add(cardMessage);


                var assistantSimpleResponse = new Simpleresponse
                {
                    TextToSpeech = model.FulFillmentMessage
                };
                model.SimpleResponses.Add(assistantSimpleResponse);
            }

            return model;

        }

        private static IntentResponse CreateResponse(AvailabilityMessageModel messages)
        {
            var cardMessage = messages.CardMessages.First();
            var assistantResponse = messages.SimpleResponses.First();
            var response = new IntentResponse
            {
                FulfillmentText = messages.FulFillmentMessage,
                FulfillmentMessages = new List<Models.Response.Fulfillmentmessage>
                {
                    new Models.Response.Fulfillmentmessage
                    {
                        Card = new Card
                        {
                            Title = cardMessage.Title,
                            Subtitle = cardMessage.SubTitle
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
                            Items = new List<Item>
                            {
                                new Item
                                {
                                    SimpleResponse = assistantResponse
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
