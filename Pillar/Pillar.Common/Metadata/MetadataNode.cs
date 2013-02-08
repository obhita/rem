using System;
using System.Linq;
using Pillar.Common.Utility;

namespace Pillar.Common.Metadata
{
    /// <summary>
    /// MetadataNode class.
    /// </summary>
    public class MetadataNode : MetadataBase
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataNode"/> class.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        public MetadataNode ( string resourceName )
            : base ( resourceName )
        {
        }

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
