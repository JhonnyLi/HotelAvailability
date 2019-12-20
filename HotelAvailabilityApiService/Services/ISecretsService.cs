using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAvailabilityApiService.Services
{
    public interface ISecretsService
    {
        Task<string> GetSecretAsync(string keyName);
        string GetSecret(string keyName);
    }
}
