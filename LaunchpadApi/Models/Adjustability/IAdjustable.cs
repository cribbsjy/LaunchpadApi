namespace Launchpad.Api.Models.Adjustability
{
    public interface IAdjustable
    {
        int? Skip { get; }
        int? Take { get; }

        /// <summary>
        /// Gets a comma-separated list of fields that orderd ascending or decending
        /// by prefixing +/- to the field name. Defaults to ascending.
        /// </summary>
        string Sort { get; }

        /// <summary>
        /// Gets a comma-separated list of fields that
        /// specifies which fields are returned in the result set
        /// </summary>
        string Fields { get; }
    }
}
