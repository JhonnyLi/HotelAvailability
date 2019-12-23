using HotelAvailabilityApiService.Models.Request;
using HotelAvailabilityApiService.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace HotelAvailabilityApiService.Controllers
{
    [Route("api/[controller]")]
    public class AvailabilityController : ControllerBase
    {
        [HttpPost]
        public JsonResult Index([FromBody] IntentRequest request)
        {
            var response = GetIntentResponse(request);
            return new JsonResult(response);
        }

        private static IntentResponse GetIntentResponse(IntentRequest request)
        {
            var response = new IntentResponse();
            response.payload = new Payload
            {
                google = new HotelAvailabilityApiService.Models.Response.Google
                {
                    expectUserResponse = false,
                    richResponse = new Richresponse
                    {
                        items = new Item[]
                        {
                            new Item
                            {
                                simpleResponse = new Simpleresponse
                                {
                                    textToSpeech = "Det finns rum lediga."
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