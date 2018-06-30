using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Launchpad.Core.DTOs;
using Launchpad.Core.Managers.Interfaces;
using Launchpad.Core.Services.Interfaces;

namespace Launchpad.Core.Managers
{
    public class LaunchpadManager : ILaunchpadManager
    {
        private readonly IMapper _mapper;
        private readonly ILaunchpadService _launchpadService;

        public LaunchpadManager(IMapper mapper, ILaunchpadService launchpadService)
        {
            _mapper = mapper;
            _launchpadService = launchpadService;
        }

        public async Task<ICollection<LaunchpadDto>> GetAllLaunchpads(SearchLaunchpadDto dto)
        {
            // Get launchpad data from data store
            var launchpadsList = await _launchpadService.GetAllLaunchpads();
            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                // Name is a "like" comparison
                launchpadsList = launchpadsList.Where(q => q.Full_Name.Contains(dto.Name));
            }

            if (!string.IsNullOrWhiteSpace(dto.Status))
            {
                // Status is an exact match
                launchpadsList = launchpadsList.Where(q => q.Status == dto.Status);
            }

            return _mapper.Map<List<LaunchpadDto>>(launchpadsList.ToList());
        }

        public async Task<LaunchpadDto> GetLaunchpadById(string id)
        {
            var response = await _launchpadService.GetLaunchpadById(id);
            return _mapper.Map<LaunchpadDto>(response);
        }
    }
}
