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
using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.PatientEditor
{
    /// <summary>
    /// Data transfer object for PatientVeteranInformation class.
    /// </summary>
    [DataContract]
    public class PatientVeteranInformationDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private string _disabilityDescription;
        private string _disabilityPercentageValue;
        private bool? _haveCombatHistoryIndicator;
        private bool? _haveServedInMilitaryIndicator;
        private string _registeredVaHospitalName;
        private DateTime? _serviceEndDate;
        private DateTime? _serviceStartDate;
        private string _vaCaseNumber;
        private LookupValueDto _veteranDischargeStatus;
        private LookupValueDto _veteranServiceBranch;
        private LookupValueDto _veteranStatus;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the disability description.
        /// </summary>
        /// <value>The disability description.</value>
        [DataMember]
        public virtual string DisabilityDescription
        {
            get { return _disabilityDescription; }
            set { ApplyPropertyChange ( ref _disabilityDescription, () => DisabilityDescription, value ); }
        }

        /// <summary>
        /// Gets or sets the disability percentage value.
        /// </summary>
        /// <value>The disability percentage value.</value>
        [DataMember]
        public virtual string DisabilityPercentageValue
        {
            get { return _disabilityPercentageValue; }
            set { ApplyPropertyChange ( ref _disabilityPercentageValue, () => DisabilityPercentageValue, value ); }
        }

        /// <summary>
        /// Gets or sets the have combat history indicator.
        /// </summary>
        /// <value>The have combat history indicator.</value>
        [DataMember]
        public virtual bool? HaveCombatHistoryIndicator
        {
            get { return _haveCombatHistoryIndicator; }
            set { ApplyPropertyChange ( ref _haveCombatHistoryIndicator, () => HaveCombatHistoryIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the have served in military indicator.
        /// </summary>
        /// <value>The have served in military indicator.</value>
        [DataMember]
        public virtual bool? HaveServedInMilitaryIndicator
        {
            get { return _haveServedInMilitaryIndicator; }
            set { ApplyPropertyChange ( ref _haveServedInMilitaryIndicator, () => HaveServedInMilitaryIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the registered va hospital.
        /// </summary>
        /// <value>The name of the registered va hospital.</value>
        [DataMember]
        public virtual string RegisteredVaHospitalName
        {
            get { return _registeredVaHospitalName; }
            set { ApplyPropertyChange ( ref _registeredVaHospitalName, () => RegisteredVaHospitalName, value ); }
        }

        /// <summary>
        /// Gets or sets the service end date.
        /// </summary>
        /// <value>The service end date.</value>
        [DataMember]
        public virtual DateTime? ServiceEndDate
        {
            get { return _serviceEndDate; }
            set { ApplyPropertyChange ( ref _serviceEndDate, () => ServiceEndDate, value ); }
        }

        /// <summary>
        /// Gets or sets the service start date.
        /// </summary>
        /// <value>The service start date.</value>
        [DataMember]
        public virtual DateTime? ServiceStartDate
        {
            get { return _serviceStartDate; }
            set { ApplyPropertyChange ( ref _serviceStartDate, () => ServiceStartDate, value ); }
        }

        /// <summary>
        /// Gets or sets the va case number.
        /// </summary>
        /// <value>The va case number.</value>
        [DataMember]
        public virtual string VaCaseNumber
        {
            get { return _vaCaseNumber; }
            set { ApplyPropertyChange ( ref _vaCaseNumber, () => VaCaseNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the veteran discharge status.
        /// </summary>
        /// <value>The veteran discharge status.</value>
        [DataMember]
        public virtual LookupValueDto VeteranDischargeStatus
        {
            get { return _veteranDischargeStatus; }
            set { ApplyPropertyChange ( ref _veteranDischargeStatus, () => VeteranDischargeStatus, value ); }
        }

        /// <summary>
        /// Gets or sets the veteran service branch.
        /// </summary>
        /// <value>The veteran service branch.</value>
        [DataMember]
        public virtual LookupValueDto VeteranServiceBranch
        {
            get { return _veteranServiceBranch; }
            set { ApplyPropertyChange ( ref _veteranServiceBranch, () => VeteranServiceBranch, value ); }
        }

        /// <summary>
        /// Gets or sets the veteran status.
        /// </summary>
        /// <value>The veteran status.</value>
        [DataMember]
        public LookupValueDto VeteranStatus
        {
            get { return _veteranStatus; }
            set { ApplyPropertyChange ( ref _veteranStatus, () => VeteranStatus, value ); }
        }

        #endregion
    }
}
