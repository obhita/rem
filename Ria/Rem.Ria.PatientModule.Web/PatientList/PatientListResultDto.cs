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
using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.PatientList
{
    /// <summary>
    /// Data transfer object for PatientListResult class.
    /// </summary>
    public partial class PatientListResultDto : KeyedDataTransferObject
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientListResultDto"/> class.
        /// </summary>
        public PatientListResultDto ()
        {
            PatientActiveMedications = new List<string> ();
            PatientActiveProblems = new List<string> ();
            PatientLabTests = new List<PatientLabTestSummaryDto> ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the patient active medications.
        /// </summary>
        /// <value>The patient active medications.</value>
        [DataMember]
        public IEnumerable<string> PatientActiveMedications { get; set; }

        /// <summary>
        /// Gets or sets the patient active problems.
        /// </summary>
        /// <value>The patient active problems.</value>
        [DataMember]
        public IEnumerable<string> PatientActiveProblems { get; set; }

        /// <summary>
        /// Gets or sets the patient age.
        /// </summary>
        /// <value>The patient age.</value>
        [DataMember]
        public int PatientAge { get; set; }

        /// <summary>
        /// Gets or sets the patient gender.
        /// </summary>
        /// <value>The patient gender.</value>
        [DataMember]
        public string PatientGender { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>The patient identifier.</value>
        [DataMember]
        public string PatientIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the patient lab tests.
        /// </summary>
        /// <value>The patient lab tests.</value>
        [DataMember]
        public IEnumerable<PatientLabTestSummaryDto> PatientLabTests { get; set; }

        /// <summary>
        /// Gets or sets the name of the patient.
        /// </summary>
        /// <value>The name of the patient.</value>
        [DataMember]
        public string PatientName { get; set; }

        #endregion
    }
}
