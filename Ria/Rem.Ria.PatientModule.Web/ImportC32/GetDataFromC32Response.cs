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
using Agatha.Common;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.Ria.PatientModule.Web.PatientEditor;

namespace Rem.Ria.PatientModule.Web.ImportC32
{
    /// <summary>
    /// Importing C32 Response class.
    /// </summary>
    public class GetDataFromC32Response : Response
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the medications.
        /// </summary>
        /// <value>
        /// The medications.
        /// </value>
        public List<MedicationDto> Medications { get; set; }

        /// <summary>
        /// Gets or sets the allergies.
        /// </summary>
        /// <value>
        /// The allergies.
        /// </value>
        public List<AllergyDto> Allergies { get; set; }

        /// <summary>
        /// Gets or sets the problems.
        /// </summary>
        /// <value>
        /// The problems.
        /// </value>
        public List<ProblemDto> Problems { get; set; }

        /// <summary>
        /// Gets or sets the immunizations.
        /// </summary>
        /// <value>
        /// The immunizations.
        /// </value>
        public List<ImmunizationDto> Immunizations { get; set; }

        /// <summary>
        /// Gets or sets the lab specimen.
        /// </summary>
        /// <value>
        /// The lab specimen.
        /// </value>
        public LabSpecimenDto LabSpecimen { get; set; }

        /// <summary>
        /// Gets or sets the provenance.
        /// </summary>
        /// <value>
        /// The provenance.
        /// </value>
        public ProvenanceDto Provenance { get; set; }

        /// <summary>
        /// Gets or sets the vital sign.
        /// </summary>
        /// <value>
        /// The vital sign.
        /// </value>
        public VitalSignDto VitalSign { get; set; }

        #endregion
    }
}
