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
using System.Reflection;
using AutoMapper;
using Pillar.Common.Metadata;
using Pillar.Common.Metadata.Dtos;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// The <see cref="MetadataMapper"/> has utilities that map metadata to an entity or dto.
    /// </summary>
    public class MetadataMapper : IMetadataMapper
    {
        #region Constants and Fields

        private readonly IMetadataRepository _metadataRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataMapper"/> class.
        /// </summary>
        /// <param name="metadataRepository">
        /// The metadata repository. 
        /// </param>
        public MetadataMapper ( IMetadataRepository metadataRepository )
        {
            _metadataRepository = metadataRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Maps Metadata to dto metadata.
        /// </summary>
        /// <param name="entityType">
        /// Type of the entity. 
        /// </param>
        /// <param name="dtoType">
        /// Type of the dto. 
        /// </param>
        /// <param name="dtoMetadata">
        /// The dto metadata to map to. 
        /// </param>
        public void MapToDto ( Type entityType, Type dtoType, MetadataDto dtoMetadata )
        {
            Check.IsNotNull ( entityType, "entityType is required." );
            Check.IsNotNull ( dtoType, "dtoType is required." );
            Check.IsNotNull ( dtoMetadata, "dtoMetadata is required." );

            var metaData = _metadataRepository.GetMetadata ( entityType.FullName );
            if ( metaData != null )
            {
                MapRecursive ( metaData, entityType, dtoType, dtoMetadata );
            }
        }

        #endregion

        #region Methods

        private static IEnumerable<IMetadataItemDto> MapMetadataItems ( IEnumerable<IMetadataItem> metadataItems )
        {
            var metadataItemDtos = Mapper.Map<IEnumerable<IMetadataItem>, IEnumerable<IMetadataItemDto>> ( metadataItems );
            return metadataItemDtos;
        }

        private static void MapRecursive ( IMetadata metadata, Type entityType, Type dtoType, MetadataDto dtoMetadata )
        {
            dtoMetadata.AddMetadataItemRange ( MapMetadataItems ( metadata.MetadataItems ) );

            foreach ( var propertyInfo in entityType.GetProperties () )
            {
                if ( ( typeof( IAggregateRoot ).IsAssignableFrom ( propertyInfo.PropertyType )
                       || typeof( IAggregateRoot ).IsAssignableFrom ( propertyInfo.PropertyType ) )
                     && metadata.HasChild ( propertyInfo.PropertyType.FullName ) )
                {
                    var dtoPropertyName = TryFindDestinationPropertyName ( entityType, dtoType, propertyInfo );
                    if ( dtoPropertyName == null )
                    {
                        continue;
                    }

                    var dtoPropertyInfo = dtoType.GetProperty ( dtoPropertyName );
                    if ( typeof( IDataTransferObject ).IsAssignableFrom ( dtoPropertyInfo.PropertyType ) )
                    {
                        MapRecursive (
                            metadata.FindChildMetadata ( propertyInfo.PropertyType.FullName ), 
                            propertyInfo.PropertyType, 
                            dtoPropertyInfo.PropertyType, 
                            dtoMetadata.GetChildMetadata ( dtoPropertyInfo.PropertyType.FullName ) );
                    }
                    else
                    {
                        var subDtoPropertyMetadata = dtoMetadata.GetChildMetadata ( dtoPropertyInfo.Name );
                        var subPropertyMetadata = metadata.FindChildMetadata ( propertyInfo.PropertyType.FullName );
                        subDtoPropertyMetadata.AddMetadataItemRange (
                            MapMetadataItems ( subPropertyMetadata.MetadataItems ) );
                    }
                }
                else if ( metadata.HasChild ( propertyInfo.Name ) )
                {
                    var dtoPropertyName = PropertyNameMapper.GetDistinctDestinationPropertyName ( entityType, dtoType, propertyInfo.Name );
                    if ( dtoPropertyName == null )
                    {
                        continue;
                    }

                    var subDtoPropertyMetadata = dtoMetadata.GetChildMetadata ( dtoPropertyName );
                    var subPropertyMetadata = metadata.FindChildMetadata ( propertyInfo.Name );
                    subDtoPropertyMetadata.AddMetadataItemRange (
                        MapMetadataItems ( subPropertyMetadata.MetadataItems ) );
                }
            }
        }

        private static string TryFindDestinationPropertyName ( Type entityType, Type dtoType, PropertyInfo propertyInfo )
        {
            var dtoPropertyName = PropertyNameMapper.GetDistinctDestinationPropertyName ( entityType, dtoType, propertyInfo.Name );

            if ( dtoPropertyName == null )
            {
                foreach ( var subPropertyInfo in entityType.GetProperties () )
                {
                    if ( typeof( IAggregateRoot ).IsAssignableFrom ( subPropertyInfo.PropertyType )
                         || typeof( IAggregateRoot ).IsAssignableFrom ( subPropertyInfo.PropertyType ) )
                    {
                        dtoPropertyName = TryFindDestinationPropertyName ( entityType, dtoType, subPropertyInfo );
                    }
                    else
                    {
                        dtoPropertyName = PropertyNameMapper.GetDistinctDestinationPropertyName ( entityType, dtoType, subPropertyInfo.Name );
                    }

                    if ( dtoPropertyName != null )
                    {
                        break;
                    }
                }
            }

            return dtoPropertyName;
        }

        #endregion
    }
}
