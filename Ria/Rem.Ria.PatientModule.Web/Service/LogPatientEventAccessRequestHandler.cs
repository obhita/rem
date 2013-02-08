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
using Agatha.Common;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;

namespace Rem.Ria.PatientModule.Web.Service
{
    /// <summary>
    /// Class for handling log patient event access request.
    /// </summary>
    public class LogPatientEventAccessRequestHandler :
        NHibernateSessionRequestHandler<LogPatientEventAccessRequest, LogPatientEventAccessResponse>
    {
        #region Constants and Fields

        private readonly ILookupValueRepository _lookupValueRepository;
        private readonly IPatientAccessEventFactory _patientAccessEventFactory;
        private readonly IAuditPatientReadAccessService _auditPatientReadAccessService;
        private readonly IPatientRepository _patientRepository;

        #endregion

        #region Constructors and Destructors


        /// <summary>
        /// Initializes a new instance of the <see cref="LogPatientEventAccessRequestHandler"/> class.
        /// </summary>
        /// <param name="patientRepository">The patient repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        /// <param name="patientAccessEventFactory">The patient access event factory.</param>
        /// <param name="auditPatientReadAccessService">The audit patient read access service.</param>
        public LogPatientEventAccessRequestHandler (
            IPatientRepository patientRepository,
            ILookupValueRepository lookupValueRepository,
            IPatientAccessEventFactory patientAccessEventFactory,
            IAuditPatientReadAccessService auditPatientReadAccessService)
        {
            _patientRepository = patientRepository;
            _lookupValueRepository = lookupValueRepository;
            _patientAccessEventFactory = patientAccessEventFactory;
            _auditPatientReadAccessService = auditPatientReadAccessService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( LogPatientEventAccessRequest request )
        {
            var logEntryDto = request.LogEntryDto;

            var patient = _patientRepository.GetByKey ( logEntryDto.PatientKey );

            if ( patient == null )
            {
                throw new ArgumentException ( "Could not find a patient  with Key =" + logEntryDto.PatientKey );
            }

            var eventType = _lookupValueRepository.GetLookupByWellKnownName (
                typeof( PatientAccessEventType ), WellKnownNames.PatientModule.PatientAccessEventType.ReadEvent ) as PatientAccessEventType;

            var formattedNote = string.Format ( logEntryDto.Note, patient.Name.Complete );
            var eventAccessEntry = _patientAccessEventFactory.CreatePatientAccessEvent(patient, eventType, logEntryDto.AuditedContextDescription, formattedNote);

            _auditPatientReadAccessService.AuditPatientReadAccess(eventAccessEntry);

            var response = CreateTypedResponse ();

            return response;
        }

        #endregion
    }
}
