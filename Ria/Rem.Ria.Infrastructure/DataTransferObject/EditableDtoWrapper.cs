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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using Pillar.Common.Collections;
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain.Primitives;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.Infrastructure.DataTransferObject
{
    /// <summary>
    /// Class for wrapping editable dto.
    /// </summary>
    public class EditableDtoWrapper : AbstractDataTransferObject, IEditableDtoWrapper
    {
        #region Constants and Fields

        private readonly List<IDisposable> _subscriptions = new List<IDisposable> ();
        private bool _disposed; // to detect redundant calls
        private AbstractDataTransferObject _editableDto;
        private bool _hasErrors;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the editable dto.
        /// </summary>
        /// <value>The editable dto.</value>
        public AbstractDataTransferObject EditableDto
        {
            get { return _editableDto; }
            set
            {
                if ( _editableDto != value )
                {
                    if ( _editableDto != null )
                    {
                        RemoveListeners ();
                    }
                    ClearAllDataErrorInfo ();
                    HasErrors = false;
                    IsDirty = false;
                    _editableDto = value;
                    if ( _editableDto != null )
                    {
                        AddListeners ();
                    }
                    RaisePropertyChanged ( () => EditableDto );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has errors.
        /// </summary>
        /// <value><c>true</c> if this instance has errors; otherwise, <c>false</c>.</value>
        public new bool HasErrors
        {
            get { return _hasErrors || base.HasErrors; }
            set
            {
                if ( _hasErrors != value )
                {
                    _hasErrors = value;
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Addings the item.
        /// </summary>
        public void AddingItem ()
        {
            IsDirty = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose ()
        {
            if (_editableDto != null)
            {
                RemoveListeners();
            }
            Dispose ( true );
            GC.SuppressFinalize ( this );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose ( bool disposing )
        {
            if ( !_disposed && disposing )
            {
                RemoveListeners ();
            }
            _disposed = true;
        }

        private static void SoftDeleteSoftDeleted ( object sender, SoftDeletedEventArgs e )
        {
            var softDelete = sender as ISoftDelete;
            if ( softDelete != null )
            {
                foreach ( var softDeletedItem in e.SoftDeletedItems )
                {
                    var keyedDto = softDeletedItem as KeyedDataTransferObject;
                    if ( keyedDto != null )
                    {
                        if ( keyedDto.Key == 0 )
                        {
                            softDelete.RemovedItems.Remove ( keyedDto );
                        }
                        else
                        {
                            var editableDto = keyedDto as EditableDataTransferObject;
                            if ( editableDto != null )
                            {
                                editableDto.EditStatus = EditStatus.Delete;
                            }
                        }
                    }
                }
            }
        }

        private void AddListener ( ISoftDelete softDelete, object pObj )
        {
            var softDeletedInput =
                (
                    from evt in Observable.FromEventPattern<SoftDeletedEventArgs> ( softDelete, "SoftDeleted" )
                    select ( evt )
                );

            var softDeletedSubscriber = softDeletedInput.ObserveOnDispatcher ().Subscribe (
                    ( evt ) =>
                        {
                            SoftDeleteSoftDeleted ( evt.Sender, evt.EventArgs );
                            var pEditingDto = pObj as EditableDataTransferObject;
                            if ( pEditingDto != null && pEditingDto.Key > 0 )
                            {
                                pEditingDto.EditStatus = EditStatus.Update;
                            }
                        }
                );

            _subscriptions.Add ( softDeletedSubscriber );
        }

        private void AddListener ( INotifyCollectionChanged collectionChanged, object pObj )
        {
            var collectionChangedInput =
                (
                    from evt in Observable.FromEventPattern<NotifyCollectionChangedEventArgs> ( collectionChanged, "CollectionChanged" )
                    select ( evt )
                );

            var collectionChangedSubscriber = collectionChangedInput.ObserveOnDispatcher ().Subscribe (
                    ( evt ) => { NotifyCollectionChangedCollectionChanged ( evt.Sender, evt.EventArgs, pObj ); }
                );

            _subscriptions.Add ( collectionChangedSubscriber );
        }

        private void AddListener ( AbstractDataTransferObject dto, object pDto )
        {
            var dirtyInput =
                (
                    from evt in Observable.FromEventPattern<PropertyChangedEventArgs> ( dto, "PropertyChanged" )
                    where evt.EventArgs.PropertyName == PropertyUtil.ExtractPropertyName<AbstractDataTransferObject, bool> ( p => p.IsDirty ) ||
                          evt.EventArgs.PropertyName == PropertyUtil.ExtractPropertyName<AbstractDataTransferObject, bool> ( p => p.HasErrors )
                    select ( evt.Sender as AbstractDataTransferObject )
                );

            var dirtySubscriber = dirtyInput.ObserveOnDispatcher ().Subscribe (
                    ( obj ) =>
                        {
                            HasErrors = HasErrors || obj.HasErrors;
                            IsDirty = IsDirty || obj.IsDirty || HasErrors;

                            var editingDto = obj as EditableDataTransferObject;
                            var pEditingDto = pDto as EditableDataTransferObject;
                            if ( editingDto != null )
                            {
                                editingDto.EditStatus = editingDto.Key > 0 ? EditStatus.Update : EditStatus.Create;
                                if ( pEditingDto != null && pEditingDto.Key > 0 )
                                {
                                    pEditingDto.EditStatus = EditStatus.Update;
                                }
                            }
                        }
                );

            _subscriptions.Add ( dirtySubscriber );
        }

        private void AddListeners ()
        {
            new LogicalTreeWalker ().RegisterAction<AbstractDataTransferObject> (
                ( dto, pObj, pinfo ) =>
                    {
                        AddListener ( dto, pObj );
                        HasErrors |= dto.HasErrors;
                        IsDirty |= dto.IsDirty;
                        if ( dto != EditableDto )
                        {
                            dto.DataErrorInfoCollection.ForEach (
                                error => EditableDto.AddDataErrorInfo ( new DataErrorInfo ( error.Message, error.ErrorLevel ) ) );
                        }
                    } )
                .RegisterAction<ISoftDelete> ( ( softDeleted, pObj, pinfo ) => AddListener ( softDeleted, pObj ) )
                .RegisterAction<INotifyCollectionChanged> ( ( collectionChanged, pObj, pinfo ) => AddListener ( collectionChanged, pObj ) )
                .Walk<AbstractDataTransferObject> ( _editableDto );

            _editableDto.PropertyChanged += new PropertyChangedEventHandler(_editableDto_PropertyChanged);
        }

        private void _editableDto_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PropertyUtil.ExtractPropertyName<AbstractDataTransferObject, bool>(p => p.IsLoading))
            {
                IsLoading = _editableDto.IsLoading;
            }
        }

        private void NotifyCollectionChangedCollectionChanged ( object sender, NotifyCollectionChangedEventArgs e, object pObj )
        {
            if ( e.Action == NotifyCollectionChangedAction.Add )
            {
                var pEditingDto = pObj as EditableDataTransferObject;
                if ( pEditingDto != null && pEditingDto.Key > 0 )
                {
                    pEditingDto.EditStatus = EditStatus.Update;
                }
            }

            IsDirty = true;
        }

        private void RemoveListeners ()
        {
            foreach ( var subscriber in _subscriptions )
            {
                subscriber.Dispose ();
            }

            _editableDto.PropertyChanged -= new PropertyChangedEventHandler(_editableDto_PropertyChanged);
        }

        #endregion
    }
}
