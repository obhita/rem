using System;
using System.Linq;
using Pillar.Common.Utility;

namespace Pillar.Common.Metadata
{
    /// <summary>
    /// MetadataRoot class.
    /// </summary>
    public class MetadataRoot : MetadataBase
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataRoot"/> class.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="metadataLayerId">The metadata layer id.</param>
        public MetadataRoot ( string resourceName, long metadataLayerId )
            : base ( resourceName )
        {
            MetadataLayerId = metadataLayerId;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id of the root.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets the metadata layer id.
        /// </summary>
        public long MetadataLayerId { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.IMetadata"/></returns>
        public override IMetadata AddChild ( string resourceName )
        {
            Check.IsNotNullOrWhitespace ( "resourceName", "resourceName is required." );

            // do not let users add resources that already exist
            if ( Children.Any ( c => c.ResourceName == resourceName ) )
            {
                // TODO: may need change to use a pre-defined exception
                throw new ApplicationException (
                    string.Format ( "The metadata with resource name {0} has already been existed.", resourceName ) );
            }

            var metadataNode = new MetadataNode ( resourceName );

            AddChild ( metadataNode );

            return metadataNode;
        }

        #endregion
    }
}
