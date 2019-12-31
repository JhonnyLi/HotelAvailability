using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelAvailabilityApiService.Models.Response
{

    public class IntentResponse
    {
        [JsonPropertyName("fulfillmentText")]
        public string FulfillmentText { get; set; }
        [JsonPropertyName("fulfillmentMessages")]
        public IList<Fulfillmentmessage> FulfillmentMessages { get; set; }
        [JsonPropertyName("source")]
        public string Source { get; set; }
        [JsonPropertyName("payload")]
        public Payload Payload { get; set; }
        [JsonPropertyName("outputContexts")]
        public IList<Outputcontext> OutputContexts { get; set; }
        [JsonPropertyName("followupEventInput")]
        public Followupeventinput FollowupEventInput { get; set; }
    }

    public class Payload
    {
        [JsonPropertyName("google")]
        public Google Google { get; set; }
        [JsonPropertyName("facebook")]
        public Facebook Facebook { get; set; }
        [JsonPropertyName("slack")]
        public Slack Slack { get; set; }
    }

    public class Google
    {
        [JsonPropertyName("expectUserResponse")]
        public bool ExpectUserResponse { get; set; }
        [JsonPropertyName("richResponse")]
        public Richresponse RichResponse { get; set; }
    }

    public class Richresponse
    {
        [JsonPropertyName("items")]
        public IList<Item> Items { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("simpleResponse")]
        public Simpleresponse SimpleResponse { get; set; }
    }

    public class Simpleresponse
    {
        [JsonPropertyName("textToSpeech")]
        public string TextToSpeech { get; set; }
    }

    public class Facebook
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class Slack
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class Followupeventinput
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("languageCode")]
        public string LanguageCode { get; set; }
        [JsonPropertyName("parameters")]
        public Parameters Parameters { get; set; }
    }

    public class Parameters
    {
        [JsonPropertyName("param")]
        public string Param { get; set; }
    }

    public class Fulfillmentmessage
    {
        [JsonPropertyName("card")]
        public Card Card { get; set; }
    }

    public class Card
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("subtitle")]
        public string Subtitle { get; set; }
        [JsonPropertyName("imageUri")]
        public string ImageUri { get; set; }
        [JsonPropertyName("buttons")]
        public IList<Button> Buttons { get; set; }
    }

    public class Button
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("postback")]
        public string Postback { get; set; }
    }

    public class Outputcontext
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("lifespanCount")]
        public int LifespanCount { get; set; }
        [JsonPropertyName("parameters")]
        public Parameters Parameters { get; set; }
    }
}
