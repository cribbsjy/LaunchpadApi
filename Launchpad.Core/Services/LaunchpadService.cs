using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Launchpad.Core.DTOs;
using Launchpad.Core.Factories;
using Launchpad.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Launchpad.Core.Services
{
    public class LaunchpadService : ILaunchpadService
    {
        private readonly HttpClient _httpClient;

        public LaunchpadService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.Create(configuration.GetConnectionString("SpaceX-API"));
        }

        public async Task<IEnumerable<SpaceXLaunchpadDto>> GetAllLaunchpads()
        {
            var httpResponse = await _httpClient.GetAsync("launchpads");
            return await httpResponse.ConvertResponseToObject<List<SpaceXLaunchpadDto>>();
        }

        public async Task<SpaceXLaunchpadDto> GetLaunchpadById(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"launchpads/{id}");
            return await httpResponse.ConvertResponseToObject<SpaceXLaunchpadDto>();
        }
    }
}
