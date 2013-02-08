using System.Collections.Generic;

namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Interface of repository for metadata.
    /// </summary>
    public interface IMetadataRepository : IRepository<IMetadata>
    {
        #region Public Methods

        /// <summary>
        /// Finds the metadata.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerable&lt;IMetadata&gt;"/></returns>
        IEnumerable<IMetadata> FindMetadata ( string searchString );

        /// <summary>
        /// Finds the metadata.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <param name="layerName">Name of the layer.</param>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerable&lt;IMetadata&gt;"/></returns>
        IEnumerable<IMetadata> FindMetadata ( string searchString, string layerName );

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.IMetadata"/></returns>
        IMetadata GetMetadata ( string resourceName );

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="layerName">Name of the layer.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.IMetadata"/></returns>
        IMetadata GetMetadata ( string resourceName, string layerName );

        #endregion
    }
}
