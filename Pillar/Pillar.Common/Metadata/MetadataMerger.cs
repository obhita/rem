using System;
using System.Collections.Generic;
using System.Linq;
using Pillar.Common.Utility;

namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Class for merging metadata.
    /// </summary>
    public class MetadataMerger : IMetadataMerger
    {
        #region Constants and Fields

        private readonly IMetadataLayerRepository _metadataLayerRepository;
        private readonly IEnumerable<MetadataLayer> _metadataLayers;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataMerger"/> class.
        /// </summary>
        /// <param name="metadataLayerRepository">The metadata layer repository.</param>
        public MetadataMerger ( IMetadataLayerRepository metadataLayerRepository )
        {
            Check.IsNotNull ( metadataLayerRepository, "metadataLayerRepository is required." );

            _metadataLayerRepository = metadataLayerRepository;
            _metadataLayers = _metadataLayerRepository.GetAllMetadataLayers ();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Merges Metadata
        /// </summary>
        /// <param name="metadataRootList">These metadatas are from all levels. They have same resource name</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.IMetadata"/></returns>
        public IMetadata MergeMetadata ( IEnumerable<MetadataRoot> metadataRootList )
        {
            if ( metadataRootList == null )
            {
                throw new ArgumentNullException ( "metadataRootList" );
            }

            if ( metadataRootList.Count () == 0 )
            {
                throw new ArgumentException ( "metadataRootList should contain at least one metadata.", "metadataRootList" );
            }

            if ( metadataRootList.Select ( x => x.ResourceName ).Distinct ().Count () > 1 )
            {
                throw new NotSupportedException ( "All metadata in metadataRootList should be for the same resource." );
            }

            var metadataWrapperList = BuildMetadataWrapperList ( metadataRootList );
            var mergedMetadata = MergeMetadata ( metadataWrapperList, null );

            return mergedMetadata;
        }

        #endregion

        // Merge metadata from different levels for the same resource

        #region Methods

        /// <summary>
        /// Builds the metadata item wrapper list.
        /// </summary>
        /// <param name="metadataWrappers">The metadata wrappers.</param>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerable&lt;MetadataItemWrapper&gt;"/></returns>
        private IEnumerable<MetadataItemWrapper> BuildMetadataItemWrapperList ( IEnumerable<MetadataWrapper> metadataWrappers )
        {
            var metadataItemWrapperList = ( from metadataWrapper in metadataWrappers
                                                                         let metadataLayerId = metadataWrapper.MetadataLayerId
                                                                         let metadataLayerLevel =
                                                                             _metadataLayers.Single ( layer => layer.Id == metadataLayerId ).Level
                                                                         from metadataItem in metadataWrapper.Metadata.MetadataItems
                                                                         select new MetadataItemWrapper ( metadataItem, metadataLayerLevel ) );
            return metadataItemWrapperList;
        }

        /// <summary>
        /// Builds the metadata wrapper list.
        /// </summary>
        /// <param name="metadataRootList">The metadata root list.</param>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerable&lt;MetadataWrapper&gt;"/></returns>
        private IEnumerable<MetadataWrapper> BuildMetadataWrapperList ( IEnumerable<MetadataRoot> metadataRootList )
        {
            var metadataWrapperList = metadataRootList.Select (
                metadataRoot => new MetadataWrapper
                    {
                        MetadataLayerId = metadataRoot.MetadataLayerId,
                        Metadata = metadataRoot
                    } );
            return metadataWrapperList;
        }

        /// <summary>
        /// Does the merge metadata.
        /// </summary>
        /// <param name="metadataWrappers">The metadata wrappers.</param>
        /// <param name="metadataRoot">The metadata root.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.IMetadata"/></returns>
        private IMetadata DoMergeMetadata ( IEnumerable<MetadataWrapper> metadataWrappers, IMetadata metadataRoot )
        {
            var combinedMetadataItemWrappers = BuildMetadataItemWrapperList ( metadataWrappers );
            var mergedMetadataItems = ExtractMergedMetadataItems ( combinedMetadataItemWrappers );
            var resourceName = metadataWrappers.First ().Metadata.ResourceName;

            var mergedMetadata = metadataRoot != null
                                           ? metadataRoot.AddChild ( resourceName )
                                           : new MetadataNode ( resourceName );
            mergedMetadata.MetadataItems = new List<IMetadataItem> ( mergedMetadataItems );

            return mergedMetadata;
        }

        /// <summary>
        /// Extracts the merged metadata items.
        /// </summary>
        /// <param name="metadataItemWrapperList">The metadata item wrapper list.</param>
        /// <returns>A <see cref="System.Collections.Generic.IList&lt;IMetadataItem&gt;"/></returns>
        private IList<IMetadataItem> ExtractMergedMetadataItems ( IEnumerable<MetadataItemWrapper> metadataItemWrapperList )
        {
            var metadataItemWrapperGroupsByItemType =
                from metadataItemWrapper in metadataItemWrapperList
                group metadataItemWrapper by metadataItemWrapper.MetadataItem.GetType ()
                into metadataItemGroup
                select new
                    {
                        MetadataItemType = metadataItemGroup.Key,
                        Item = metadataItemGroup.OrderBy ( item => item.MetadataLayerLevel ).Last ()
                    };

            var metadataItems = new List<IMetadataItem> ();

            foreach ( var metadataItemWrapperGroupByType in metadataItemWrapperGroupsByItemType )
            {
                var choosenMetadataItemWrapper = metadataItemWrapperGroupByType.Item;
                metadataItems.Add ( choosenMetadataItemWrapper.MetadataItem );
            }

            return metadataItems;
        }

        /// <summary>
        /// Merges the metadata.
        /// </summary>
        /// <param name="metadataWrapperList">The metadata wrapper list.</param>
        /// <param name="metadataRoot">The metadata root.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.IMetadata"/></returns>
        private IMetadata MergeMetadata ( IEnumerable<MetadataWrapper> metadataWrapperList, IMetadata metadataRoot )
        {
            // merge root metadata
            var mergedMetadata = DoMergeMetadata ( metadataWrapperList, metadataRoot );

            // merge children metadata
            var combinedChildMetadataWrappers = new List<MetadataWrapper> ();

            foreach ( var metadataWrapper in metadataWrapperList )
            {
                var metadataLayerId = metadataWrapper.MetadataLayerId;

                // combine all the child meta datas across all levels 
                var childMetadataWrappers = metadataWrapper.Metadata.Children
                    .Select ( x => new MetadataWrapper { Metadata = x, MetadataLayerId = metadataLayerId } );
                combinedChildMetadataWrappers.AddRange ( childMetadataWrappers );
            }

            if ( combinedChildMetadataWrappers.Count > 0 )
            {
                // to several meta data lists, each one of them has same resource name for all the meta datas in it
                var metadataGroupsByResourceName =
                    combinedChildMetadataWrappers.GroupBy ( p => p.Metadata.ResourceName );

                foreach ( var metadataGroup in metadataGroupsByResourceName )
                {
                    // recursive call to merge metadata from different levels for the same resource
                    MergeMetadata ( metadataGroup, mergedMetadata );
                }
            }

            return mergedMetadata;
        }

        #endregion
    }
}
