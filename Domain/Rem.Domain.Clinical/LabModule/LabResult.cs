#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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

using System.Text;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.LabModule
{
    /// <summary>
    /// LabResult defines information about a laboratory test result.
    /// </summary>
    public class LabResult : AuditableAggregateNodeBase
    {
        private string _unitOfMeasureCode;
        private double? _value;
        private CodedConcept _labTestResultNameCodedConcept;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabResult"/> class.
        /// </summary>
        protected internal LabResult ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabResult"/> class.
        /// </summary>
        /// <param name="labTestResultNameCodedConcept">The lab test result name coded concept.</param>
        /// <param name="value">The value.</param>
        /// <param name="unitOfMeasureCode">The unit of measure code.</param>
        protected internal LabResult ( CodedConcept labTestResultNameCodedConcept, double? value, string unitOfMeasureCode )
        {
            Check.IsNotNull ( labTestResultNameCodedConcept, () => LabTestResultNameCodedConcept );

            _labTestResultNameCodedConcept = labTestResultNameCodedConcept;
            _value = value;
            _unitOfMeasureCode = unitOfMeasureCode;
        }

        /// <summary>
        /// Gets the unit of measure code.
        /// </summary>
        public virtual string UnitOfMeasureCode
        {
            get { return _unitOfMeasureCode; }
            private set { ApplyPropertyChange ( ref _unitOfMeasureCode, () => UnitOfMeasureCode, value ); }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public virtual double? Value
        {
            get { return _value; }
            private set { ApplyPropertyChange ( ref _value, () => Value, value ); }
        }

        /// <summary>
        /// Gets the lab test result name coded concept.
        /// </summary>
        [NotNull]
        public virtual CodedConcept LabTestResultNameCodedConcept
        {
            get { return _labTestResultNameCodedConcept; }
            private set
            {
                Check.IsNotNull ( value, () => LabTestResultNameCodedConcept );
                ApplyPropertyChange ( ref _labTestResultNameCodedConcept, () => LabTestResultNameCodedConcept, value );
            }
        }

        /// <summary>
        /// Gets or sets the lab test.
        /// </summary>
        /// <value>
        /// The lab test.
        /// </value>
        [NotNull]
        public virtual LabTest LabTest { get; protected internal set; }

        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        public override IAggregateRoot AggregateRoot
        {
            get { return LabTest.LabSpecimen; }
        }

        #region Overrides

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString ()
        {
            var sb = new StringBuilder ();
            sb.Append ( "Lab Result Unit: " );
            if ( _unitOfMeasureCode != null )
            {
                sb.Append ( _unitOfMeasureCode );
            }
            sb.Append ( "; " );
            sb.Append ( "Lab Result Value: " );
            if ( _value != null )
            {
                sb.Append ( _value.Value.ToString () );
            }

            return sb.ToString ();
        }

        #endregion
    }
}
