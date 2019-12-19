using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Models.Request
{
    public class IntentRequest
    {
        public string responseId { get; set; }
        public string session { get; set; }
        public Queryresult queryResult { get; set; }
        public Originaldetectintentrequest originalDetectIntentRequest { get; set; }
    }

    public class Queryresult
    {
        public string queryText { get; set; }
        public Parameters parameters { get; set; }
        public bool allRequiredParamsPresent { get; set; }
        public string fulfillmentText { get; set; }
        public Fulfillmentmessage[] fulfillmentMessages { get; set; }
        public Outputcontext[] outputContexts { get; set; }
        public Intent intent { get; set; }
        public int intentDetectionConfidence { get; set; }
        public Diagnosticinfo diagnosticInfo { get; set; }
        public string languageCode { get; set; }
    }

    public class Parameters
    {
        public string param { get; set; }
    }

    public class Intent
    {
        public string name { get; set; }
        public string displayName { get; set; }
    }

    public class Diagnosticinfo
    {
    }

    public class Fulfillmentmessage
    {
        public Text text { get; set; }
    }

    public class Text
    {
        public string[] text { get; set; }
    }

    public class Outputcontext
    {
        public string name { get; set; }
        public int lifespanCount { get; set; }
        public Parameters1 parameters { get; set; }
    }

    public class Parameters1
    {
        public string param { get; set; }
    }

    public class Originaldetectintentrequest
    {
    }

}
