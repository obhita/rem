namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Metadata for marking as hidden or not.
    /// </summary>
    public class HiddenMetadataItem : UiMetadataItem
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this is hidden.
        /// </summary>
        /// <value><c>true</c> if this is hidden; otherwise, <c>false</c>.</value>
        public bool IsHidden { get; set; }

        #endregion
    }
}
