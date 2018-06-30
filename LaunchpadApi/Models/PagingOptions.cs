using System.ComponentModel.DataAnnotations;
using Launchpad.Api.Models.Adjustability;

namespace Launchpad.Api.Models
{
    public abstract class PagingOptions : IAdjustable
    {
        /// <summary>
        /// How many results to return
        /// </summary>
        [Range(0, int.MaxValue)]
        public int? Take { get; set; }

        /// <summary>
        /// How many results to skip before returning the results
        /// </summary>
        [Range(0, int.MaxValue)]
        public int? Skip { get; set; }

        /// <summary>
        /// Comma-delimited list of fields, denoting sort order.
        /// Prefix + to sort by field ascending
        /// Prefix - to sort by field descending
        /// </summary>
        [StringLength(1000)]
        public string Sort { get; set; }

        /// <summary>
        /// Comma-delimited list of fields, denoting dataset to return
        /// </summary>
        [StringLength(1000)]
        public string Fields { get; set; }
    }
}
