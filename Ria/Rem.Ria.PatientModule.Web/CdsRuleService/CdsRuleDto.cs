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

using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Web.DataTransferObject;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.CdsRuleService
{
    /// <summary>
    /// Data transfer object for CdsRule class.
    /// </summary>
    public class CdsRuleDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private int? _age;
        private LookupValueDto _labTestName;
        private CodedConceptDto _medicationCodedConcept;
        private string _name;
        private ProblemDto _problemCodedConcept;
        private string _recommendation;
        private int? _validLabOrderMonthCount;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>The age for the rule.</value>
        public int? Age
        {
            get { return _age; }
            set { ApplyPropertyChange ( ref _age, () => Age, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the lab test.
        /// </summary>
        /// <value>The name of the lab test.</value>
        public LookupValueDto LabTestName
        {
            get { return _labTestName; }
            set { ApplyPropertyChange ( ref _labTestName, () => LabTestName, value ); }
        }

        /// <summary>
        /// Gets or sets the medication coded concept.
        /// </summary>
        /// <value>The medication coded concept.</value>
        public CodedConceptDto MedicationCodedConcept
        {
            get { return _medicationCodedConcept; }
            set { ApplyPropertyChange ( ref _medicationCodedConcept, () => MedicationCodedConcept, value ); }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the rule.</value>
        public string Name
        {
            get { return _name; }
            set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        /// Gets or sets the problem coded concept.
        /// </summary>
        /// <value>The problem coded concept.</value>
        public ProblemDto ProblemCodedConcept
        {
            get { return _problemCodedConcept; }
            set { ApplyPropertyChange ( ref _problemCodedConcept, () => ProblemCodedConcept, value ); }
        }

        /// <summary>
        /// Gets or sets the recommendation note.
        /// </summary>
        /// <value>The recommendation note.</value>
        public string RecommendationNote
        {
            get { return _recommendation; }
            set { ApplyPropertyChange ( ref _recommendation, () => RecommendationNote, value ); }
        }

        /// <summary>
        /// Gets or sets the valid lab order month count.
        /// </summary>
        /// <value>The valid lab order month count.</value>
        public int? ValidLabOrderMonthCount
        {
            get { return _validLabOrderMonthCount; }
            set { ApplyPropertyChange ( ref _validLabOrderMonthCount, () => ValidLabOrderMonthCount, value ); }
        }

        #endregion
    }
}
