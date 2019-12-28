using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelAvailabilityApiService.Models.Request;
using HotelAvailabilityApiService.Models.Response;

namespace HotelAvailabilityApiService.Services
{
    public class IntentService : IIntentService
    {
        private readonly IHttpService _httpClient;
        public IntentService(IHttpService httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IntentResponse> GetIntentResponse(IntentRequest request)
        {
            var response = new IntentResponse();
            response.payload = new Payload
            {
                google = new Google
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
