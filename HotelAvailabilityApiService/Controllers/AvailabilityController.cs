﻿using HotelAvailabilityApiService.Models.Request;
using HotelAvailabilityApiService.Models.Response;
using HotelAvailabilityApiService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Controllers
{
    [Route("api/[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IIntentService _intentService;
        public AvailabilityController(IIntentService intentService)
        {
            _intentService = intentService;
        }

        [HttpPost]
        public async Task<JsonResult> Index([FromBody] IntentRequest request)
        {
            try
            {
                var response = await _intentService.GetIntentResponse(request);
                return new JsonResult(response);
            }catch(Exception ex)
            {
                return new JsonResult(ex);
            }
        }
    }
}