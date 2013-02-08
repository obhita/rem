namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Metadata for specifying Display Name.
    /// </summary>
    public class DisplayNameMetadataItem : DisplayNameMetadataItemBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the item.</value>
        public string Name { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the display value.
        /// </summary>
        /// <returns>A <see cref="System.String"/></returns>
        public override string GetDisplayValue ()
        {
            return Name;
        }

        #endregion
    }
}
