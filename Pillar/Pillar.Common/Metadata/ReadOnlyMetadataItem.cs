namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Metadata for marking as read-only or not.
    /// </summary>
    public class ReadonlyMetadataItem : UiMetadataItem
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this is readonly.
        /// </summary>
        /// <value><c>true</c> if this is readonly; otherwise, <c>false</c>.</value>
        public bool IsReadonly { get; set; }

        #endregion
    }
}
