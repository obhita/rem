namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Base class for DisplayNameMetadataItem
    /// </summary>
    public abstract class DisplayNameMetadataItemBase : UiMetadataItem
    {
        #region Public Methods

        /// <summary>
        /// Gets the display value.
        /// </summary>
        /// <returns>A <see cref="System.String"/></returns>
        public abstract string GetDisplayValue ();

        #endregion
    }
}
