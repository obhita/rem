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
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientDashboard.ProgramEnrollmentTile
{
    /// <summary>
    /// Class for handling get all program enrollments by clinical case request.
    /// </summary>
    public class GetAllProgramEnrollmentsByClinicalCaseRequestHandler :
        QueryRequestHandler<GetAllProgramEnrollmentsByClinicalCaseRequest, GetAllProgramEnrollmentsByClinicalCaseResponse>
    {
        #region Constants and Fields

        private readonly IProgramEnrollmentRepository _programEnrollmentRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllProgramEnrollmentsByClinicalCaseRequestHandler"/> class.
        /// </summary>
        /// <param name="programEnrollmentRepository">The program enrollment repository.</param>
        public GetAllProgramEnrollmentsByClinicalCaseRequestHandler ( IProgramEnrollmentRepository programEnrollmentRepository )
        {
            _programEnrollmentRepository = programEnrollmentRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        protected override void HandleRequest (
            GetAllProgramEnrollmentsByClinicalCaseRequest request, GetAllProgramEnrollmentsByClinicalCaseResponse response )
        {
            var clinicalCaseKey = request.ClinicalCaseKey;

            var programEnrollments = _programEnrollmentRepository.GetProgramEnrollmentsByClinicalCase ( clinicalCaseKey );

            var programEnrollmentDtos = Mapper.Map<IList<ProgramEnrollment>, IList<ProgramEnrollmentDto>> (
                programEnrollments );

            response.ProgramEnrollmentDtos = programEnrollmentDtos;
        }

        #endregion
    }
}
