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
using System.Linq;
using System.Linq.Expressions;
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// TedsDischargeInterview is an <see cref="Activity">Activity</see> that defines 
    /// TEDS discharge data with national outcomes measures (NOMS).
    /// </summary>
    /// <remarks>
    /// TEDS stands for Treatment Episode Data Set.
    /// </remarks> 
    public class TedsDischargeInterview : Activity
    {
        private readonly IList<TedsDischargeInterviewSubstanceUsage> _substanceUsages;

        #region Constructors
       
        /// <summary>
        /// Initializes a new instance of the <see cref="TedsDischargeInterview"/> class.
        /// </summary>
        protected internal TedsDischargeInterview ()
        {
            _substanceUsages = new List<TedsDischargeInterviewSubstanceUsage> ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsDischargeInterview"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected internal TedsDischargeInterview(
            Visit visit,
            ActivityType activityType
            )
            : base(visit, activityType)
        {
            _substanceUsages = new List<TedsDischargeInterviewSubstanceUsage>();
        } 
        #endregion
        
        #region Properties
       
        /// <summary>
        /// Gets the teds admission interview.
        /// </summary>
        public virtual TedsAdmissionInterview TedsAdmissionInterview { get; private set; }
     
        /// <summary>
        /// Gets the last face to face contact date.
        /// </summary>
        public virtual DateTime? LastFaceToFaceContactDate { get; private set; }
       
        /// <summary>
        /// Gets the discharge reason.
        /// </summary>
        public virtual TedsAnswer<TedsDischargeReason> TedsDischargeReason { get; private set; }

        /// <summary>
        /// Gets the primary substance problem and frequency.
        /// </summary>
        [NoneCascading]
        public virtual TedsDischargeInterviewSubstanceUsage PrimaryTedsDischargeInterviewSubstanceUsage { get; private set; }

        /// <summary>
        /// Gets the secondary substance problem and frequency.
        /// </summary>
        [NoneCascading]
        public virtual TedsDischargeInterviewSubstanceUsage SecondaryTedsDischargeInterviewSubstanceUsage { get; private set; }

        /// <summary>
        /// Gets the tertiary substance problem and frequency.
        /// </summary>
        [NoneCascading]
        public virtual TedsDischargeInterviewSubstanceUsage TertiaryTedsDischargeInterviewSubstanceUsage { get; private set; }

        /// <summary>
        /// Gets the living arrangements.
        /// </summary>
        public virtual TedsAnswer<LivingArrangementsType> LivingArrangementsType { get; private set; }

        /// <summary>
        /// Gets the teds employment status information.
        /// </summary>
        public virtual TedsEmploymentStatusInformation TedsEmploymentStatusInformation { get; private set; }

        /// <summary>
        /// Gets the number of arrests in thirty days.
        /// </summary>
        public virtual TedsAnswer<int?> ArrestsInPastThirtyDaysCount { get; private set; }

        /// <summary>
        /// Gets the participated in self help group in past thirty days count.
        /// </summary>
        public virtual TedsAnswer<ParticipatedSelfHelpGroupInPastThirtyDaysType> ParticipatedSelfHelpGroupInPastThirtyDaysType { get; private set; }

        /// <summary>
        /// Gets the substance usages.
        /// </summary>
        public virtual IEnumerable<TedsDischargeInterviewSubstanceUsage> SubstanceUsages
        {
            get { return _substanceUsages.ToList().AsReadOnly(); }
            private set { }
        }

        #endregion

        #region Public Mehtods

        /// <summary>
        /// Revises the teds admission interview.
        /// </summary>
        /// <param name="tedsAdmissionInterview">The teds admission interview.</param>
        public virtual void ReviseTedsAdmissionInterview (TedsAdmissionInterview  tedsAdmissionInterview)
        {
            Check.IsNotNull ( tedsAdmissionInterview, () => TedsAdmissionInterview );
            TedsAdmissionInterview = tedsAdmissionInterview; 
        }

        
        /// <summary>
        /// Revises the last contact date.
        /// </summary>
        /// <param name="lastContactDate">The last contact date.</param>
        public virtual void ReviseLastFaceToFaceContactDate(DateTime? lastContactDate)
        {
            Check.IsNotNull(lastContactDate, () => LastFaceToFaceContactDate);

            DomainRuleEngine.CreateRuleEngine ( this, "ReviseLastFaceToFaceContactDate" )
                .WithContext(lastContactDate)
                .Execute (
                    () =>
                        {
                            LastFaceToFaceContactDate = lastContactDate;
                        } );
        }


        /// <summary>
        /// Revises the teds discharge reason.
        /// </summary>
        /// <param name="tedsDischargeReason">The teds discharge reason.</param>
        public virtual void ReviseTedsDischargeReason (TedsAnswer<TedsDischargeReason> tedsDischargeReason)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(tedsDischargeReason, () => TedsDischargeReason, "Discharge reason");
            TedsDischargeReason = tedsDischargeReason;
        }

        /// <summary>
        /// Revises the teds discharge interview substance usages.
        /// </summary>
        /// <param name="primaryTedsDischargeInterviewSubstanceUsage">The primary teds discharge interview substance usage.</param>
        /// <param name="secondaryTedsDischargeInterviewSubstanceUsage">The secondary teds discharge interview substance usage.</param>
        /// <param name="tertiaryTedsDischargeInterviewSubstanceUsage">The tertiary teds discharge interview substance usage.</param>
        public virtual void ReviseTedsDischargeInterviewSubstanceUsages(TedsDischargeInterviewSubstanceUsage primaryTedsDischargeInterviewSubstanceUsage, TedsDischargeInterviewSubstanceUsage secondaryTedsDischargeInterviewSubstanceUsage, TedsDischargeInterviewSubstanceUsage tertiaryTedsDischargeInterviewSubstanceUsage)
        {
            if (tertiaryTedsDischargeInterviewSubstanceUsage != null)
            {
                if (primaryTedsDischargeInterviewSubstanceUsage == null || secondaryTedsDischargeInterviewSubstanceUsage == null)
                {
                    throw new ArgumentException("Primary or secondary substance usage cannot be null if tertiary substance usage is not null.");
                }
            }

            if (secondaryTedsDischargeInterviewSubstanceUsage != null)
            {
                if (primaryTedsDischargeInterviewSubstanceUsage == null)
                {
                    throw new ArgumentException("Primary substance usage cannot be null if secondary substance usage is not null.");
                }
            }

            var tedsAdmissionInterviewSubstanceUsages = new List<TedsDischargeInterviewSubstanceUsage>();
            tedsAdmissionInterviewSubstanceUsages.Add(primaryTedsDischargeInterviewSubstanceUsage);
            tedsAdmissionInterviewSubstanceUsages.Add(secondaryTedsDischargeInterviewSubstanceUsage);
            tedsAdmissionInterviewSubstanceUsages.Add(tertiaryTedsDischargeInterviewSubstanceUsage);

            ReviseTedsDischargeInterviewSubstanceUsages(tedsAdmissionInterviewSubstanceUsages);
        }

        private void ReviseTedsDischargeInterviewSubstanceUsages(IList<TedsDischargeInterviewSubstanceUsage> tedsDischargeInterviewSubstanceUsages)
        {
            if ( tedsDischargeInterviewSubstanceUsages != null )
            {
                if ( tedsDischargeInterviewSubstanceUsages.Count > 3 )
                {
                    throw new ArgumentException ( "Substance usages for TEDS discharge interview allow three entries at most." );
                }
                if (
                    tedsDischargeInterviewSubstanceUsages.Any (
                        tedsAdmissionInterviewSubstanceUsage =>
                        tedsAdmissionInterviewSubstanceUsage != null
                        &&
                        tedsDischargeInterviewSubstanceUsages.Any (
                            p => p != null && p != tedsAdmissionInterviewSubstanceUsage && p.ValuesEqual ( tedsAdmissionInterviewSubstanceUsage ) ) ) )
                {
                    throw new ArgumentException ( "Shouldn’t have duplicated substance usage entry in the list." );
                }
            }

            PrimaryTedsDischargeInterviewSubstanceUsage = null;
            SecondaryTedsDischargeInterviewSubstanceUsage = null;
            TertiaryTedsDischargeInterviewSubstanceUsage = null;

            //var count = _substanceUsages.Count;
            //for ( var i = 0; i < count; i++ )
            //{
            //    _substanceUsages.RemoveAt ( 0 );
            //}
            _substanceUsages.Clear ();

            if ( tedsDischargeInterviewSubstanceUsages != null )
            {
                foreach ( var tedsDischargeInterviewSubstanceUsage in tedsDischargeInterviewSubstanceUsages )
                {
                    if ( tedsDischargeInterviewSubstanceUsage != null )
                    {
                        tedsDischargeInterviewSubstanceUsage.TedsDischargeInterview = this;
                        _substanceUsages.Add ( tedsDischargeInterviewSubstanceUsage );
                    }
                }

                var count = _substanceUsages.Count;
                PrimaryTedsDischargeInterviewSubstanceUsage = count > 0 ? _substanceUsages[0] : null;
                SecondaryTedsDischargeInterviewSubstanceUsage = count > 1 ? _substanceUsages[1] : null;
                TertiaryTedsDischargeInterviewSubstanceUsage = count > 2 ? _substanceUsages[2] : null;
            }
        }

        /// <summary>
        /// Revises the type of the living arrangements.
        /// </summary>
        /// <param name="livingArrangementsType">Type of the living arrangements.</param>
        public virtual void ReviseLivingArrangementsType( TedsAnswer<LivingArrangementsType>  livingArrangementsType)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(livingArrangementsType, () => LivingArrangementsType, "Living arrangement type");
            LivingArrangementsType = livingArrangementsType;
        }

        /// <summary>
        /// Revises the teds employment status information.
        /// </summary>
        /// <param name="tedsEmploymentStatusInformation">The teds employment status information.</param>
        public virtual void ReviseTedsEmploymentStatusInformation(TedsEmploymentStatusInformation tedsEmploymentStatusInformation)
        {
            TedsEmploymentStatusInformation = tedsEmploymentStatusInformation;
        }

        /// <summary>
        /// Revises the arrests in thirty days number.
        /// </summary>
        /// <param name="arrestsInThirtyDaysNumber">The arrests in thirty days number.</param>
        public virtual void ReviseArrestsInThirtyDaysNumber( TedsAnswer<int?> arrestsInThirtyDaysNumber )
        {
            CheckIfTedsAnswerHasInvalidNonResponse(arrestsInThirtyDaysNumber, () => ArrestsInPastThirtyDaysCount, "Number of Arrests in Past Thirty Days");
            ArrestsInPastThirtyDaysCount = arrestsInThirtyDaysNumber;
        }

        /// <summary>
        /// Revises the type of the frequency of attendance at self help programs.
        /// </summary>
        /// <param name="frequencyOfAttendanceAtSelfHelpProgramsType">Type of the frequency of attendance at self help programs.</param>
        public virtual void ReviseFrequencyOfAttendanceAtSelfHelpProgramsType (TedsAnswer<ParticipatedSelfHelpGroupInPastThirtyDaysType> frequencyOfAttendanceAtSelfHelpProgramsType)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(frequencyOfAttendanceAtSelfHelpProgramsType, () => ParticipatedSelfHelpGroupInPastThirtyDaysType, "Frequency of attendance at self help programs");
            ParticipatedSelfHelpGroupInPastThirtyDaysType = frequencyOfAttendanceAtSelfHelpProgramsType;
        }

        /// <summary>
        /// Determines whether this instance is complete, which means all required fields are populated.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance is complete; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsComplete ()
        {
            return TedsAdmissionInterview == null ||
                   LastFaceToFaceContactDate == ( DateTime )typeof( DateTime ).GetDefault () ||
                   TedsDischargeReason == null ||
                   ParticipatedSelfHelpGroupInPastThirtyDaysType == null;
        }

        /// <summary>
        /// Gets the default non response lookup well known names.
        /// </summary>
        public static IEnumerable<string> DefaultNonResponseLookupWellKnownNames
        {
            get
            {
                return new List<string>
                           {
                               WellKnownNames.TedsModule.TedsNonResponse.Unknown
                           };
            }
        }

        /// <summary>
        /// Gets the default non response lookup well known names.
        /// </summary>
        /// <returns>
        /// A IEnumerable&lt;string&gt;.
        /// </returns>
        public virtual IEnumerable<string> GetDefaultNonResponseLookupWellKnownNames()
        {
            return new List<string>
                           {
                               WellKnownNames.TedsModule.TedsNonResponse.Unknown
                           };
        }

        private IEnumerable<string> GetNonResponseLookupWellKnownNames<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            IEnumerable<string> wellKnownNames = GetDefaultNonResponseLookupWellKnownNames();

            //var wellKnownNamesForMostProperties = new List<string> { WellKnownNames.TedsModule.TedsNonResponse.NotApplicable, WellKnownNames.TedsModule.TedsNonResponse.Unknown };

            //string propertyName = PropertyUtil.ExtractPropertyName(propertyExpression);

            return wellKnownNames;
        }

        private void CheckIfTedsAnswerHasInvalidNonResponse<T>(TedsAnswer<T> tedsAnswer, Expression<Func<TedsAnswer<T>>> propertyExpression, string tedsQuestion)
        {
            if (tedsAnswer != null
                && !tedsAnswer.HasResponse
                && !GetNonResponseLookupWellKnownNames(propertyExpression).ToList().Contains(tedsAnswer.TedsNonResponse.WellKnownName)
                )
            {
                throw new ArgumentException(
                    string.Format("{0} has invalid non-response.", tedsQuestion.Substring(0, 1).ToUpper() + tedsQuestion.Substring(1)));
            }
        }
        #endregion
    }
}
