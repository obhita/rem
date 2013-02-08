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
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.NamingStrategy;

namespace Rem.Domain.Clinical.SbirtModule
{
    /// <summary>
    /// The NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse contains information for patient drug Type and frequency Of use.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse : IEquatable<NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse>
    {
        private const int DrugUseFrequencyThreshold = 3;

        private NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse"/> class.
        /// </summary>
        /// <param name="cannabisUseAnswerNumber">The cannabis use answer number.</param>
        /// <param name="cocaineUseAnswerNumber">The cocaine use answer number.</param>
        /// <param name="opioidsUseAnswerNumber">The opioids use answer number.</param>
        /// <param name="methamphetamineUseAnswerNumber">The methamphetamine use answer number.</param>
        /// <param name="sedativesUseAnswerNumber">The sedatives use answer number.</param>
        /// <param name="otherDrugInfo1">The other drug info1.</param>
        /// <param name="otherDrugInfo2">The other drug info2.</param>
        /// <param name="otherDrugInfo3">The other drug info3.</param>
        public NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse ( int? cannabisUseAnswerNumber, 
                                                                int? cocaineUseAnswerNumber, 
                                                                int? opioidsUseAnswerNumber, 
                                                                int? methamphetamineUseAnswerNumber,
                                                                int? sedativesUseAnswerNumber,
                                                                NidaDrugQuestionnaireOtherDrugInfo otherDrugInfo1, 
                                                                NidaDrugQuestionnaireOtherDrugInfo otherDrugInfo2, 
                                                                NidaDrugQuestionnaireOtherDrugInfo otherDrugInfo3)
        {
            Check.IsInRange ( cannabisUseAnswerNumber, 0, 4, () => CannabisUseAnswerNumber );
            Check.IsInRange(cocaineUseAnswerNumber, 0, 4, () => CocaineUseAnswerNumber);
            Check.IsInRange(opioidsUseAnswerNumber, 0, 4, () => OpioidsUseAnswerNumber);
            Check.IsInRange(methamphetamineUseAnswerNumber, 0, 4, () => MethamphetamineUseAnswerNumber);
            Check.IsInRange(sedativesUseAnswerNumber, 0, 4, () => SedativesUseAnswerNumber);
            Check.IsNotNull(otherDrugInfo1, () => OtherDrug1NidaDrugQuestionnaireOtherDrugInfo);
            Check.IsNotNull(otherDrugInfo2, () => OtherDrug2NidaDrugQuestionnaireOtherDrugInfo);
            Check.IsNotNull(otherDrugInfo3, () => OtherDrug3NidaDrugQuestionnaireOtherDrugInfo);

            CannabisUseAnswerNumber = cannabisUseAnswerNumber;
            CocaineUseAnswerNumber = cocaineUseAnswerNumber;
            OpioidsUseAnswerNumber = opioidsUseAnswerNumber;
            MethamphetamineUseAnswerNumber = methamphetamineUseAnswerNumber;
            SedativesUseAnswerNumber = sedativesUseAnswerNumber;

            OtherDrug1NidaDrugQuestionnaireOtherDrugInfo = otherDrugInfo1;
            OtherDrug2NidaDrugQuestionnaireOtherDrugInfo = otherDrugInfo2;
            OtherDrug3NidaDrugQuestionnaireOtherDrugInfo = otherDrugInfo3;
        }

        /// <summary>
        /// Gets the cannabis use answer number.
        /// Cannabis (marijuana, pot, grass, hash, synthetic cannabinoids, etc.) 
        /// </summary>
        public virtual int? CannabisUseAnswerNumber { get; private set; }

        /// <summary>
        /// Gets the cocaine use answer number.
        /// Cocaine (coke, crack, etc.) 
        /// </summary>
        public virtual int? CocaineUseAnswerNumber { get; private set; }

        /// <summary>
        /// Gets the opioids use answer number.
        /// Opioids (heroin, opium, prescription pain meds such as OxyContin, Percocet, hydrocodone [Vicodin], methadone, buprenorphine, etc.)
        /// </summary>
        public virtual int? OpioidsUseAnswerNumber { get; private set; }

        /// <summary>
        /// Gets the methamphetamine use answer number.
        /// Methamphetamine (speed, crystal meth, ice, etc.)
        /// </summary>
        public virtual int? MethamphetamineUseAnswerNumber { get; private set; }

        /// <summary>
        /// Gets the sedatives use answer number.
        /// Sedatives or sleeping pills (Valium, Serepax, Ativan, Librium, Xanax, Rohypnol, GHB, etc.) 
        /// </summary>
        public virtual int? SedativesUseAnswerNumber { get; private set; }

        /// <summary>
        /// Gets the other drug1 nida drug questionnaire other drug info.
        /// Other drug 1 info
        /// </summary>
        public virtual NidaDrugQuestionnaireOtherDrugInfo OtherDrug1NidaDrugQuestionnaireOtherDrugInfo { get; private set; }

        /// <summary>
        /// Gets the other drug2 nida drug questionnaire other drug info.
        /// Gets the other drug2 nida drug questionnaire other drug info.
        /// </summary>
        public virtual NidaDrugQuestionnaireOtherDrugInfo OtherDrug2NidaDrugQuestionnaireOtherDrugInfo { get; private set; }

        /// <summary>
        /// Gets the other drug3 nida drug questionnaire other drug info.
        /// Other drug 3 info
        /// </summary>
        public virtual NidaDrugQuestionnaireOtherDrugInfo OtherDrug3NidaDrugQuestionnaireOtherDrugInfo { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [Nida drug questionnaire drug type and frequency of use indicator].
        /// </summary>
        /// <value>
        /// <c>true</c> if [Nida drug questionnaire drug type and frequency of use indicator]; otherwise, <c>false</c>.
        /// </value>
        [IgnoreMapping]
        public bool NidaDrugQuestionnaireDrugTypeAndFrequencyOfUseIndicator
        {
            get
            {
                return CannabisUseAnswerNumber.GetValueOrDefault () >= DrugUseFrequencyThreshold
                       || CocaineUseAnswerNumber.GetValueOrDefault () >= DrugUseFrequencyThreshold
                       || OpioidsUseAnswerNumber.GetValueOrDefault () >= DrugUseFrequencyThreshold
                       || MethamphetamineUseAnswerNumber.GetValueOrDefault () >= DrugUseFrequencyThreshold
                       || SedativesUseAnswerNumber.GetValueOrDefault () >= DrugUseFrequencyThreshold
                       || (OtherDrug1NidaDrugQuestionnaireOtherDrugInfo != null && OtherDrug1NidaDrugQuestionnaireOtherDrugInfo.AnswerNumber.GetValueOrDefault() >= DrugUseFrequencyThreshold)
                       || (OtherDrug2NidaDrugQuestionnaireOtherDrugInfo != null && OtherDrug2NidaDrugQuestionnaireOtherDrugInfo.AnswerNumber.GetValueOrDefault() >= DrugUseFrequencyThreshold)
                       || (OtherDrug3NidaDrugQuestionnaireOtherDrugInfo != null && OtherDrug3NidaDrugQuestionnaireOtherDrugInfo.AnswerNumber.GetValueOrDefault() >= DrugUseFrequencyThreshold);
            }
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return other.CannabisUseAnswerNumber.Equals ( CannabisUseAnswerNumber ) && other.CocaineUseAnswerNumber.Equals ( CocaineUseAnswerNumber ) && other.OpioidsUseAnswerNumber.Equals ( OpioidsUseAnswerNumber ) && other.MethamphetamineUseAnswerNumber.Equals ( MethamphetamineUseAnswerNumber ) && other.SedativesUseAnswerNumber.Equals ( SedativesUseAnswerNumber ) && Equals ( other.OtherDrug1NidaDrugQuestionnaireOtherDrugInfo, OtherDrug1NidaDrugQuestionnaireOtherDrugInfo ) && Equals ( other.OtherDrug2NidaDrugQuestionnaireOtherDrugInfo, OtherDrug2NidaDrugQuestionnaireOtherDrugInfo ) && Equals ( other.OtherDrug3NidaDrugQuestionnaireOtherDrugInfo, OtherDrug3NidaDrugQuestionnaireOtherDrugInfo );
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. 
        /// </param><exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.
        /// </exception><filterpriority>2</filterpriority>
        public override bool Equals ( object obj )
        {
            if ( ReferenceEquals ( null, obj ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, obj ) )
            {
                return true;
            }
            if ( obj.GetType () != typeof ( NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse ) )
            {
                return false;
            }
            return Equals ( ( NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse ) obj );
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode ()
        {
            unchecked
            {
                int result = ( CannabisUseAnswerNumber.HasValue ? CannabisUseAnswerNumber.Value : 0 );
                result = ( result * 397 ) ^ ( CocaineUseAnswerNumber.HasValue ? CocaineUseAnswerNumber.Value : 0 );
                result = ( result * 397 ) ^ ( OpioidsUseAnswerNumber.HasValue ? OpioidsUseAnswerNumber.Value : 0 );
                result = ( result * 397 ) ^ ( MethamphetamineUseAnswerNumber.HasValue ? MethamphetamineUseAnswerNumber.Value : 0 );
                result = ( result * 397 ) ^ ( SedativesUseAnswerNumber.HasValue ? SedativesUseAnswerNumber.Value : 0 );
                result = ( result * 397 ) ^ ( OtherDrug1NidaDrugQuestionnaireOtherDrugInfo != null ? OtherDrug1NidaDrugQuestionnaireOtherDrugInfo.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( OtherDrug2NidaDrugQuestionnaireOtherDrugInfo != null ? OtherDrug2NidaDrugQuestionnaireOtherDrugInfo.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( OtherDrug3NidaDrugQuestionnaireOtherDrugInfo != null ? OtherDrug3NidaDrugQuestionnaireOtherDrugInfo.GetHashCode () : 0 );
                return result;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Frequency of use score is " + NidaDrugQuestionnaireDrugTypeAndFrequencyOfUseIndicator;
        }
    }
}