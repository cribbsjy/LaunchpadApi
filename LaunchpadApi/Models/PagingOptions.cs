using Launchpad.Api.Models.Adjustability;

namespace Launchpad.Api.Models
{
    public abstract class PagingOptions : IAdjustable
    {
        public int? Take { get; set; }

        public int? Skip { get; set; }

        public string Sort { get; set; }

        public string Fields { get; set; }
    }
}
