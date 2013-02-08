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

using Pillar.Common.Utility;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.Clinical.SbirtModule
{
    /// <summary>
    /// AUDIT-C is an <see cref="Activity">Activity</see> in the form of a
    /// questionnaire, Alcohol Use Disorders Identification Test - Consumption, used for assessment of alcohol abuse
    /// <see cref="Patient">Patient</see> in a given <see
    /// cref="Visit">Visit</see>.
    /// The AUDIT-C is admininstered as a prescreen to see if further screening is needed.
    /// </summary>
    /// <remarks>
    /// The SBIRT Initiative is a 5-year national program funded by the Substance Abuse and Mental Health Services Administration (SAMHSA), 
    /// Center for Substance Abuse Treatment (CSAT) to implement screening, brief intervention, referral to treatment services for adults in 
    /// primary care and community health settings for substance misuse and substance abuse disorders.
    /// </remarks>
    public class AuditC : Activity
    {
        #region Member Variables

        private int? _howOftenYouDrinkNumber;
        private int? _alcoholicDrinksPerDayNumber;
        private int? _howOftenYouHaveSixOrMoreDrinksNumber;
        private int? _auditCScore;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditC"/> class.
        /// </summary>
        protected internal AuditC ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditC"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected internal AuditC (
            Visit visit,
            ActivityType activityType )
            : base ( visit, activityType )
        {        
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the how often you drink number.
        /// </summary>
        public virtual int? HowOftenYouDrinkNumber
        {
            get { return _howOftenYouDrinkNumber; }
            private set { ApplyPropertyChange ( ref _howOftenYouDrinkNumber, () => HowOftenYouDrinkNumber, value ); }
        }

        /// <summary>
        /// Gets the alcoholic drinks per day number.
        /// </summary>
        public virtual int? AlcoholicDrinksPerDayNumber
        {
            get { return _alcoholicDrinksPerDayNumber; }
            private set { ApplyPropertyChange(ref _alcoholicDrinksPerDayNumber, () => AlcoholicDrinksPerDayNumber, value); }
        }

        /// <summary>
        /// Gets the how often you have six or more drinks number.
        /// </summary>
        public virtual int? HowOftenYouHaveSixOrMoreDrinksNumber
        {
            get { return _howOftenYouHaveSixOrMoreDrinksNumber; }
            private set { ApplyPropertyChange ( ref _howOftenYouHaveSixOrMoreDrinksNumber, () => HowOftenYouHaveSixOrMoreDrinksNumber, value ); }
        }

        /// <summary>
        /// Gets the audit C score.
        /// </summary>
        public virtual int? AuditCScore
        {
            get { return _auditCScore; }
            private set { }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Revises the and calculate.
        /// </summary>
        /// <param name="howOftenYouDrinkNumber">The how often you drink number.</param>
        /// <param name="alcoholicDrinksPerDayNumber">The alcoholic drinks per day number.</param>
        /// <param name="howOftenYouHaveSixOrMoreDrinksNumber">The how often you have six or more drinks number.</param>
        public virtual void ReviseAndCalculate(int? howOftenYouDrinkNumber, int? alcoholicDrinksPerDayNumber, int? howOftenYouHaveSixOrMoreDrinksNumber)
        {
            Check.IsInRange(howOftenYouDrinkNumber, 0, 4, () => HowOftenYouDrinkNumber);
            Check.IsInRange(alcoholicDrinksPerDayNumber, 0, 4, () => AlcoholicDrinksPerDayNumber);
            Check.IsInRange(howOftenYouHaveSixOrMoreDrinksNumber, 0, 4, () => HowOftenYouHaveSixOrMoreDrinksNumber);

            HowOftenYouDrinkNumber = howOftenYouDrinkNumber;

            // NOTE: If the Patient never drinks, then the next answers are always 0.
            if (howOftenYouDrinkNumber == 0)
            {
                AlcoholicDrinksPerDayNumber
                    = HowOftenYouHaveSixOrMoreDrinksNumber
                      = 0;
            }
            else
            {
                AlcoholicDrinksPerDayNumber = alcoholicDrinksPerDayNumber;
                HowOftenYouHaveSixOrMoreDrinksNumber = howOftenYouHaveSixOrMoreDrinksNumber;
            }

            _auditCScore = HowOftenYouDrinkNumber.GetValueOrDefault()
                          + AlcoholicDrinksPerDayNumber.GetValueOrDefault()
                          + HowOftenYouHaveSixOrMoreDrinksNumber.GetValueOrDefault();
        }

        #endregion
    }
}