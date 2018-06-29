using System.Collections.Generic;
using System.Threading.Tasks;
using Launchpad.Core.DTOs;

namespace Launchpad.Core.Services.Interfaces
{
    public interface ILaunchpadService
    {
        Task<ICollection<LaunchpadDto>> GetAllLaunchpads();
        Task<ICollection<LaunchpadDto>> GetLaunchpadById(string id);
    }
}
