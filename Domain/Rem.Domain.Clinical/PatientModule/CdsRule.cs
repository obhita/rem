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

using Rem.Domain.Clinical.LabModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// CdsRule defines a clinical decision support rule.
    /// </summary>
    public class CdsRule : AuditableAggregateRootBase
    {
        private int? _age;
        private LabTestName _labTestName;
        private CodedConcept _medicationCodedConcept;
        private string _name;
        private CodedConcept _problemCodedConcept;
        private string _recommendationNote;
        private int? _validLabOrderMonthCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="CdsRule"/> class.
        /// </summary>
        protected internal CdsRule ()
        {
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            private set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        /// Gets the recommendation note.
        /// </summary>
        public virtual string RecommendationNote
        {
            get { return _recommendationNote; }
            private set { ApplyPropertyChange ( ref _recommendationNote, () => RecommendationNote, value ); }
        }

        /// <summary>
        /// Gets the medication coded concept.
        /// </summary>
        public virtual CodedConcept MedicationCodedConcept
        {
            get { return _medicationCodedConcept; }
            private set { ApplyPropertyChange ( ref _medicationCodedConcept, () => MedicationCodedConcept, value ); }
        }

        /// <summary>
        /// Gets the problem coded concept.
        /// </summary>
        public virtual CodedConcept ProblemCodedConcept
        {
            get { return _problemCodedConcept; }
            private set { ApplyPropertyChange ( ref _problemCodedConcept, () => ProblemCodedConcept, value ); }
        }

        /// <summary>
        /// Gets the name of the lab test.
        /// </summary>
        /// <value>
        /// The name of the lab test.
        /// </value>
        public virtual LabTestName LabTestName
        {
            get { return _labTestName; }
            private set { ApplyPropertyChange ( ref _labTestName, () => LabTestName, value ); }
        }

        /// <summary>
        /// Gets the age.
        /// </summary>
        public virtual int? Age
        {
            get { return _age; }
            private set { ApplyPropertyChange ( ref _age, () => Age, value ); }
        }

        /// <summary>
        /// Gets the valid lab order month count.
        /// </summary>
        public virtual int? ValidLabOrderMonthCount
        {
            get { return _validLabOrderMonthCount; }
            private set { ApplyPropertyChange ( ref _validLabOrderMonthCount, () => ValidLabOrderMonthCount, value ); }
        }

        /// <summary>
        /// Renames the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public virtual void Rename(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Revises the recommendation note.
        /// </summary>
        /// <param name="recommendationNote">The recommendation note.</param>
        public virtual void ReviseRecommendationNote(string recommendationNote)
        {
            _recommendationNote = recommendationNote;
        }

        /// <summary>
        /// Revises the medication coded concept.
        /// </summary>
        /// <param name="medicationCodedConcept">The medication coded concept.</param>
        public virtual void ReviseMedicationCodedConcept(CodedConcept medicationCodedConcept)
        {
            _medicationCodedConcept = medicationCodedConcept;
        }

        /// <summary>
        /// Revises the problem coded concept.
        /// </summary>
        /// <param name="problemCodedConcept">The problem coded concept.</param>
        public virtual void ReviseProblemCodedConcept(CodedConcept problemCodedConcept)
        {
            _problemCodedConcept = problemCodedConcept;
        }

        /// <summary>
        /// Revises the name of the lab test.
        /// </summary>
        /// <param name="labTestName">Name of the lab test.</param>
        public virtual void ReviseLabTestName(LabTestName labTestName)
        {
            _labTestName = labTestName;
        }

        /// <summary>
        /// Revises the age.
        /// </summary>
        /// <param name="age">The age.</param>
        public virtual void ReviseAge(int? age)
        {
            _age = age;
        }

        /// <summary>
        /// Revises the valid lab order month count.
        /// </summary>
        /// <param name="validLabOrderMonthCount">The valid lab order month count.</param>
        public virtual void ReviseValidLabOrderMonthCount(int? validLabOrderMonthCount)
        {
            _validLabOrderMonthCount = validLabOrderMonthCount;
        }
    }
}