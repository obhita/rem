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

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Data transfer object for LabResult class.
    /// </summary>
    [DataContract]
    public partial class LabResultDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private CodedConceptDto _labTestResultNameCodedConcept;
        private string _referenceRange;
        private string _unitOfMeasureCode;
        private double _value;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the lab test result name coded concept.
        /// </summary>
        /// <value>The lab test result name coded concept.</value>
        [DataMember]
        public CodedConceptDto LabTestResultNameCodedConcept
        {
            get { return _labTestResultNameCodedConcept; }
            set { ApplyPropertyChange ( ref _labTestResultNameCodedConcept, () => LabTestResultNameCodedConcept, value ); }
        }

        /// <summary>
        /// Gets or sets the reference range.
        /// </summary>
        /// <value>The reference range.</value>
        [DataMember]
        public string ReferenceRange
        {
            get { return _referenceRange; }
            set { ApplyPropertyChange ( ref _referenceRange, () => ReferenceRange, value ); }
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        public string Result
        {
            get { return string.Format ( "{0} {1}", Value, UnitOfMeasureCode ); }
        }

        /// <summary>
        /// Gets the name of the test.
        /// </summary>
        public string TestName
        {
            get { return string.Format ( "{0} ({1})", LabTestResultNameCodedConcept.DisplayName, ReferenceRange ); }
        }

        /// <summary>
        /// Gets or sets the unit of measure code.
        /// </summary>
        /// <value>The unit of measure code.</value>
        [DataMember]
        public string UnitOfMeasureCode
        {
            get { return _unitOfMeasureCode; }
            set { ApplyPropertyChange ( ref _unitOfMeasureCode, () => UnitOfMeasureCode, value ); }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [DataMember]
        public double Value
        {
            get { return _value; }
            set { ApplyPropertyChange ( ref _value, () => Value, value ); }
        }

        #endregion
    }
}
