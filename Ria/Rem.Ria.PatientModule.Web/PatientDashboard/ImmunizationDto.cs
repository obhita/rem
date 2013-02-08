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

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Data transfer object for Immunization class.
    /// </summary>
    [DataContract]
    public partial class ImmunizationDto : ActivityDto
    {
        #region Constants and Fields

        private double? _administeredAmount;
        private LookupValueDto _immunizationNotGivenReason;
        private LookupValueDto _immunizationUnitOfMeasure;
        private CodedConceptDto _vaccineCodedConcept;
        private string _vaccineLotNumber;
        private string _vaccinemanufacturerCode;
        private string _vaccinemanufacturerName;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the administered amount.
        /// </summary>
        /// <value>The administered amount.</value>
        [DataMember]
        public double? AdministeredAmount
        {
            get { return _administeredAmount; }
            set { ApplyPropertyChange ( ref _administeredAmount, () => AdministeredAmount, value ); }
        }

        /// <summary>
        /// Gets or sets the immunization not given reason.
        /// </summary>
        /// <value>The immunization not given reason.</value>
        [DataMember]
        public LookupValueDto ImmunizationNotGivenReason
        {
            get { return _immunizationNotGivenReason; }
            set { ApplyPropertyChange ( ref _immunizationNotGivenReason, () => ImmunizationNotGivenReason, value ); }
        }

        /// <summary>
        /// Gets or sets the immunization unit of measure.
        /// </summary>
        /// <value>The immunization unit of measure.</value>
        [DataMember]
        public LookupValueDto ImmunizationUnitOfMeasure
        {
            get { return _immunizationUnitOfMeasure; }
            set { ApplyPropertyChange ( ref _immunizationUnitOfMeasure, () => ImmunizationUnitOfMeasure, value ); }
        }

        /// <summary>
        /// Gets or sets the vaccine coded concept.
        /// </summary>
        /// <value>The vaccine coded concept.</value>
        [DataMember]
        public CodedConceptDto VaccineCodedConcept
        {
            get { return _vaccineCodedConcept; }
            set { ApplyPropertyChange ( ref _vaccineCodedConcept, () => VaccineCodedConcept, value ); }
        }

        /// <summary>
        /// Gets or sets the vaccine lot number.
        /// </summary>
        /// <value>The vaccine lot number.</value>
        [DataMember]
        public string VaccineLotNumber
        {
            get { return _vaccineLotNumber; }
            set { ApplyPropertyChange ( ref _vaccineLotNumber, () => VaccineLotNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the vaccine manufacturer code.
        /// </summary>
        /// <value>The vaccine manufacturer code.</value>
        [DataMember]
        public string VaccineManufacturerCode
        {
            get { return _vaccinemanufacturerCode; }
            set { ApplyPropertyChange ( ref _vaccinemanufacturerCode, () => VaccineManufacturerCode, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the vaccine manufacturer.
        /// </summary>
        /// <value>The name of the vaccine manufacturer.</value>
        [DataMember]
        public string VaccineManufacturerName
        {
            get { return _vaccinemanufacturerName; }
            set { ApplyPropertyChange ( ref _vaccinemanufacturerName, () => VaccineManufacturerName, value ); }
        }

        #endregion
    }
}
