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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <summary>
    /// This class defines a data transfer object for TedsAdmissionInterview.
    /// </summary>
    public partial class TedsAdmissionInterviewDto : ActivityDto
    {
        private bool? _coDependentIndicator;
        private TedsAnswerDto<int?> _priorTreatmentEpisodesCount;
        private TedsAnswerDto<LookupValueDto> _tedsGenderInformationTedsGender;
        private TedsAnswerDto<bool?> _tedsGenderInformationPregnantIndicator;
        private TedsAnswerDto<LookupValueDto> _tedsRace;
        private TedsAnswerDto<LookupValueDto> _tedsEthnicity;
        private TedsAnswerDto<int?> _tedsEducationYearCount;

        private TedsAnswerDto<LookupValueDto> _tedsEmploymentStatusInformationTedsEmploymentStatus;
        private TedsAnswerDto<LookupValueDto> _tedsEmploymentStatusInformationDetailedNotInLaborForce;

        private TedsAnswerDto<LookupValueDto> _primarySubstanceProblemType;
        private TedsAnswerDto<LookupValueDto> _primaryUseFrequencyType;

        private TedsAnswerDto<LookupValueDto> _primaryUsualAdministrationRouteType;
        private TedsAnswerDto<int?> _primaryFirstUseAge;
        private TedsAnswerDto<DetailedDrugCodeDto> _primaryDetailedDrugCode;

        private TedsAnswerDto<LookupValueDto> _secondarySubstanceProblemType;
        private TedsAnswerDto<LookupValueDto> _secondaryUseFrequencyType;

        private TedsAnswerDto<LookupValueDto> _secondaryUsualAdministrationRouteType;
        private TedsAnswerDto<int?> _secondaryFirstUseAge;
        private TedsAnswerDto<DetailedDrugCodeDto> _secondaryDetailedDrugCode;

        private TedsAnswerDto<LookupValueDto> _tertiarySubstanceProblemType;
        private TedsAnswerDto<LookupValueDto> _tertiaryUseFrequencyType;

        private TedsAnswerDto<LookupValueDto> _tertiaryUsualAdministrationRouteType;
        private TedsAnswerDto<int?> _tertiaryFirstUseAge;
        private TedsAnswerDto<DetailedDrugCodeDto> _tertiaryDetailedDrugCode;

        private TedsAnswerDto<bool?> _medicationAssistedOpioidTherapyIndicator;

        private TedsAnswerDto<string> _dsmDiagnosis;

        private TedsAnswerDto<bool?> _otherPsychiatricProblemIndicator;
        private TedsAnswerDto<bool?> _veteranStatusIndicator;
        private TedsAnswerDto<LookupValueDto> _livingArrangementsType;
        private TedsAnswerDto<LookupValueDto> _incomeSourceType;
        private TedsAnswerDto<LookupValueDto> _primaryPaymentSourceType;
        private TedsAnswerDto<LookupValueDto> _maritalStatus;
        private TedsAnswerDto<int?> _arrestsInPastThirtyDaysCount;
        private TedsAnswerDto<LookupValueDto> _participatedSelfHelpGroupInPastThirtyDaysType;

        /// <summary>
        /// Gets or sets a value indicating whether [co dependent indicator].
        /// </summary>
        /// <value>
        /// <c>true</c> If [co dependent indicator]; otherwise, <c>false</c>.
        /// </value>
        public bool? CoDependentIndicator
        {
            get { return _coDependentIndicator; }
            set { ApplyPropertyChange(ref _coDependentIndicator, () => CoDependentIndicator, value); }
        }

        /// <summary>
        /// Gets or sets the prior treatment episodes count.
        /// </summary>
        /// <value>
        /// The prior treatment episodes count.
        /// </value>
        public TedsAnswerDto<int?> PriorTreatmentEpisodesCount
        {
            get { return _priorTreatmentEpisodesCount; }
            set { ApplyPropertyChange(ref _priorTreatmentEpisodesCount, () => PriorTreatmentEpisodesCount, value); }
        }

        /// <summary>
        /// Gets or sets the teds gender information teds gender.
        /// </summary>
        /// <value>
        /// The teds gender information teds gender.
        /// </value>
        public TedsAnswerDto<LookupValueDto> TedsGenderInformationTedsGender
        {
            get { return _tedsGenderInformationTedsGender; }
            set { ApplyPropertyChange(ref _tedsGenderInformationTedsGender, () => TedsGenderInformationTedsGender, value); }
        }

        /// <summary>
        /// Gets or sets the teds gender information pregnant indicator.
        /// </summary>
        /// <value>
        /// The teds gender information pregnant indicator.
        /// </value>
        public TedsAnswerDto<bool?> TedsGenderInformationPregnantIndicator
        {
            get { return _tedsGenderInformationPregnantIndicator; }
            set { ApplyPropertyChange(ref _tedsGenderInformationPregnantIndicator, () => TedsGenderInformationPregnantIndicator, value); }
        }

        /// <summary>
        /// Gets or sets the teds race.
        /// </summary>
        /// <value>
        /// The teds race.
        /// </value>
        public TedsAnswerDto<LookupValueDto> TedsRace
        {
            get { return _tedsRace; }
            set { ApplyPropertyChange(ref _tedsRace, () => TedsRace, value); }
        }

        /// <summary>
        /// Gets or sets the teds ethnicity.
        /// </summary>
        /// <value>
        /// The teds ethnicity.
        /// </value>
        public TedsAnswerDto<LookupValueDto> TedsEthnicity
        {
            get { return _tedsEthnicity; }
            set { ApplyPropertyChange(ref _tedsEthnicity, () => TedsEthnicity, value); }
        }

        /// <summary>
        /// Gets or sets the teds education year count.
        /// </summary>
        /// <value>
        /// The teds education year count.
        /// </value>
        public TedsAnswerDto<int?> TedsEducationYearCount
        {
            get { return _tedsEducationYearCount; }
            set { ApplyPropertyChange(ref _tedsEducationYearCount, () => TedsEducationYearCount, value); }
        }

        /// <summary>
        /// Gets or sets the teds employment status information teds employment status.
        /// </summary>
        /// <value>
        /// The teds employment status information teds employment status.
        /// </value>
        public TedsAnswerDto<LookupValueDto> TedsEmploymentStatusInformationTedsEmploymentStatus
        {
            get { return _tedsEmploymentStatusInformationTedsEmploymentStatus; }
            set { ApplyPropertyChange(ref _tedsEmploymentStatusInformationTedsEmploymentStatus, () => TedsEmploymentStatusInformationTedsEmploymentStatus, value); }
        }

        /// <summary>
        /// Gets or sets the teds employment status information detailed not in labor force.
        /// </summary>
        /// <value>
        /// The teds employment status information detailed not in labor force.
        /// </value>
        public TedsAnswerDto<LookupValueDto> TedsEmploymentStatusInformationDetailedNotInLaborForce
        {
            get { return _tedsEmploymentStatusInformationDetailedNotInLaborForce; }
            set { ApplyPropertyChange(ref _tedsEmploymentStatusInformationDetailedNotInLaborForce, () => TedsEmploymentStatusInformationDetailedNotInLaborForce, value); }
        }

        /// <summary>
        /// Gets or sets the type of the primary substance problem.
        /// </summary>
        /// <value>
        /// The type of the primary substance problem.
        /// </value>
        public TedsAnswerDto<LookupValueDto> PrimarySubstanceProblemType
        {
            get { return _primarySubstanceProblemType; }
            set { ApplyPropertyChange(ref _primarySubstanceProblemType, () => PrimarySubstanceProblemType, value); }
        }

        /// <summary>
        /// Gets or sets the type of the primary use frequency.
        /// </summary>
        /// <value>
        /// The type of the primary use frequency.
        /// </value>
        public TedsAnswerDto<LookupValueDto> PrimaryUseFrequencyType
        {
            get { return _primaryUseFrequencyType; }
            set { ApplyPropertyChange(ref _primaryUseFrequencyType, () => PrimaryUseFrequencyType, value); }
        }

        /// <summary>
        /// Gets or sets the type of the primary usual administration route.
        /// </summary>
        /// <value>
        /// The type of the primary usual administration route.
        /// </value>
        public TedsAnswerDto<LookupValueDto> PrimaryUsualAdministrationRouteType
        {
            get { return _primaryUsualAdministrationRouteType; }
            set { ApplyPropertyChange(ref _primaryUsualAdministrationRouteType, () => PrimaryUsualAdministrationRouteType, value); }
        }

        /// <summary>
        /// Gets or sets the primary first use age.
        /// </summary>
        /// <value>
        /// The primary first use age.
        /// </value>
        public TedsAnswerDto<int?> PrimaryFirstUseAge
        {
            get { return _primaryFirstUseAge; }
            set { ApplyPropertyChange(ref _primaryFirstUseAge, () => PrimaryFirstUseAge, value); }
        }

        /// <summary>
        /// Gets or sets the primary detailed drug code.
        /// </summary>
        /// <value>
        /// The primary detailed drug code.
        /// </value>
        public TedsAnswerDto<DetailedDrugCodeDto> PrimaryDetailedDrugCode
        {
            get { return _primaryDetailedDrugCode; }
            set { ApplyPropertyChange(ref _primaryDetailedDrugCode, () => PrimaryDetailedDrugCode, value); }
        }

        /// <summary>
        /// Gets or sets the type of the secondary substance problem.
        /// </summary>
        /// <value>
        /// The type of the secondary substance problem.
        /// </value>
        public TedsAnswerDto<LookupValueDto> SecondarySubstanceProblemType
        {
            get { return _secondarySubstanceProblemType; }
            set { ApplyPropertyChange(ref _secondarySubstanceProblemType, () => SecondarySubstanceProblemType, value); }
        }

        /// <summary>
        /// Gets or sets the type of the secondary use frequency.
        /// </summary>
        /// <value>
        /// The type of the secondary use frequency.
        /// </value>
        public TedsAnswerDto<LookupValueDto> SecondaryUseFrequencyType
        {
            get { return _secondaryUseFrequencyType; }
            set { ApplyPropertyChange(ref _secondaryUseFrequencyType, () => SecondaryUseFrequencyType, value); }
        }

        /// <summary>
        /// Gets or sets the type of the secondary usual administration route.
        /// </summary>
        /// <value>
        /// The type of the secondary usual administration route.
        /// </value>
        public TedsAnswerDto<LookupValueDto> SecondaryUsualAdministrationRouteType
        {
            get { return _secondaryUsualAdministrationRouteType; }
            set { ApplyPropertyChange(ref _secondaryUsualAdministrationRouteType, () => SecondaryUsualAdministrationRouteType, value); }
        }

        /// <summary>
        /// Gets or sets the secondary first use age.
        /// </summary>
        /// <value>
        /// The secondary first use age.
        /// </value>
        public TedsAnswerDto<int?> SecondaryFirstUseAge
        {
            get { return _secondaryFirstUseAge; }
            set { ApplyPropertyChange(ref _secondaryFirstUseAge, () => SecondaryFirstUseAge, value); }
        }

        /// <summary>
        /// Gets or sets the secondary detailed drug code.
        /// </summary>
        /// <value>
        /// The secondary detailed drug code.
        /// </value>
        public TedsAnswerDto<DetailedDrugCodeDto> SecondaryDetailedDrugCode
        {
            get { return _secondaryDetailedDrugCode; }
            set { ApplyPropertyChange(ref _secondaryDetailedDrugCode, () => SecondaryDetailedDrugCode, value); }
        }

        /// <summary>
        /// Gets or sets the type of the tertiary substance problem.
        /// </summary>
        /// <value>
        /// The type of the tertiary substance problem.
        /// </value>
        public TedsAnswerDto<LookupValueDto> TertiarySubstanceProblemType
        {
            get { return _tertiarySubstanceProblemType; }
            set { ApplyPropertyChange(ref _tertiarySubstanceProblemType, () => TertiarySubstanceProblemType, value); }
        }

        /// <summary>
        /// Gets or sets the type of the tertiary use frequency.
        /// </summary>
        /// <value>
        /// The type of the tertiary use frequency.
        /// </value>
        public TedsAnswerDto<LookupValueDto> TertiaryUseFrequencyType
        {
            get { return _tertiaryUseFrequencyType; }
            set { ApplyPropertyChange(ref _tertiaryUseFrequencyType, () => TertiaryUseFrequencyType, value); }
        }

        /// <summary>
        /// Gets or sets the type of the tertiary usual administration route.
        /// </summary>
        /// <value>
        /// The type of the tertiary usual administration route.
        /// </value>
        public TedsAnswerDto<LookupValueDto> TertiaryUsualAdministrationRouteType
        {
            get { return _tertiaryUsualAdministrationRouteType; }
            set { ApplyPropertyChange(ref _tertiaryUsualAdministrationRouteType, () => TertiaryUsualAdministrationRouteType, value); }
        }

        /// <summary>
        /// Gets or sets the tertiary first use age.
        /// </summary>
        /// <value>
        /// The tertiary first use age.
        /// </value>
        public TedsAnswerDto<int?> TertiaryFirstUseAge
        {
            get { return _tertiaryFirstUseAge; }
            set { ApplyPropertyChange(ref _tertiaryFirstUseAge, () => TertiaryFirstUseAge, value); }
        }

        /// <summary>
        /// Gets or sets the tertiary detailed drug code.
        /// </summary>
        /// <value>
        /// The tertiary detailed drug code.
        /// </value>
        public TedsAnswerDto<DetailedDrugCodeDto> TertiaryDetailedDrugCode
        {
            get { return _tertiaryDetailedDrugCode; }
            set { ApplyPropertyChange(ref _tertiaryDetailedDrugCode, () => TertiaryDetailedDrugCode, value); }
        }

        /// <summary>
        /// Gets or sets the medication assisted opioid therapy indicator.
        /// </summary>
        /// <value>
        /// The medication assisted opioid therapy indicator.
        /// </value>
        public TedsAnswerDto<bool?> MedicationAssistedOpioidTherapyIndicator
        {
            get { return _medicationAssistedOpioidTherapyIndicator; }
            set { ApplyPropertyChange(ref _medicationAssistedOpioidTherapyIndicator, () => MedicationAssistedOpioidTherapyIndicator, value); }
        }

        /// <summary>
        /// Gets or sets the DSM diagnosis.
        /// </summary>
        /// <value>
        /// The DSM diagnosis.
        /// </value>
        public TedsAnswerDto<string> DsmDiagnosis
        {
            get { return _dsmDiagnosis; }
            set { ApplyPropertyChange(ref _dsmDiagnosis, () => DsmDiagnosis, value); }
        }

        /// <summary>
        /// Gets or sets the other psychiatric problem indicator.
        /// </summary>
        /// <value>
        /// The other psychiatric problem indicator.
        /// </value>
        public TedsAnswerDto<bool?> OtherPsychiatricProblemIndicator
        {
            get { return _otherPsychiatricProblemIndicator; }
            set { ApplyPropertyChange(ref _otherPsychiatricProblemIndicator, () => OtherPsychiatricProblemIndicator, value); }
        }

        /// <summary>
        /// Gets or sets the veteran status indicator.
        /// </summary>
        /// <value>
        /// The veteran status indicator.
        /// </value>
        public TedsAnswerDto<bool?> VeteranStatusIndicator 
        {
            get { return _veteranStatusIndicator; }
            set { ApplyPropertyChange(ref _veteranStatusIndicator, () => VeteranStatusIndicator, value); }
        }

        /// <summary>
        /// Gets or sets the type of the living arrangements.
        /// </summary>
        /// <value>
        /// The type of the living arrangements.
        /// </value>
        public TedsAnswerDto<LookupValueDto> LivingArrangementsType
        {
            get { return _livingArrangementsType; }
            set { ApplyPropertyChange(ref _livingArrangementsType, () => LivingArrangementsType, value); }
        }

        /// <summary>
        /// Gets or sets the type of the income source.
        /// </summary>
        /// <value>
        /// The type of the income source.
        /// </value>
        public TedsAnswerDto<LookupValueDto> IncomeSourceType
        {
            get { return _incomeSourceType; }
            set { ApplyPropertyChange(ref _incomeSourceType, () => IncomeSourceType, value); }
        }

        /// <summary>
        /// Gets or sets the type of the primary payment source.
        /// </summary>
        /// <value>
        /// The type of the primary payment source.
        /// </value>
        public TedsAnswerDto<LookupValueDto> PrimaryPaymentSourceType
        {
            get { return _primaryPaymentSourceType; }
            set { ApplyPropertyChange(ref _primaryPaymentSourceType, () => PrimaryPaymentSourceType, value); }
        }

        /// <summary>
        /// Gets or sets the marital status.
        /// </summary>
        /// <value>
        /// The marital status.
        /// </value>
        public TedsAnswerDto<LookupValueDto> MaritalStatus
        {
            get { return _maritalStatus; }
            set { ApplyPropertyChange(ref _maritalStatus, () => MaritalStatus, value); }
        }

        /// <summary>
        /// Gets or sets the arrests in past thirty days count.
        /// </summary>
        /// <value>
        /// The arrests in past thirty days count.
        /// </value>
        public TedsAnswerDto<int?> ArrestsInPastThirtyDaysCount
        {
            get { return _arrestsInPastThirtyDaysCount; }
            set { ApplyPropertyChange(ref _arrestsInPastThirtyDaysCount, () => ArrestsInPastThirtyDaysCount, value); }
        }

        /// <summary>
        /// Gets or sets the type of the participated self help group in past thirty days.
        /// </summary>
        /// <value>
        /// The type of the participated self help group in past thirty days.
        /// </value>
        public TedsAnswerDto<LookupValueDto> ParticipatedSelfHelpGroupInPastThirtyDaysType
        {
            get { return _participatedSelfHelpGroupInPastThirtyDaysType; }
            set { ApplyPropertyChange(ref _participatedSelfHelpGroupInPastThirtyDaysType, () => ParticipatedSelfHelpGroupInPastThirtyDaysType, value); }
        }

        /// <summary>
        /// Gets or sets the default non response lookup well known names.
        /// </summary>
        /// <value>
        /// The default non response lookup well known names.
        /// </value>
        public IEnumerable<string> DefaultNonResponseLookupWellKnownNames { get; set; }

        #region Public Methods

        /// <summary>
        /// Applies the property change.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="value">The value.</param>
        public override void ApplyPropertyChange<TProperty, TField> ( ref TField field, Expression<Func<TProperty>> propertyExpression, TField value )
        {
            CheckHandleChildPropertyChanges ( ref field, propertyExpression, value );
            base.ApplyPropertyChange ( ref field, propertyExpression, value );
        }

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="value">The value.</param>
        public override void RaisePropertyChanged<TProperty, TField> (
            ref TField field, Expression<Func<TProperty>> propertyExpression, TField value )
        {
            CheckHandleChildPropertyChanges ( ref field, propertyExpression, value );
            base.RaisePropertyChanged ( ref field, propertyExpression, value );
        }

        #endregion

        #region Methods

        private void CheckHandleChildPropertyChanges<TProperty, TField> (
            ref TField field, Expression<Func<TProperty>> propertyExpression, TField value )
        {
            if ( !Equals ( field, value ) )
            {
                var type = typeof( TField );
                if ( type.IsGenericType && type.GetGenericTypeDefinition () == typeof( TedsAnswerDto<> ) )
                {
                    if ( value != null )
                    {
                        ( value as INotifyPropertyChanged ).PropertyChanged += ( s, e ) => RaisePropertyChanged ( propertyExpression );
                    }
                }
            }
        }

        #endregion
    }
}
