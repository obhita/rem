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
using Agatha.Common;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling get patient documents by patient request.
    /// </summary>
    public class GetPatientDocumentsByPatientRequestHandler :
        NHibernateSessionRequestHandler<GetPatientDocumentsByPatientRequest, GetPatientDocumentsByPatientResponse>
    {
        #region Constants and Fields

        private readonly IPatientDocumentRepository _patientDocumentRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPatientDocumentsByPatientRequestHandler"/> class.
        /// </summary>
        /// <param name="patientDocumentRepository">The patient document repository.</param>
        public GetPatientDocumentsByPatientRequestHandler ( IPatientDocumentRepository patientDocumentRepository )
        {
            _patientDocumentRepository = patientDocumentRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetPatientDocumentsByPatientRequest request )
        {
            var patientKey = request.PatientKey;
            var patientDocuments = _patientDocumentRepository.GetAllPatientDocumentsByPatientKey ( patientKey );

            var patientDocumentDtos = ( from pd in patientDocuments
                                                             select new PatientDocumentDto
                                                                 {
                                                                     Key = pd.Key,
                                                                     ClinicalStartDate =
                                                                         ( pd.ClinicalDateRange == null )
                                                                             ? null
                                                                             : pd.ClinicalDateRange.StartDate,
                                                                     ClinicalEndDate =
                                                                         ( pd.ClinicalDateRange == null ) ? null : pd.ClinicalDateRange.EndDate,
                                                                     Description = pd.Description,
                                                                     DocumentProviderName = pd.DocumentProviderName,
                                                                     FileName = pd.FileName,
                                                                     OtherDocumentTypeName = pd.OtherDocumentTypeName,
                                                                     PatientKey = patientKey,
                                                                     PatientDocumentType = new LookupValueDto
                                                                         {
                                                                             Key = pd.PatientDocumentType.Key,
                                                                             WellKnownName =
                                                                                 pd.PatientDocumentType.WellKnownName,
                                                                             Name = pd.PatientDocumentType.Name
                                                                         },
                                                                     CreatedDate = pd.CreatedTimestamp.DateTime,
                                                                     DocumentHashValue = pd.DocumentHashValue,
                                                                     C32ImportedIndicator = pd.C32ImportedIndicator
                                                                 }
                                                           )
                .ToList ();

            var response = CreateTypedResponse ();
            response.PatientDocumentDtos = patientDocumentDtos;

            return response;
        }

        #endregion
    }
}
