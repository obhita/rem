namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Interface of factory for metadata layer.
    /// </summary>
    public interface IMetadataLayerFactory
    {
        #region Public Methods

        /// <summary>
        /// Creates the metadata layer.
        /// </summary>
        /// <param name="layerName">Name of the layer.</param>
        /// <param name="layerLevel">The layer level.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.MetadataLayer"/></returns>
        MetadataLayer CreateMetadataLayer ( string layerName, int layerLevel );

        /// <summary>
        /// Destroys the metadata layer.
        /// </summary>
        /// <param name="metadataLayer">The metadata layer.</param>
        void DestroyMetadataLayer ( MetadataLayer metadataLayer );

        #endregion
    }
}
