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
using AutoMapper;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.ProgramModule;
using Rem.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientDashboard.ProgramEnrollmentTile
{
    /// <summary>
    /// Class for handling create program enrollment request.
    /// </summary>
    public class CreateProgramEnrollmentRequestHandler :
        CommandRequestHandler<CreateProgramEnrollmentRequest, CreateProgramEnrollmentResponse, ProgramEnrollmentDto>
    {
        #region Constants and Fields

        private readonly IClinicalCaseRepository _clinicalCaseRepository;
        private readonly IProgramEnrollmentFactory _programEnrollmentFactory;
        private readonly IProgramOfferingRepository _programOfferingRepository;
        private readonly IStaffRepository _staffRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProgramEnrollmentRequestHandler"/> class.
        /// </summary>
        /// <param name="programEnrollmentFactory">The program enrollment factory.</param>
        /// <param name="programOfferingRepository">The program offering repository.</param>
        /// <param name="clinicalCaseRepository">The clinical case repository.</param>
        /// <param name="staffRepository">The staff repository.</param>
        public CreateProgramEnrollmentRequestHandler (
            IProgramEnrollmentFactory programEnrollmentFactory,
            IProgramOfferingRepository programOfferingRepository,
            IClinicalCaseRepository clinicalCaseRepository,
            IStaffRepository staffRepository )
        {
            _programEnrollmentFactory = programEnrollmentFactory;
            _programOfferingRepository = programOfferingRepository;
            _clinicalCaseRepository = clinicalCaseRepository;
            _staffRepository = staffRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the dto from request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Rem.Ria.PatientModule.Web.Common.ProgramEnrollmentDto"/></returns>
        protected override ProgramEnrollmentDto CreateDtoFromRequest ( CreateProgramEnrollmentRequest request )
        {
            var dto = new ProgramEnrollmentDto
                {
                    ProgramOfferingKey = request.ProgramOfferingKey,
                    ClinicalCaseKey = request.ClinicalCaseKey,
                    EnrollmentDate = request.EnrollmentDate,
                    CommentsNote = request.CommentsNote,
                    DaysOnWaitingListCount = request.DaysOnWaitingListCount
                };
            return dto;
        }

        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        protected override void HandleRequest ( CreateProgramEnrollmentRequest request, CreateProgramEnrollmentResponse response )
        {
            var programOffering = _programOfferingRepository.GetByKey ( request.ProgramOfferingKey );
            var clinicalCase = _clinicalCaseRepository.GetByKey ( request.ClinicalCaseKey );
            var enrollmentDate = request.EnrollmentDate;
            var enrollingStaff = _staffRepository.GetByKey ( request.EnrollingStaffKey );

            var programEnrollment = _programEnrollmentFactory.CreateProgramEnrollment (
                programOffering, clinicalCase, enrollmentDate, enrollingStaff );

            if ( programEnrollment != null )
            {
                programEnrollment.ReviseDaysOnWaitingListCount ( request.DaysOnWaitingListCount );
                programEnrollment.ReviseComments ( request.CommentsNote );

                if ( Success )
                {
                    FlushSession ();

                    var dto = Mapper.Map<ProgramEnrollment, ProgramEnrollmentDto> ( programEnrollment );
                    response.DataTransferObject = dto;
                }
            }
        }

        #endregion
    }
}
