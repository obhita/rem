using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rem.Domain.Billing.PatientAccountModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// This class defines basic repository services for the <see cref="PatientAccount"/> aggregate.
    /// </summary>
    public class PatientAccountRepository : NHibernateRepositoryBase<PatientAccount>, IPatientAccountRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAccountRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public PatientAccountRepository ( ISessionProvider sessionProvider )
            : base ( sessionProvider )
        {
        }

        /// <summary>
        /// Gets the by medical record number.
        /// </summary>
        /// <param name="medicalRecordNumber">The medical record number.</param>
        /// <returns>
        /// A patient account.
        /// </returns>
        public PatientAccount GetByMedicalRecordNumber(long medicalRecordNumber)
        {
            var patientAccount =
                Session.QueryOver<PatientAccount>()
                .Where(p => p.MedicalRecordNumber == medicalRecordNumber)
                .List()
                .FirstOrDefault();
            return patientAccount;
        }

        /// <summary>
        /// Gets the by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A <see cref="PatientAccount">PatientAccount</see>.</returns>
        public PatientAccount GetByKey(long key)
        {
            return Helper.GetEntityByKey ( key );
        }

        /// <summary>
        /// Makes a PatientAccount persistent.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A <see cref="PatientAccount">PatientAccount</see></returns>
        public PatientAccount MakePersistent(PatientAccount entity)
        {
            return Helper.MakePersistent ( entity );
        }

        /// <summary>
        /// Makes a PatientAccount transient.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void MakeTransient(PatientAccount entity)
        {
            Helper.MakeTransient(entity);
        }
    }
}
