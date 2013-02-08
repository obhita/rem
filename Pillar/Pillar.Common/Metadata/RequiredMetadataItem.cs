namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Metadata for marking as required or not.
    /// </summary>
    public class RequiredMetadataItem : UiMetadataItem
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this is required.
        /// </summary>
        /// <value><c>true</c> if this is required; otherwise, <c>false</c>.</value>
        public bool IsRequired { get; set; }

        #endregion
    }
}
