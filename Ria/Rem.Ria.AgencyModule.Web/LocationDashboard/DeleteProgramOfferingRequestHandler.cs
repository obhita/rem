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
using AutoMapper;
using Rem.Domain.Clinical.ProgramModule;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.ProgramOfferingEditor;

namespace Rem.Ria.AgencyModule.Web.LocationDashboard
{
    /// <summary>
    /// Class for handling delete program offering request.
    /// </summary>
    public class DeleteProgramOfferingRequestHandler :
        CommandRequestHandler<DeleteProgramOfferingRequest, DeleteProgramOfferingResponse, ProgramOfferingDto>
    {
        #region Constants and Fields

        private readonly IProgramOfferingFactory _programOfferingFactory;
        private readonly IProgramOfferingRepository _programOfferingRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProgramOfferingRequestHandler"/> class.
        /// </summary>
        /// <param name="programOfferingRepository">The program offering repository.</param>
        /// <param name="programOfferingFactory">The program offering factory.</param>
        public DeleteProgramOfferingRequestHandler (
            IProgramOfferingRepository programOfferingRepository,
            IProgramOfferingFactory programOfferingFactory )
        {
            _programOfferingRepository = programOfferingRepository;
            _programOfferingFactory = programOfferingFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the dto from request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Rem.Ria.AgencyModule.Web.ProgramOfferingEditor.ProgramOfferingDto"/></returns>
        protected override ProgramOfferingDto CreateDtoFromRequest ( DeleteProgramOfferingRequest request )
        {
            return new ProgramOfferingDto { Key = request.ProgramOfferingKey };
        }

        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        protected override void HandleRequest ( DeleteProgramOfferingRequest request, DeleteProgramOfferingResponse response )
        {
            var programOffering = _programOfferingRepository.GetByKey ( request.ProgramOfferingKey );

            if ( programOffering != null )
            {
                _programOfferingFactory.DestroyProgramOffering ( programOffering );

                if ( Success )
                {
                    FlushSession ();

                    var programOfferings = _programOfferingRepository.GetProgramOfferingsByLocationKey ( request.LocationKey );
                    var dtos = Mapper.Map<IList<ProgramOffering>, IList<ProgramOfferingDto>> ( programOfferings );
                    response.ProgramOfferingDtos = dtos;
                }
            }
        }

        #endregion
    }
}
