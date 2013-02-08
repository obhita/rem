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

using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <summary>
    /// Data transfer object for GpraProblemsTreatmentRecovery class.
    /// </summary>
    public class GpraProblemsTreatmentRecoveryDto : GpraDtoBase
    {
        #region Constants and Fields

        private GpraNonResponseTypeDto<int?> _anxietyDayCount;
        private GpraNonResponseTypeDto<int?> _brainMisfunctionDayCount;
        private GpraNonResponseTypeDto<int?> _depressionDayCount;
        private GpraNonResponseTypeDto<int?> _eRMentalEmotionalDifficultiesTimeCount;
        private bool? _erAlcoholSubstanceAbuseIndicator;
        private GpraNonResponseTypeDto<int?> _erAlcoholSubstanceAbuseTimeCount;
        private bool? _erMentalEmotionalDifficultiesIndicator;
        private bool? _erPhysicalComplaintIndicator;
        private GpraNonResponseTypeDto<int?> _erPhysicalComplaintTimeCount;
        private GpraNonResponseTypeDto<LookupValueDto> _gpraOverallHealth;
        private GpraNonResponseTypeDto<LookupValueDto> _gpraPsychologicalImpact;
        private GpraNonResponseTypeDto<LookupValueDto> _gpraSexualActivity;
        private GpraNonResponseTypeDto<int?> _hallucinationsDayCount;
        private GpraNonResponseTypeDto<bool?> _hivTestIndicator;
        private bool? _hivTestResultsKnownIndicator;
        private bool? _inpatientAlcoholSubstanceAbuseIndicator;
        private GpraNonResponseTypeDto<int?> _inpatientAlcoholSubstanceAbuseNightCount;
        private bool? _inpatientMentalEmotionalDifficultiesIndicator;
        private GpraNonResponseTypeDto<int?> _inpatientMentalEmotionalDifficultiesNightCount;
        private bool? _inpatientPhysicalComplaintIndicator;
        private GpraNonResponseTypeDto<int?> _inpatientPhysicalComplaintNightCount;
        private bool? _outpatientAlcoholSubstanceAbuseIndicator;
        private GpraNonResponseTypeDto<int?> _outpatientAlcoholSubstanceAbuseTimeCount;
        private bool? _outpatientMentalEmotionalDifficultiesIndicator;
        private GpraNonResponseTypeDto<int?> _outpatientMentalEmotionalDifficultiesTimeCount;
        private bool? _outpatientPhysicalComplaintIndicator;
        private GpraNonResponseTypeDto<int?> _outpatientPhysicalComplaintTimeCount;
        private GpraNonResponseTypeDto<int?> _psychologicalEmotionalMedicationDayCount;
        private GpraNonResponseTypeDto<int?> _sexualContactsCount;
        private GpraNonResponseTypeDto<int?> _suicideDayCount;
        private GpraNonResponseTypeDto<int?> _unProtectedSexualContactsCount;
        private GpraNonResponseTypeDto<int?> _unprotectedSexualHighSaContactsCount;
        private GpraNonResponseTypeDto<int?> _unprotectedSexualHivContactsCount;
        private GpraNonResponseTypeDto<int?> _unprotectedSexualInjectionDrugContactsCount;
        private GpraNonResponseTypeDto<int?> _violentBehaviorDayCount;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the anxiety day count.
        /// </summary>
        /// <value>The anxiety day count.</value>
        public GpraNonResponseTypeDto<int?> AnxietyDayCount
        {
            get { return _anxietyDayCount; }
            set { ApplyPropertyChange ( ref _anxietyDayCount, () => AnxietyDayCount, value ); }
        }

        /// <summary>
        /// Gets or sets the brain misfunction day count.
        /// </summary>
        /// <value>The brain misfunction day count.</value>
        public GpraNonResponseTypeDto<int?> BrainMisfunctionDayCount
        {
            get { return _brainMisfunctionDayCount; }
            set { ApplyPropertyChange ( ref _brainMisfunctionDayCount, () => BrainMisfunctionDayCount, value ); }
        }

        /// <summary>
        /// Gets or sets the depression day count.
        /// </summary>
        /// <value>The depression day count.</value>
        public GpraNonResponseTypeDto<int?> DepressionDayCount
        {
            get { return _depressionDayCount; }
            set { ApplyPropertyChange ( ref _depressionDayCount, () => DepressionDayCount, value ); }
        }

        /// <summary>
        /// Gets or sets the er alcohol substance abuse indicator.
        /// </summary>
        /// <value>The er alcohol substance abuse indicator.</value>
        public bool? ErAlcoholSubstanceAbuseIndicator
        {
            get { return _erAlcoholSubstanceAbuseIndicator; }
            set { ApplyPropertyChange ( ref _erAlcoholSubstanceAbuseIndicator, () => ErAlcoholSubstanceAbuseIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the er alcohol substance abuse time count.
        /// </summary>
        /// <value>The er alcohol substance abuse time count.</value>
        public GpraNonResponseTypeDto<int?> ErAlcoholSubstanceAbuseTimeCount
        {
            get { return _erAlcoholSubstanceAbuseTimeCount; }
            set { ApplyPropertyChange ( ref _erAlcoholSubstanceAbuseTimeCount, () => ErAlcoholSubstanceAbuseTimeCount, value ); }
        }

        /// <summary>
        /// Gets or sets the er mental emotional difficulties indicator.
        /// </summary>
        /// <value>The er mental emotional difficulties indicator.</value>
        public bool? ErMentalEmotionalDifficultiesIndicator
        {
            get { return _erMentalEmotionalDifficultiesIndicator; }
            set { ApplyPropertyChange ( ref _erMentalEmotionalDifficultiesIndicator, () => ErMentalEmotionalDifficultiesIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the er mental emotional difficulties time count.
        /// </summary>
        /// <value>The er mental emotional difficulties time count.</value>
        public GpraNonResponseTypeDto<int?> ErMentalEmotionalDifficultiesTimeCount
        {
            get { return _eRMentalEmotionalDifficultiesTimeCount; }
            set { ApplyPropertyChange ( ref _eRMentalEmotionalDifficultiesTimeCount, () => ErMentalEmotionalDifficultiesTimeCount, value ); }
        }

        /// <summary>
        /// Gets or sets the er physical complaint indicator.
        /// </summary>
        /// <value>The er physical complaint indicator.</value>
        public bool? ErPhysicalComplaintIndicator
        {
            get { return _erPhysicalComplaintIndicator; }
            set { ApplyPropertyChange ( ref _erPhysicalComplaintIndicator, () => ErPhysicalComplaintIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the er physical complaint time count.
        /// </summary>
        /// <value>The er physical complaint time count.</value>
        public GpraNonResponseTypeDto<int?> ErPhysicalComplaintTimeCount
        {
            get { return _erPhysicalComplaintTimeCount; }
            set { ApplyPropertyChange ( ref _erPhysicalComplaintTimeCount, () => ErPhysicalComplaintTimeCount, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra overall health.
        /// </summary>
        /// <value>The gpra overall health.</value>
        public GpraNonResponseTypeDto<LookupValueDto> GpraOverallHealth
        {
            get { return _gpraOverallHealth; }
            set { ApplyPropertyChange ( ref _gpraOverallHealth, () => GpraOverallHealth, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra psychological impact.
        /// </summary>
        /// <value>The gpra psychological impact.</value>
        public GpraNonResponseTypeDto<LookupValueDto> GpraPsychologicalImpact
        {
            get { return _gpraPsychologicalImpact; }
            set { ApplyPropertyChange ( ref _gpraPsychologicalImpact, () => GpraPsychologicalImpact, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra sexual activity.
        /// </summary>
        /// <value>The gpra sexual activity.</value>
        public GpraNonResponseTypeDto<LookupValueDto> GpraSexualActivity
        {
            get { return _gpraSexualActivity; }
            set { ApplyPropertyChange ( ref _gpraSexualActivity, () => GpraSexualActivity, value ); }
        }

        /// <summary>
        /// Gets or sets the hallucinations day count.
        /// </summary>
        /// <value>The hallucinations day count.</value>
        public GpraNonResponseTypeDto<int?> HallucinationsDayCount
        {
            get { return _hallucinationsDayCount; }
            set { ApplyPropertyChange ( ref _hallucinationsDayCount, () => HallucinationsDayCount, value ); }
        }

        /// <summary>
        /// Gets or sets the hiv test indicator.
        /// </summary>
        /// <value>The hiv test indicator.</value>
        public GpraNonResponseTypeDto<bool?> HivTestIndicator
        {
            get { return _hivTestIndicator; }
            set { ApplyPropertyChange ( ref _hivTestIndicator, () => HivTestIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the hiv test results known indicator.
        /// </summary>
        /// <value>The hiv test results known indicator.</value>
        public bool? HivTestResultsKnownIndicator
        {
            get { return _hivTestResultsKnownIndicator; }
            set { ApplyPropertyChange ( ref _hivTestResultsKnownIndicator, () => HivTestResultsKnownIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the inpatient alcohol substance abuse indicator.
        /// </summary>
        /// <value>The inpatient alcohol substance abuse indicator.</value>
        public bool? InpatientAlcoholSubstanceAbuseIndicator
        {
            get { return _inpatientAlcoholSubstanceAbuseIndicator; }
            set { ApplyPropertyChange ( ref _inpatientAlcoholSubstanceAbuseIndicator, () => InpatientAlcoholSubstanceAbuseIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the inpatient alcohol substance abuse night count.
        /// </summary>
        /// <value>The inpatient alcohol substance abuse night count.</value>
        public GpraNonResponseTypeDto<int?> InpatientAlcoholSubstanceAbuseNightCount
        {
            get { return _inpatientAlcoholSubstanceAbuseNightCount; }
            set { ApplyPropertyChange ( ref _inpatientAlcoholSubstanceAbuseNightCount, () => InpatientAlcoholSubstanceAbuseNightCount, value ); }
        }

        /// <summary>
        /// Gets or sets the inpatient mental emotional difficulties indicator.
        /// </summary>
        /// <value>The inpatient mental emotional difficulties indicator.</value>
        public bool? InpatientMentalEmotionalDifficultiesIndicator
        {
            get { return _inpatientMentalEmotionalDifficultiesIndicator; }
            set { ApplyPropertyChange ( ref _inpatientMentalEmotionalDifficultiesIndicator, () => InpatientMentalEmotionalDifficultiesIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the inpatient mental emotional difficulties night count.
        /// </summary>
        /// <value>The inpatient mental emotional difficulties night count.</value>
        public GpraNonResponseTypeDto<int?> InpatientMentalEmotionalDifficultiesNightCount
        {
            get { return _inpatientMentalEmotionalDifficultiesNightCount; }
            set
            {
                ApplyPropertyChange (
                    ref _inpatientMentalEmotionalDifficultiesNightCount, () => InpatientMentalEmotionalDifficultiesNightCount, value );
            }
        }

        /// <summary>
        /// Gets or sets the inpatient physical complaint indicator.
        /// </summary>
        /// <value>The inpatient physical complaint indicator.</value>
        public bool? InpatientPhysicalComplaintIndicator
        {
            get { return _inpatientPhysicalComplaintIndicator; }
            set { ApplyPropertyChange ( ref _inpatientPhysicalComplaintIndicator, () => InpatientPhysicalComplaintIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the inpatient physical complaint night count.
        /// </summary>
        /// <value>The inpatient physical complaint night count.</value>
        public GpraNonResponseTypeDto<int?> InpatientPhysicalComplaintNightCount
        {
            get { return _inpatientPhysicalComplaintNightCount; }
            set { ApplyPropertyChange ( ref _inpatientPhysicalComplaintNightCount, () => InpatientPhysicalComplaintNightCount, value ); }
        }

        /// <summary>
        /// Gets or sets the outpatient alcohol substance abuse indicator.
        /// </summary>
        /// <value>The outpatient alcohol substance abuse indicator.</value>
        public bool? OutpatientAlcoholSubstanceAbuseIndicator
        {
            get { return _outpatientAlcoholSubstanceAbuseIndicator; }
            set { ApplyPropertyChange ( ref _outpatientAlcoholSubstanceAbuseIndicator, () => OutpatientAlcoholSubstanceAbuseIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the outpatient alcohol substance abuse time count.
        /// </summary>
        /// <value>The outpatient alcohol substance abuse time count.</value>
        public GpraNonResponseTypeDto<int?> OutpatientAlcoholSubstanceAbuseTimeCount
        {
            get { return _outpatientAlcoholSubstanceAbuseTimeCount; }
            set { ApplyPropertyChange ( ref _outpatientAlcoholSubstanceAbuseTimeCount, () => OutpatientAlcoholSubstanceAbuseTimeCount, value ); }
        }

        /// <summary>
        /// Gets or sets the outpatient mental emotional difficulties indicator.
        /// </summary>
        /// <value>The outpatient mental emotional difficulties indicator.</value>
        public bool? OutpatientMentalEmotionalDifficultiesIndicator
        {
            get { return _outpatientMentalEmotionalDifficultiesIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _outpatientMentalEmotionalDifficultiesIndicator, () => OutpatientMentalEmotionalDifficultiesIndicator, value );
            }
        }

        /// <summary>
        /// Gets or sets the outpatient mental emotional difficulties time count.
        /// </summary>
        /// <value>The outpatient mental emotional difficulties time count.</value>
        public GpraNonResponseTypeDto<int?> OutpatientMentalEmotionalDifficultiesTimeCount
        {
            get { return _outpatientMentalEmotionalDifficultiesTimeCount; }
            set
            {
                ApplyPropertyChange (
                    ref _outpatientMentalEmotionalDifficultiesTimeCount, () => OutpatientMentalEmotionalDifficultiesTimeCount, value );
            }
        }

        /// <summary>
        /// Gets or sets the outpatient physical complaint indicator.
        /// </summary>
        /// <value>The outpatient physical complaint indicator.</value>
        public bool? OutpatientPhysicalComplaintIndicator
        {
            get { return _outpatientPhysicalComplaintIndicator; }
            set { ApplyPropertyChange ( ref _outpatientPhysicalComplaintIndicator, () => OutpatientPhysicalComplaintIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the outpatient physical complaint time count.
        /// </summary>
        /// <value>The outpatient physical complaint time count.</value>
        public GpraNonResponseTypeDto<int?> OutpatientPhysicalComplaintTimeCount
        {
            get { return _outpatientPhysicalComplaintTimeCount; }
            set { ApplyPropertyChange ( ref _outpatientPhysicalComplaintTimeCount, () => OutpatientPhysicalComplaintTimeCount, value ); }
        }

        /// <summary>
        /// Gets or sets the psychological emotional medication day count.
        /// </summary>
        /// <value>The psychological emotional medication day count.</value>
        public GpraNonResponseTypeDto<int?> PsychologicalEmotionalMedicationDayCount
        {
            get { return _psychologicalEmotionalMedicationDayCount; }
            set { ApplyPropertyChange ( ref _psychologicalEmotionalMedicationDayCount, () => PsychologicalEmotionalMedicationDayCount, value ); }
        }

        /// <summary>
        /// Gets or sets the sexual contacts count.
        /// </summary>
        /// <value>The sexual contacts count.</value>
        public GpraNonResponseTypeDto<int?> SexualContactsCount
        {
            get { return _sexualContactsCount; }
            set { ApplyPropertyChange ( ref _sexualContactsCount, () => SexualContactsCount, value ); }
        }

        /// <summary>
        /// Gets or sets the suicide day count.
        /// </summary>
        /// <value>The suicide day count.</value>
        public GpraNonResponseTypeDto<int?> SuicideDayCount
        {
            get { return _suicideDayCount; }
            set { ApplyPropertyChange ( ref _suicideDayCount, () => SuicideDayCount, value ); }
        }

        /// <summary>
        /// Gets or sets the unprotected sexual contacts count.
        /// </summary>
        /// <value>The unprotected sexual contacts count.</value>
        public GpraNonResponseTypeDto<int?> UnprotectedSexualContactsCount
        {
            get { return _unProtectedSexualContactsCount; }
            set { ApplyPropertyChange ( ref _unProtectedSexualContactsCount, () => UnprotectedSexualContactsCount, value ); }
        }

        /// <summary>
        /// Gets or sets the unprotected sexual high sa contacts count.
        /// </summary>
        /// <value>The unprotected sexual high sa contacts count.</value>
        public GpraNonResponseTypeDto<int?> UnprotectedSexualHighSaContactsCount
        {
            get { return _unprotectedSexualHighSaContactsCount; }
            set { ApplyPropertyChange ( ref _unprotectedSexualHighSaContactsCount, () => UnprotectedSexualHighSaContactsCount, value ); }
        }

        /// <summary>
        /// Gets or sets the unprotected sexual hiv contacts count.
        /// </summary>
        /// <value>The unprotected sexual hiv contacts count.</value>
        public GpraNonResponseTypeDto<int?> UnprotectedSexualHivContactsCount
        {
            get { return _unprotectedSexualHivContactsCount; }
            set { ApplyPropertyChange ( ref _unprotectedSexualHivContactsCount, () => UnprotectedSexualHivContactsCount, value ); }
        }

        /// <summary>
        /// Gets or sets the unprotected sexual injection drug contacts count.
        /// </summary>
        /// <value>The unprotected sexual injection drug contacts count.</value>
        public GpraNonResponseTypeDto<int?> UnprotectedSexualInjectionDrugContactsCount
        {
            get { return _unprotectedSexualInjectionDrugContactsCount; }
            set { ApplyPropertyChange ( ref _unprotectedSexualInjectionDrugContactsCount, () => UnprotectedSexualInjectionDrugContactsCount, value ); }
        }

        /// <summary>
        /// Gets or sets the violent behavior day count.
        /// </summary>
        /// <value>The violent behavior day count.</value>
        public GpraNonResponseTypeDto<int?> ViolentBehaviorDayCount
        {
            get { return _violentBehaviorDayCount; }
            set { ApplyPropertyChange ( ref _violentBehaviorDayCount, () => ViolentBehaviorDayCount, value ); }
        }

        #endregion
    }
}
