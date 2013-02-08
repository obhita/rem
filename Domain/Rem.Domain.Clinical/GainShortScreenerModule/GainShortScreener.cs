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

using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.Clinical.GainShortScreenerModule
{
    /// <summary>
    /// The <see cref="GainShortScreener">Global Appraisal of Individual Needs–Short Screener (GAIN-SS)</see> is an <see cref="Activity">Activity</see> that serves as  
    /// screener in general populations to identify individuals having one or more behavioral health disorders (e.g., internalizing or externalizing
    /// psychiatric disorders, substance use disorders, or crime or violence problems)
    /// </summary>
    public class GainShortScreener : Activity
    {
        private GainShortScreenerCrimeViolence _gainShortScreenerCrimeViolence;
        private GainShortScreenerExternalizingDisorder _gainShortScreenerExternalizingDisorder;
        private GainShortScreenerInternalizingDisorder _gainShortScreenerInternalizingDisorder;
        private GainShortScreenerSubstanceDisorder _gainShortScreenerSubstanceDisorder;

        private int? _totalScreenerLifetimeScore;
        private int? _totalScreenerPastMonthScore;
        private int? _totalScreenerPastYearScore;

        /// <summary>
        /// Initializes a new instance of the <see cref="GainShortScreener"/> class.
        /// </summary>
        protected internal GainShortScreener ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GainShortScreener"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected internal GainShortScreener ( Visit visit, ActivityType activityType )
            : base ( visit, activityType )
        {
        }

        /// <summary>
        /// Gets the gain short screener internalizing disorder.
        /// </summary>
        /// <value>GainShortScreenerInternalizingDisorder containing the value.</value>
        public virtual GainShortScreenerInternalizingDisorder GainShortScreenerInternalizingDisorder
        {
            get { return _gainShortScreenerInternalizingDisorder; }
            private set { ApplyPropertyChange ( ref _gainShortScreenerInternalizingDisorder, () => GainShortScreenerInternalizingDisorder, value ); }
        }

        /// <summary>
        /// Gets the gain short screener externalizing disorder.
        /// </summary>
        /// <value>GainShortScreenerExternalizingDisorder containing the value.</value>
        public virtual GainShortScreenerExternalizingDisorder GainShortScreenerExternalizingDisorder
        {
            get { return _gainShortScreenerExternalizingDisorder; }
            private set { ApplyPropertyChange ( ref _gainShortScreenerExternalizingDisorder, () => GainShortScreenerExternalizingDisorder, value ); }
        }

        /// <summary>
        /// Gets the gain short screener substance disorder.
        /// </summary>
        /// <value>GainShortScreenerSubstanceDisorder containing the value.</value>
        public virtual GainShortScreenerSubstanceDisorder GainShortScreenerSubstanceDisorder
        {
            get { return _gainShortScreenerSubstanceDisorder; }
            private set { ApplyPropertyChange ( ref _gainShortScreenerSubstanceDisorder, () => GainShortScreenerSubstanceDisorder, value ); }
        }

        /// <summary>
        /// Gets the gain short screener crime violence.
        /// </summary>
        /// <value>GainShortScreenerCrimeViolence containing the value.</value>
        public virtual GainShortScreenerCrimeViolence GainShortScreenerCrimeViolence
        {
            get { return _gainShortScreenerCrimeViolence; }
            private set { ApplyPropertyChange ( ref _gainShortScreenerCrimeViolence, () => GainShortScreenerCrimeViolence, value ); }
        }

        /// <summary>
        /// Gets or sets the total screener past month score.
        /// </summary>
        /// <value>
        /// The total screener past month score.
        /// </value>
        public virtual int? TotalScreenerPastMonthScore
        {
            get { return _totalScreenerPastMonthScore; }
            set { ApplyPropertyChange ( ref _totalScreenerPastMonthScore, () => TotalScreenerPastMonthScore, value ); }
        }

        /// <summary>
        /// Gets or sets the total screener past year score.
        /// </summary>
        /// <value>
        /// The total screener past year score.
        /// </value>
        public virtual int? TotalScreenerPastYearScore
        {
            get { return _totalScreenerPastYearScore; }
            set { ApplyPropertyChange ( ref _totalScreenerPastYearScore, () => TotalScreenerPastYearScore, value ); }
        }

        /// <summary>
        /// Gets or sets the total screener lifetime score.
        /// </summary>
        /// <value>
        /// The total screener lifetime score.
        /// </value>
        public virtual int? TotalScreenerLifetimeScore
        {
            get { return _totalScreenerLifetimeScore; }
            set { ApplyPropertyChange ( ref _totalScreenerLifetimeScore, () => TotalScreenerLifetimeScore, value ); }
        }


        /// <summary>
        /// Accepts the questionnaire's constituent sectional value types and calculates total scores.
        /// </summary>
        /// <param name="gainShortScreenerInternalizingDisorder">The gain short screener internalizing disorder.</param>
        /// <param name="gainShortScreenerExternalizingDisorder">The gain short screener externalizing disorder.</param>
        /// <param name="gainShortScreenerSubstanceDisorder">The gain short screener substance disorder.</param>
        /// <param name="gainShortScreenerCrimeViolence">The gain short screener crime violence.</param>
        public virtual void ReviseAndCalculateScores ( GainShortScreenerInternalizingDisorder gainShortScreenerInternalizingDisorder,
                                                       GainShortScreenerExternalizingDisorder gainShortScreenerExternalizingDisorder,
                                                       GainShortScreenerSubstanceDisorder gainShortScreenerSubstanceDisorder,
                                                       GainShortScreenerCrimeViolence gainShortScreenerCrimeViolence )
        {
            _gainShortScreenerInternalizingDisorder = gainShortScreenerInternalizingDisorder;
            _gainShortScreenerExternalizingDisorder = gainShortScreenerExternalizingDisorder;
            _gainShortScreenerSubstanceDisorder = gainShortScreenerSubstanceDisorder;
            _gainShortScreenerCrimeViolence = gainShortScreenerCrimeViolence;

            // Calculate total scores.
            TotalScreenerPastMonthScore = _gainShortScreenerInternalizingDisorder.InternalizingDisorderScreenerPastMonthScore +
                                          _gainShortScreenerExternalizingDisorder.ExternalizingDisorderScreenerPastMonthScore +
                                          _gainShortScreenerSubstanceDisorder.SubstanceDisorderScreenerPastMonthScore +
                                          _gainShortScreenerCrimeViolence.CrimeViolenceScreenerPastMonthScore;

            TotalScreenerPastYearScore = _gainShortScreenerInternalizingDisorder.InternalizingDisorderScreenerPastYearScore +
                                         _gainShortScreenerExternalizingDisorder.ExternalizingDisorderScreenerPastYearScore +
                                         _gainShortScreenerSubstanceDisorder.SubstanceDisorderScreenerPastYearScore +
                                         _gainShortScreenerCrimeViolence.CrimeViolenceScreenerPastYearScore;

            TotalScreenerLifetimeScore = _gainShortScreenerInternalizingDisorder.InternalizingDisorderScreenerLifetimeScore +
                                         _gainShortScreenerExternalizingDisorder.ExternalizingDisorderScreenerLifetimeScore +
                                         _gainShortScreenerSubstanceDisorder.SubstanceDisorderScreenerLifetimeScore +
                                         _gainShortScreenerCrimeViolence.CrimeViolenceScreenerLifetimeScore;
        }
    }
}