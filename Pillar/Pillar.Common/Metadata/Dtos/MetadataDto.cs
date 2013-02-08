using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Pillar.Common.Metadata.Dtos
{
    /// <summary>
    /// Data transfer object for Metadata class.
    /// </summary>
    [DataContract]
    public partial class MetadataDto
    {
        #region Constants and Fields

        private ObservableCollection<MetadataDto> _children;
        private ObservableCollection<IMetadataItemDto> _metadataItemDtos;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataDto"/> class.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        public MetadataDto ( string resourceName )
        {
            ResourceName = resourceName;

            _children = new ObservableCollection<MetadataDto> ();
            _metadataItemDtos = new ObservableCollection<IMetadataItemDto> ();

            InitializeChildrenAndMetadataItemsDtos ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the children.
        /// </summary>
        public ReadOnlyObservableCollection<MetadataDto> Children { get; private set; }

        /// <summary>
        /// Gets the children list.
        /// </summary>
        [EditorBrowsable ( EditorBrowsableState.Never )]
        [DataMember]
        public IList<MetadataDto> ChildrenList
        {
            get { return _children.ToList ().AsReadOnly (); }
            internal set { _children = new ObservableCollection<MetadataDto> ( value ); }
        }

        /// <summary>
        /// Gets the metadata item dto list.
        /// </summary>
        [EditorBrowsable ( EditorBrowsableState.Never )]
        [DataMember]
        public IList<IMetadataItemDto> MetadataItemDtoList
        {
            get { return _metadataItemDtos.ToList ().AsReadOnly (); }
            internal set { _metadataItemDtos = new ObservableCollection<IMetadataItemDto> ( value ); }
        }

        /// <summary>
        /// Gets the metadata item dtos.
        /// </summary>
        public ReadOnlyObservableCollection<IMetadataItemDto> MetadataItemDtos { get; private set; }

        /// <summary>
        /// Gets the name of the resource.
        /// </summary>
        [DataMember]
        public string ResourceName { get; internal set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the child metadata.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.Dtos.MetadataDto"/></returns>
        public MetadataDto AddChildMetadata ( string resourceName )
        {
            if ( string.IsNullOrWhiteSpace ( resourceName ) )
            {
                throw new ArgumentNullException ( "resourceName" );
            }

            if ( ChildMetadataExists ( resourceName ) )
            {
                throw new ArgumentException ( string.Format ( "Child {0} already exists.", resourceName ) );
            }

            var child = new MetadataDto ( resourceName );
            _children.Add ( child );
            return child;
        }

        /// <summary>
        /// Adds the metadata item.
        /// </summary>
        /// <param name="metadataItemDto">The metadata item dto.</param>
        public void AddMetadataItem ( IMetadataItemDto metadataItemDto )
        {
            if ( metadataItemDto == null )
            {
                throw new ArgumentNullException ( "metadataItemDto" );
            }

            _metadataItemDtos.Add ( metadataItemDto );
        }

        /// <summary>
        /// Adds the metadata item range.
        /// </summary>
        /// <param name="metadataItemDtos">The metadata item dtos.</param>
        public void AddMetadataItemRange ( IEnumerable<IMetadataItemDto> metadataItemDtos )
        {
            if ( metadataItemDtos != null )
            {
                foreach ( var metadataItemDto in metadataItemDtos )
                {
                    if ( metadataItemDto == null )
                    {
                        throw new ArgumentException ( "Cannot add null MetadataItem." );
                    }
                    _metadataItemDtos.Add ( metadataItemDto );
                }
            }
        }

        /// <summary>
        /// Childs the metadata exists.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool ChildMetadataExists ( string resourceName )
        {
            if ( string.IsNullOrWhiteSpace ( resourceName ) )
            {
                throw new ArgumentNullException ( "resourceName" );
            }

            var childExists = _children.Any ( c => c.ResourceName == resourceName );

            return childExists;
        }

        /// <summary>
        /// Finds the metadata item.
        /// </summary>
        /// <typeparam name="TMetadataItem">The type of the metadata item.</typeparam>
        /// <returns>The metadata item or null.</returns>
        public TMetadataItem FindMetadataItem<TMetadataItem> () where TMetadataItem : IMetadataItemDto
        {
            var item = _metadataItemDtos.SingleOrDefault ( m => m.GetType () == typeof( TMetadataItem ) );
            return ( TMetadataItem )item;
        }

        /// <summary>
        /// Gets the child metadata.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>The metadata item or null.</returns>
        public MetadataDto GetChildMetadata ( string resourceName )
        {
            if ( string.IsNullOrWhiteSpace ( resourceName ) )
            {
                throw new ArgumentNullException ( "resourceName" );
            }

            var child = _children.SingleOrDefault ( c => c.ResourceName == resourceName ) ?? AddChildMetadata ( resourceName );

            return child;
        }

        /// <summary>
        /// Metadatas the item exists.
        /// </summary>
        /// <typeparam name="TMetadataItem">The type of the metadata item.</typeparam>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool MetadataItemExists<TMetadataItem> () where TMetadataItem : IMetadataItemDto
        {
            var itemExists = _metadataItemDtos.Any ( m => m.GetType () == typeof( TMetadataItem ) );
            return itemExists;
        }

        /// <summary>
        /// Called when [deserialization].
        /// </summary>
        /// <param name="context">The context.</param>
        [OnDeserialized]
        public void OnDeserialization ( StreamingContext context )
        {
            InitializeChildrenAndMetadataItemsDtos ();
        }

        /// <summary>
        /// Removes the child metadata.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        public void RemoveChildMetadata ( string resourceName )
        {
            if ( string.IsNullOrWhiteSpace ( resourceName ) )
            {
                throw new ArgumentNullException ( "resourceName" );
            }

            var child = GetChildMetadata ( resourceName );
            if ( child == null )
            {
                throw new ArgumentException ( string.Format ( "Cannot find child {0}.", resourceName ) );
            }

            _children.Remove ( child );
        }

        /// <summary>
        /// Removes the metadata item.
        /// </summary>
        /// <typeparam name="TMetadataItem">The type of the metadata item.</typeparam>
        public void RemoveMetadataItem<TMetadataItem> () where TMetadataItem : IMetadataItemDto
        {
            var item = FindMetadataItem<TMetadataItem> ();
            if ( item != null )
            {
                _metadataItemDtos.Remove ( item );
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the children and metadata items dtos.
        /// </summary>
        private void InitializeChildrenAndMetadataItemsDtos ()
        {
            Children = new ReadOnlyObservableCollection<MetadataDto> ( _children );
            MetadataItemDtos = new ReadOnlyObservableCollection<IMetadataItemDto> ( _metadataItemDtos );
        }

        #endregion
    }
}
