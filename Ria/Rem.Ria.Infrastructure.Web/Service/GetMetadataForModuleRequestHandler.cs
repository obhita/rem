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

using System.Collections.Generic;
using Agatha.Common;
using Agatha.ServiceLayer;
using AutoMapper;
using Pillar.Common.Metadata;
using Pillar.Common.Metadata.Dtos;
using Rem.Infrastructure.Service;

namespace Rem.Ria.Infrastructure.Web.Service
{
    /// <summary>
    /// Class for handling get metadata for module request.
    /// </summary>
    public class GetMetadataForModuleRequestHandler : RequestHandler<GetMetadataForModuleRequest, GetMetadataForModuleResponse>
    {
        #region Constants and Fields

        private readonly IMetadataRepository _metadataRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMetadataForModuleRequestHandler"/> class.
        /// </summary>
        /// <param name="metadataRepository">The metadata repository.</param>
        public GetMetadataForModuleRequestHandler ( IMetadataRepository metadataRepository )
        {
            _metadataRepository = metadataRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetMetadataForModuleRequest request )
        {
            var metadatas = new List<IMetadata> ();
            foreach ( var moduleName in request.ModuleNames )
            {
                var result = _metadataRepository.FindMetadata ( moduleName );
                metadatas.AddRange ( result );
            }
            var metadataDtos = MapMetadataList ( metadatas );

            var response = CreateTypedResponse ();
            response.MetadataDtos = metadataDtos;

            return response;
        }

        #endregion

        #region Methods

        private static IEnumerable<IMetadataItemDto> MapMetadataItems ( IEnumerable<IMetadataItem> metadataItems )
        {
            var metadataItemDtos = Mapper.Map<IEnumerable<IMetadataItem>, IEnumerable<IMetadataItemDto>> ( metadataItems );
            return metadataItemDtos;
        }

        private static IList<MetadataDto> MapMetadataList ( IEnumerable<IMetadata> metadatas )
        {
            IList<MetadataDto> metadataDtos = new List<MetadataDto> ();

            foreach ( var metadata in metadatas )
            {
                var metadataDto = new MetadataDto ( metadata.ResourceName );
                metadataDtos.Add ( metadataDto );

                var items = MapMetadataItems ( metadata.MetadataItems );
                foreach ( var metadataItemDto in items )
                {
                    metadataDto.AddMetadataItem ( metadataItemDto );
                }

                var children = MapMetadataList ( metadata.Children );
                foreach ( var child in children )
                {
                    metadataDto.AddChildMetadata ( child );
                }
            }

            return metadataDtos;
        }

        #endregion
    }
}
