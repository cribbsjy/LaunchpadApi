using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Launchpad.Core.DTOs;
using Launchpad.Core.Managers.Interfaces;
using Launchpad.Core.Services.Interfaces;

namespace Launchpad.Core.Managers
{
    public class LaunchpadManager : ILaunchpadManager
    {
        private readonly ILaunchpadService _launchpadService;

        public LaunchpadManager(ILaunchpadService launchpadService)
        {
            _launchpadService = launchpadService;
        }

        public Task<ICollection<LaunchpadDto>> GetAllLaunchpads()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<LaunchpadDto>> GetLaunchpadById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
