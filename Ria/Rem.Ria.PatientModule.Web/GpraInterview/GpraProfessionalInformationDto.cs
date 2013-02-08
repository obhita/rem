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

using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <summary>
    /// Data transfer object for GpraProfessionalInformation class.
    /// </summary>
    public class GpraProfessionalInformationDto : GpraDtoBase
    {
        #region Constants and Fields

        private GpraNonResponseTypeDto<int?> _disabilityPretaxIncomeAmount;
        private GpraNonResponseTypeDto<int?> _familyFriendsPretaxIncomeAmount;
        private GpraNonResponseTypeDto<LookupValueDto> _gpraEmploymentStatus;
        private GpraNonResponseTypeDto<LookupValueDto> _gpraJobTrainingProgram;
        private GpraNonResponseTypeDto<LookupValueDto> _highestGpraEducationLevel;
        private GpraNonResponseTypeDto<int?> _nonLegalPretaxIncomeAmount;
        private string _otherEmploymentTypeSpecificationNote;
        private string _otherJobTrainingProgramSpecificationNote;
        private GpraNonResponseTypeDto<int?> _otherPretaxIncomeAmount;
        private string _otherPretaxIncomeSpecificationNote;
        private GpraNonResponseTypeDto<int?> _publicAssistancePretaxIncomeAmount;
        private GpraNonResponseTypeDto<int?> _retirementPretaxIncomeAmount;
        private GpraNonResponseTypeDto<int?> _wagesPretaxIncomeAmount;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the disability pretax income amount.
        /// </summary>
        /// <value>The disability pretax income amount.</value>
        public GpraNonResponseTypeDto<int?> DisabilityPretaxIncomeAmount
        {
            get { return _disabilityPretaxIncomeAmount; }
            set { ApplyPropertyChange ( ref _disabilityPretaxIncomeAmount, () => DisabilityPretaxIncomeAmount, value ); }
        }

        /// <summary>
        /// Gets or sets the family friends pretax income amount.
        /// </summary>
        /// <value>The family friends pretax income amount.</value>
        public GpraNonResponseTypeDto<int?> FamilyFriendsPretaxIncomeAmount
        {
            get { return _familyFriendsPretaxIncomeAmount; }
            set { ApplyPropertyChange ( ref _familyFriendsPretaxIncomeAmount, () => FamilyFriendsPretaxIncomeAmount, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra employment status.
        /// </summary>
        /// <value>The gpra employment status.</value>
        public GpraNonResponseTypeDto<LookupValueDto> GpraEmploymentStatus
        {
            get { return _gpraEmploymentStatus; }
            set { ApplyPropertyChange ( ref _gpraEmploymentStatus, () => GpraEmploymentStatus, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra job training program.
        /// </summary>
        /// <value>The gpra job training program.</value>
        public GpraNonResponseTypeDto<LookupValueDto> GpraJobTrainingProgram
        {
            get { return _gpraJobTrainingProgram; }
            set { ApplyPropertyChange ( ref _gpraJobTrainingProgram, () => GpraJobTrainingProgram, value ); }
        }

        /// <summary>
        /// Gets or sets the highest gpra education level.
        /// </summary>
        /// <value>The highest gpra education level.</value>
        public GpraNonResponseTypeDto<LookupValueDto> HighestGpraEducationLevel
        {
            get { return _highestGpraEducationLevel; }
            set { ApplyPropertyChange ( ref _highestGpraEducationLevel, () => HighestGpraEducationLevel, value ); }
        }

        /// <summary>
        /// Gets or sets the non legal pretax income amount.
        /// </summary>
        /// <value>The non legal pretax income amount.</value>
        public GpraNonResponseTypeDto<int?> NonLegalPretaxIncomeAmount
        {
            get { return _nonLegalPretaxIncomeAmount; }
            set { ApplyPropertyChange ( ref _nonLegalPretaxIncomeAmount, () => NonLegalPretaxIncomeAmount, value ); }
        }

        /// <summary>
        /// Gets or sets the other employment type specification note.
        /// </summary>
        /// <value>The other employment type specification note.</value>
        public string OtherEmploymentTypeSpecificationNote
        {
            get { return _otherEmploymentTypeSpecificationNote; }
            set { ApplyPropertyChange ( ref _otherEmploymentTypeSpecificationNote, () => OtherEmploymentTypeSpecificationNote, value ); }
        }

        /// <summary>
        /// Gets or sets the other job training program specification note.
        /// </summary>
        /// <value>The other job training program specification note.</value>
        public string OtherJobTrainingProgramSpecificationNote
        {
            get { return _otherJobTrainingProgramSpecificationNote; }
            set { ApplyPropertyChange ( ref _otherJobTrainingProgramSpecificationNote, () => OtherJobTrainingProgramSpecificationNote, value ); }
        }

        /// <summary>
        /// Gets or sets the other pretax income amount.
        /// </summary>
        /// <value>The other pretax income amount.</value>
        public GpraNonResponseTypeDto<int?> OtherPretaxIncomeAmount
        {
            get { return _otherPretaxIncomeAmount; }
            set { ApplyPropertyChange ( ref _otherPretaxIncomeAmount, () => OtherPretaxIncomeAmount, value ); }
        }

        /// <summary>
        /// Gets or sets the other pretax income specification note.
        /// </summary>
        /// <value>The other pretax income specification note.</value>
        public string OtherPretaxIncomeSpecificationNote
        {
            get { return _otherPretaxIncomeSpecificationNote; }
            set { ApplyPropertyChange ( ref _otherPretaxIncomeSpecificationNote, () => OtherPretaxIncomeSpecificationNote, value ); }
        }

        /// <summary>
        /// Gets or sets the public assistance pretax income amount.
        /// </summary>
        /// <value>The public assistance pretax income amount.</value>
        public GpraNonResponseTypeDto<int?> PublicAssistancePretaxIncomeAmount
        {
            get { return _publicAssistancePretaxIncomeAmount; }
            set { ApplyPropertyChange ( ref _publicAssistancePretaxIncomeAmount, () => PublicAssistancePretaxIncomeAmount, value ); }
        }

        /// <summary>
        /// Gets or sets the retirement pretax income amount.
        /// </summary>
        /// <value>The retirement pretax income amount.</value>
        public GpraNonResponseTypeDto<int?> RetirementPretaxIncomeAmount
        {
            get { return _retirementPretaxIncomeAmount; }
            set { ApplyPropertyChange ( ref _retirementPretaxIncomeAmount, () => RetirementPretaxIncomeAmount, value ); }
        }

        /// <summary>
        /// Gets or sets the wages pretax income amount.
        /// </summary>
        /// <value>The wages pretax income amount.</value>
        public GpraNonResponseTypeDto<int?> WagesPretaxIncomeAmount
        {
            get { return _wagesPretaxIncomeAmount; }
            set { ApplyPropertyChange ( ref _wagesPretaxIncomeAmount, () => WagesPretaxIncomeAmount, value ); }
        }

        #endregion
    }
}
