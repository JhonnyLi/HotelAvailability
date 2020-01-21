using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelAvailabilityApiService.Models.Availability
{

    public class GetAvailabilityResponse
    {
        [JsonPropertyName("data")]
        public List<AvailabilityData> Data { get; set; } = new List<AvailabilityData>();
        [JsonPropertyName("links")]
        public SelfLinks SelfLinks { get; set; }
    }

    public class SelfLinks
    {
        [JsonPropertyName("self")]
        public string Self { get; set; }
    }

    public class AvailabilityData
    {
        [JsonPropertyName("attributes")]
        public AvailabilityAttributes Attributes { get; set; }
        [JsonPropertyName("relationships")]
        public Relationships Relationships { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("links")]
        public SelfLinks Links { get; set; }
    }

    public class AvailabilityAttributes
    {
        [JsonPropertyName("persistedUntil")]
        public DateTime PersistedUntil { get; set; }
        [JsonPropertyName("timeZoneId")]
        public string TimeZoneId { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("roomsLeft")]
        public int RoomsLeft { get; set; }
        [JsonPropertyName("shouldDisplayRoomsLeft")]
        public bool ShouldDisplayRoomsLeft { get; set; }
        [JsonPropertyName("rates")]
        public List<Rate> Rates { get; set; } = new List<Rate>();
        [JsonPropertyName("roomTypes")]
        public List<string> RoomTypes { get; set; } = new List<string>();
        [JsonPropertyName("roomCategoryContentId")]
        public int RoomCategoryContentId { get; set; }
        [JsonPropertyName("requestData")]
        public AvailabilityRequestdata RequestData { get; set; }
        [JsonPropertyName("occupancy")]
        public Occupancy Occupancy { get; set; }
    }

    public class AvailabilityRequestdata
    {
        [JsonPropertyName("checkInDate")]
        public string CheckInDate { get; set; }
        [JsonPropertyName("checkOutDate")]
        public string CheckOutDate { get; set; }
        [JsonPropertyName("occupancies")]
        public List<Occupancy> Occupancies { get; set; } = new List<Occupancy>();
        [JsonPropertyName("hotel")]
        public string Hotel { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("language")]
        public string Language { get; set; }
    }

    public class Occupancy
    {
        [JsonPropertyName("adults")]
        public int Adults { get; set; }
    }

    public class Rate
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("rateType")]
        public string RateType { get; set; }
        [JsonPropertyName("rateCode")]
        public string RateCode { get; set; }
        [JsonPropertyName("generalTerms")]
        public List<string> GeneralTerms { get; set; } = new List<string>();
        [JsonPropertyName("cancellationRule")]
        public string CancellationRule { get; set; }
        [JsonPropertyName("mustBeGuaranteed")]
        public bool MustBeGuaranteed { get; set; }
        [JsonPropertyName("pricePerNight")]
        public Pricepernight PricePerNight { get; set; }
        [JsonPropertyName("pricePerStay")]
        public Priceperstay PricePerStay { get; set; }
        [JsonPropertyName("bookingCode")]
        public object BookingCode { get; set; }
        [JsonPropertyName("breakfastIncluded")]
        public bool BreakfastIncluded { get; set; }
    }

    public class Pricepernight
    {
        [JsonPropertyName("localCurrency")]
        public CurrencyData LocalCurrency { get; set; }
        [JsonPropertyName("requestedCurrency")]
        public CurrencyData RequestedCurrency { get; set; }
    }

    public class CurrencyData
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("amount")]
        public float Amount { get; set; }
        [JsonPropertyName("regularAmount")]
        public float RegularAmount { get; set; }
        [JsonPropertyName("discountRate")]
        public float DiscountRate { get; set; }
        [JsonPropertyName("discountAmount")]
        public float DiscountAmount { get; set; }
        [JsonPropertyName("points")]
        public int Points { get; set; }
        [JsonPropertyName("numberOfVouchers")]
        public int NumberOfVouchers { get; set; }
        [JsonPropertyName("numberOfBonusCheques")]
        public int NumberOfBonusCheques { get; set; }
    }

    public class Priceperstay
    {
        [JsonPropertyName("localCurrency")]
        public CurrencyData LocalCurrency { get; set; }
        [JsonPropertyName("requestedCurrency")]
        public CurrencyData RequestedCurrency { get; set; }
    }

    public class Relationships
    {
        [JsonPropertyName("roomCategory")]
        public Roomcategory RoomCategory { get; set; }
        [JsonPropertyName("hotel")]
        public AvailabilityHotel Hotel { get; set; }
    }

    public class Roomcategory
    {
        [JsonPropertyName("links")]
        public RelatedLinks RelatedLinks { get; set; }
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }

    public class RelatedLinks
    {
        [JsonPropertyName("related")]
        public string Related { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class AvailabilityHotel
    {
        [JsonPropertyName("links")]
        public RelatedLinks RelatedLinks { get; set; }
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }
}
