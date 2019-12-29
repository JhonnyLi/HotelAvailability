using HotelAvailabilityApiService.Models.Request;
using HotelAvailabilityApiService.Models.Response;
using System.Threading.Tasks;

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
            _httpClient.SetHeader("Test", "TestValue");
            return CreateResponse(request);
        }

        private static IntentResponse CreateResponse(IntentRequest request)
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
                                    textToSpeech = request.queryResult.fulfillmentText
                                }
                            },
                            new Item
                            {
                                simpleResponse = new Simpleresponse
                                {
                                    textToSpeech = request.queryResult.queryText
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
