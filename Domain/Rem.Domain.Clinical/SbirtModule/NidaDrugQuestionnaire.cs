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
using Pillar.Domain.Event;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.SbirtModule.Event;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.Clinical.SbirtModule
{
    /// <summary>
    /// NidaDrugQuestionnaire is an <see cref="Activity">Activity</see> in the form of a
    /// questionnaireused for assessment of possible involvement with drugs not including alcoholic beverages during the past 12 months for a 
    /// <see cref="Patient">Patient</see> in a given <see
    /// cref="Visit">Visit</see>.
    /// </summary>
    /// <remarks>
    /// The SBIRT Initiative is a 5-year national program funded by the Substance Abuse and Mental Health Services Administration (SAMHSA), 
    /// Center for Substance Abuse Treatment (CSAT) to implement screening, brief intervention, referral to treatment services for adults in 
    /// primary care and community health settings for substance misuse and substance abuse disorders.
    /// </remarks>
    public class NidaDrugQuestionnaire : Activity
    {
        #region Member Variables

        private bool? _nidaDrugQuestionnaireIndicator;
        private NidaDrugQuestionnaireInjectionDrugUse _nidaDrugQuestionnaireInjectionDrugUse;
        private NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse _nidaDrugQuestionnaireDrugTypeAndFrequencyOfUse;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NidaDrugQuestionnaire"/> class.
        /// </summary>
        protected internal NidaDrugQuestionnaire ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NidaDrugQuestionnaire"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected internal NidaDrugQuestionnaire (
            Visit visit,
            ActivityType activityType )
            : base ( visit, activityType )
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Nida drug questionnaire drug type and frequency of use.
        /// </summary>
        public virtual NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse
        {
            get { return _nidaDrugQuestionnaireDrugTypeAndFrequencyOfUse; }
            private set { ApplyPropertyChange(ref _nidaDrugQuestionnaireDrugTypeAndFrequencyOfUse, () => NidaDrugQuestionnaireIndicator, value); }
        }

        /// <summary>
        /// Gets the Nida drug questionnaire injection drug use.
        /// </summary>
        public virtual NidaDrugQuestionnaireInjectionDrugUse NidaDrugQuestionnaireInjectionDrugUse
        {
            get { return _nidaDrugQuestionnaireInjectionDrugUse; }
            private set { ApplyPropertyChange(ref _nidaDrugQuestionnaireInjectionDrugUse, () => NidaDrugQuestionnaireIndicator, value); }
        }

        /// <summary>
        /// Gets a boolean value indicating the Nida drug questionnaire.
        /// Result = TRUE when, (any drugtype is used daily/weekly ie, has value greater than or equal to 3) Or (injection drug used in the past 90 days ie, value is 2)
        /// </summary>
        public virtual bool? NidaDrugQuestionnaireIndicator
        {
            get { return _nidaDrugQuestionnaireIndicator; }
            private set { ApplyPropertyChange(ref _nidaDrugQuestionnaireIndicator, () => NidaDrugQuestionnaireIndicator, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Revises values and calculates the Nida score.
        /// </summary>
        /// <param name="nidaDrugQuestionnaireDrugTypeAndFrequencyOfUse">The Nida drug questionnaire drug type and frequency of use.</param>
        /// <param name="nidaDrugQuestionnaireInjectionDrugUse">The Nida drug questionnaire injection drug use.</param>
        public virtual void ReviseAndCalculate(NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse nidaDrugQuestionnaireDrugTypeAndFrequencyOfUse, NidaDrugQuestionnaireInjectionDrugUse nidaDrugQuestionnaireInjectionDrugUse)
        {
            Check.IsNotNull ( nidaDrugQuestionnaireDrugTypeAndFrequencyOfUse, () => NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse );
            Check.IsNotNull (nidaDrugQuestionnaireInjectionDrugUse, () => NidaDrugQuestionnaireInjectionDrugUse  );

            NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse = nidaDrugQuestionnaireDrugTypeAndFrequencyOfUse;
            NidaDrugQuestionnaireInjectionDrugUse = nidaDrugQuestionnaireInjectionDrugUse;

            NidaDrugQuestionnaireIndicator = NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse.NidaDrugQuestionnaireDrugTypeAndFrequencyOfUseIndicator
                                             || NidaDrugQuestionnaireInjectionDrugUse.NidaDrugQuestionnaireInjectionDrugUseIndicator;

            DomainEvent.Raise(new NidaDrugQuestionnaireIndicatorCalculatedEvent { NidaDrugQuestionnaire = this });
        }

        #endregion
    }
}