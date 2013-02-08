using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pillar.Common.Metadata.Dtos
{
    /// <summary>
    /// Data transfer object for FilterLookupMetadataItem class.
    /// </summary>
    [DataContract]
    public class FilterLookupMetadataItemDto : IMetadataItemDto
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the well known names.
        /// </summary>
        /// <value>The well known names.</value>
        [DataMember]
        public IEnumerable<string> WellKnownNames { get; set; }

        #endregion
    }
}
