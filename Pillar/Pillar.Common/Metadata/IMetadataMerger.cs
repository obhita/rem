using System.Collections.Generic;

namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Interface for merging metadata.
    /// </summary>
    public interface IMetadataMerger
    {
        #region Public Methods

        /// <summary>
        /// Merges the metadata.
        /// </summary>
        /// <param name="metadataRootList">The metadata root list.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.IMetadata"/></returns>
        IMetadata MergeMetadata ( IEnumerable<MetadataRoot> metadataRootList );

        #endregion
    }
}
