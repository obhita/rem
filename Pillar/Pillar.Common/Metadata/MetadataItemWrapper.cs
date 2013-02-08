namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Class for wrapping metadata item.
    /// </summary>
    internal class MetadataItemWrapper
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataItemWrapper"/> class.
        /// </summary>
        /// <param name="metadataItem">The metadata item.</param>
        /// <param name="metadataLayerLevel">The metadata layer level.</param>
        public MetadataItemWrapper ( IMetadataItem metadataItem, int metadataLayerLevel )
        {
            MetadataItem = metadataItem;
            MetadataLayerLevel = metadataLayerLevel;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the metadata item.
        /// </summary>
        public IMetadataItem MetadataItem { get; private set; }

        /// <summary>
        /// Gets the metadata layer level.
        /// </summary>
        public int MetadataLayerLevel { get; private set; }

        #endregion
    }
}
