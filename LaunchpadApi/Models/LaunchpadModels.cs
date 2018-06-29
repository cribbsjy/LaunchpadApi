namespace Launchpad.Api.Models
{
    public class LaunchpadModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }

    public class LaunchpadSearchRequest : PagingOptions
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
