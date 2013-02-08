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

using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.LabModule
{
    /// <summary>
    /// LabResultBuilder provides a fluent interface for creating a LabResult.
    /// </summary>
    public class LabResultBuilder
    {
        private string _unitOfMeasureCode;
        private double? _value;
        private CodedConcept _labTestResultNameCodedConcept;

        /// <summary>
        /// Performs an implicit conversion from <see cref="LabResultBuilder"/> to <see cref="LabResult"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator LabResult(LabResultBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the lab test result name coded concept.
        /// </summary>
        /// <param name="labTestResultNameCodedConcept">The lab test result name coded concept.</param>
        /// <returns>A LabResultBuilder.</returns>
        public LabResultBuilder WithLabTestResultNameCodedConcept(CodedConcept labTestResultNameCodedConcept)
        {
            _labTestResultNameCodedConcept = labTestResultNameCodedConcept;
            return this;
        }

        /// <summary>
        /// Assigns the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A LabResultBuilder.</returns>
        public LabResultBuilder WithValue(double? value)
        {
            _value = value;
            return this;
        }

        /// <summary>
        /// Assigns the unit of measure code.
        /// </summary>
        /// <param name="unitOfMeasureCode">The unit of measure code.</param>
        /// <returns>A LabResultBuilder.</returns>
        public LabResultBuilder WithUnitOfMeasureCode(string unitOfMeasureCode)
        {
            _unitOfMeasureCode = unitOfMeasureCode;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A LabResult.</returns>
        public LabResult Build()
        {
            return new LabResult(
                _labTestResultNameCodedConcept,
                _value,
                _unitOfMeasureCode);
        }
    }
}
