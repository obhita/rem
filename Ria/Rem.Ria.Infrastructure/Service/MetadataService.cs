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
using Agatha.Common;
using Microsoft.Practices.Prism.Modularity;
using Pillar.Common.Metadata.Dtos;
using Rem.Ria.Infrastructure.Web.Service;

namespace Rem.Ria.Infrastructure.Service
{
    /// <summary>
    /// MetadataService class.
    /// </summary>
    public class MetadataService : IMetadataService
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private IList<MetadataDto> _metadataDtos;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataService"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        public MetadataService ( IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;

            _metadataDtos = new List<MetadataDto> ();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        /// <param name="type">The type of the object.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.Dtos.MetadataDto"/></returns>
        public MetadataDto GetMetadata ( Type type )
        {
            var metadata = ( from metadataDto in _metadataDtos
                                     where metadataDto.ResourceName == type.FullName
                                     select metadataDto ).SingleOrDefault ();
            var result = metadata ?? new MetadataDto ( string.Empty );

            return result;
        }

        /// <summary>
        /// Loads the metadata for module.
        /// </summary>
        /// <param name="modules">The modules.</param>
        public void LoadMetadataForModule ( params IModule[] modules )
        {
            var moduleNames = GetModuleNames ( modules );

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetMetadataForModuleRequest { ModuleNames = moduleNames } );
            requestDispatcher.ProcessRequests ( HandleInitializationCompleted, HandleInitializationException );
        }

        #endregion

        #region Methods

        private static IList<string> GetModuleNames ( IEnumerable<IModule> modules )
        {
            return modules.Select ( module => module.GetType () ).Select ( type => type.Namespace ).ToList ();
        }

        private static void HandleInitializationException ( ExceptionInfo exceptionInfo )
        {
            throw new Exception ( "Load metadata for modules failed. Inner exception: " + exceptionInfo.Message );
        }

        private void HandleInitializationCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetMetadataForModuleResponse> ();
            _metadataDtos = response.MetadataDtos;
        }

        #endregion
    }
}
