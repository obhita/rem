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

using Pillar.Domain;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// The GpraProfessionalInformationSection contains patient professional information from the Gpra interview.
    /// </summary>
    [Component]
    public class GpraProfessionalInformationSection : GpraInterviewSectionBase
    {
        private readonly GpraNonResponseType<GpraJobTrainingProgram> _gpraJobTrainingProgram;
        private readonly string _otherJobTrainingProgramSpecificationNote;
        private readonly GpraNonResponseType<GpraEducationLevel> _highestGpraEducationLevel;
        private readonly GpraNonResponseType<GpraEmploymentStatus> _gpraEmploymentStatus;
        private readonly string _otherEmploymentTypeSpecificationNote;
        private readonly GpraNonResponseType<int?> _wagesPretaxIncomeAmount;
        private readonly GpraNonResponseType<int?> _publicAssistancePretaxIncomeAmount;
        private readonly GpraNonResponseType<int?> _retirementPretaxIncomeAmount;
        private readonly GpraNonResponseType<int?> _disabilityPretaxIncomeAmount;
        private readonly GpraNonResponseType<int?> _nonLegalPretaxIncomeAmount;
        private readonly GpraNonResponseType<int?> _familyFriendsPretaxIncomeAmount;
        private readonly GpraNonResponseType<int?> _otherPretaxIncomeAmount;
        private readonly string _otherPretaxIncomeSpecificationNote;

        private GpraProfessionalInformationSection()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraProfessionalInformationSection"/> class.
        /// </summary>
        /// <param name="gpraJobTrainingProgram">The gpra job training program.</param>
        /// <param name="otherJobTrainingProgramSpecificationNote">The other job training program specification note.</param>
        /// <param name="highestGpraEducationLevel">The highest gpra education level.</param>
        /// <param name="gpraEmploymentStatus">The gpra employment status.</param>
        /// <param name="otherEmploymentTypeSpecificationNote">The other employment type specification note.</param>
        /// <param name="wagesPretaxIncomeAmount">The wages pretax income amount.</param>
        /// <param name="publicAssistancePretaxIncomeAmount">The public assistance pretax income amount.</param>
        /// <param name="retirementPretaxIncomeAmount">The retirement pretax income amount.</param>
        /// <param name="disabilityPretaxIncomeAmount">The disability pretax income amount.</param>
        /// <param name="nonLegalPretaxIncomeAmount">The non legal pretax income amount.</param>
        /// <param name="familyFriendsPretaxIncomeAmount">The family friends pretax income amount.</param>
        /// <param name="otherPretaxIncomeAmount">The other pretax income amount.</param>
        /// <param name="otherPretaxIncomeSpecificationNote">The other pretax income specification note.</param>
        public GpraProfessionalInformationSection(GpraNonResponseType<GpraJobTrainingProgram> gpraJobTrainingProgram,
                                                  string otherJobTrainingProgramSpecificationNote,
                                                  GpraNonResponseType<GpraEducationLevel> highestGpraEducationLevel,
                                                  GpraNonResponseType<GpraEmploymentStatus> gpraEmploymentStatus,
                                                  string otherEmploymentTypeSpecificationNote,
                                                  GpraNonResponseType<int?> wagesPretaxIncomeAmount,
                                                  GpraNonResponseType<int?> publicAssistancePretaxIncomeAmount,
                                                  GpraNonResponseType<int?> retirementPretaxIncomeAmount,
                                                  GpraNonResponseType<int?> disabilityPretaxIncomeAmount,
                                                  GpraNonResponseType<int?> nonLegalPretaxIncomeAmount,
                                                  GpraNonResponseType<int?> familyFriendsPretaxIncomeAmount,
                                                  GpraNonResponseType<int?> otherPretaxIncomeAmount,
                                                  string otherPretaxIncomeSpecificationNote)
        {
            _gpraJobTrainingProgram = gpraJobTrainingProgram;
            _otherJobTrainingProgramSpecificationNote = otherJobTrainingProgramSpecificationNote;
            _highestGpraEducationLevel = highestGpraEducationLevel;
            _gpraEmploymentStatus = gpraEmploymentStatus;
            _otherEmploymentTypeSpecificationNote = otherEmploymentTypeSpecificationNote;
            _wagesPretaxIncomeAmount = wagesPretaxIncomeAmount;
            _publicAssistancePretaxIncomeAmount = publicAssistancePretaxIncomeAmount;
            _retirementPretaxIncomeAmount = retirementPretaxIncomeAmount;
            _disabilityPretaxIncomeAmount = disabilityPretaxIncomeAmount;
            _nonLegalPretaxIncomeAmount = nonLegalPretaxIncomeAmount;
            _familyFriendsPretaxIncomeAmount = familyFriendsPretaxIncomeAmount;
            _otherPretaxIncomeAmount = otherPretaxIncomeAmount;
            _otherPretaxIncomeSpecificationNote = otherPretaxIncomeSpecificationNote;
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraJobTrainingProgram">GpraJobTrainingProgram</see>
        /// denoting patient need for a job training program.
        /// </summary>
        public virtual GpraNonResponseType<GpraJobTrainingProgram> GpraJobTrainingProgram
        {
            get { return _gpraJobTrainingProgram; }
            private set { }
        }

        /// <summary>
        /// Gets the other job training program specification note.
        /// </summary>
        public virtual string OtherJobTrainingProgramSpecificationNote
        {
            get { return _otherJobTrainingProgramSpecificationNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraEducationLevel">GpraEducationLevel</see>
        /// denoting patient education level.
        /// </summary>
        public virtual GpraNonResponseType<GpraEducationLevel> HighestGpraEducationLevel
        {
            get { return _highestGpraEducationLevel; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraEmploymentStatus">GpraEmploymentStatus</see>
        /// denoting patient employment status.
        /// </summary>
        public virtual GpraNonResponseType<GpraEmploymentStatus> GpraEmploymentStatus
        {
            get { return _gpraEmploymentStatus; }
            private set { }
        }

        /// <summary>
        /// Gets the other employment type specification note.
        /// </summary>
        public virtual string OtherEmploymentTypeSpecificationNote
        {
            get { return _otherEmploymentTypeSpecificationNote; }
            private set { }
        }

        /// <summary>
        /// Gets the wages pretax income amount.
        /// </summary>
        public virtual GpraNonResponseType<int?> WagesPretaxIncomeAmount
        {
            get { return _wagesPretaxIncomeAmount; }
            private set { }
        }

        /// <summary>
        /// Gets the public assistance pretax income amount.
        /// </summary>
        public virtual GpraNonResponseType<int?> PublicAssistancePretaxIncomeAmount
        {
            get { return _publicAssistancePretaxIncomeAmount; }
            private set { }
        }

        /// <summary>
        /// Gets the retirement pretax income amount.
        /// </summary>
        public virtual GpraNonResponseType<int?> RetirementPretaxIncomeAmount
        {
            get { return _retirementPretaxIncomeAmount; }
            private set { }
        }

        /// <summary>
        /// Gets the disability pretax income amount.
        /// </summary>
        public virtual GpraNonResponseType<int?> DisabilityPretaxIncomeAmount
        {
            get { return _disabilityPretaxIncomeAmount; }
            private set { }
        }

        /// <summary>
        /// Gets the non legal pretax income amount.
        /// </summary>
        public virtual GpraNonResponseType<int?> NonLegalPretaxIncomeAmount
        {
            get { return _nonLegalPretaxIncomeAmount; }
            private set { }
        }

        /// <summary>
        /// Gets the family friends pretax income amount.
        /// </summary>
        public virtual GpraNonResponseType<int?> FamilyFriendsPretaxIncomeAmount
        {
            get { return _familyFriendsPretaxIncomeAmount; }
            private set { }
        }

        /// <summary>
        /// Gets the other pretax income amount.
        /// </summary>
        public virtual GpraNonResponseType<int?> OtherPretaxIncomeAmount
        {
            get { return _otherPretaxIncomeAmount; }
            private set { }
        }

        /// <summary>
        /// Gets the other pretax income specification note.
        /// </summary>
        public virtual string OtherPretaxIncomeSpecificationNote
        {
            get { return _otherPretaxIncomeSpecificationNote; }
            private set { }
        }
    }
}