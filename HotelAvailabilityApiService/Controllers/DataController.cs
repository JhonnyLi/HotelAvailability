using HotelAvailabilityApiService.Models.Data;
using HotelAvailabilityApiService.Models.Hotels;
using HotelAvailabilityApiService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HotelAvailabilityApiService.Services.HotelService;

namespace HotelAvailabilityApiService.Controllers
{
    [Route("data")]
    public class DataController : Controller
    {
        private readonly IHotelService _hotelService;
        public DataController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet("entities")]
        public async Task<JsonResult> GetEntitiesAsJson()
        {
            var hotelResult = await _hotelService.GetHotelsWithFieldsAsync(new List<HotelAttributeFields>());
            var entitiesResponse = CreateEntitiesModelFromHotelResult(hotelResult);
            return Json(entitiesResponse);
        }

        private DialogFlowEntities CreateEntitiesModelFromHotelResult(GetHotelsResponse hotelsResult)
        {
            var model = new DialogFlowEntities();
            foreach(var hotel in hotelsResult.Data)
            {
                var entity = new DialogFlowEntity();
                entity.Value = hotel.Attributes.Name;
                foreach(var keyword in hotel.Attributes.KeyWords)
                {
                    entity.Synonyms.Add(keyword);
                }
                model.Entities.Add(entity);
            }
            return model;
        }
    }
}