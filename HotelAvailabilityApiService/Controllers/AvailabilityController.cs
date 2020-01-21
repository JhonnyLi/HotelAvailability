using HotelAvailabilityApiService.Models.Request;
using HotelAvailabilityApiService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly IIntentService _intentService;
        public AvailabilityController(IIntentService intentService)
        {
            _intentService = intentService;
        }

        [HttpPost]
        public async Task<JsonResult> RequestIntentResponse()
        {
            string jsonString;
            using (var reader = new StreamReader(Request.Body))
            {
                jsonString = await reader.ReadToEndAsync().ConfigureAwait(false);
                
            }
            var request = JsonConvert.DeserializeObject<IntentRequest>(jsonString,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});

            var response = await _intentService.GetIntentResponse(request);
            return new JsonResult(response);
        }
    }
}