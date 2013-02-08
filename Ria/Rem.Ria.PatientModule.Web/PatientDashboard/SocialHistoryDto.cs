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
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Data transfer object for SocialHistory class.
    /// </summary>
    [DataContract]
    public class SocialHistoryDto : ActivityDto
    {
        #region Constants and Fields

        private bool? _drinkBeerWineOrOtherAlcoholicBeveragesIndicator;
        private bool? _isPhq2ScoreAbovePhq9ThresholdIndicator;
        private int? _phq2FeelingDownAnswerNumber;
        private int? _phq2LittleInterestInDoingThingsAnswerNumber;
        private int? _phq2Score;
        private LookupValueDto _smokingStatus;
        private DateTime? _smokingStatusAreYouWillingToQuitDate;
        private bool? _smokingStatusAreYouWillingToQuitIndicator;
        private int? _timesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the audit C drink beer wine or other alcoholic beverages indicator.
        /// </summary>
        /// <value>The audit C drink beer wine or other alcoholic beverages indicator.</value>
        [DataMember]
        public virtual bool? AuditCDrinkBeerWineOrOtherAlcoholicBeveragesIndicator
        {
            get { return _drinkBeerWineOrOtherAlcoholicBeveragesIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _drinkBeerWineOrOtherAlcoholicBeveragesIndicator, () => AuditCDrinkBeerWineOrOtherAlcoholicBeveragesIndicator, value );
            }
        }

        /// <summary>
        /// Gets or sets the dast10 times past year used illegal drug or prescription medication for non medical reasons number.
        /// </summary>
        /// <value>The dast10 times past year used illegal drug or prescription medication for non medical reasons number.</value>
        [DataMember]
        public virtual int? Dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber
        {
            get { return _timesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber; }
            set
            {
                ApplyPropertyChange (
                    ref _timesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber,
                    () => Dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber,
                    value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is audit C created.
        /// </summary>
        /// <value><c>true</c> if this instance is audit C created; otherwise, <c>false</c>.</value>
        [DataMember]
        public virtual bool IsAuditCCreated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is dast10 created.
        /// </summary>
        /// <value><c>true</c> if this instance is dast10 created; otherwise, <c>false</c>.</value>
        [DataMember]
        public virtual bool IsDast10Created { get; set; }

        /// <summary>
        /// Gets or sets the is PHQ2 score above PHQ9 threshold indicator.
        /// </summary>
        /// <value>The is PHQ2 score above PHQ9 threshold indicator.</value>
        [DataMember]
        public virtual bool? IsPhq2ScoreAbovePhq9ThresholdIndicator
        {
            get { return _isPhq2ScoreAbovePhq9ThresholdIndicator; }
            set { ApplyPropertyChange ( ref _isPhq2ScoreAbovePhq9ThresholdIndicator, () => IsPhq2ScoreAbovePhq9ThresholdIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is PHQ9 created.
        /// </summary>
        /// <value><c>true</c> if this instance is PHQ9 created; otherwise, <c>false</c>.</value>
        [DataMember]
        public virtual bool IsPhq9Created { get; set; }

        /// <summary>
        /// Gets or sets the PHQ2 feeling down answer number.
        /// </summary>
        /// <value>The PHQ2 feeling down answer number.</value>
        [DataMember]
        public virtual int? Phq2FeelingDownAnswerNumber
        {
            get { return _phq2FeelingDownAnswerNumber; }
            set { ApplyPropertyChange ( ref _phq2FeelingDownAnswerNumber, () => Phq2FeelingDownAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the PHQ2 little interest in doing things answer number.
        /// </summary>
        /// <value>The PHQ2 little interest in doing things answer number.</value>
        [DataMember]
        public virtual int? Phq2LittleInterestInDoingThingsAnswerNumber
        {
            get { return _phq2LittleInterestInDoingThingsAnswerNumber; }
            set { ApplyPropertyChange ( ref _phq2LittleInterestInDoingThingsAnswerNumber, () => Phq2LittleInterestInDoingThingsAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the PHQ2 score.
        /// </summary>
        /// <value>The PHQ2 score.</value>
        [DataMember]
        public virtual int? Phq2Score
        {
            get { return _phq2Score; }
            set { ApplyPropertyChange ( ref _phq2Score, () => Phq2Score, value ); }
        }

        /// <summary>
        /// Gets or sets the smoking status.
        /// </summary>
        /// <value>The smoking status.</value>
        [DataMember]
        public virtual LookupValueDto SmokingStatus
        {
            get { return _smokingStatus; }
            set { ApplyPropertyChange ( ref _smokingStatus, () => SmokingStatus, value ); }
        }

        /// <summary>
        /// Gets or sets the smoking status are you willing to quit date.
        /// </summary>
        /// <value>The smoking status are you willing to quit date.</value>
        [DataMember]
        public virtual DateTime? SmokingStatusAreYouWillingToQuitDate
        {
            get { return _smokingStatusAreYouWillingToQuitDate; }
            set { ApplyPropertyChange ( ref _smokingStatusAreYouWillingToQuitDate, () => SmokingStatusAreYouWillingToQuitDate, value ); }
        }

        /// <summary>
        /// Gets or sets the smoking status are you willing to quit indicator.
        /// </summary>
        /// <value>The smoking status are you willing to quit indicator.</value>
        [DataMember]
        public virtual bool? SmokingStatusAreYouWillingToQuitIndicator
        {
            get { return _smokingStatusAreYouWillingToQuitIndicator; }
            set { ApplyPropertyChange ( ref _smokingStatusAreYouWillingToQuitIndicator, () => SmokingStatusAreYouWillingToQuitIndicator, value ); }
        }

        #endregion
    }
}
