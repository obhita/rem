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
using AutoMapper;
using NHibernate;
using NHibernate.Linq;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.ProgramModule;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Service;

namespace Rem.Ria.PatientModule.Web.PatientDashboard.ProgramEnrollmentTile
{
    /// <summary>
    /// Class for handling get available program offering locations request.
    /// </summary>
    public class GetAvailableProgramOfferingLocationsRequestHandler :
        QueryRequestHandler<GetAvailableProgramOfferingLocationsRequest, GetAvailableProgramOfferingLocationsResponse>
    {
        #region Constants and Fields

        private readonly ISessionProvider _sessionProvider;
        private readonly IStaffRepository _staffRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailableProgramOfferingLocationsRequestHandler"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        /// <param name="staffRepository">The staff repository.</param>
        public GetAvailableProgramOfferingLocationsRequestHandler (
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
        protected override void HandleRequest (
            GetAvailableProgramOfferingLocationsRequest request, GetAvailableProgramOfferingLocationsResponse response )
        {
            // program associated program offerings, at the locations that the current staff member has access to.
            var session = _sessionProvider.GetSession ();

            var staff = _staffRepository.GetByKey ( request.CurrentStaffKey );
            var locationKeys = staff.StaffLocationAssignments
                .Select ( x => x.Location.Key )
                .ToList ();

            var today = DateTime.Now;
            var programOfferings =
                session.Query<ProgramOffering> ()
                    .Where (
                        x => x.Program.Key == request.ProgramKey
                             && today >= x.StartDate && ( !x.EndDate.HasValue || today <= x.EndDate.Value ) )
                    .Fetch ( x => x.Location )
                    .ToList ();

            var availableProgramOfferingDics = new Dictionary<long, ProgramOffering> ();

            programOfferings.ForEach (
                programOffering =>
                    {
                        if ( locationKeys.Contains ( programOffering.Location.Key )
                             && !availableProgramOfferingDics.ContainsKey ( programOffering.Location.Key ) )
                        {
                            availableProgramOfferingDics.Add ( programOffering.Location.Key, programOffering );
                        }
                    } );

            var dtos = Mapper.Map<IList<ProgramOffering>, IList<ProgramOfferingLocationDto>> (
                availableProgramOfferingDics.Values.ToList () );

            response.ProgramOfferingLocations = dtos;
        }

        #endregion
    }
}
