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

using Pillar.Common.Utility;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// The Gpra Interview is an <see cref="Activity">Activity</see> in the form of a
    /// questionnaire used for assessment of substance abuse for a 
    /// <see cref="Patient">Patient</see> in a given <see
    /// cref="Visit">Visit</see>.
    /// </summary>
    /// <remarks>
    /// Gpra stands for Government Performance and Results Act.
    /// </remarks>
    /// <seealso cref="Activity">Activity</seealso>
    /// <seealso cref="Patient">Patient</seealso>
    public class GpraInterview : Activity
    {
        private readonly GpraCrimeCriminalJustice _gpraCrimeCriminalJustice;
        private readonly GpraDemographics _gpraDemographics;
        private readonly GpraDischarge _gpraDischarge;
        private readonly GpraDrugAlcoholUse _gpraDrugAlcoholUse;
        private readonly GpraFamilyLivingConditions _gpraFamilyLivingConditions;
        private readonly GpraFollowUp _gpraFollowUp;
        private readonly GpraPlannedServices _gpraPlannedServices;
        private readonly GpraProblemsTreatmentRecovery _gpraProblemsTreatmentRecovery;
        private readonly GpraProfessionalInformation _gpraProfessionalInformation;
        private readonly GpraSocialConnectedness _gpraSocialConnectedness;

        private GpraInterviewInformation _gpraInterviewInformation;

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraInterview"/> class.
        /// </summary>
        protected internal GpraInterview ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraInterview"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected internal GpraInterview (
            Visit visit,
            ActivityType activityType )
            : base ( visit, activityType )
        {
            _gpraCrimeCriminalJustice = new GpraCrimeCriminalJustice(this);
            _gpraDemographics = new GpraDemographics(this);
            _gpraDischarge = new GpraDischarge(this);
            _gpraDrugAlcoholUse = new GpraDrugAlcoholUse(this);
            _gpraFamilyLivingConditions = new GpraFamilyLivingConditions(this);
            _gpraFollowUp = new GpraFollowUp(this);
            _gpraPlannedServices = new GpraPlannedServices(this);
            _gpraProblemsTreatmentRecovery = new GpraProblemsTreatmentRecovery(this);
            _gpraProfessionalInformation = new GpraProfessionalInformation(this);
            _gpraSocialConnectedness = new GpraSocialConnectedness (this);
        }

        /// <summary>
        /// Gets the Gpra interview information.
        /// </summary>
        public virtual GpraInterviewInformation GpraInterviewInformation
        { 
            get { return _gpraInterviewInformation; }
            private set { ApplyPropertyChange ( ref _gpraInterviewInformation, () => GpraInterviewInformation, value ); }
        }

        /// <summary>
        /// Gets the Gpra planned services.
        /// </summary>
        public virtual GpraPlannedServices GpraPlannedServices
        {
            get { return _gpraPlannedServices; }
            private set { }
        }

        /// <summary>
        /// Gets the Gpra demographics.
        /// </summary>
        public virtual GpraDemographics GpraDemographics
        {
            get { return _gpraDemographics; }
            private set { }
        }

        /// <summary>
        /// Gets the gpra drug alcohol use.
        /// </summary>
        public virtual GpraDrugAlcoholUse GpraDrugAlcoholUse
        {
            get { return _gpraDrugAlcoholUse; }
            private set { }
        }

        /// <summary>
        /// Gets the Gpra family living conditions.
        /// </summary>
        public virtual GpraFamilyLivingConditions GpraFamilyLivingConditions
        {
            get { return _gpraFamilyLivingConditions; }
            private set { }
        }

        /// <summary>
        /// Gets the Gpra professional information.
        /// </summary>
        public virtual GpraProfessionalInformation GpraProfessionalInformation
        {
            get { return _gpraProfessionalInformation; }
            private set { }
        }

        /// <summary>
        /// Gets the Gpra crime criminal justice.
        /// </summary>
        public virtual GpraCrimeCriminalJustice GpraCrimeCriminalJustice
        {
            get { return _gpraCrimeCriminalJustice; }
            private set { }
        }

        /// <summary>
        /// Gets the Gpra problems treatment recovery.
        /// </summary>
        public virtual GpraProblemsTreatmentRecovery GpraProblemsTreatmentRecovery
        {
            get { return _gpraProblemsTreatmentRecovery; }
            private set { }
        }

        /// <summary>
        /// Gets the Gpra social connectedness.
        /// </summary>
        public virtual GpraSocialConnectedness GpraSocialConnectedness
        {
            get { return _gpraSocialConnectedness; }
            private set { }
        }

        /// <summary>
        /// Gets the Gpra follow up.
        /// </summary>
        public virtual GpraFollowUp GpraFollowUp
        {
            get { return _gpraFollowUp; }
            private set { }
        }

        /// <summary>
        /// Gets the Gpra discharge.
        /// </summary>
        public virtual GpraDischarge GpraDischarge
        {
            get { return _gpraDischarge; }
            private set { }
        }

        /// <summary>
        /// Revises the Gpra interview information.
        /// </summary>
        /// <param name="gpraInterviewInformation">The gpra interview information.</param>
        public virtual void ReviseGpraInterviewInformation(GpraInterviewInformation gpraInterviewInformation)
        {
            Check.IsNotNull ( gpraInterviewInformation, () => GpraInterviewInformation );
            GpraInterviewInformation = gpraInterviewInformation;
        }

        /// <summary>
        /// Revises the Gpra crime criminal justice.
        /// </summary>
        /// <param name="gpraCrimeCriminalJustice">The gpra crime criminal justice.</param>
        public virtual void ReviseGpraCrimeCriminalJustice(GpraCrimeCriminalJusticeSection gpraCrimeCriminalJustice)
        {
            Check.IsNotNull(gpraCrimeCriminalJustice, () => GpraCrimeCriminalJustice);
            _gpraCrimeCriminalJustice.GpraCrimeCriminalJusticeSection = gpraCrimeCriminalJustice;
        }

        /// <summary>
        /// Revises the Gpra demographics.
        /// </summary>
        /// <param name="gpraDemographics">The gpra demographics.</param>
        public virtual void ReviseGpraDemographics(GpraDemographicsSection gpraDemographics)
        {
            Check.IsNotNull(gpraDemographics, () => GpraDemographics);
            _gpraDemographics.GpraDemographicsSection = gpraDemographics;
        }

        /// <summary>
        /// Revises the Gpra drug alcohol use.
        /// </summary>
        /// <param name="gpraDrugAlcoholUse">The gpra drug alcohol use.</param>
        public virtual void ReviseGpraDrugAlcoholUse(GpraDrugAlcoholUseSection gpraDrugAlcoholUse)
        {
            Check.IsNotNull(gpraDrugAlcoholUse, () => GpraDrugAlcoholUse);
            _gpraDrugAlcoholUse.GpraDrugAlcoholUseSection = gpraDrugAlcoholUse;
        }

        /// <summary>
        /// Revises the Gpra family living conditions.
        /// </summary>
        /// <param name="gpraFamilyLivingConditions">The gpra family living conditions.</param>
        public virtual void ReviseGpraFamilyLivingConditions(GpraFamilyLivingConditionsSection gpraFamilyLivingConditions)
        {
            Check.IsNotNull(gpraFamilyLivingConditions, () => GpraFamilyLivingConditions);
            _gpraFamilyLivingConditions.GpraFamilyLivingConditionsSection = gpraFamilyLivingConditions;
        }

        /// <summary>
        /// Revises the Gpra planned services.
        /// </summary>
        /// <param name="gpraPlannedServices">The gpra planned services.</param>
        public virtual void ReviseGpraPlannedServices(GpraPlannedServicesSection gpraPlannedServices)
        {
            Check.IsNotNull(gpraPlannedServices, () => GpraPlannedServices);
            _gpraPlannedServices.GpraPlannedServicesSection = gpraPlannedServices;
        }

        /// <summary>
        /// Revises the Gpra problems treatment recovery.
        /// </summary>
        /// <param name="gpraProblemsTreatmentRecovery">The gpra problems treatment recovery.</param>
        public virtual void ReviseGpraProblemsTreatmentRecovery(GpraProblemsTreatmentRecoverySection gpraProblemsTreatmentRecovery)
        {
            Check.IsNotNull(gpraProblemsTreatmentRecovery, () => GpraProblemsTreatmentRecovery);
            _gpraProblemsTreatmentRecovery.GpraProblemsTreatmentRecoverySection = gpraProblemsTreatmentRecovery;
        }

        /// <summary>
        /// Revises the Gpra professional information.
        /// </summary>
        /// <param name="gpraProfessionalInformation">The gpra professional information.</param>
        public virtual void ReviseGpraProfessionalInformation(GpraProfessionalInformationSection gpraProfessionalInformation)
        {
            Check.IsNotNull(gpraProfessionalInformation, () => GpraProfessionalInformation);
           _gpraProfessionalInformation.GpraProfessionalInformationSection = gpraProfessionalInformation;
        }

        /// <summary>
        /// Revises the Gpra social connectedness.
        /// </summary>
        /// <param name="gpraSocialConnectedness">The gpra social connectedness.</param>
        public virtual void ReviseGpraSocialConnectedness(GpraSocialConnectednessSection gpraSocialConnectedness)
        {
            Check.IsNotNull(gpraSocialConnectedness, () => GpraSocialConnectedness);
            _gpraSocialConnectedness.GpraSocialConnectednessSection = gpraSocialConnectedness;
        }

        /// <summary>
        /// Revises the Gpra follow up.
        /// </summary>
        /// <param name="gpraFollowUp">The gpra follow up.</param>
        public virtual void ReviseGpraFollowUp(GpraFollowUpSection gpraFollowUp)
        {
            Check.IsNotNull(gpraFollowUp, () => GpraFollowUp);
            _gpraFollowUp.GpraFollowUpSection = gpraFollowUp;
        }

        /// <summary>
        /// Revises the Gpra discharge.
        /// </summary>
        /// <param name="gpraDischarge">The gpra discharge.</param>
        public virtual void ReviseGpraDischarge(GpraDischargeSection gpraDischarge)
        {
            Check.IsNotNull(gpraDischarge, () => GpraDischarge);
            _gpraDischarge.GpraDischargeSection = gpraDischarge;
        }
    }
}