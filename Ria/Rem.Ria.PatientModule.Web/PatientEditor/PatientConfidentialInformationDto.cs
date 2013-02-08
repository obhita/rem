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
    /// Data transfer object for PatientConfidentialInformation class.
    /// </summary>
    public class PatientConfidentialInformationDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private string _confidentialFamilyInformationDescription;
        private DateTime? _convictedOfArsonDate;
        private bool? _convictedOfArsonIndicator;
        private bool? _domesticAbuseVictimIndicator;
        private bool? _physicalAbuseVictimIndicator;
        private DateTime? _registeredSexOffenderDate;
        private bool? _registeredSexOffenderIndicator;
        private bool? _sexualAbuseVictimIndicator;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the confidential family information description.
        /// </summary>
        /// <value>The confidential family information description.</value>
        [DataMember]
        public string ConfidentialFamilyInformationDescription
        {
            get { return _confidentialFamilyInformationDescription; }
            set { ApplyPropertyChange ( ref _confidentialFamilyInformationDescription, () => ConfidentialFamilyInformationDescription, value ); }
        }

        /// <summary>
        /// Gets or sets the convicted of arson date.
        /// </summary>
        /// <value>The convicted of arson date.</value>
        [DataMember]
        public DateTime? ConvictedOfArsonDate
        {
            get { return _convictedOfArsonDate; }
            set { ApplyPropertyChange ( ref _convictedOfArsonDate, () => ConvictedOfArsonDate, value ); }
        }

        /// <summary>
        /// Gets or sets the convicted of arson indicator.
        /// </summary>
        /// <value>The convicted of arson indicator.</value>
        [DataMember]
        public bool? ConvictedOfArsonIndicator
        {
            get { return _convictedOfArsonIndicator; }
            set { ApplyPropertyChange ( ref _convictedOfArsonIndicator, () => ConvictedOfArsonIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the domestic abuse victim indicator.
        /// </summary>
        /// <value>The domestic abuse victim indicator.</value>
        [DataMember]
        public bool? DomesticAbuseVictimIndicator
        {
            get { return _domesticAbuseVictimIndicator; }
            set { ApplyPropertyChange ( ref _domesticAbuseVictimIndicator, () => DomesticAbuseVictimIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the physical abuse victim indicator.
        /// </summary>
        /// <value>The physical abuse victim indicator.</value>
        [DataMember]
        public bool? PhysicalAbuseVictimIndicator
        {
            get { return _physicalAbuseVictimIndicator; }
            set { ApplyPropertyChange ( ref _physicalAbuseVictimIndicator, () => PhysicalAbuseVictimIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the registered sex offender date.
        /// </summary>
        /// <value>The registered sex offender date.</value>
        [DataMember]
        public DateTime? RegisteredSexOffenderDate
        {
            get { return _registeredSexOffenderDate; }
            set { ApplyPropertyChange ( ref _registeredSexOffenderDate, () => RegisteredSexOffenderDate, value ); }
        }

        /// <summary>
        /// Gets or sets the registered sex offender indicator.
        /// </summary>
        /// <value>The registered sex offender indicator.</value>
        [DataMember]
        public bool? RegisteredSexOffenderIndicator
        {
            get { return _registeredSexOffenderIndicator; }
            set { ApplyPropertyChange ( ref _registeredSexOffenderIndicator, () => RegisteredSexOffenderIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the sexual abuse victim indicator.
        /// </summary>
        /// <value>The sexual abuse victim indicator.</value>
        [DataMember]
        public bool? SexualAbuseVictimIndicator
        {
            get { return _sexualAbuseVictimIndicator; }
            set { ApplyPropertyChange ( ref _sexualAbuseVictimIndicator, () => SexualAbuseVictimIndicator, value ); }
        }

        #endregion
    }
}
