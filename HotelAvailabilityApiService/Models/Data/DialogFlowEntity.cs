using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Models.Data
{
    public class DialogFlowEntities
    {
        public IList<DialogFlowEntity> Entities { get; set; } = new List<DialogFlowEntity>();
    }

    public class DialogFlowEntity
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("synonyms")]
        public List<string> Synonyms { get; set; } = new List<string>();
    }

}
