namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Class for wrapping metadata.
    /// </summary>
    internal class MetadataWrapper
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        /// <value>The metadata.</value>
        public IMetadata Metadata { get; set; }

        /// <summary>
        /// Gets or sets the metadata layer id.
        /// </summary>
        /// <value>The metadata layer id.</value>
        public long MetadataLayerId { get; set; }

        #endregion
    }
}
