using System.Collections.Generic;
using System.Threading.Tasks;
using Launchpad.Core.DTOs;

namespace Launchpad.Core.Managers.Interfaces
{
    public interface ILaunchpadManager
    {
        Task<ICollection<LaunchpadDto>> GetAllLaunchpads();
        Task<ICollection<LaunchpadDto>> GetLaunchpadById(string id);
    }
}
