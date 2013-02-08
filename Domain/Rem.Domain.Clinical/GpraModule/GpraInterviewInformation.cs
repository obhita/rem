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
using System.Text;
using Pillar.Domain;
using Pillar.Domain.NamingStrategy;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// The GpraInterviewInformation contains values related to Gpra Interview information.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class GpraInterviewInformation : IEquatable<GpraInterviewInformation>
    {
        private GpraInterviewInformation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraInterviewInformation"/> class.
        /// </summary>
        /// <param name="gpraInterviewType">Type of the Gpra interview.</param>
        /// <param name="gpraPatientType">Type of the Gpra patient.</param>
        /// <param name="sbirtSbiPositiveIndicator">The sbirt sbi positive indicator.</param>
        /// <param name="sbirtWillingIndicator">The sbirt willing indicator.</param>
        /// <param name="auditCScore">The audit C score.</param>
        /// <param name="cageScore">The cage score.</param>
        /// <param name="dastScore">The dast score.</param>
        /// <param name="dast10Score">The dast10 score.</param>
        /// <param name="niaaaGuideScore">The niaaa guide score.</param>
        /// <param name="assistAlcoholSubScore">The assist alcohol sub score.</param>
        /// <param name="otherScore">The other score.</param>
        /// <param name="otherSpecificationDescription">The other specification description.</param>
        /// <param name="contractGrantIdentifier">The contract grant identifier.</param>
        /// <param name="conductedInterviewIndicator">The conducted interview indicator.</param>
        /// <param name="cooccuringMhSaScreenerIndicator">The cooccuring mh sa screener indicator.</param>
        /// <param name="positiveCooccuringMhSaScreenerIndicator">The positive cooccuring mh sa screener indicator.</param>
        public GpraInterviewInformation(GpraInterviewType gpraInterviewType,
                                          GpraPatientType gpraPatientType,  
                                          bool? sbirtSbiPositiveIndicator,
                                          bool? sbirtWillingIndicator,
                                          int? auditCScore,
                                          int? cageScore,
                                          int? dastScore,
                                          int? dast10Score,
                                          int? niaaaGuideScore,
                                          int? assistAlcoholSubScore,
                                          int? otherScore,
                                          string otherSpecificationDescription,
                                          string contractGrantIdentifier,
                                          bool? conductedInterviewIndicator,
                                          bool? cooccuringMhSaScreenerIndicator,
                                          bool? positiveCooccuringMhSaScreenerIndicator)
        {
            GpraInterviewType = gpraInterviewType;
            GpraPatientType = gpraPatientType;
            SbirtSbiPositiveIndicator = sbirtSbiPositiveIndicator;
            SbirtWillingIndicator = sbirtWillingIndicator;
            AuditCScore = auditCScore;
            CageScore = cageScore;
            DastScore = dastScore;
            Dast10Score = dast10Score;
            NiaaaGuideScore = niaaaGuideScore;
            AssistAlcoholSubScore = assistAlcoholSubScore;
            OtherScore = otherScore;
            OtherSpecificationDescription = otherSpecificationDescription;
            ContractGrantIdentifier = contractGrantIdentifier;
            ConductedInterviewIndicator = conductedInterviewIndicator;
            CooccuringMhSaScreenerIndicator = cooccuringMhSaScreenerIndicator;
            PositiveCooccuringMhSaScreenerIndicator = positiveCooccuringMhSaScreenerIndicator;
        }


        /// <summary>
        /// Patient Type
        /// </summary>
        public virtual GpraPatientType GpraPatientType { get; private set; }

        /// <summary>
        /// Interview Type
        /// </summary>
        public virtual GpraInterviewType GpraInterviewType { get; private set; }

        /// <summary>
        /// Gets the sbirt sbi positive indicator.
        /// </summary>
        public virtual bool? SbirtSbiPositiveIndicator { get; private set; }

        /// <summary>
        /// Gets the sbirt willing indicator.
        /// </summary>
        public virtual bool? SbirtWillingIndicator { get; private set; }

        /// <summary>
        /// Gets the audit C score.
        /// </summary>
        public virtual int? AuditCScore { get; private set; }

        /// <summary>
        /// Gets the cage score.
        /// </summary>
        public virtual int? CageScore { get; private set; }

        /// <summary>
        /// Gets the dast score.
        /// </summary>
        public virtual int? DastScore { get; private set; }

        /// <summary>
        /// Gets the dast10 score.
        /// </summary>
        public virtual int? Dast10Score { get; private set; }

        /// <summary>
        /// Gets the niaaa guide score.
        /// </summary>
        public virtual int? NiaaaGuideScore { get; private set; }

        /// <summary>
        /// Gets the assist alcohol sub score.
        /// </summary>
        public virtual int? AssistAlcoholSubScore { get; private set; }

        /// <summary>
        /// Gets the other score.
        /// </summary>
        public virtual int? OtherScore { get; private set; }

        /// <summary>
        /// Gets the other specification description.
        /// </summary>
        public virtual string OtherSpecificationDescription { get; private set; }

        /// <summary>
        /// Contract/Grant ID
        /// </summary>
        public virtual string ContractGrantIdentifier { get; private set; }

        /// <summary>
        /// Did you conduct an interview?
        /// </summary>
        public virtual bool? ConductedInterviewIndicator { get; private set; }

        /// <summary>
        /// Was the client screened by your program for co-occurring mental health and substance use disorders?
        /// </summary>
        public virtual bool? CooccuringMhSaScreenerIndicator { get; private set; }

        /// <summary>
        /// Did the client screen positive for co-occurring mental health and substance use disorders?
        /// </summary>
        public virtual bool? PositiveCooccuringMhSaScreenerIndicator { get; private set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(GpraInterviewInformation left, GpraInterviewInformation right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(GpraInterviewInformation left, GpraInterviewInformation right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            const string Spacer = " - ";
            foreach (var propertyInfo in GetType().GetProperties())
            {
                sb.Append(Spacer + propertyInfo.GetValue(this, null));
            }
            return sb.ToString().Substring(Spacer.Length);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(GpraInterviewInformation other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.GpraPatientType, this.GpraPatientType) && Equals(other.GpraInterviewType, this.GpraInterviewType) && other.SbirtSbiPositiveIndicator.Equals(this.SbirtSbiPositiveIndicator) && other.SbirtWillingIndicator.Equals(this.SbirtWillingIndicator) && other.AuditCScore.Equals(this.AuditCScore) && other.CageScore.Equals(this.CageScore) && other.DastScore.Equals(this.DastScore) && other.Dast10Score.Equals(this.Dast10Score) && other.NiaaaGuideScore.Equals(this.NiaaaGuideScore) && other.AssistAlcoholSubScore.Equals(this.AssistAlcoholSubScore) && other.OtherScore.Equals(this.OtherScore) && Equals(other.OtherSpecificationDescription, this.OtherSpecificationDescription) && Equals(other.ContractGrantIdentifier, this.ContractGrantIdentifier) && other.ConductedInterviewIndicator.Equals(this.ConductedInterviewIndicator) && other.CooccuringMhSaScreenerIndicator.Equals(this.CooccuringMhSaScreenerIndicator) && other.PositiveCooccuringMhSaScreenerIndicator.Equals(this.PositiveCooccuringMhSaScreenerIndicator);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof(GpraInterviewInformation))
            {
                return false;
            }
            return Equals((GpraInterviewInformation)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = (this.GpraPatientType != null ? this.GpraPatientType.GetHashCode() : 0);
                result = (result * 397) ^ (this.GpraInterviewType != null ? this.GpraInterviewType.GetHashCode() : 0);
                result = (result * 397) ^ (this.SbirtSbiPositiveIndicator.HasValue ? this.SbirtSbiPositiveIndicator.Value.GetHashCode() : 0);
                result = (result * 397) ^ (this.SbirtWillingIndicator.HasValue ? this.SbirtWillingIndicator.Value.GetHashCode() : 0);
                result = (result * 397) ^ (this.AuditCScore.HasValue ? this.AuditCScore.Value : 0);
                result = (result * 397) ^ (this.CageScore.HasValue ? this.CageScore.Value : 0);
                result = (result * 397) ^ (this.DastScore.HasValue ? this.DastScore.Value : 0);
                result = (result * 397) ^ (this.Dast10Score.HasValue ? this.Dast10Score.Value : 0);
                result = (result * 397) ^ (this.NiaaaGuideScore.HasValue ? this.NiaaaGuideScore.Value : 0);
                result = (result * 397) ^ (this.AssistAlcoholSubScore.HasValue ? this.AssistAlcoholSubScore.Value : 0);
                result = (result * 397) ^ (this.OtherScore.HasValue ? this.OtherScore.Value : 0);
                result = (result * 397) ^ (this.OtherSpecificationDescription != null ? this.OtherSpecificationDescription.GetHashCode() : 0);
                result = (result * 397) ^ (this.ContractGrantIdentifier != null ? this.ContractGrantIdentifier.GetHashCode() : 0);
                result = (result * 397) ^ (this.ConductedInterviewIndicator.HasValue ? this.ConductedInterviewIndicator.Value.GetHashCode() : 0);
                result = (result * 397) ^ (this.CooccuringMhSaScreenerIndicator.HasValue ? this.CooccuringMhSaScreenerIndicator.Value.GetHashCode() : 0);
                result = (result * 397) ^ (this.PositiveCooccuringMhSaScreenerIndicator.HasValue ? this.PositiveCooccuringMhSaScreenerIndicator.Value.GetHashCode() : 0);
                return result;
            }
        }
    }
}
