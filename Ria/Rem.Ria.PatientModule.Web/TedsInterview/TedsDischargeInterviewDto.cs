using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <summary>
    /// Data transfer object for TedsDischargeInterview class.
    /// </summary>
    [DataContract]
    public partial class TedsDischargeInterviewDto : ActivityDto
    {
        private long _tedsAdmissionInterviewKey;
        private DateTime? _lastFaceToFaceContactDate;
        private TedsAnswerDto<TedsLookupBaseDto> _tedsDischargeReason;
        private TedsAnswerDto<SubstanceProblemTypeDto> _primarySubstanceProblemType;
        private TedsAnswerDto<TedsLookupBaseDto> _primaryUseFrequencyType;
        private TedsAnswerDto<SubstanceProblemTypeDto> _secondarySubstanceProblemType;
        private TedsAnswerDto<TedsLookupBaseDto> _secondaryUseFrequencyType;
        private TedsAnswerDto<SubstanceProblemTypeDto> _tertiarySubstanceProblemType;
        private TedsAnswerDto<TedsLookupBaseDto> _tertiaryUseFrequencyType;
        private TedsAnswerDto<TedsLookupBaseDto> _livingArrangementsType;
        private TedsAnswerDto<TedsLookupBaseDto> _tedsEmploymentStatus;
        private TedsAnswerDto<TedsLookupBaseDto> _detailedNotInLaborForce;
        private TedsAnswerDto<int?> _arrestsInPastThirtyDaysCount;
        private TedsAnswerDto<TedsLookupBaseDto> _participatedSelfHelpGroupInPastThirtyDaysType;

        /// <summary>
        /// Gets or sets the teds admission interview key.
        /// </summary>
        /// <value>The teds admission interview key.</value>
        [DataMember]
        public long TedsAdmissionInterviewKey
        {
            get { return _tedsAdmissionInterviewKey; }
            set { ApplyPropertyChange(ref _tedsAdmissionInterviewKey, () => TedsAdmissionInterviewKey, value); }
        }

        /// <summary>
        /// Gets or sets the last face to face contact date.
        /// </summary>
        /// <value>The last face to face contact date.</value>
        [DataMember]
        public DateTime? LastFaceToFaceContactDate
        {
            get { return _lastFaceToFaceContactDate; }
            set { ApplyPropertyChange(ref _lastFaceToFaceContactDate, () => LastFaceToFaceContactDate, value); }
        }

        /// <summary>
        /// Gets or sets the teds discharge reason.
        /// </summary>
        /// <value>The teds discharge reason.</value>
        [DataMember]
        public TedsAnswerDto<TedsLookupBaseDto> TedsDischargeReason
        {
            get { return _tedsDischargeReason; }
            set { ApplyPropertyChange(ref _tedsDischargeReason, () => TedsDischargeReason, value); }
        }

        /// <summary>
        /// Gets or sets the type of the primary substance problem.
        /// </summary>
        /// <value>The type of the primary substance problem.</value>
        [DataMember]
        public TedsAnswerDto<SubstanceProblemTypeDto> PrimarySubstanceProblemType
        {
            get { return _primarySubstanceProblemType; }
            set { ApplyPropertyChange(ref _primarySubstanceProblemType, () => PrimarySubstanceProblemType, value); }
        }

        /// <summary>
        /// Gets or sets the type of the primary use frequency.
        /// </summary>
        /// <value>The type of the primary use frequency.</value>
        [DataMember]
        public TedsAnswerDto<TedsLookupBaseDto> PrimaryUseFrequencyType
        {
            get { return _primaryUseFrequencyType; }
            set { ApplyPropertyChange(ref _primaryUseFrequencyType, () => PrimaryUseFrequencyType, value); }
        }

        /// <summary>
        /// Gets or sets the type of the secondary substance problem.
        /// </summary>
        /// <value>The type of the secondary substance problem.</value>
        [DataMember]
        public TedsAnswerDto<SubstanceProblemTypeDto> SecondarySubstanceProblemType
        {
            get { return _secondarySubstanceProblemType; }
            set { ApplyPropertyChange(ref _secondarySubstanceProblemType, () => SecondarySubstanceProblemType, value); }
        }

        /// <summary>
        /// Gets or sets the type of the secondary use frequency.
        /// </summary>
        /// <value>The type of the secondary use frequency.</value>
        [DataMember]
        public TedsAnswerDto<TedsLookupBaseDto> SecondaryUseFrequencyType
        {
            get { return _secondaryUseFrequencyType; }
            set { ApplyPropertyChange(ref _secondaryUseFrequencyType, () => SecondaryUseFrequencyType, value); }
        }

        /// <summary>
        /// Gets or sets the type of the tertiary substance problem.
        /// </summary>
        /// <value>The type of the tertiary substance problem.</value>
        [DataMember]
        public TedsAnswerDto<SubstanceProblemTypeDto> TertiarySubstanceProblemType
        {
            get { return _tertiarySubstanceProblemType; }
            set { ApplyPropertyChange(ref _tertiarySubstanceProblemType, () => TertiarySubstanceProblemType, value); }
        }

        /// <summary>
        /// Gets or sets the type of the tertiary use frequency.
        /// </summary>
        /// <value>The type of the tertiary use frequency.</value>
        [DataMember]
        public TedsAnswerDto<TedsLookupBaseDto> TertiaryUseFrequencyType
        {
            get { return _tertiaryUseFrequencyType; }
            set { ApplyPropertyChange(ref _tertiaryUseFrequencyType, () => TertiaryUseFrequencyType, value); }
        }

        /// <summary>
        /// Gets or sets the type of the living arrangements.
        /// </summary>
        /// <value>The type of the living arrangements.</value>
        [DataMember]
        public TedsAnswerDto<TedsLookupBaseDto> LivingArrangementsType
        {
            get { return _livingArrangementsType; }
            set { ApplyPropertyChange(ref _livingArrangementsType, () => LivingArrangementsType, value); }
        }

        /// <summary>
        /// Gets or sets the teds employment status.
        /// </summary>
        /// <value>The teds employment status.</value>
        [DataMember]
        public TedsAnswerDto<TedsLookupBaseDto> TedsEmploymentStatus
        {
            get { return _tedsEmploymentStatus; }
            set { ApplyPropertyChange(ref _tedsEmploymentStatus, () => TedsEmploymentStatus, value); }
        }

        /// <summary>
        /// Gets or sets the detailed not in labor force.
        /// </summary>
        /// <value>The detailed not in labor force.</value>
        [DataMember]
        public TedsAnswerDto<TedsLookupBaseDto> DetailedNotInLaborForce
        {
            get { return _detailedNotInLaborForce; }
            set { ApplyPropertyChange(ref _detailedNotInLaborForce, () => DetailedNotInLaborForce, value); }
        }

        /// <summary>
        /// Gets or sets the arrests in past thirty days count.
        /// </summary>
        /// <value>The arrests in past thirty days count.</value>
        [DataMember]
        public TedsAnswerDto<int?> ArrestsInPastThirtyDaysCount
        {
            get { return _arrestsInPastThirtyDaysCount; }
            set { ApplyPropertyChange(ref _arrestsInPastThirtyDaysCount, () => ArrestsInPastThirtyDaysCount, value); }
        }

        /// <summary>
        /// Gets or sets the type of the participated self help group in past thirty days.
        /// </summary>
        /// <value>The type of the participated self help group in past thirty days.</value>
        [DataMember]
        public TedsAnswerDto<TedsLookupBaseDto> ParticipatedSelfHelpGroupInPastThirtyDaysType
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
        [DataMember]
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
        public override void ApplyPropertyChange<TProperty, TField>(ref TField field, Expression<Func<TProperty>> propertyExpression, TField value)
        {
            CheckHandleChildPropertyChanges(ref field, propertyExpression, value);
            base.ApplyPropertyChange(ref field, propertyExpression, value);
        }

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="value">The value.</param>
        public override void RaisePropertyChanged<TProperty, TField>(
            ref TField field, Expression<Func<TProperty>> propertyExpression, TField value)
        {
            CheckHandleChildPropertyChanges(ref field, propertyExpression, value);
            base.RaisePropertyChanged(ref field, propertyExpression, value);
        }

        #endregion

        #region Methods

        private void CheckHandleChildPropertyChanges<TProperty, TField>(
            ref TField field, Expression<Func<TProperty>> propertyExpression, TField value)
        {
            if (!Equals(field, value))
            {
                var type = typeof(TField);
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(TedsAnswerDto<>))
                {
                    if (value != null)
                    {
                        (value as INotifyPropertyChanged).PropertyChanged += (s, e) => RaisePropertyChanged(propertyExpression);
                    }
                }
            }
        }

        #endregion
    }
}
