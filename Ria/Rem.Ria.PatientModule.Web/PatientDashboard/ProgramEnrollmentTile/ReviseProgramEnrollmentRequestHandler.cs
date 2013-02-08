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

using AutoMapper;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.ProgramModule;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientDashboard.ProgramEnrollmentTile
{
    /// <summary>
    /// Class for handling revise program enrollment request.
    /// </summary>
    public class ReviseProgramEnrollmentRequestHandler :
        CommandRequestHandler<ReviseProgramEnrollmentRequest, ReviseProgramEnrollmentResponse, ProgramEnrollmentDto>
    {
        #region Constants and Fields

        private readonly IKeyedDtoFactory<ProgramEnrollmentDto> _keyedDtoFactory;
        private readonly IProgramEnrollmentRepository _programEnrollmentRepository;
        private readonly IStaffRepository _staffRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviseProgramEnrollmentRequestHandler"/> class.
        /// </summary>
        /// <param name="keyedDtoFactory">The keyed dto factory.</param>
        /// <param name="programEnrollmentRepository">The program enrollment repository.</param>
        /// <param name="staffRepository">The staff repository.</param>
        public ReviseProgramEnrollmentRequestHandler (
            IKeyedDtoFactory<ProgramEnrollmentDto> keyedDtoFactory,
            IProgramEnrollmentRepository programEnrollmentRepository,
            IStaffRepository staffRepository )
        {
            _keyedDtoFactory = keyedDtoFactory;
            _programEnrollmentRepository = programEnrollmentRepository;
            _staffRepository = staffRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the dto from request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Rem.Ria.PatientModule.Web.Common.ProgramEnrollmentDto"/></returns>
        protected override ProgramEnrollmentDto CreateDtoFromRequest ( ReviseProgramEnrollmentRequest request )
        {
            var dto = _keyedDtoFactory.CreateKeyedDto ( request.ProgramEnrollmentKey );
            var staff = _staffRepository.GetByKey ( request.EnrollingStaffKey );
            dto.EnrollingStaff = Mapper.Map<Staff, StaffSummaryDto> ( staff );
            dto.EnrollmentDate = request.EnrollmentDate;
            dto.CommentsNote = request.CommentsNote;
            dto.DaysOnWaitingListCount = request.DaysOnWaitingListCount;

            return dto;
        }

        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        protected override void HandleRequest ( ReviseProgramEnrollmentRequest request, ReviseProgramEnrollmentResponse response )
        {
            var programEnrollment = _programEnrollmentRepository.GetByKey ( request.ProgramEnrollmentKey );
            var enrollingStaff = _staffRepository.GetByKey ( request.EnrollingStaffKey );

            programEnrollment.ReviseEnrollmentDate ( request.EnrollmentDate );
            programEnrollment.ReviseEnrollingStaff ( enrollingStaff );
            programEnrollment.ReviseComments ( request.CommentsNote );
            programEnrollment.ReviseDaysOnWaitingListCount ( request.DaysOnWaitingListCount );

            if ( Success )
            {
                FlushSession ();

                var dto = Mapper.Map<ProgramEnrollment, ProgramEnrollmentDto> ( programEnrollment );
                response.DataTransferObject = dto;
            }
        }

        #endregion
    }
}
