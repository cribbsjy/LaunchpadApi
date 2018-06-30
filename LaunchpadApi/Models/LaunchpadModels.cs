namespace Launchpad.Api.Models
{
    public class LaunchpadModel
    {
        public string LaunchpadId { get; set; }
        public string LaunchpadName { get; set; }
        public string LaunchpadStatus { get; set; }
    }

    public class LaunchpadSearchRequest : PagingOptions
    {
        public string LaunchpadName { get; set; }
        public string LaunchpadStatus { get; set; }
    }
}
