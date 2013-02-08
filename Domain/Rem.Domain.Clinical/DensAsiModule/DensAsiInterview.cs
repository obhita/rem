using System.Linq;
using Pillar.Common.Utility;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;
using Ethnicity = Rem.WellKnownNames.PatientModule.Ethnicity;
using PatientAddressType = Rem.WellKnownNames.PatientModule.PatientAddressType;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DesAsi Interview is an <see cref="Activity">Activity</see> in the form of a
    /// questionnaire used for assessment of Addiction Severity in a substance abuse
    /// <see cref="Patient">Patient</see> in a given <see
    /// cref="Visit">Visit</see>.
    /// </summary>
    /// <remarks>
    /// DENS stands for Drug Evaluation Network System. 'ASI' stands for Addiction
    /// Severity Index.
    /// </remarks>
    /// <seealso cref="Activity">Activity</seealso>
    /// <seealso cref="Patient">Patient</seealso>
    public class DensAsiInterview : Activity
    {
        private readonly DensAsiClosure _densAsiClosure;
        private readonly DensAsiDrugAlcoholUse _densAsiDrugAlcoholUse;
        private readonly DensAsiDsmIv _densAsiDsmIv;
        private readonly DensAsiEmploymentStatus _densAsiEmploymentStatus;
        private readonly DensAsiFamilySocialRelationships _densAsiFamilySocialRelationships;
        private readonly DensAsiLegalStatus _densAsiLegalStatus;
        private readonly DensAsiMedicalStatus _densAsiMedicalStatus;
        private readonly DensAsiPatientProfile _densAsiPatientProfile;
        private readonly DensAsiPsychiatricStatus _densAsiPsychiatricStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiInterview"/> class.
        /// </summary>
        protected internal DensAsiInterview ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="DensAsiInterview">DensAsiInterview</see> class.
        /// </summary>
        /// <param name="visit">The <see cref="Visit">Visit</see> in
        /// which the <see cref="DensAsiInterview">DensAsiInterview</see> is
        /// performed.</param>
        /// <param name="activityType">Type of the <see
        /// cref="Activity">Activity</see>.</param>
        protected internal DensAsiInterview (
            Visit visit,
            ActivityType activityType )
            : base ( visit, activityType )
        {
            _densAsiPatientProfile = new DensAsiPatientProfile(this);
            ReviseDensAsiPatientProfile(new DensAsiPatientProfileSectionBuilder());
            _densAsiMedicalStatus = new DensAsiMedicalStatus(this);
            _densAsiEmploymentStatus = new DensAsiEmploymentStatus(this);
            _densAsiDrugAlcoholUse = new DensAsiDrugAlcoholUse(this);
            _densAsiLegalStatus = new DensAsiLegalStatus(this);
            _densAsiFamilySocialRelationships = new DensAsiFamilySocialRelationships(this);
            _densAsiPsychiatricStatus = new DensAsiPsychiatricStatus(this);
            _densAsiDsmIv = new DensAsiDsmIv(this);
            _densAsiClosure = new DensAsiClosure(this);
        }

        /// <summary>
        /// Gets the DensAsi Interview patient profile section.
        /// </summary>
        public virtual DensAsiPatientProfile DensAsiPatientProfile
        {
            get { return _densAsiPatientProfile; }
            private set { }
        }

        /// <summary>
        /// Gets the DensAsi Interview medical status section.
        /// </summary>
        public virtual DensAsiMedicalStatus DensAsiMedicalStatus
        {
            get { return _densAsiMedicalStatus; }
            private set { }
        }

        /// <summary>
        /// Gets the DensAsi Interview employment status section.
        /// </summary>
        public virtual DensAsiEmploymentStatus DensAsiEmploymentStatus
        {
            get { return _densAsiEmploymentStatus; }
            private set { }
        }

        /// <summary>
        /// Gets the DensAsi Interview drug alcohol use section.
        /// </summary>
        public virtual DensAsiDrugAlcoholUse DensAsiDrugAlcoholUse
        {
            get { return _densAsiDrugAlcoholUse; }
            private set { }
        }

        /// <summary>
        /// Gets the DensAsi Interview legal status section.
        /// </summary>
        public virtual DensAsiLegalStatus DensAsiLegalStatus
        {
            get { return _densAsiLegalStatus; }
            private set { }
        }

        /// <summary>
        /// Gets the DensAsi Interview family social relationships section.
        /// </summary>
        public virtual DensAsiFamilySocialRelationships DensAsiFamilySocialRelationships
        {
            get { return _densAsiFamilySocialRelationships; }
            private set { }
        }

        /// <summary>
        /// Gets the DensAsi Interview psychiatric status section.
        /// </summary>
        public virtual DensAsiPsychiatricStatus DensAsiPsychiatricStatus
        {
            get { return _densAsiPsychiatricStatus; }
            private set { }
        }

        /// <summary>
        /// Gets the DensAsi Interview DSM IV section.
        /// </summary>
        public virtual DensAsiDsmIv DensAsiDsmIv 
        {
            get { return _densAsiDsmIv; }
            private set { }
        }

        /// <summary>
        /// Gets the DensAsi Interview closure section.
        /// </summary>
        public virtual DensAsiClosure DensAsiClosure
        {
            get { return _densAsiClosure; }
            private set { }
        }

        /// <summary>
        /// Revises the DensAsi Interview patient profile.
        /// </summary>
        /// <param name="densAsiPatientProfile">The dens asi patient profile.</param>
        public virtual void ReviseDensAsiPatientProfile(DensAsiPatientProfileSection densAsiPatientProfile)
        {
            Check.IsNotNull(densAsiPatientProfile, "densAsiPatientProfile is required.");

            densAsiPatientProfile.AdmissionDate = this.Visit.ClinicalCase.ClinicalCaseAdmission == null ? null : this.Visit.ClinicalCase.ClinicalCaseAdmission.AdmissionDate;
            densAsiPatientProfile.PatientAdministrativeGender = this.Visit.ClinicalCase.Patient.Profile.PatientGender.AdministrativeGender;
            densAsiPatientProfile.BirthDate = this.Visit.ClinicalCase.Patient.Profile.BirthDate;
            densAsiPatientProfile.FirstName = this.Visit.ClinicalCase.Patient.Name.First;
            densAsiPatientProfile.LastName = this.Visit.ClinicalCase.Patient.Name.Last;
            densAsiPatientProfile.FirstStreetAddress = this.Visit.ClinicalCase.Patient.Addresses.Where(addr => addr.PatientAddressType.WellKnownName == PatientAddressType.Home).Count() > 0 ? this.Visit.ClinicalCase.Patient.Addresses.Where(addr => addr.PatientAddressType.WellKnownName == PatientAddressType.Home).First().Address.FirstStreetAddress : null;
            densAsiPatientProfile.SecondStreetAddress = this.Visit.ClinicalCase.Patient.Addresses.Where(addr => addr.PatientAddressType.WellKnownName == PatientAddressType.Home).Count() > 0 ? this.Visit.ClinicalCase.Patient.Addresses.Where(addr => addr.PatientAddressType.WellKnownName == PatientAddressType.Home).First().Address.SecondStreetAddress : null;
            densAsiPatientProfile.CityName = this.Visit.ClinicalCase.Patient.Addresses.Where(addr => addr.PatientAddressType.WellKnownName == PatientAddressType.Home).Count() > 0 ? this.Visit.ClinicalCase.Patient.Addresses.Where(addr => addr.PatientAddressType.WellKnownName == PatientAddressType.Home).First().Address.CityName : null;
            densAsiPatientProfile.StateProvince = this.Visit.ClinicalCase.Patient.Addresses.Where(addr => addr.PatientAddressType.WellKnownName == PatientAddressType.Home).Count() > 0 ? this.Visit.ClinicalCase.Patient.Addresses.Where(addr => addr.PatientAddressType.WellKnownName == PatientAddressType.Home).First().Address.StateProvince : null;
            densAsiPatientProfile.PostalCode = this.Visit.ClinicalCase.Patient.Addresses.Where(addr => addr.PatientAddressType.WellKnownName == PatientAddressType.Home).Count() > 0 ? this.Visit.ClinicalCase.Patient.Addresses.Where(addr => addr.PatientAddressType.WellKnownName == PatientAddressType.Home).First().Address.PostalCode: null;
            densAsiPatientProfile.HispanicOrLatinoIndicator = this.Visit.ClinicalCase.Patient.Ethnicity == null ? null : (this.Visit.ClinicalCase.Patient.Ethnicity.Ethnicity.WellKnownName == Ethnicity.HispanicOrLatino ? true : (this.Visit.ClinicalCase.Patient.Ethnicity.Ethnicity.WellKnownName == Ethnicity.NotHispanicOrLatino ? false : (bool?)null));
            densAsiPatientProfile.Race = this.Visit.ClinicalCase.Patient.PrimaryPatientRace == null ? null : this.Visit.ClinicalCase.Patient.PrimaryPatientRace.Race;

            _densAsiPatientProfile.DensAsiPatientProfileSection = densAsiPatientProfile;
        }

        /// <summary>
        /// Revises the DensAsi medical status.
        /// </summary>
        /// <param name="densAsiMedicalStatus">The dens asi medical status.</param>
        public virtual void ReviseDensAsiMedicalStatus(DensAsiMedicalStatusSection densAsiMedicalStatus)
        {
            Check.IsNotNull(densAsiMedicalStatus, "densAsiMedicalStatus is required.");

            _densAsiMedicalStatus.DensAsiMedicalStatusSection = densAsiMedicalStatus;
        }

        /// <summary>
        /// Revises the DensAsi employment status.
        /// </summary>
        /// <param name="densAsiEmploymentStatus">The dens asi employment status.</param>
        public virtual void ReviseDensAsiEmploymentStatus(DensAsiEmploymentStatusSection densAsiEmploymentStatus)
        {
            Check.IsNotNull(densAsiEmploymentStatus, "densAsiEmploymentStatus is required.");

            _densAsiEmploymentStatus.DensAsiEmploymentStatusSection = densAsiEmploymentStatus;
        }

        /// <summary>
        /// Revises the DensAsi drug alcohol use.
        /// </summary>
        /// <param name="densAsiDrugAlcoholUse">The dens asi drug alcohol use.</param>
        public virtual void ReviseDensAsiDrugAlcoholUse(DensAsiDrugAlcoholUseSection densAsiDrugAlcoholUse)
        {
            Check.IsNotNull(densAsiDrugAlcoholUse, "densAsiDrugAlcoholUse is required.");

            _densAsiDrugAlcoholUse.DensAsiDrugAlcoholUseSection = densAsiDrugAlcoholUse;
        }

        /// <summary>
        /// Revises the DensAsi legal status.
        /// </summary>
        /// <param name="densAsiLegalStatus">The dens asi legal status.</param>
        public virtual void ReviseDensAsiLegalStatus(DensAsiLegalStatusSection densAsiLegalStatus)
        {
            Check.IsNotNull(densAsiLegalStatus, "densAsiLegalStatus is required.");

            _densAsiLegalStatus.DensAsiLegalStatusSection = densAsiLegalStatus;
        }

        /// <summary>
        /// Revises the DensAsi family social relationships.
        /// </summary>
        /// <param name="densAsiFamilySocialRelationships">The dens asi family social relationships.</param>
        public virtual void ReviseDensAsiFamilySocialRelationships(DensAsiFamilySocialRelationshipsSection densAsiFamilySocialRelationships)
        {
            Check.IsNotNull(densAsiFamilySocialRelationships, "densAsiFamilySocialRelationships is required.");

            _densAsiFamilySocialRelationships.DensAsiFamilySocialRelationshipsSection = densAsiFamilySocialRelationships;
        }

        /// <summary>
        /// Revises the DensAsi psychiatric status.
        /// </summary>
        /// <param name="densAsiPsychiatricStatus">The dens asi psychiatric status.</param>
        public virtual void ReviseDensAsiPsychiatricStatus(DensAsiPsychiatricStatusSection densAsiPsychiatricStatus)
        {
            Check.IsNotNull(densAsiPsychiatricStatus, "densAsiPsychiatricStatus is required.");

            _densAsiPsychiatricStatus.DensAsiPsychiatricStatusSection = densAsiPsychiatricStatus;
        }

        /// <summary>
        /// Revises the DensAsi DSM IV.
        /// </summary>
        /// <param name="densAsiDsmIv">The dens asi DSM iv.</param>
        public virtual void ReviseDensAsiDsmIv(DensAsiDsmIvSection densAsiDsmIv)
        {
            Check.IsNotNull(densAsiDsmIv, "densAsiDsmIv is required.");

            _densAsiDsmIv.DensAsiDsmIvSection = densAsiDsmIv;
        }

        /// <summary>
        /// Revises the DensAsi closure.
        /// </summary>
        /// <param name="densAsiClosure">The dens asi closure.</param>
        public virtual void ReviseDensAsiClosure(DensAsiClosureSection densAsiClosure)
        {
            Check.IsNotNull(densAsiClosure, "densAsiClosure is required.");

            _densAsiClosure.DensAsiClosureSection = densAsiClosure;
        }
    }
}
