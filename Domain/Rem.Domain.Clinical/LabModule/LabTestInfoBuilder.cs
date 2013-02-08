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

using System;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.LabModule
{
    /// <summary>
    /// LabTestInfoBuilder provides a fluent interface for creating a LabTestInfo.
    /// </summary>
    public class LabTestInfoBuilder
    {
        private CodedConcept _labTestTypeCodedConcept;
        private LabTestName _labTestName;
        private string _normalRangeDescription;
        private CodedConcept _interpretationCodeCodedConcept;
        private CodedConcept _labResultStatusCodedConcept;
        private DateTime? _testReportDate;
        private string _labTestNote;

        /// <summary>
        /// Performs an implicit conversion from <see cref="LabTestInfoBuilder"/> to <see cref="LabTestInfo"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator LabTestInfo(LabTestInfoBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the lab test type coded concept.
        /// </summary>
        /// <param name="labTestTypeCodedConcept">The lab test type coded concept.</param>
        /// <returns>A LabTestInfoBuilder.</returns>
        public LabTestInfoBuilder WithLabTestTypeCodedConcept(CodedConcept labTestTypeCodedConcept)
        {
            _labTestTypeCodedConcept = labTestTypeCodedConcept;
            return this;
        }

        /// <summary>
        /// Assigns the name of the lab test.
        /// </summary>
        /// <param name="labTestName">Name of the lab test.</param>
        /// <returns>A LabTestInfoBuilder.</returns>
        public LabTestInfoBuilder WithLabTestName(LabTestName labTestName)
        {
            _labTestName = labTestName;
            return this;
        }

        /// <summary>
        /// Assigns the normal range description.
        /// </summary>
        /// <param name="normalRangeDescription">The normal range description.</param>
        /// <returns>A LabTestInfoBuilder.</returns>
        public LabTestInfoBuilder WithNormalRangeDescription(string normalRangeDescription)
        {
            _normalRangeDescription = normalRangeDescription;
            return this;
        }

        /// <summary>
        /// Assigns the interpretation coded concept.
        /// </summary>
        /// <param name="interpretationCodeCodedConcept">The interpretation code coded concept.</param>
        /// <returns>A LabTestInfoBuilder.</returns>
        public LabTestInfoBuilder WithInterpretationCodedConcept(CodedConcept interpretationCodeCodedConcept)
        {
            _interpretationCodeCodedConcept = interpretationCodeCodedConcept;
            return this;
        }

        /// <summary>
        /// Assigns the lab result status coded concept.
        /// </summary>
        /// <param name="labResultStatusCodedConcept">The lab result status coded concept.</param>
        /// <returns>A LabTestInfoBuilder.</returns>
        public LabTestInfoBuilder WithLabResultStatusCodedConcept(CodedConcept labResultStatusCodedConcept)
        {
            _labResultStatusCodedConcept = labResultStatusCodedConcept;
            return this;
        }

        /// <summary>
        /// Assigns the test report date.
        /// </summary>
        /// <param name="testReportDate">The test report date.</param>
        /// <returns>A LabTestInfoBuilder.</returns>
        public LabTestInfoBuilder WithTestReportDate(DateTime? testReportDate)
        {
            _testReportDate = testReportDate;
            return this;
        }

        /// <summary>
        /// Assigns the lab test note.
        /// </summary>
        /// <param name="labTestNote">The lab test note.</param>
        /// <returns>A LabTestInfoBuilder.</returns>
        public LabTestInfoBuilder WithLabTestNote(string labTestNote)
        {
            _labTestNote = labTestNote;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A LabTestInfo.</returns>
        public LabTestInfo Build()
        {
            return new LabTestInfo ( 
                _labTestName,
                _labTestTypeCodedConcept,
                _normalRangeDescription,
                _interpretationCodeCodedConcept,
                _labResultStatusCodedConcept,
                _testReportDate,
                _labTestNote);
        }
    }
}
