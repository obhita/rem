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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// The GpraProblemsTreatmentRecoverySection contains patient treatment recovery information from the Gpra interview.
    /// </summary>
    [Component]
    public class GpraProblemsTreatmentRecoverySection : GpraInterviewSectionBase
    {
        private readonly GpraNonResponseType<int?> _anxietyDayCount;
        private readonly GpraNonResponseType<int?> _brainMisfunctionDayCount;
        private readonly GpraNonResponseType<int?> _depressionDayCount;
        private readonly GpraNonResponseType<int?> _erMentalEmotionalDifficultiesTimeCount;
        private readonly bool? _erPhysicalComplaintIndicator;
        private readonly GpraNonResponseType<int?> _erPhysicalComplaintTimeCount;
        private readonly bool? _erAlcoholSubstanceAbuseIndicator;
        private readonly GpraNonResponseType<int?> _erAlcoholSubstanceAbuseTimeCount;
        private readonly bool? _erMentalEmotionalDifficultiesIndicator;
        private readonly GpraNonResponseType<GpraOverallHealth> _gpraOverallHealth;
        private readonly GpraNonResponseType<GpraPsychologicalImpact> _gpraPsychologicalImpact;
        private readonly GpraNonResponseType<GpraSexualActivity> _gpraSexualActivity;
        private readonly GpraNonResponseType<int?> _hallucinationsDayCount;
        private readonly GpraNonResponseType<bool?> _hivTestIndicator;
        private readonly bool? _hivTestResultsKnownIndicator;
        private readonly bool? _inpatientAlcoholSubstanceAbuseIndicator;
        private readonly GpraNonResponseType<int?> _inpatientAlcoholSubstanceAbuseNightCount;
        private readonly bool? _inpatientMentalEmotionalDifficultiesIndicator;
        private readonly GpraNonResponseType<int?> _inpatientMentalEmotionalDifficultiesNightCount;
        private readonly bool? _inpatientPhysicalComplaintIndicator;
        private readonly GpraNonResponseType<int?> _inpatientPhysicalComplaintNightCount;
        private readonly bool? _outpatientAlcoholSubstanceAbuseIndicator;
        private readonly GpraNonResponseType<int?> _outpatientAlcoholSubstanceAbuseTimeCount;
        private readonly bool? _outpatientMentalEmotionalDifficultiesIndicator;
        private readonly GpraNonResponseType<int?> _outpatientMentalEmotionalDifficultiesTimeCount;
        private readonly bool? _outpatientPhysicalComplaintIndicator;
        private readonly GpraNonResponseType<int?> _outpatientPhysicalComplaintTimeCount;
        private readonly GpraNonResponseType<int?> _psychologicalEmotionalMedicationDayCount;
        private readonly GpraNonResponseType<int?> _sexualContactsCount;
        private readonly GpraNonResponseType<int?> _suicideDayCount;
        private readonly GpraNonResponseType<int?> _unprotectedSexualContactsCount;
        private readonly GpraNonResponseType<int?> _unprotectedSexualHighSaContactsCount;
        private readonly GpraNonResponseType<int?> _unprotectedSexualHivContactsCount;
        private readonly GpraNonResponseType<int?> _unprotectedSexualInjectionDrugContactsCount;
        private readonly GpraNonResponseType<int?> _violentBehaviorDayCount;

        private GpraProblemsTreatmentRecoverySection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraProblemsTreatmentRecoverySection"/> class.
        /// </summary>
        /// <param name="anxietyDayCount">The anxiety day count.</param>
        /// <param name="brainMisfunctionDayCount">The brain misfunction day count.</param>
        /// <param name="depressionDayCount">The depression day count.</param>
        /// <param name="erMentalEmotionalDifficultiesTimeCount">The er mental emotional difficulties time count.</param>
        /// <param name="erPhysicalComplaintIndicator">The er physical complaint indicator.</param>
        /// <param name="erPhysicalComplaintTimeCount">The er physical complaint time count.</param>
        /// <param name="erAlcoholSubstanceAbuseIndicator">The er alcohol substance abuse indicator.</param>
        /// <param name="erAlcoholSubstanceAbuseTimeCount">The er alcohol substance abuse time count.</param>
        /// <param name="erMentalEmotionalDifficultiesIndicator">The er mental emotional difficulties indicator.</param>
        /// <param name="gpraOverallHealth">The gpra overall health.</param>
        /// <param name="gpraPsychologicalImpact">The gpra psychological impact.</param>
        /// <param name="gpraSexualActivity">The gpra sexual activity.</param>
        /// <param name="hallucinationsDayCount">The hallucinations day count.</param>
        /// <param name="hivTestIndicator">The hiv test indicator.</param>
        /// <param name="hivTestResultsKnownIndicator">The hiv test results known indicator.</param>
        /// <param name="inpatientAlcoholSubstanceAbuseIndicator">The inpatient alcohol substance abuse indicator.</param>
        /// <param name="inpatientAlcoholSubstanceAbuseNightCount">The inpatient alcohol substance abuse night count.</param>
        /// <param name="inpatientMentalEmotionalDifficultiesIndicator">The inpatient mental emotional difficulties indicator.</param>
        /// <param name="inpatientMentalEmotionalDifficultiesNightCount">The inpatient mental emotional difficulties night count.</param>
        /// <param name="inpatientPhysicalComplaintIndicator">The inpatient physical complaint indicator.</param>
        /// <param name="inpatientPhysicalComplaintNightCount">The inpatient physical complaint night count.</param>
        /// <param name="outpatientAlcoholSubstanceAbuseIndicator">The outpatient alcohol substance abuse indicator.</param>
        /// <param name="outpatientAlcoholSubstanceAbuseTimeCount">The outpatient alcohol substance abuse time count.</param>
        /// <param name="outpatientMentalEmotionalDifficultiesIndicator">The outpatient mental emotional difficulties indicator.</param>
        /// <param name="outpatientMentalEmotionalDifficultiesTimeCount">The outpatient mental emotional difficulties time count.</param>
        /// <param name="outpatientPhysicalComplaintIndicator">The outpatient physical complaint indicator.</param>
        /// <param name="outpatientPhysicalComplaintTimeCount">The outpatient physical complaint time count.</param>
        /// <param name="psychologicalEmotionalMedicationDayCount">The psychological emotional medication day count.</param>
        /// <param name="sexualContactsCount">The sexual contacts count.</param>
        /// <param name="suicideDayCount">The suicide day count.</param>
        /// <param name="unprotectedSexualContactsCount">The unprotected sexual contacts count.</param>
        /// <param name="unprotectedSexualHighSaContactsCount">The unprotected sexual high sa contacts count.</param>
        /// <param name="unprotectedSexualHivContactsCount">The unprotected sexual hiv contacts count.</param>
        /// <param name="unprotectedSexualInjectionDrugContactsCount">The unprotected sexual injection drug contacts count.</param>
        /// <param name="violentBehaviorDayCount">The violent behavior day count.</param>
        public GpraProblemsTreatmentRecoverySection(GpraNonResponseType<int?> anxietyDayCount,
                                                    GpraNonResponseType<int?> brainMisfunctionDayCount,
                                                    GpraNonResponseType<int?> depressionDayCount,
                                                    GpraNonResponseType<int?> erMentalEmotionalDifficultiesTimeCount,
                                                    bool? erPhysicalComplaintIndicator,
                                                    GpraNonResponseType<int?> erPhysicalComplaintTimeCount,
                                                    bool? erAlcoholSubstanceAbuseIndicator,
                                                    GpraNonResponseType<int?> erAlcoholSubstanceAbuseTimeCount,
                                                    bool? erMentalEmotionalDifficultiesIndicator,
                                                    GpraNonResponseType<GpraOverallHealth> gpraOverallHealth,
                                                    GpraNonResponseType<GpraPsychologicalImpact> gpraPsychologicalImpact,
                                                    GpraNonResponseType<GpraSexualActivity> gpraSexualActivity,
                                                    GpraNonResponseType<int?> hallucinationsDayCount,
                                                    GpraNonResponseType<bool?> hivTestIndicator,
                                                    bool? hivTestResultsKnownIndicator,
                                                    bool? inpatientAlcoholSubstanceAbuseIndicator,
                                                    GpraNonResponseType<int?> inpatientAlcoholSubstanceAbuseNightCount,
                                                    bool? inpatientMentalEmotionalDifficultiesIndicator,
                                                    GpraNonResponseType<int?> inpatientMentalEmotionalDifficultiesNightCount,
                                                    bool? inpatientPhysicalComplaintIndicator,
                                                    GpraNonResponseType<int?> inpatientPhysicalComplaintNightCount,
                                                    bool? outpatientAlcoholSubstanceAbuseIndicator,
                                                    GpraNonResponseType<int?> outpatientAlcoholSubstanceAbuseTimeCount,
                                                    bool? outpatientMentalEmotionalDifficultiesIndicator,
                                                    GpraNonResponseType<int?> outpatientMentalEmotionalDifficultiesTimeCount,
                                                    bool? outpatientPhysicalComplaintIndicator,
                                                    GpraNonResponseType<int?> outpatientPhysicalComplaintTimeCount,
                                                    GpraNonResponseType<int?> psychologicalEmotionalMedicationDayCount,
                                                    GpraNonResponseType<int?> sexualContactsCount,
                                                    GpraNonResponseType<int?> suicideDayCount,
                                                    GpraNonResponseType<int?> unprotectedSexualContactsCount,
                                                    GpraNonResponseType<int?> unprotectedSexualHighSaContactsCount,
                                                    GpraNonResponseType<int?> unprotectedSexualHivContactsCount,
                                                    GpraNonResponseType<int?> unprotectedSexualInjectionDrugContactsCount,
                                                    GpraNonResponseType<int?> violentBehaviorDayCount)
        {
            if (hivTestIndicator.GpraNonResponse != null && !GetPossibleGpraNonResponseWellKnownNames(() => HivTestIndicator).Contains(hivTestIndicator.GpraNonResponse.WellKnownName))
            {
                throw new ArgumentException(string.Format("NonResponse of {0} is not a valid option for Hiv Test Indicator.", hivTestIndicator.GpraNonResponse.Name), "HivTestIndicator");
            }
            _anxietyDayCount = anxietyDayCount;
            _brainMisfunctionDayCount = brainMisfunctionDayCount;
            _depressionDayCount = depressionDayCount;
            _erMentalEmotionalDifficultiesTimeCount = erMentalEmotionalDifficultiesTimeCount;
            _erPhysicalComplaintIndicator = erPhysicalComplaintIndicator;
            _erPhysicalComplaintTimeCount = erPhysicalComplaintTimeCount;
            _erAlcoholSubstanceAbuseIndicator = erAlcoholSubstanceAbuseIndicator;
            _erAlcoholSubstanceAbuseTimeCount = erAlcoholSubstanceAbuseTimeCount;
            _erMentalEmotionalDifficultiesIndicator = erMentalEmotionalDifficultiesIndicator;
            _gpraOverallHealth = gpraOverallHealth;
            _gpraPsychologicalImpact = gpraPsychologicalImpact;
            _gpraSexualActivity = gpraSexualActivity;
            _hallucinationsDayCount = hallucinationsDayCount;
            _hivTestIndicator = hivTestIndicator;
            _hivTestResultsKnownIndicator = hivTestResultsKnownIndicator;
            _inpatientAlcoholSubstanceAbuseIndicator = inpatientAlcoholSubstanceAbuseIndicator;
            _inpatientAlcoholSubstanceAbuseNightCount = inpatientAlcoholSubstanceAbuseNightCount;
            _inpatientMentalEmotionalDifficultiesIndicator = inpatientMentalEmotionalDifficultiesIndicator;
            _inpatientMentalEmotionalDifficultiesNightCount = inpatientMentalEmotionalDifficultiesNightCount;
            _inpatientPhysicalComplaintIndicator = inpatientPhysicalComplaintIndicator;
            _inpatientPhysicalComplaintNightCount = inpatientPhysicalComplaintNightCount;
            _outpatientAlcoholSubstanceAbuseIndicator = outpatientAlcoholSubstanceAbuseIndicator;
            _outpatientAlcoholSubstanceAbuseTimeCount = outpatientAlcoholSubstanceAbuseTimeCount;
            _outpatientMentalEmotionalDifficultiesIndicator = outpatientMentalEmotionalDifficultiesIndicator;
            _outpatientMentalEmotionalDifficultiesTimeCount = outpatientMentalEmotionalDifficultiesTimeCount;
            _outpatientPhysicalComplaintIndicator = outpatientPhysicalComplaintIndicator;
            _outpatientPhysicalComplaintTimeCount = outpatientPhysicalComplaintTimeCount;
            _psychologicalEmotionalMedicationDayCount = psychologicalEmotionalMedicationDayCount;
            _sexualContactsCount = sexualContactsCount;
            _suicideDayCount = suicideDayCount;
            _unprotectedSexualContactsCount = unprotectedSexualContactsCount;
            _unprotectedSexualHighSaContactsCount = unprotectedSexualHighSaContactsCount;
            _unprotectedSexualHivContactsCount = unprotectedSexualHivContactsCount;
            _unprotectedSexualInjectionDrugContactsCount = unprotectedSexualInjectionDrugContactsCount;
            _violentBehaviorDayCount = violentBehaviorDayCount;
        }

        /// <summary>
        /// Gets the Gpra overall health.
        /// </summary>
        public virtual GpraNonResponseType<GpraOverallHealth> GpraOverallHealth
        {
            get { return _gpraOverallHealth; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating inpatient physical complaint.
        /// </summary>
        public virtual bool? InpatientPhysicalComplaintIndicator
        {
            get { return _inpatientPhysicalComplaintIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the inpatient physical complaint night count.
        /// </summary>
        public virtual GpraNonResponseType<int?> InpatientPhysicalComplaintNightCount
        {
            get { return _inpatientPhysicalComplaintNightCount; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating inpatient mental emotional difficulties.
        /// </summary>
        public virtual bool? InpatientMentalEmotionalDifficultiesIndicator
        {
            get { return _inpatientMentalEmotionalDifficultiesIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the inpatient mental emotional difficulties night count.
        /// </summary>
        public virtual GpraNonResponseType<int?> InpatientMentalEmotionalDifficultiesNightCount
        {
            get { return _inpatientMentalEmotionalDifficultiesNightCount; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating inpatient alcohol substance abuse.
        /// </summary>
        public virtual bool? InpatientAlcoholSubstanceAbuseIndicator
        {
            get { return _inpatientAlcoholSubstanceAbuseIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the inpatient alcohol substance abuse night count.
        /// </summary>
        public virtual GpraNonResponseType<int?> InpatientAlcoholSubstanceAbuseNightCount
        {
            get { return _inpatientAlcoholSubstanceAbuseNightCount; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating outpatient physical complaint.
        /// </summary>
        public virtual bool? OutpatientPhysicalComplaintIndicator
        {
            get { return _outpatientPhysicalComplaintIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the outpatient physical complaint time count.
        /// </summary>
        public virtual GpraNonResponseType<int?> OutpatientPhysicalComplaintTimeCount
        {
            get { return _outpatientPhysicalComplaintTimeCount; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating outpatient mental emotional difficulties.
        /// </summary>
        public virtual bool? OutpatientMentalEmotionalDifficultiesIndicator
        {
            get { return _outpatientMentalEmotionalDifficultiesIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the outpatient mental emotional difficulties time count.
        /// </summary>
        public virtual GpraNonResponseType<int?> OutpatientMentalEmotionalDifficultiesTimeCount
        {
            get { return _outpatientMentalEmotionalDifficultiesTimeCount; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating outpatient alcohol substance abuse.
        /// </summary>
        public virtual bool? OutpatientAlcoholSubstanceAbuseIndicator
        {
            get { return _outpatientAlcoholSubstanceAbuseIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the outpatient alcohol substance abuse time count.
        /// </summary>
        public virtual GpraNonResponseType<int?> OutpatientAlcoholSubstanceAbuseTimeCount
        {
            get { return _outpatientAlcoholSubstanceAbuseTimeCount; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating ER physical complaint.
        /// </summary>
        public virtual bool? ErPhysicalComplaintIndicator
        {
            get { return _erPhysicalComplaintIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the ER physical complaint time count.
        /// </summary>
        public virtual GpraNonResponseType<int?> ErPhysicalComplaintTimeCount
        {
            get { return _erPhysicalComplaintTimeCount; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating ERmental emotional difficulties.
        /// </summary>
        public virtual bool? ErMentalEmotionalDifficultiesIndicator
        {
            get { return _erMentalEmotionalDifficultiesIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the ER mental emotional difficulties time count.
        /// </summary>
        public virtual GpraNonResponseType<int?> ErMentalEmotionalDifficultiesTimeCount
        {
            get { return _erMentalEmotionalDifficultiesTimeCount; }
            private set { }
        }

        /// <summary>
        /// Gets the ER alcohol substance abuse indicator.
        /// </summary>
        public virtual bool? ErAlcoholSubstanceAbuseIndicator
        {
            get { return _erAlcoholSubstanceAbuseIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the ER alcohol substance abuse time count.
        /// </summary>
        public virtual GpraNonResponseType<int?> ErAlcoholSubstanceAbuseTimeCount
        {
            get { return _erAlcoholSubstanceAbuseTimeCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraSexualActivity">GpraSexualActivity</see>
        /// denoting patient sexual activity.
        /// </summary>
        public virtual GpraNonResponseType<GpraSexualActivity> GpraSexualActivity
        {
            get { return _gpraSexualActivity; }
            private set { }
        }

        /// <summary>
        /// Gets the sexual contacts count.
        /// </summary>
        public virtual GpraNonResponseType<int?> SexualContactsCount
        {
            get { return _sexualContactsCount; }
            private set { }
        }

        /// <summary>
        /// Gets the unprotected sexual contacts count.
        /// </summary>
        public virtual GpraNonResponseType<int?> UnprotectedSexualContactsCount
        {
            get { return _unprotectedSexualContactsCount; }
            private set { }
        }

        /// <summary>
        /// Gets the unprotected sexual HIV contacts count.
        /// </summary>
        public virtual GpraNonResponseType<int?> UnprotectedSexualHivContactsCount
        {
            get { return _unprotectedSexualHivContactsCount; }
            private set { }
        }

        /// <summary>
        /// Gets the unprotected sex with injection drug contacts count.
        /// </summary>
        public virtual GpraNonResponseType<int?> UnprotectedSexualInjectionDrugContactsCount
        {
            get { return _unprotectedSexualInjectionDrugContactsCount; }
            private set { }
        }

        /// <summary>
        /// Gets the unprotected sexual high SA contacts count.
        /// </summary>
        public virtual GpraNonResponseType<int?> UnprotectedSexualHighSaContactsCount
        {
            get { return _unprotectedSexualHighSaContactsCount; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating HIV test.
        /// </summary>
        public virtual GpraNonResponseType<bool?> HivTestIndicator
        {
            get { return _hivTestIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating HIV test results known.
        /// </summary>
        public virtual bool? HivTestResultsKnownIndicator
        {
            get { return _hivTestResultsKnownIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the depression day count.
        /// </summary>
        public virtual GpraNonResponseType<int?> DepressionDayCount
        {
            get { return _depressionDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the anxiety day count.
        /// </summary>
        public virtual GpraNonResponseType<int?> AnxietyDayCount
        {
            get { return _anxietyDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the hallucinations day count.
        /// </summary>
        public virtual GpraNonResponseType<int?> HallucinationsDayCount
        {
            get { return _hallucinationsDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the brain misfunction day count.
        /// </summary>
        public virtual GpraNonResponseType<int?> BrainMisfunctionDayCount
        {
            get { return _brainMisfunctionDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the violent behavior day count.
        /// </summary>
        public virtual GpraNonResponseType<int?> ViolentBehaviorDayCount
        {
            get { return _violentBehaviorDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the suicide day count.
        /// </summary>
        public virtual GpraNonResponseType<int?> SuicideDayCount
        {
            get { return _suicideDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the psychological emotional medication day count.
        /// </summary>
        public virtual GpraNonResponseType<int?> PsychologicalEmotionalMedicationDayCount
        {
            get { return _psychologicalEmotionalMedicationDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraPsychologicalImpact">GpraPsychologicalImpact</see>
        /// denoting psychological impact.
        /// </summary>
        public virtual GpraNonResponseType<GpraPsychologicalImpact> GpraPsychologicalImpact
        {
            get { return _gpraPsychologicalImpact; }
            private set { }
        }

        /// <summary>
        /// Gets the possible Gpra non response well known names for this interview section.
        /// <remarks>NotAnswered is included in this base class because it is used in most Nonresponse lists.</remarks>
        /// </summary>
        /// <typeparam name="TProperty">The property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1">
        /// IEnumerable&lt;string&gt;</see> list of WellKNownNames.
        /// </returns>
        public override IEnumerable<string> GetPossibleGpraNonResponseWellKnownNames<TProperty> ( Expression<Func<TProperty>> propertyExpression )
        {
            IEnumerable<string> possibleGpraNonResponseWellKnownNames;
            string propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );

            if ( propertyName == PropertyUtil.ExtractPropertyName ( () => HivTestIndicator ) )
            {
                possibleGpraNonResponseWellKnownNames = new List<string>
                    {
                        WellKnownNames.GpraModule.GpraNonResponse.Refused,
                        WellKnownNames.GpraModule.GpraNonResponse.DontKnow
                    };
            }
            else
            {
                possibleGpraNonResponseWellKnownNames = base.GetPossibleGpraNonResponseWellKnownNames ( propertyExpression );
            }

            return possibleGpraNonResponseWellKnownNames;
        }


        /// <summary>
        /// Gets the well known names filters dictionary.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IDictionary`2">Dictionary&lt;string,
        /// IEnumerable&lt;string&gt;</see>
        /// </returns>
        public override Dictionary<string, IEnumerable<string>> GetFiltersDictionary ()
        {
            var filters = new Dictionary<string, IEnumerable<string>>
                {
                    { PropertyUtil.ExtractPropertyName ( () => HivTestIndicator ), GetPossibleGpraNonResponseWellKnownNames ( () => HivTestIndicator ) }
                };
            return filters;
        }
    }
}