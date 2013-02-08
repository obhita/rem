using System.Collections.Generic;

namespace Pillar.Common.Metadata
{
    /// <summary>
    /// FilterLookupMetadataItem class.
    /// </summary>
    public class FilterLookupMetadataItem : UiMetadataItem
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the well known names.
        /// </summary>
        /// <value>The well known names.</value>
        public IEnumerable<string> WellKnownNames { get; set; }

        #endregion
    }
}
