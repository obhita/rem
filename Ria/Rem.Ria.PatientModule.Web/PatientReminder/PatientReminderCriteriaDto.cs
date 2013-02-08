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

using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Web.DataTransferObject;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientReminder
{
    /// <summary>
    /// Data transfer object for PatientReminderCriteria class.
    /// </summary>
    public class PatientReminderCriteriaDto : PageSortBaseDto
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>The age of the patient.</value>
        [DataMember]
        public int? Age { get; set; }

        /// <summary>
        /// Gets or sets the age filter modifier.
        /// </summary>
        /// <value>The age filter modifier.</value>
        [DataMember]
        public string AgeFilterModifier { get; set; }

        /// <summary>
        /// Gets or sets the allergy.
        /// </summary>
        /// <value>The allergy.</value>
        [DataMember]
        public CodedConceptDto Allergy { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        [DataMember]
        public LookupValueDto Gender { get; set; }

        /// <summary>
        /// Gets or sets the lab result filter modifier.
        /// </summary>
        /// <value>The lab result filter modifier.</value>
        [DataMember]
        public string LabResultFilterModifier { get; set; }

        /// <summary>
        /// Gets or sets the lab result value.
        /// </summary>
        /// <value>The lab result value.</value>
        [DataMember]
        public int? LabResultValue { get; set; }

        /// <summary>
        /// Gets or sets the lab test.
        /// </summary>
        /// <value>The lab test.</value>
        [DataMember]
        public LookupValueDto LabTest { get; set; }

        /// <summary>
        /// Gets or sets the medication.
        /// </summary>
        /// <value>The medication.</value>
        [DataMember]
        public CodedConceptDto Medication { get; set; }

        /// <summary>
        /// Gets or sets the problem.
        /// </summary>
        /// <value>The problem.</value>
        [DataMember]
        public ProblemDto Problem { get; set; }

        #endregion
    }
}
