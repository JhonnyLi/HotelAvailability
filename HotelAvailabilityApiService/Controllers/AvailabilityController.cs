using HotelAvailabilityApiService.Models.Request;
using HotelAvailabilityApiService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
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
                jsonString = await reader.ReadToEndAsync();
                
            }
            var request = JsonConvert.DeserializeObject<IntentRequest>(jsonString,new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore});
            DateTime date;
            string hotel;
            DateTime stayingDays;
            if (request.QueryResult.Action == "availability_action")
            {
                var requestParameters = request.QueryResult.Parameters;
                date = requestParameters.Date;
                hotel = requestParameters.Hotel;
                stayingDays = requestParameters.LeavingDate;
            }
            var response = await _intentService.GetIntentResponse(request);
            return new JsonResult(response);
        }
    }
}