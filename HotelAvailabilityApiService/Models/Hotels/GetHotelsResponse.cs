using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelAvailabilityApiService.Models.Hotels
{

    public class GetHotelsResponse
    {
        [JsonPropertyName("data")]
        public IList<Hotel> Data { get; set; }
        [JsonPropertyName("links")]
        public Links Links { get; set; }
    }

    public class Links
    {
        [JsonPropertyName("self")]
        public string Self { get; set; }
    }

    public class Hotel
    {
        [JsonPropertyName("attributes")]
        public Attributes Attributes { get; set; }
        [JsonPropertyName("relationships")]
        public Relationships Relationships { get; set; }
        [JsonPropertyName("type")]
        public string HotelType { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("links")]
        public HotelLinks Links { get; set; }
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }
    }

    public class Attributes
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("address")]
        public Address Address { get; set; }
    }

    public class Address
    {
        [JsonPropertyName("streetAddress")]
        public string StreetAddress { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("zipCode")]
        public string ZipCode { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
    }

    public class Relationships
    {
    }

    public class HotelLinks
    {
        [JsonPropertyName("self")]
        public string Self { get; set; }
    }

    public class Meta
    {
        [JsonPropertyName("specialAlerts")]
        public IList<SpecialAlert> SpecialAlerts { get; set; }
    }

    public class SpecialAlert
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("displayInBookingFlow")]
        public bool DisplayInBookingFlow { get; set; }
        [JsonPropertyName("startDate")]
        public string StartDate { get; set; }
        [JsonPropertyName("endDate")]
        public string EndDate { get; set; }
    }

}
