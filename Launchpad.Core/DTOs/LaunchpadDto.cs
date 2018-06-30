using System.Collections.Generic;

namespace Launchpad.Core.DTOs
{
    public class LaunchpadDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }

    public class SearchLaunchpadDto
    {
        public string Name { get; set; }
        public string Status { get; set; }
    }

    public class SpaceXLaunchpadDto
    {
        public string Id { get; set; }
        public string Full_Name { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public IEnumerable<string> Vehicles_Launched { get; set; }
        public string Details { get; set; }
    }
}
