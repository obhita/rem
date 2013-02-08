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

using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <summary>
    /// Data transfer object for GpraInterview class.
    /// </summary>
    public class GpraInterviewDto : ActivityDto
    {
        #region Constants and Fields

        private GpraCrimeCriminalJusticeDto _gpraCrimeCriminalJustice;
        private GpraDemographicsDto _gpraDemographics;
        private GpraDischargeDto _gpraDischarge;
        private GpraDrugAlcoholUseDto _gpraDrugAlcoholUse;
        private GpraFamilyLivingConditionsDto _gpraFamilyLivingConditions;
        private GpraFollowUpDto _gpraFollowUp;
        private GpraInterviewInformationDto _gpraInterviewInfromationDto;
        private GpraPlannedServicesDto _gpraPlannedServices;
        private GpraProblemsTreatmentRecoveryDto _gpraProblemsTreatmentRecovery;
        private GpraProfessionalInformationDto _gpraProfessionalInformation;
        private GpraSocialConnectednessDto _gpraSocialConnectedness;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the gpra crime criminal justice.
        /// </summary>
        /// <value>The gpra crime criminal justice.</value>
        public GpraCrimeCriminalJusticeDto GpraCrimeCriminalJustice
        {
            get { return _gpraCrimeCriminalJustice; }
            set { RaisePropertyChanged ( ref _gpraCrimeCriminalJustice, () => GpraCrimeCriminalJustice, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra demographics.
        /// </summary>
        /// <value>The gpra demographics.</value>
        public GpraDemographicsDto GpraDemographics
        {
            get { return _gpraDemographics; }
            set { RaisePropertyChanged ( ref _gpraDemographics, () => GpraDemographics, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra discharge.
        /// </summary>
        /// <value>The gpra discharge.</value>
        public virtual GpraDischargeDto GpraDischarge
        {
            get { return _gpraDischarge; }
            set { RaisePropertyChanged ( ref _gpraDischarge, () => GpraDischarge, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra drug alcohol use.
        /// </summary>
        /// <value>The gpra drug alcohol use.</value>
        public GpraDrugAlcoholUseDto GpraDrugAlcoholUse
        {
            get { return _gpraDrugAlcoholUse; }
            set { RaisePropertyChanged ( ref _gpraDrugAlcoholUse, () => GpraDrugAlcoholUse, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra family living conditions.
        /// </summary>
        /// <value>The gpra family living conditions.</value>
        public GpraFamilyLivingConditionsDto GpraFamilyLivingConditions
        {
            get { return _gpraFamilyLivingConditions; }
            set { RaisePropertyChanged ( ref _gpraFamilyLivingConditions, () => GpraFamilyLivingConditions, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra follow up.
        /// </summary>
        /// <value>The gpra follow up.</value>
        public virtual GpraFollowUpDto GpraFollowUp
        {
            get { return _gpraFollowUp; }
            set { RaisePropertyChanged ( ref _gpraFollowUp, () => GpraFollowUp, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra interview infromation dto.
        /// </summary>
        /// <value>The gpra interview infromation dto.</value>
        public virtual GpraInterviewInformationDto GpraInterviewInfromationDto
        {
            get { return _gpraInterviewInfromationDto; }
            set { RaisePropertyChanged ( ref _gpraInterviewInfromationDto, () => GpraInterviewInfromationDto, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra planned services.
        /// </summary>
        /// <value>The gpra planned services.</value>
        public GpraPlannedServicesDto GpraPlannedServices
        {
            get { return _gpraPlannedServices; }
            set { RaisePropertyChanged ( ref _gpraPlannedServices, () => GpraPlannedServices, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra problems treatment recovery.
        /// </summary>
        /// <value>The gpra problems treatment recovery.</value>
        public GpraProblemsTreatmentRecoveryDto GpraProblemsTreatmentRecovery
        {
            get { return _gpraProblemsTreatmentRecovery; }
            set { RaisePropertyChanged ( ref _gpraProblemsTreatmentRecovery, () => GpraProblemsTreatmentRecovery, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra professional information.
        /// </summary>
        /// <value>The gpra professional information.</value>
        public GpraProfessionalInformationDto GpraProfessionalInformation
        {
            get { return _gpraProfessionalInformation; }
            set { RaisePropertyChanged ( ref _gpraProfessionalInformation, () => GpraProfessionalInformation, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra social connectedness.
        /// </summary>
        /// <value>The gpra social connectedness.</value>
        public GpraSocialConnectednessDto GpraSocialConnectedness
        {
            get { return _gpraSocialConnectedness; }
            set { RaisePropertyChanged ( ref _gpraSocialConnectedness, () => GpraSocialConnectedness, value ); }
        }

        #endregion
    }
}
