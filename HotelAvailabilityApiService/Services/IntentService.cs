using HotelAvailabilityApiService.Models.Availability;
using HotelAvailabilityApiService.Models.AvailabilityMessageModel;
using HotelAvailabilityApiService.Models.Request;
using HotelAvailabilityApiService.Models.Response;
using System;
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
            var hotelId = HotelService.GetHotelGuidFromDictionary(request.QueryResult.Parameters.Hotel);
            var parameters = request.QueryResult.Parameters;
            var availability = !string.IsNullOrEmpty(hotelId) ? await _availabilityService.GetAvailabilityForHotelByIdAndStartDateAsync(hotelId, parameters.Date, parameters.LeavingDate, parameters.Adults) : new GetAvailabilityResponse{Data = new List<AvailabilityData>()};
            var messages = CreateResponseMessages(request, availability, hotelId);
            return CreateResponse(messages);


            //var hotel = await _hotelService.GetHotelByNameAsync(request.QueryResult.Parameters.Hotel);
            //var parameters = request.QueryResult.Parameters;
            //var availability = await _availabilityService.GetAvailabilityForHotelByIdAndStartDateAsync(hotel.Id, parameters.Date, parameters.LeavingDate, parameters.Adults);
            //var messages = CreateResponseMessages(request, availability);
            //return CreateResponse(messages);
        }

        private static AvailabilityMessageModel CreateResponseMessages(IntentRequest request, GetAvailabilityResponse availability, string hotelId)
        {
            var roomsAvailable = availability.Data.Any();
            var model = new AvailabilityMessageModel(request, availability);

            if (string.IsNullOrEmpty(hotelId))
            {
                model.FulFillmentMessage = $"I could not find {model.HotelName} in my database.";

                return model;
            }

            var ifNumberOfPeopleProvided = request.QueryResult.Parameters.Adults > 1 ? $" for {request.QueryResult.Parameters.Adults} people" : string.Empty;

            if (roomsAvailable)
            {
                var roomsWithRates = availability.Data.Where(d => d.Attributes.Rates.Any()).ToList();
                var allRates = roomsWithRates.Select(r => (r, r.Attributes.Rates.First())).ToList();
                allRates = allRates.OrderBy(o => (int)Math.Round(o.Item2.PricePerNight.LocalCurrency.Amount)).ToList();
                var (bestRate, pricePerNight) = allRates.First();
                
                var roomString = availability.Data.Count == 1 ? "a room" : "rooms";
                model.FulFillmentMessage = $"{model.HotelName} has {roomString} available{ifNumberOfPeopleProvided} between {model.CheckInDate} and {model.CheckoutDate}. The best price is {pricePerNight.PricePerNight.LocalCurrency.Amount}Sek per night.";
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
                
                model.FulFillmentMessage = $"{model.HotelName} has no available rooms{ifNumberOfPeopleProvided} between {model.CheckInDate} and {model.CheckoutDate}.";
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
