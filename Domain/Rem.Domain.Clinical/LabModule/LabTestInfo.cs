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
using System.Text;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.NamingStrategy;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.LabModule
{
    /// <summary>
    /// LabTestInfo defines the characteristics of a laboratory test.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class LabTestInfo : IEquatable<LabTestInfo>
    {
        private LabTestInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabTestInfo"/> class.
        /// </summary>
        /// <param name="labTestName">Name of the lab test.</param>
        /// <param name="labTestTypeCodedConcept">The lab test type coded concept.</param>
        /// <param name="normalRangeDescription">The normal range description.</param>
        /// <param name="interpretationCodeCodedConcept">The interpretation code coded concept.</param>
        /// <param name="labResultStatusCodedConcept">The lab result status coded concept.</param>
        /// <param name="testReportDate">The test report date.</param>
        /// <param name="labTestNote">The lab test note.</param>
        internal LabTestInfo(LabTestName labTestName,
                           CodedConcept labTestTypeCodedConcept,
                           string normalRangeDescription,
                           CodedConcept interpretationCodeCodedConcept,
                           CodedConcept labResultStatusCodedConcept,
                           DateTime? testReportDate,
                           string labTestNote)
        {
            Check.IsNotNull ( labTestName, () => LabTestName );

            LabTestName = labTestName;
            LabTestTypeCodedConcept = labTestTypeCodedConcept;
            NormalRangeDescription = normalRangeDescription;
            InterpretationCodeCodedConcept = interpretationCodeCodedConcept;
            LabResultStatusCodedConcept = labResultStatusCodedConcept;
            TestReportDate = testReportDate;
            LabTestNote = labTestNote;
        }

        /// <summary>
        /// This is the "Test Type" field in the HL7 and ProcedureCode in C32
        /// </summary>
        public virtual CodedConcept LabTestTypeCodedConcept { get; private set; }

        /// <summary>
        /// This is the "Test Name" field in the HL7 and the ResultType in C32
        /// </summary>
        [NotNull]
        public virtual LabTestName LabTestName { get; private set; }

        /// <summary>
        /// The references range in hl7
        /// </summary>
        public virtual string NormalRangeDescription { get; private set; }

        /// <summary>
        /// The Abnormal Flag in hl7
        /// </summary>
        public virtual CodedConcept InterpretationCodeCodedConcept { get; private set; }

        /// <summary>
        /// The ObservationResultStatus in hl7
        /// Indicates the status of the test (i.e. final, preliminary results, partial results, etc)
        /// </summary>
        public virtual CodedConcept LabResultStatusCodedConcept { get; private set; }

        /// <summary>
        /// Gets the test report date.
        /// </summary>
        public virtual DateTime? TestReportDate { get; private set; }

        /// <summary>
        /// Gets the lab test note.
        /// </summary>
        public virtual string LabTestNote { get; private set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( LabTestInfo other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.LabTestTypeCodedConcept, LabTestTypeCodedConcept ) && Equals ( other.LabTestName, LabTestName ) && Equals ( other.NormalRangeDescription, NormalRangeDescription ) && Equals ( other.InterpretationCodeCodedConcept, InterpretationCodeCodedConcept ) && Equals ( other.LabResultStatusCodedConcept, LabResultStatusCodedConcept ) && other.TestReportDate.Equals ( TestReportDate ) && Equals ( other.LabTestNote, LabTestNote );
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals ( object obj )
        {
            return (this as IEquatable<LabTestInfo>).Equals(obj as LabTestInfo);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode ()
        {
            unchecked
            {
                int result = ( LabTestTypeCodedConcept != null ? LabTestTypeCodedConcept.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( LabTestName != null ? LabTestName.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( NormalRangeDescription != null ? NormalRangeDescription.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( InterpretationCodeCodedConcept != null ? InterpretationCodeCodedConcept.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( LabResultStatusCodedConcept != null ? LabResultStatusCodedConcept.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( TestReportDate.HasValue ? TestReportDate.Value.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( LabTestNote != null ? LabTestNote.GetHashCode () : 0 );
                return result;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder ();
            const string Spacer = " - ";
            foreach ( var propertyInfo in GetType().GetProperties() )
            {
                sb.Append ( Spacer + propertyInfo.GetValue ( this, null ) );
            }
            return sb.ToString ().Substring ( Spacer.Length );
        }
    }
}
