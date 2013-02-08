namespace Pillar.Common.Metadata.Dtos
{
    /// <summary>
    /// Data transfer object for Metadata class.
    /// </summary>
    public partial class MetadataDto
    {
        #region Public Methods

        /// <summary>
        /// Adds the child metadata.
        /// </summary>
        /// <param name="metadataDto">The metadata dto.</param>
        public void AddChildMetadata ( MetadataDto metadataDto )
        {
            _children.Add ( metadataDto );
        }

        #endregion
    }
}
