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
using System.Linq;
using AutoMapper;
using NHibernate;
using NHibernate.Linq;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.ProgramModule;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.ProgramOfferingEditor;

namespace Rem.Ria.PatientModule.Web.PatientDashboard.ProgramEnrollmentTile
{
    /// <summary>
    /// Class for handling get available programs request.
    /// </summary>
    public class GetAvailableProgramsRequestHandler :
        QueryRequestHandler<GetAvailableProgramsRequest, GetAvailableProgramsResponse>
    {
        #region Constants and Fields

        private readonly ISessionProvider _sessionProvider;
        private readonly IStaffRepository _staffRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailableProgramsRequestHandler"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        /// <param name="staffRepository">The staff repository.</param>
        public GetAvailableProgramsRequestHandler (
            ISessionProvider sessionProvider,
            IStaffRepository staffRepository )
        {
            _sessionProvider = sessionProvider;
            _staffRepository = staffRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        protected override void HandleRequest ( GetAvailableProgramsRequest request, GetAvailableProgramsResponse response )
        {
            // Programs in current agency
            // where there exists a program offering
            // where current staff has access to a location
            var session = _sessionProvider.GetSession ();

            var staff = _staffRepository.GetByKey ( request.CurrentStaffKey );
            var locationKeys = staff.StaffLocationAssignments
                .Select ( x => x.Location.Key )
                .ToList ();

            var programOfferings = session.Query<ProgramOffering> ()
                .Where ( x => x.Program.Agency.Key == request.AgencyKey )
                .Fetch ( x => x.Program )
                .ToList ();

            var programDics = new Dictionary<long, Program> ();
            programOfferings.ForEach (
                programOffering =>
                    {
                        if ( locationKeys.Contains ( programOffering.Location.Key ) )
                        {
                            if ( !programDics.ContainsKey ( programOffering.Program.Key ) )
                            {
                                programDics.Add ( programOffering.Program.Key, programOffering.Program );
                            }
                        }
                    } );

            var dtos = Mapper.Map<IList<Program>, IList<ProgramDisplayNameDto>> ( programDics.Values.ToList () );

            response.ProgramDisplayNames = dtos;
        }

        #endregion
    }
}
