using Pillar.Common.Utility;

namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Layer of metadata.
    /// </summary>
    public class MetadataLayer
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataLayer"/> class.
        /// </summary>
        internal MetadataLayer ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataLayer"/> class.
        /// </summary>
        /// <param name="name">The name of the layer.</param>
        /// <param name="level">The level of the layer.</param>
        internal MetadataLayer ( string name, int level )
        {
            Check.IsNotNullOrWhitespace ( name, () => Name );

            Name = name;
            Level = level;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id of the layer.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level of the layer.</value>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the layer.</value>
        public string Name { get; set; }

        #endregion
    }
}
