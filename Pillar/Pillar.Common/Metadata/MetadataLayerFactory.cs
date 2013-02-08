using Pillar.Common.Utility;

namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Factory for metadata layer.
    /// </summary>
    public class MetadataLayerFactory : IMetadataLayerFactory
    {
        #region Constants and Fields

        private readonly IMetadataLayerRepository _metadataLayerRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataLayerFactory"/> class.
        /// </summary>
        /// <param name="metadataLayerRepository">The metadata layer repository.</param>
        public MetadataLayerFactory ( IMetadataLayerRepository metadataLayerRepository )
        {
            _metadataLayerRepository = metadataLayerRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the metadata layer.
        /// </summary>
        /// <param name="layerName">Name of the layer.</param>
        /// <param name="layerLevel">The layer level.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.MetadataLayer"/></returns>
        public MetadataLayer CreateMetadataLayer ( string layerName, int layerLevel )
        {
            var entity = new MetadataLayer ( layerName, layerLevel );
            _metadataLayerRepository.MakePersistent ( entity );

            return entity;
        }

        /// <summary>
        /// Destroys the metadata layer.
        /// </summary>
        /// <param name="metadataLayer">The metadata layer.</param>
        public void DestroyMetadataLayer ( MetadataLayer metadataLayer )
        {
            Check.IsNotNull ( metadataLayer, "metadataLayer is required." );

            _metadataLayerRepository.MakeTransient ( metadataLayer );
        }

        #endregion
    }
}
