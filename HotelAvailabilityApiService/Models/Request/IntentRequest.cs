using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelAvailabilityApiService.Models.Request
{

    public class IntentRequest
    {
        [JsonPropertyName("responseId")]
        public string ResponseId { get; set; }
        [JsonPropertyName("queryResult")]
        public Queryresult QueryResult { get; set; }
        [JsonPropertyName("originalDetectIntentRequest")]
        public Originaldetectintentrequest OriginalDetectIntentRequest { get; set; }
        [JsonPropertyName("session")]
        public string Session { get; set; }
    }

    public class Queryresult
    {
        [JsonPropertyName("queryText")]
        public string QueryText { get; set; }
        [JsonPropertyName("action")]
        public string Action { get; set; }
        [JsonPropertyName("parameters")]
        public Parameters Parameters { get; set; }
        [JsonPropertyName("allRequiredParamsPresent")]
        public bool AllRequiredParamsPresent { get; set; }
        [JsonPropertyName("fulfillmentText")]
        public string FulfillmentText { get; set; }
        [JsonPropertyName("fulfillmentMessages")]
        public IList<Fulfillmentmessage> FulfillmentMessages { get; set; }
        [JsonPropertyName("intent")]
        public Intent Intent { get; set; }
        [JsonPropertyName("intentDetectionConfidence")]
        public double IntentDetectionConfidence { get; set; }
        [JsonPropertyName("languageCode")]
        public string LanguageCode { get; set; }
    }

    public class Parameters
    {
        [JsonPropertyName("hotel")]
        public string Hotel { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("leavingdate")]
        public DateTime LeavingDate { get; set; }
        [JsonPropertyName("adults")]
        public double Adults { get; set; } = 1;
    }

    public class DateTimeInfo
    {
        [JsonPropertyName("startDateTime")]
        public string StartDateTime { get; set; }
        [JsonPropertyName("endDateTime")]
        public string EndDateTime { get; set; }
    }

    public class Intent
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }

    public class Fulfillmentmessage
    {
        [JsonPropertyName("text")]
        public Text Text { get; set; }
    }

    public class Text
    {
        [JsonPropertyName("text")]
        public IList<string> Texts { get; set; }
    }

    public class Originaldetectintentrequest
    {
        [JsonPropertyName("source")]
        public string Source { get; set; }
        [JsonPropertyName("version")]
        public string Version { get; set; }
        [JsonPropertyName("payload")]
        public Payload Payload { get; set; }
    }

    public class Payload
    {
        [JsonPropertyName("user")]
        public User User { get; set; }
        [JsonPropertyName("conversation")]
        public Conversation Conversation { get; set; }
        [JsonPropertyName("inputs")]
        public IList<Input> Inputs { get; set; }
        [JsonPropertyName("surface")]
        public Surface Surface { get; set; }
        [JsonPropertyName("isInSandbox")]
        public bool IsInSandbox { get; set; }
        [JsonPropertyName("availableSurfaces")]
        public IList<Availablesurface> AvailableSurfaces { get; set; }
        [JsonPropertyName("requestType")]
        public string RequestType { get; set; }
    }

    public class User
    {
        [JsonPropertyName("locale")]
        public string Locale { get; set; }
        [JsonPropertyName("userVerificationStatus")]
        public string UserVerificationStatus { get; set; }
    }

    public class Conversation
    {
        [JsonPropertyName("conversationId")]
        public string ConversationId { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("conversationToken")]
        public string ConversationToken { get; set; }
    }

    public class Surface
    {
        [JsonPropertyName("capabilities")]
        public IList<Capability> Capabilities { get; set; }
    }

    public class Capability
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Input
    {
        [JsonPropertyName("intent")]
        public string Intent { get; set; }
        [JsonPropertyName("rawInputs")]
        public IList<Rawinput> RawInputs { get; set; }
        [JsonPropertyName("arguments")]
        public IList<Argument> Arguments { get; set; }
    }

    public class Rawinput
    {
        [JsonPropertyName("inputType")]
        public string InputType { get; set; }
        [JsonPropertyName("query")]
        public string Query { get; set; }
    }

    public class Argument
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("rawText")]
        public string RawText { get; set; }
        [JsonPropertyName("textValue")]
        public string TextValue { get; set; }
    }

    public class Availablesurface
    {
        [JsonPropertyName("capabilities")]
        public IList<Capability> Capabilities { get; set; }
    }
}
