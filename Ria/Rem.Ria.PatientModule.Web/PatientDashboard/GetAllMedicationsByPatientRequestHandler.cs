﻿#region License

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
using Agatha.Common;
using AutoMapper;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling get all medications by patient request.
    /// </summary>
    public class GetAllMedicationsByPatientRequestHandler :
        NHibernateSessionRequestHandler<GetAllMedicationsByPatientRequest, GetAllMedicationsByPatientResponse>
    {
        #region Constants and Fields

        private readonly IPatientRepository _patientRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllMedicationsByPatientRequestHandler"/> class.
        /// </summary>
        /// <param name="patientRepository">The patient repository.</param>
        public GetAllMedicationsByPatientRequestHandler ( IPatientRepository patientRepository )
        {
            _patientRepository = patientRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetAllMedicationsByPatientRequest request )
        {
            var patientKey = request.PatientKey;

            var medications = _patientRepository.GetAllMedicationsByPatientKey ( patientKey );
            var medicationDtos = Mapper.Map<IList<Medication>, IList<MedicationDto>> ( medications );

            var response = CreateTypedResponse ();
            response.MedicationDtos = medicationDtos;

            return response;
        }

        #endregion
    }
}
