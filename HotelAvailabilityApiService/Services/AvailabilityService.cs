﻿using HotelAvailabilityApiService.Models.Availability;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IHttpService _httpClient;
        private readonly string _baseUrl;
        public AvailabilityService(IHttpService httpService, ISecretsService secrets)
        {
            _httpClient = httpService;
            _baseUrl = secrets.GetSecret("AvailabilityApiBaseUrl");
        }

        public async Task<GetAvailabilityResponse> GetAvailabilityForHotelByIdAndStartDateAsync(string id, DateTime checkinDate,DateTime checkoutDate)
        {
            var uri = $"{_baseUrl}/availability/availabilities/?language=en&filter[hotel]={id}&filter[checkindate]={checkinDate.ToShortDateString()}&filter[checkoutdate]={checkoutDate.ToShortDateString()}&filter[occupancies]=[1]";
            var result = await _httpClient.GetAsync<GetAvailabilityResponse>(uri).ConfigureAwait(false);
            return result;
        }
    }
}