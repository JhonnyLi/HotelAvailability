using HotelAvailabilityApiService.Models.Availability;
using HotelAvailabilityApiService.Models.Request;
using HotelAvailabilityApiService.Models.Response;
using System.Collections.Generic;
using System.Globalization;

namespace HotelAvailabilityApiService.Models.AvailabilityMessageModel
{
    public class AvailabilityMessageModel
    {
        public AvailabilityMessageModel(IntentRequest request, GetAvailabilityResponse availability)
        {
            HotelName = request.QueryResult.Parameters.Hotel;
            CheckInDate = request.QueryResult.Parameters.Date.ToString("ddd d MMM", CultureInfo.CreateSpecificCulture("en-US"));
            CheckoutDate = request.QueryResult.Parameters.LeavingDate.ToString("ddd d MMM", CultureInfo.CreateSpecificCulture("en-US"));
        }

        public string FulFillmentMessage { get; set; }
        public string HotelName { get; }
        public string CheckInDate { get; }
        public string CheckoutDate { get; }

        public List<CardMessage> CardMessages { get; set; } = new List<CardMessage>();
        public List<Simpleresponse> SimpleResponses { get; set; } = new List<Simpleresponse>();

        public class CardMessage
        {
            public string Title { get; set; }
            public string SubTitle { get; set; }
        }
    }
}
