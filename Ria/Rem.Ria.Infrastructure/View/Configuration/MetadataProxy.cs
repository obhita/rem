#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Pillar.Common.Metadata;
using Pillar.Common.Metadata.Dtos;
using Rem.Ria.Infrastructure.Service;

namespace Rem.Ria.Infrastructure.View.Configuration
{
    /// <summary>
    /// Proxy for metadata.
    /// </summary>
    public class MetadataProxy
    {
        #region Constants and Fields

        private readonly string _childMetadataName;
        private readonly IMetadataProvider _metadataProvider;
        private readonly IMetadataService _metadataService;
        private MetadataDto _customizedMetadata;
        private MetadataDto _defaultMetadata;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataProxy"/> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="metadataProvider">The metadata provider.</param>
        public MetadataProxy ( IMetadataService metadataService, IMetadataProvider metadataProvider )
            : this ( metadataService, metadataProvider, null )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataProxy"/> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <param name="childMetadataName">Name of the child metadata.</param>
        public MetadataProxy (
            IMetadataService metadataService,
            IMetadataProvider metadataProvider,
            string childMetadataName )
        {
            if ( metadataService == null )
            {
                throw new ArgumentNullException ( "metadataService" );
            }
            if ( metadataProvider == null )
            {
                throw new ArgumentNullException ( "metadataProvider" );
            }

            _metadataService = metadataService;
            _metadataProvider = metadataProvider;
            _childMetadataName = childMetadataName;
            InitializeProxy ();
        }

        #endregion

        #region Delegates

        /// <summary>
        /// Delegate Handler definition for when the metadata has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Rem.Ria.Infrastructure.View.Configuration.MetadataChangedEventArgs"/> instance containing the event data.</param>
        public delegate void MetadataChangedEventHandler ( object sender, MetadataChangedEventArgs eventArgs );

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [metadata changed].
        /// </summary>
        public event MetadataChangedEventHandler MetadataChanged;

        #endregion

        #region Public Methods

