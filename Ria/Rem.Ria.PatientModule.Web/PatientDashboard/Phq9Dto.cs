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

using System.Runtime.Serialization;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Data transfer object for Phq9 class.
    /// </summary>
    public class Phq9Dto : ActivityDto
    {
        #region Constants and Fields

        private int? _actingSluggishOrFidgityAnswerNumber;
        private int? _feelingBadAboutSelfAnswerNumber;
        private int? _feelingDownAnswerNumber;
        private int? _feelingTiredAnswerNumber;
        private int? _haveTheseProblemsAffectedYouAnswerNumber;
        private int? _littleInterestInDoingThingsAnswerNumber;
        private int? _poorAppetiteAnswerNumber;
        private int? _severityScore;
        private int? _thoughtsOfHurtingSelfAnswerNumber;
        private int? _troubleConcentratingAnswerNumber;
        private int? _troubleSleepingAnswerNumber;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the acting sluggish or fidgity answer number.
        /// </summary>
        /// <value>The acting sluggish or fidgity answer number.</value>
        [DataMember]
        public int? ActingSluggishOrFidgityAnswerNumber
        {
            get { return _actingSluggishOrFidgityAnswerNumber; }
            set { ApplyPropertyChange ( ref _actingSluggishOrFidgityAnswerNumber, () => ActingSluggishOrFidgityAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the feeling bad about self answer number.
        /// </summary>
        /// <value>The feeling bad about self answer number.</value>
        [DataMember]
        public int? FeelingBadAboutSelfAnswerNumber
        {
            get { return _feelingBadAboutSelfAnswerNumber; }
            set { ApplyPropertyChange ( ref _feelingBadAboutSelfAnswerNumber, () => FeelingBadAboutSelfAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the feeling down answer number.
        /// </summary>
        /// <value>The feeling down answer number.</value>
        [DataMember]
        public int? FeelingDownAnswerNumber
        {
            get { return _feelingDownAnswerNumber; }
            set { ApplyPropertyChange ( ref _feelingDownAnswerNumber, () => FeelingDownAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the feeling tired answer number.
        /// </summary>
        /// <value>The feeling tired answer number.</value>
        [DataMember]
        public int? FeelingTiredAnswerNumber
        {
            get { return _feelingTiredAnswerNumber; }
            set { ApplyPropertyChange ( ref _feelingTiredAnswerNumber, () => FeelingTiredAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the have these problems affected you answer number.
        /// </summary>
        /// <value>The have these problems affected you answer number.</value>
        [DataMember]
        public int? HaveTheseProblemsAffectedYouAnswerNumber
        {
            get { return _haveTheseProblemsAffectedYouAnswerNumber; }
            set { ApplyPropertyChange ( ref _haveTheseProblemsAffectedYouAnswerNumber, () => HaveTheseProblemsAffectedYouAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the little interest in doing things answer number.
        /// </summary>
        /// <value>The little interest in doing things answer number.</value>
        [DataMember]
        public int? LittleInterestInDoingThingsAnswerNumber
        {
            get { return _littleInterestInDoingThingsAnswerNumber; }
            set { ApplyPropertyChange ( ref _littleInterestInDoingThingsAnswerNumber, () => LittleInterestInDoingThingsAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the poor appetite answer number.
        /// </summary>
        /// <value>The poor appetite answer number.</value>
        [DataMember]
        public int? PoorAppetiteAnswerNumber
        {
            get { return _poorAppetiteAnswerNumber; }
            set { ApplyPropertyChange ( ref _poorAppetiteAnswerNumber, () => PoorAppetiteAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the severity score.
        /// </summary>
        /// <value>The severity score.</value>
        [DataMember]
        public int? SeverityScore
        {
            get { return _severityScore; }
            set { ApplyPropertyChange ( ref _severityScore, () => SeverityScore, value ); }
        }

        /// <summary>
        /// Gets or sets the thoughts of hurting self answer number.
        /// </summary>
        /// <value>The thoughts of hurting self answer number.</value>
        [DataMember]
        public int? ThoughtsOfHurtingSelfAnswerNumber
        {
            get { return _thoughtsOfHurtingSelfAnswerNumber; }
            set { ApplyPropertyChange ( ref _thoughtsOfHurtingSelfAnswerNumber, () => ThoughtsOfHurtingSelfAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the trouble concentrating answer number.
        /// </summary>
        /// <value>The trouble concentrating answer number.</value>
        [DataMember]
        public int? TroubleConcentratingAnswerNumber
        {
            get { return _troubleConcentratingAnswerNumber; }
            set { ApplyPropertyChange ( ref _troubleConcentratingAnswerNumber, () => TroubleConcentratingAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the trouble sleeping answer number.
        /// </summary>
        /// <value>The trouble sleeping answer number.</value>
        [DataMember]
        public int? TroubleSleepingAnswerNumber
        {
            get { return _troubleSleepingAnswerNumber; }
            set { ApplyPropertyChange ( ref _troubleSleepingAnswerNumber, () => TroubleSleepingAnswerNumber, value ); }
        }

        #endregion
    }
}
