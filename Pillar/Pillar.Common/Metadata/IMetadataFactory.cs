namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Interface of factory for metadata.
    /// </summary>
    public interface IMetadataFactory
    {
        #region Public Methods

        /// <summary>
        /// Creates the metadata root.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="metadataLayer">The metadata layer.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.IMetadata"/></returns>
        IMetadata CreateMetadataRoot ( string resourceName, MetadataLayer metadataLayer );

        /// <summary>
        /// Destroys the metadata root.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        void DestroyMetadataRoot ( IMetadata metadata );

        #endregion
    }
}
