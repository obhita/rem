using Pillar.Common.Utility;
using Rem.Domain.Clinical.PatientModule;

namespace Rem.Domain.Clinical.ClinicalCaseModule
{
    /// <summary>
    /// The ClinicalCaseFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.ClinicalCaseModule.ClinicalCase">ClinicalCase</see>.
    /// </summary>
    public class ClinicalCaseFactory : IClinicalCaseFactory
    {
        private readonly IClinicalCaseRepository _clinicalCaseRepository;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalCaseFactory"/> class.
        /// </summary>
        /// <param name="clinicalCaseRepository">The clinical case repository.</param>
        public ClinicalCaseFactory ( IClinicalCaseRepository clinicalCaseRepository )
        {
            _clinicalCaseRepository = clinicalCaseRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the clinical case.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="clinicalCaseProfile">The clinical case profile.</param>
        /// <returns>A ClinicalCase.</returns>
        public ClinicalCase CreateClinicalCase (
            Patient patient,
            ClinicalCaseProfile clinicalCaseProfile )
        {
            Check.IsNotNull(patient, "Patient is required.");
            Check.IsNotNull(clinicalCaseProfile, "Clinical case profile is required.");

            long mostRecentCaseNumber = _clinicalCaseRepository.GetMostRecentCaseNumber ( patient.Key );

            var clinicalCase = new ClinicalCase(patient, clinicalCaseProfile, mostRecentCaseNumber);

            _clinicalCaseRepository.MakePersistent ( clinicalCase );

            return clinicalCase;
        }

        /// <summary>
        /// Destroys the clinical case.
        /// </summary>
        /// <param name="clinicalCase">The clinical case.</param>
        public void DestroyClinicalCase ( ClinicalCase clinicalCase )
        {
            _clinicalCaseRepository.MakeTransient ( clinicalCase );
        }

        #endregion
    }
}