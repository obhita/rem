using System.Collections.Generic;

namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Interface of repository for metadata layer.
    /// </summary>
    public interface IMetadataLayerRepository : IRepository<MetadataLayer>
    {
        #region Public Methods

        /// <summary>
        /// Gets all metadata layers.
        /// </summary>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerable&lt;MetadataLayer&gt;"/></returns>
        IEnumerable<MetadataLayer> GetAllMetadataLayers ();

        /// <summary>
        /// Gets the name of the metadata layer by.
        /// </summary>
        /// <param name="layerName">Name of the layer.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.MetadataLayer"/></returns>
        MetadataLayer GetMetadataLayerByName ( string layerName );

        #endregion
    }
}
