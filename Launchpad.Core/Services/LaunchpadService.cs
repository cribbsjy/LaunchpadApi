using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Launchpad.Core.DTOs;
using Launchpad.Core.Factories;
using Launchpad.Core.Services.Interfaces;

namespace Launchpad.Core.Services
{
    public class LaunchpadService : ILaunchpadService
    {
        private readonly HttpClient _httpClient;

        public LaunchpadService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.Create("blah"); // TODO: Get the URI from the Configuration
        }

        public Task<ICollection<LaunchpadDto>> GetAllLaunchpads()
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<LaunchpadDto>> GetLaunchpadById(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
