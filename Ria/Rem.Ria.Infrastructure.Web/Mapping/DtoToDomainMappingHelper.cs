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
using System.Linq;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.Infrastructure.Web.Mapping
{
    /// <summary>
    /// Class for helping dto to domain mapping.
    /// </summary>
    public class DtoToDomainMappingHelper : IDtoToDomainMappingHelper
    {
        #region Constants and Fields

        private readonly ILookupValueRepository _lookupValueRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DtoToDomainMappingHelper"/> class.
        /// </summary>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public DtoToDomainMappingHelper (
            ILookupValueRepository lookupValueRepository )
        {
            _lookupValueRepository = lookupValueRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Maps the collection.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="dtoList">The dto list.</param>
        /// <param name="domainList">The domain list.</param>
        /// <param name="mapPropertiesMethod">The map properties method.</param>
        /// <param name="deleteDomainObjMethod">The delete domain obj method.</param>
        /// <param name="createNewFunc">The create new func.</param>
        public void MapCollection<TDto, TEntity> (
            IEnumerable<TDto> dtoList,
            IEnumerable<TEntity> domainList,
            Action<TDto, TEntity> mapPropertiesMethod,
            Action<TEntity> deleteDomainObjMethod,
            Func<TEntity> createNewFunc )
            where TDto : KeyedDataTransferObject
            where TEntity : Entity
        {
            if ( deleteDomainObjMethod != null )
            {
                ProcessDeletedEntities ( dtoList, domainList, deleteDomainObjMethod );
            }

            if ( dtoList != null )
            {
                foreach ( var dto in dtoList )
                {
                    var domainEntity = GetNewOrExisting ( dto.Key, domainList, createNewFunc );
                    mapPropertiesMethod ( dto, domainEntity );
                }
            }
        }

        /// <summary>
        /// Maps the lookup collection.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="dtoList">The dto list.</param>
        /// <param name="domainList">The domain list.</param>
        /// <param name="mapPropertiesMethod">The map properties method.</param>
        /// <param name="deleteDomainObjMethod">The delete domain obj method.</param>
        /// <param name="createNewFunc">The create new func.</param>
        /// <param name="domainKeyFunc">The domain key func.</param>
        public void MapLookupCollection<TDto, TEntity> (
            IEnumerable<TDto> dtoList,
            IEnumerable<TEntity> domainList,
            Action<TDto, TEntity> mapPropertiesMethod,
            Action<TEntity> deleteDomainObjMethod,
            Func<TEntity> createNewFunc,
            Func<TEntity, long> domainKeyFunc )
            where TDto : KeyedDataTransferObject
            where TEntity : Entity
        {
            if ( deleteDomainObjMethod != null )
            {
                CheckForLookupDeletions ( dtoList, domainList, deleteDomainObjMethod, domainKeyFunc );
            }

            if ( dtoList != null )
            {
                foreach ( var dto in dtoList )
                {
                    var domainEntity = GetNewOrExistingLookup ( dto.Key, domainList, createNewFunc, domainKeyFunc );
                    mapPropertiesMethod ( dto, domainEntity );
                }
            }
        }

        /// <summary>
        /// Maps a LookupBase derived class to a LookupDto
        /// </summary>
        /// <typeparam name="TLookupBase">The type of the lookup base.</typeparam>
        /// <param name="lookupDto">The lookup dto.</param>
        /// <returns>The mapped lookup.</returns>
        public TLookupBase MapLookupField<TLookupBase> ( LookupValueDto lookupDto )
            where TLookupBase : LookupBase
        {
            var result = lookupDto != null ? MapLookupObject<TLookupBase> ( lookupDto.Key ) : null;
            return result;
        }

        /// <summary>
        /// Maps the lookup field.
        /// </summary>
        /// <typeparam name="TLookupBase">The type of the lookup base.</typeparam>
        /// <param name="wellKnownName">The well known name of the lookup.</param>
        /// <returns>The mapped lookup.</returns>
        public TLookupBase MapLookupField<TLookupBase>(string wellKnownName)
            where TLookupBase : LookupBase
        {
            var result = !string.IsNullOrEmpty ( wellKnownName ) ? MapLookupObject<TLookupBase>(wellKnownName) : null;
            return result;
        }

        /// <summary>
        /// Maps the name of the lookup field by.
        /// </summary>
        /// <typeparam name="TLookupBase">The type of the lookup base.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>
        /// A LookupBase
        /// </returns>
        public TLookupBase MapLookupFieldByName<TLookupBase>(string name)
           where TLookupBase : LookupBase
        {
            var result = !string.IsNullOrEmpty(name) ? MapLookupObjectByName<TLookupBase>(name) : null;
            return result;
        }

        /// <summary>
        /// Processes the deleted aggregate.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <typeparam name="TAggregate">The type of the aggregate.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="dtoList">The dto list.</param>
        /// <param name="domainList">The domain list.</param>
        /// <param name="baseAggregateRoot">The base aggregate root.</param>
        /// <param name="deleteDomainObjMethod">The delete domain obj method.</param>
        public void ProcessDeletedAggregate<TDto, TAggregate, TEntity> (
            IEnumerable<TDto> dtoList,
            IEnumerable<TEntity> domainList,
            TAggregate baseAggregateRoot,
            Action<TAggregate, TEntity> deleteDomainObjMethod )
            where TDto : KeyedDataTransferObject
            where TEntity : Entity
        {
            var objectsToDelete = new List<TEntity> ();

            foreach ( var domainObj in domainList )
            {
                var obj = domainObj;
                TDto foundObj = null;

                if ( dtoList != null )
                {
                    foundObj = dtoList.FirstOrDefault ( p => p.Key == obj.Key );
                }

                if ( foundObj == null )
                {
                    objectsToDelete.Add ( domainObj );
                }
            }

            foreach ( var obj in objectsToDelete )
            {
                deleteDomainObjMethod ( baseAggregateRoot, obj );
            }
        }

        /// <summary>
        /// Processes the deleted entities.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="dtoList">The dto list.</param>
        /// <param name="domainList">The domain list.</param>
        /// <param name="deleteDomainObjMethod">The delete domain obj method.</param>
        public void ProcessDeletedEntities<TDto, TEntity> (
            IEnumerable<TDto> dtoList,
            IEnumerable<TEntity> domainList,
            Action<TEntity> deleteDomainObjMethod )
            where TDto : KeyedDataTransferObject
            where TEntity : Entity
        {
            var objectsToDelete = new List<TEntity> ();

            foreach ( var domainObj in domainList )
            {
                var obj = domainObj;
                TDto foundObj = null;

                if ( dtoList != null )
                {
                    foundObj = dtoList.FirstOrDefault ( p => p.Key == obj.Key );
                }

                if ( foundObj == null )
                {
                    objectsToDelete.Add ( domainObj );
                }
            }

            foreach ( var obj in objectsToDelete )
            {
                deleteDomainObjMethod ( obj );
            }
        }

        #endregion

        #region Methods

        private static void CheckForLookupDeletions<TDto, TEntity> (
            IEnumerable<TDto> dtoList,
            IEnumerable<TEntity> domainList,
            Action<TEntity> deleteDomainObjMethod,
            Func<TEntity, long> domainKeyFunc )
            where TDto : KeyedDataTransferObject
            where TEntity : Entity
        {
            var objectsToDelete = new List<TEntity> ();

            foreach ( var domainObj in domainList )
            {
                var obj = domainObj;
                var foundObj = dtoList.FirstOrDefault ( p => p.Key == domainKeyFunc ( obj ) );

                if ( foundObj == null )
                {
                    objectsToDelete.Add ( domainObj );
                }
            }

            foreach ( var obj in objectsToDelete )
            {
                deleteDomainObjMethod ( obj );
            }
        }

        private static TEntity GetNewOrExisting<TEntity> (
            long key,
            IEnumerable<TEntity> domainList,
            Func<TEntity> createNewFunc )
            where TEntity : Entity
        {
            if ( domainList == null || domainList.Count () == 0 )
            {
                return createNewFunc ();
            }

            var entity = key != 0 ? domainList.First ( p => p.Key == key ) : createNewFunc ();

            return entity;
        }

        private static TEntity GetNewOrExistingLookup<TEntity> (
            long key,
            IEnumerable<TEntity> domainList,
            Func<TEntity> createNewFunc,
            Func<TEntity, long> domainKeyFunc )
            where TEntity : Entity
        {
            if ( domainList == null || domainList.Count () == 0 )
            {
                return createNewFunc ();
            }

            var entity = domainList.FirstOrDefault ( p => domainKeyFunc ( p ) == key ) ?? createNewFunc ();

            return entity;
        }

        private T MapLookupObject<T> ( long key )
            where T : LookupBase
        {
            var lookupObject = _lookupValueRepository.GetLookupByKey ( typeof( T ), key );

            if ( lookupObject == null )
            {
                throw new SystemException (
                    "LookupValue for type " + typeof( T ).Name + " and key " + key +
                    " cannot be found." );
            }

            var typedLookup = lookupObject as T;
            return typedLookup;
        }

        private T MapLookupObject<T>(string wellKnownName)
            where T : LookupBase
        {
            var lookupObject = _lookupValueRepository.GetLookupByWellKnownName(typeof(T), wellKnownName);

            if (lookupObject == null)
            {
                throw new SystemException(
                    "LookupValue for type " + typeof(T).Name + " and well known name " + wellKnownName +
                    " cannot be found.");
            }

            var typedLookup = lookupObject as T;
            return typedLookup;
        }

        private T MapLookupObjectByName<T>(string name)
            where T : LookupBase
        {
            var lookupObject = _lookupValueRepository.GetLookupByName(typeof(T), name);

            if (lookupObject == null)
            {
                throw new SystemException(
                    "LookupValue for type " + typeof(T).Name + " and name " + name +
                    " cannot be found.");
            }

            var typedLookup = lookupObject as T;
            return typedLookup;
        }
        
        #endregion
    }
}
