using System.Collections.Generic;
using System.Threading.Tasks;
using Launchpad.Core.DTOs;

namespace Launchpad.Core.Services.Interfaces
{
    public interface ILaunchpadService
    {
        Task<IEnumerable<SpaceXLaunchpadDto>> GetAllLaunchpads();
        Task<SpaceXLaunchpadDto> GetLaunchpadById(string id);
    }
}