        /// <summary>
        /// Reports the initial metadata.
        /// </summary>
        public void ReportInitialMetadata ()
        {
            var itemList = new List<IMetadataItemDto> ();
            if ( _customizedMetadata != null )
            {
                itemList.AddRange ( _customizedMetadata.MetadataItemDtos );
            }

            if ( _defaultMetadata != null )
            {
                foreach ( var metadataItemDto in _defaultMetadata.MetadataItemDtos )
                {
                    var item = metadataItemDto;
                    if ( !itemList.Any ( i => i.GetType () == item.GetType () ) )
                    {
                        itemList.Add ( metadataItemDto );
                    }
                }
            }

            foreach ( var metadataItemDto in itemList )
            {
                OnMetadataChanged ( new MetadataChangedEventArgs ( MetadataAction.Added, metadataItemDto ) );
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="E:MetadataChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Rem.Ria.Infrastructure.View.Configuration.MetadataChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnMetadataChanged ( MetadataChangedEventArgs e )
        {
            if ( MetadataChanged != null )
            {
                MetadataChanged ( this, e );
            }
        }

        private static bool ContainsMetadata ( IList items, string metaDataName )
        {
            var containsMetadata = false;
            foreach ( var newItem in items )
            {
                var metadata = ( MetadataDto )newItem;
                if ( metadata.ResourceName == metaDataName )
                {
                    containsMetadata = true;
                }
            }

            return containsMetadata;
        }

        private MetadataDto GetMetadataForSelectedResource ( MetadataDto metadataDto )
        {
            // Initially, assume that we are selecting the base metadata object
            var metadataForSelectedResource = metadataDto;

            // however, if the ChildMetaDataName property is set then it means we want 
            // a metadata child.
            if ( metadataDto != null && !string.IsNullOrWhiteSpace ( _childMetadataName ) )
            {
                metadataForSelectedResource = metadataDto.Children.FirstOrDefault ( m => m.ResourceName == _childMetadataName );
            }

            return metadataForSelectedResource;
        }

        private void InitializeCustomizedMetadata ()
        {
            // Case 1: _metadataProvider.MetadataDto is null
            // Case 2: Child is not found
            // Case 3: _metadataProvider.MetadataDto is changed
            // Case 4: Child is replaced or removed

            if ( _customizedMetadata != null )
            {
                var customizedMetadataItemsCollection = _customizedMetadata.MetadataItemDtos as INotifyCollectionChanged;
                customizedMetadataItemsCollection.CollectionChanged -= MetadataItemDtosCollectionChanged;
            }

            if ( _metadataProvider.MetadataDto != null )
            {
                var metadataChildrenCollection = _metadataProvider.MetadataDto.Children as INotifyCollectionChanged;
                metadataChildrenCollection.CollectionChanged -= MetadataChildrenCollectionChanged;
            }

            _customizedMetadata = null;

            _customizedMetadata = GetMetadataForSelectedResource ( _metadataProvider.MetadataDto );
            if ( _customizedMetadata != null )
            {
                var customizedMetadataItemsCollection = _customizedMetadata.MetadataItemDtos as INotifyCollectionChanged;
                customizedMetadataItemsCollection.CollectionChanged += MetadataItemDtosCollectionChanged;
            }

            if ( _metadataProvider.MetadataDto != null )
            {
                var metadataChildrenCollection = _metadataProvider.MetadataDto.Children as INotifyCollectionChanged;
                metadataChildrenCollection.CollectionChanged += MetadataChildrenCollectionChanged;
            }
        }

        private void InitializeDefaultMetadata ()
        {
            _defaultMetadata = GetMetadataForSelectedResource ( _metadataService.GetMetadata ( _metadataProvider.GetType () ) );
            if ( _defaultMetadata == null )
            {
                _defaultMetadata = new MetadataDto ( string.Empty );
            }
        }

        private void InitializeProxy ()
        {
            InitializeDefaultMetadata ();
            InitializeCustomizedMetadata ();

            _metadataProvider.PropertyChanged += MetadataProviderPropertyChanged;
        }

        private void MetadataChildrenCollectionChanged ( object sender, NotifyCollectionChangedEventArgs eventArgs )
        {
            if ( !string.IsNullOrWhiteSpace ( _childMetadataName ) )
            {
                if ( eventArgs.Action == NotifyCollectionChangedAction.Add )
                {
                    if ( ContainsMetadata ( eventArgs.NewItems, _childMetadataName ) )
                    {
                        InitializeCustomizedMetadata ();
                    }
                }
                else if ( eventArgs.Action == NotifyCollectionChangedAction.Remove )
                {
                    if ( ContainsMetadata ( eventArgs.OldItems, _childMetadataName ) )
                    {
                        InitializeCustomizedMetadata ();
                    }
                }
                else
                {
                    throw new NotSupportedException ( string.Format ( "Unsupported action {0}", eventArgs.Action ) );
                }
            }
        }

        private void MetadataItemDtosCollectionChanged ( object sender, NotifyCollectionChangedEventArgs eventArgs )
        {
            var action = eventArgs.Action;
            if ( action == NotifyCollectionChangedAction.Add )
            {
                foreach ( var newItem in eventArgs.NewItems )
                {
                    OnMetadataChanged ( new MetadataChangedEventArgs ( MetadataAction.Added, newItem as IMetadataItemDto ) );
                }
            }
            else if ( action == NotifyCollectionChangedAction.Remove )
            {
                foreach ( var oldItem in eventArgs.OldItems )
                {
                    var type = oldItem.GetType ();
                    var metadataItemDto = oldItem as IMetadataItemDto;
                    if ( metadataItemDto == null )
                    {
                        throw new ArgumentException ( "Unexpected meta data type: " + type );
                    }

                    OnMetadataChanged ( new MetadataChangedEventArgs ( MetadataAction.Removed, metadataItemDto ) );

                    var defaultItem = _defaultMetadata.MetadataItemDtos.FirstOrDefault ( m => m.GetType () == type );
                    if ( defaultItem != null )
                    {
                        OnMetadataChanged ( new MetadataChangedEventArgs ( MetadataAction.Added, defaultItem ) );
                    }
                }
            }
            else
            {
                throw new NotSupportedException ( "There is currently no support for NotifyCollectionChangedAction: " + action );
            }
        }

        private void MetadataProviderPropertyChanged ( object sender, PropertyChangedEventArgs eventArgs )
        {
            if ( eventArgs.PropertyName == "MetadataDto" )
            {
                InitializeCustomizedMetadata ();
            }
        }

        #endregion
    }
}
