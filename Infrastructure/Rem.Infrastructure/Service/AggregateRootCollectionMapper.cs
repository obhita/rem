#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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
using Pillar.Common.Collections;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.Primitives;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// The <see cref="AggregateRootCollectionMapper&lt;TCollectionDto, TAggregateRoot&gt;"/> class is used map a collection of aggregate roots.
    /// </summary>
    /// <typeparam name="TCollectionDto">
    /// The type of the collection dto. 
    /// </typeparam>
    /// <typeparam name="TAggregateRoot">
    /// The type of the aggregate root. 
    /// </typeparam>
    public class AggregateRootCollectionMapper<TCollectionDto, TAggregateRoot>
        where TCollectionDto : EditableDataTransferObject
        where TAggregateRoot : class, IAggregateRoot
    {
        #region Constants and Fields

        private readonly ISoftDelete<TCollectionDto> _dtoCollection;
        private Action<TCollectionDto> _addItem;
        private Action<TCollectionDto, TAggregateRoot> _changeItem;
        private Func<long, TAggregateRoot> _findEntity;
        private Action<TCollectionDto, TAggregateRoot> _removeItem;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootCollectionMapper&lt;TCollectionDto, TAggregateRoot&gt;"/> class.
        /// </summary>
        /// <param name="dtoCollection">
        /// The dto collection. 
        /// </param>
        public AggregateRootCollectionMapper ( ISoftDelete<TCollectionDto> dtoCollection )
        {
            Check.IsNotNull ( dtoCollection, "dtoCollection is required" );

            _dtoCollection = dtoCollection;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Finds the collection entity.
        /// </summary>
        /// <param name="findEntity">
        /// The find entity. 
        /// </param>
        /// <returns>
        /// Returns an instance of <see cref="AggregateRootCollectionMapper&lt;TCollectionDto, TAggregateRoot&gt;"/> to support fluent style method chaining. 
        /// </returns>
        public AggregateRootCollectionMapper<TCollectionDto, TAggregateRoot> FindCollectionEntity (
            Func<long, TAggregateRoot> findEntity )
        {
            _findEntity = findEntity;
            return this;
        }

        /// <summary>
        /// Maps the collection of aggregate nodes.
        /// </summary>
        /// <returns>
        /// Returns 'true' on successful mapping, else returns 'false'. 
        /// </returns>
        public bool Map ()
        {
            var result = true;

            foreach ( var removedDto in _dtoCollection.RemovedItems )
            {
                result &= TryRemoveItem ( removedDto );
            }

            foreach ( var currentDto in _dtoCollection.CurrentItems )
            {
                switch ( currentDto.EditStatus )
                {
                    case EditStatus.Noop:

                        // do nothing
                        break;

                    case EditStatus.Create:
                        result &= TryAddItem ( currentDto );
                        break;

                    case EditStatus.Update:
                        result &= TryChangeItem ( currentDto );
                        break;

                    default:
                        throw new ArgumentException (
                            string.Format (
                                "Unexpected action '{0}' in {1} with Key {2}", 
                                currentDto.EditStatus, 
                                typeof( TCollectionDto ).Name, 
                                currentDto.Key ) );
                }
            }

            return result;
        }

        /// <summary>
        /// Maps the added item.
        /// </summary>
        /// <param name="addItem">
        /// The add item. 
        /// </param>
        /// <returns>
        /// Returns an instance of <see cref="AggregateRootCollectionMapper&lt;TCollectionDto, TAggregateRoot&gt;"/> to support fluent style method chaining. 
        /// </returns>
        public AggregateRootCollectionMapper<TCollectionDto, TAggregateRoot> MapAddedItem (
            Action<TCollectionDto> addItem )
        {
            _addItem = addItem;
            return this;
        }

        /// <summary>
        /// Maps the changed item in the collection.
        /// </summary>
        /// <param name="changeItem">
        /// The item that was changed. 
        /// </param>
        /// <returns>
        /// Returns an instance of <see cref="AggregateRootCollectionMapper&lt;TCollectionDto, TAggregateRoot&gt;"/> to support fluent style method chaining. 
        /// </returns>
        public AggregateRootCollectionMapper<TCollectionDto, TAggregateRoot> MapChangedItem (
            Action<TCollectionDto, TAggregateRoot> changeItem )
        {
            _changeItem = changeItem;
            return this;
        }

        /// <summary>
        /// Maps the item removed from the collection.
        /// </summary>
        /// <param name="removeItem">
        /// The item that was removed. 
        /// </param>
        /// <returns>
        /// Returns an instance of <see cref="AggregateRootCollectionMapper&lt;TCollectionDto, TAggregateRoot&gt;"/> to support fluent style method chaining. 
        /// </returns>
        public AggregateRootCollectionMapper<TCollectionDto, TAggregateRoot> MapRemovedItem (
            Action<TCollectionDto, TAggregateRoot> removeItem )
        {
            _removeItem = removeItem;
            return this;
        }

        #endregion

        #region Methods

        private bool TryAddItem ( TCollectionDto currentDto )
        {
            var result = true;

            Check.IsNotNull ( _addItem, "_addItem cannot be null" );

            try
            {
                _addItem ( currentDto );
            }
            catch ( Exception e )
            {
                result = false;
                currentDto.AddDataErrorInfo ( new DataErrorInfo ( e.Message, ErrorLevel.Error ) );
            }

            return result;
        }

        private bool TryChangeItem ( TCollectionDto currentDto )
        {
            var result = true;

            Check.IsNotNull ( _findEntity, "_findEntity cannot be null" );
            Check.IsNotNull ( _changeItem, "_changeItem cannot be null" );

            try
            {
                var entityToChange = _findEntity ( currentDto.Key );
                _changeItem ( currentDto, entityToChange );
            }
            catch ( Exception e )
            {
                result = false;
                currentDto.AddDataErrorInfo ( new DataErrorInfo ( e.Message, ErrorLevel.Error ) );
            }

            return result;
        }

        private bool TryRemoveItem ( TCollectionDto removedDto )
        {
            var result = true;

            Check.IsNotNull ( _findEntity, "_findEntity cannot be null" );
            Check.IsNotNull ( _removeItem, "_removeItem cannot be null" );

            try
            {
                var entityToRemove = _findEntity ( removedDto.Key );
                _removeItem ( removedDto, entityToRemove );
            }
            catch ( Exception e )
            {
                result = false;
                removedDto.AddDataErrorInfo ( new DataErrorInfo ( e.Message, ErrorLevel.Error ) );
            }

            return result;
        }

        #endregion
    }
}
