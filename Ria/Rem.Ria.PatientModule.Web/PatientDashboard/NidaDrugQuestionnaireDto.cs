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
    /// Data transfer object for NidaDrugQuestionnaire class.
    /// </summary>
    public class NidaDrugQuestionnaireDto : ActivityDto
    {
        #region Constants and Fields

        private int? _cannabisUseAnswerNumber;
        private int? _cocaineUseAnswerNumber;
        private bool? _drugUseByInjectionIndicator;
        private bool _isDast10ResultChanged;
        private int? _lastDrugUseByInjectionAnswerNumber;
        private int? _methamphetamineUseAnswerNumber;
        private bool? _nidaDrugQuestionnaireIndicator;
        private int? _opioidsUseAnswerNumber;
        private int? _otherDrug1AnswerNumber;
        private string _otherDrug1TypeName;
        private int? _otherDrug2AnswerNumber;
        private string _otherDrug2TypeName;
        private int? _otherDrug3AnswerNumber;
        private string _otherDrug3TypeName;
        private int? _sedativesUseAnswerNumber;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the cannabis use answer number.
        /// </summary>
        /// <value>The cannabis use answer number.</value>
        [DataMember]
        public int? CannabisUseAnswerNumber
        {
            get { return _cannabisUseAnswerNumber; }
            set { ApplyPropertyChange ( ref _cannabisUseAnswerNumber, () => CannabisUseAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the cocaine use answer number.
        /// </summary>
        /// <value>The cocaine use answer number.</value>
        [DataMember]
        public int? CocaineUseAnswerNumber
        {
            get { return _cocaineUseAnswerNumber; }
            set { ApplyPropertyChange ( ref _cocaineUseAnswerNumber, () => CocaineUseAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the drug use by injection indicator.
        /// </summary>
        /// <value>The drug use by injection indicator.</value>
        [DataMember]
        public bool? DrugUseByInjectionIndicator
        {
            get { return _drugUseByInjectionIndicator; }
            set { ApplyPropertyChange ( ref _drugUseByInjectionIndicator, () => DrugUseByInjectionIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is dast10 result changed.
        /// </summary>
        /// <value><c>true</c> if this instance is dast10 result changed; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool IsDast10ResultChanged
        {
            get { return _isDast10ResultChanged; }
            set { ApplyPropertyChange ( ref _isDast10ResultChanged, () => IsDast10ResultChanged, value ); }
        }

        /// <summary>
        /// Gets or sets the last drug use by injection answer number.
        /// </summary>
        /// <value>The last drug use by injection answer number.</value>
        [DataMember]
        public int? LastDrugUseByInjectionAnswerNumber
        {
            get { return _lastDrugUseByInjectionAnswerNumber; }
            set { ApplyPropertyChange ( ref _lastDrugUseByInjectionAnswerNumber, () => LastDrugUseByInjectionAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the methamphetamine use answer number.
        /// </summary>
        /// <value>The methamphetamine use answer number.</value>
        [DataMember]
        public int? MethamphetamineUseAnswerNumber
        {
            get { return _methamphetamineUseAnswerNumber; }
            set { ApplyPropertyChange ( ref _methamphetamineUseAnswerNumber, () => MethamphetamineUseAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the nida drug questionnaire indicator.
        /// </summary>
        /// <value>The nida drug questionnaire indicator.</value>
        [DataMember]
        public bool? NidaDrugQuestionnaireIndicator
        {
            get { return _nidaDrugQuestionnaireIndicator; }
            set { ApplyPropertyChange ( ref _nidaDrugQuestionnaireIndicator, () => NidaDrugQuestionnaireIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the opioids use answer number.
        /// </summary>
        /// <value>The opioids use answer number.</value>
        [DataMember]
        public int? OpioidsUseAnswerNumber
        {
            get { return _opioidsUseAnswerNumber; }
            set { ApplyPropertyChange ( ref _opioidsUseAnswerNumber, () => OpioidsUseAnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the other drug1 answer number.
        /// </summary>
        /// <value>The other drug1 answer number.</value>
        [DataMember]
        public int? OtherDrug1AnswerNumber
        {
            get { return _otherDrug1AnswerNumber; }
            set { ApplyPropertyChange ( ref _otherDrug1AnswerNumber, () => OtherDrug1AnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the other drug1 type.
        /// </summary>
        /// <value>The name of the other drug1 type.</value>
        [DataMember]
        public string OtherDrug1TypeName
        {
            get { return _otherDrug1TypeName; }
            set { ApplyPropertyChange ( ref _otherDrug1TypeName, () => OtherDrug1TypeName, value ); }
        }

        /// <summary>
        /// Gets or sets the other drug2 answer number.
        /// </summary>
        /// <value>The other drug2 answer number.</value>
        [DataMember]
        public int? OtherDrug2AnswerNumber
        {
            get { return _otherDrug2AnswerNumber; }
            set { ApplyPropertyChange ( ref _otherDrug2AnswerNumber, () => OtherDrug2AnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the other drug2 type.
        /// </summary>
        /// <value>The name of the other drug2 type.</value>
        [DataMember]
        public string OtherDrug2TypeName
        {
            get { return _otherDrug2TypeName; }
            set { ApplyPropertyChange ( ref _otherDrug2TypeName, () => OtherDrug2TypeName, value ); }
        }

        /// <summary>
        /// Gets or sets the other drug3 answer number.
        /// </summary>
        /// <value>The other drug3 answer number.</value>
        [DataMember]
        public int? OtherDrug3AnswerNumber
        {
            get { return _otherDrug3AnswerNumber; }
            set { ApplyPropertyChange ( ref _otherDrug3AnswerNumber, () => OtherDrug3AnswerNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the other drug3 type.
        /// </summary>
        /// <value>The name of the other drug3 type.</value>
        [DataMember]
        public string OtherDrug3TypeName
        {
            get { return _otherDrug3TypeName; }
            set { ApplyPropertyChange ( ref _otherDrug3TypeName, () => OtherDrug3TypeName, value ); }
        }

        /// <summary>
        /// Gets or sets the sedatives use answer number.
        /// </summary>
        /// <value>The sedatives use answer number.</value>
        [DataMember]
        public int? SedativesUseAnswerNumber
        {
            get { return _sedativesUseAnswerNumber; }
            set { ApplyPropertyChange ( ref _sedativesUseAnswerNumber, () => SedativesUseAnswerNumber, value ); }
        }

        #endregion
    }
}
