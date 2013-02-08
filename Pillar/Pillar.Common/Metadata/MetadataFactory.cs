using System;

namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Factory for metadata.
    /// </summary>
    public class MetadataFactory : IMetadataFactory
    {
        #region Constants and Fields

        private readonly IMetadataRepository _metadataRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataFactory"/> class.
        /// </summary>
        /// <param name="metadataRepository">The metadata repository.</param>
        public MetadataFactory ( IMetadataRepository metadataRepository )
        {
            _metadataRepository = metadataRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the metadata root.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="metadataLayer">The metadata layer.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.IMetadata"/></returns>
        public IMetadata CreateMetadataRoot ( string resourceName, MetadataLayer metadataLayer )
        {
            if ( metadataLayer == null )
            {
                throw new ArgumentNullException ( "metadataLayer" );
            }

            var metadataRoot = new MetadataRoot ( resourceName, metadataLayer.Id );
            _metadataRepository.MakePersistent ( metadataRoot );

            return metadataRoot;
        }

        /// <summary>
        /// Destroys the metadata root.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        public void DestroyMetadataRoot ( IMetadata metadata )
        {
            if ( metadata == null )
            {
                throw new ArgumentNullException ( "metadata" );
            }

            if ( !( metadata is MetadataRoot ) )
            {
                throw new ArgumentException ( "metadata should be MetadataRoot.", "metadata" );
            }

            _metadataRepository.MakeTransient ( metadata );
        }

        #endregion
    }
}
