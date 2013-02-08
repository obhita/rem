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
using System.Collections.Generic;
using Pillar.Common.Collections;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// The <see cref="AggregateNodeLookupCollectionMapper&lt;TCollectionDto, TContainerEntity, TAggregateNode&gt;"/> class maps the collection of lookup values.
    /// </summary>
    /// <typeparam name="TCollectionDto">
    /// The type of the collection dto. 
    /// </typeparam>
    /// <typeparam name="TContainerEntity">
    /// The type of the container entity. 
    /// </typeparam>
    /// <typeparam name="TAggregateNode">
    /// The type of the aggregate node. 
    /// </typeparam>
    public class AggregateNodeLookupCollectionMapper<TCollectionDto, TContainerEntity, TAggregateNode>
        where TCollectionDto : KeyedDataTransferObject
        where TContainerEntity : class, IEntity
        where TAggregateNode : class, IAggregateNode
    {
        #region Constants and Fields

        private readonly IEnumerable<TAggregateNode> _aggregateNodeCollection;

        private readonly TContainerEntity _containerEntity;

        private readonly ISoftDelete<TCollectionDto> _dtoCollection;

        private Action<TCollectionDto, TContainerEntity> _addItem;

        private Func<IEnumerable<TAggregateNode>, long, TAggregateNode> _findEntity;

        private Action<TCollectionDto, TContainerEntity, TAggregateNode> _removeItem;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateNodeLookupCollectionMapper&lt;TCollectionDto, TContainerEntity, TAggregateNode&gt;"/> class.
        /// </summary>
        /// <param name="dtoCollection">
        /// The dto collection. 
        /// </param>
        /// <param name="containerEntity">
        /// The container entity. 
        /// </param>
        /// <param name="aggregateNodeCollection">
        /// The aggregate node collection. 
        /// </param>
        public AggregateNodeLookupCollectionMapper (
            ISoftDelete<TCollectionDto> dtoCollection, 
            TContainerEntity containerEntity, 
            IEnumerable<TAggregateNode> aggregateNodeCollection )
        {
            Check.IsNotNull ( dtoCollection, "dtoCollection is required" );
            Check.IsNotNull ( containerEntity, "containerEntity is required" );
            Check.IsNotNull ( aggregateNodeCollection, "aggregateNodeCollection is required" );

            _dtoCollection = dtoCollection;
            _containerEntity = containerEntity;
            _aggregateNodeCollection = aggregateNodeCollection;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Finds the entity in the collection.
        /// </summary>
        /// <param name="findEntity">
        /// The item to find. 
        /// </param>
        /// <returns>
        /// Returns an instance of <see cref="AggregateNodeLookupCollectionMapper&lt;TCollectionDto, TContainerEntity, TAggregateNode&gt;"/> to support fluent style method chaining. 
        /// </returns>
        public AggregateNodeLookupCollectionMapper<TCollectionDto, TContainerEntity, TAggregateNode> FindCollectionEntity (
            Func<IEnumerable<TAggregateNode>, long, TAggregateNode> findEntity )
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
            Check.IsNotNull ( _findEntity, "You must specify a FindCollectionEntity Function." );

            var result = true;

            foreach ( var removedDto in _dtoCollection.RemovedItems )
            {
                result &= TryRemoveItem ( removedDto );
            }

            foreach ( var currentDto in _dtoCollection.CurrentItems )
            {
                var entity = _findEntity ( _aggregateNodeCollection, currentDto.Key );
                if ( entity == null )
                {
                    result &= TryAddItem ( currentDto );
                }
            }

            return result;
        }

        /// <summary>
        /// Maps the item added to the collection.
        /// </summary>
        /// <param name="addItem">
        /// The item that was added. 
        /// </param>
        /// <returns>
        /// Returns an instance of <see cref="AggregateNodeLookupCollectionMapper&lt;TCollectionDto, TContainerEntity, TAggregateNode&gt;"/> to support fluent style method chaining. 
        /// </returns>
        public AggregateNodeLookupCollectionMapper<TCollectionDto, TContainerEntity, TAggregateNode> MapAddedItem (
            Action<TCollectionDto, TContainerEntity> addItem )
        {
            _addItem = addItem;
            return this;
        }

        /// <summary>
        /// Maps the item removed from the collection.
        /// </summary>
        /// <param name="removeItem">
        /// The item that was removed. 
        /// </param>
        /// <returns>
        /// Returns an instance of <see cref="AggregateNodeLookupCollectionMapper&lt;TCollectionDto, TContainerEntity, TAggregateNode&gt;"/> to support fluent style method chaining. 
        /// </returns>
        public AggregateNodeLookupCollectionMapper<TCollectionDto, TContainerEntity, TAggregateNode> MapRemovedItem (
            Action<TCollectionDto, TContainerEntity, TAggregateNode> removeItem )
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
                _addItem ( currentDto, _containerEntity );
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

            Check.IsNotNull ( _removeItem, "_removeItem cannot be null" );

            try
            {
                var entityToRemove = _findEntity ( _aggregateNodeCollection, removedDto.Key );
                _removeItem ( removedDto, _containerEntity, entityToRemove );
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
