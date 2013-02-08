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

using Agatha.Common;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling delete patient document request.
    /// </summary>
    public class DeletePatientDocumentRequestHandler :
        NHibernateSessionRequestHandler<DeletePatientDocumentRequest, DeletePatientDocumentResponse>
    {
        #region Constants and Fields

        private readonly GetPatientDocumentsByPatientRequestHandler _getPatientDocumentsByPatientRequestHandler;
        private readonly IPatientDocumentFactory _patientDocumentFactory;
        private readonly IPatientDocumentRepository _patientDocumentRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePatientDocumentRequestHandler"/> class.
        /// </summary>
        /// <param name="patientDocumentRepository">The patient document repository.</param>
        /// <param name="patientDocumentFactory">The patient document factory.</param>
        /// <param name="getPatientDocumentsByPatientRequestHandler">The get patient documents by patient request handler.</param>
        public DeletePatientDocumentRequestHandler (
            IPatientDocumentRepository patientDocumentRepository,
            IPatientDocumentFactory patientDocumentFactory,
            GetPatientDocumentsByPatientRequestHandler getPatientDocumentsByPatientRequestHandler )
        {
            _patientDocumentRepository = patientDocumentRepository;
            _patientDocumentFactory = patientDocumentFactory;
            _getPatientDocumentsByPatientRequestHandler = getPatientDocumentsByPatientRequestHandler;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( DeletePatientDocumentRequest request )
        {
            var patientDocumentKey = request.PatientDocumentKey;
            var patientDocument = _patientDocumentRepository.GetByKey ( patientDocumentKey );

            _patientDocumentFactory.DestroyPatientDocument ( patientDocument );

            var patientDocumentResponse = _getPatientDocumentsByPatientRequestHandler.Handle (
                new GetPatientDocumentsByPatientRequest { PatientKey = patientDocument.Patient.Key } )
                                          as GetPatientDocumentsByPatientResponse;

            var response = CreateTypedResponse ();
            if ( patientDocumentResponse != null )
            {
                response.PatientDocumentDtos = patientDocumentResponse.PatientDocumentDtos;
            }

            return response;
        }

        #endregion
    }
}
