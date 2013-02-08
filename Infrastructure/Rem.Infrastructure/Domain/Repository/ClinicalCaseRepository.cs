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
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Clinical.ClinicalCaseModule.ClinicalCase">ClinicalCase</see>.
    /// </summary>
    public class ClinicalCaseRepository : NHibernateRepositoryBase<ClinicalCase>, IClinicalCaseRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalCaseRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public ClinicalCaseRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        #region IClinicalCaseRepository Members

        /// <summary>
        /// Gets a ClinicalCase by key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>A ClinicalCase object.</returns>
        public ClinicalCase GetByKey ( long key )
        {
            return Helper.GetEntityByKey ( key );
        }

        /// <summary>
        /// Save a ClinicalCase.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <returns>A ClinicalCase.</returns>
        public ClinicalCase MakePersistent ( ClinicalCase entity )
        {
            return Helper.MakePersistent ( entity );
        }

        /// <summary>
        /// Deletes a ClinicalCase.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void MakeTransient ( ClinicalCase entity )
        {
            Helper.MakeTransient ( entity );
        }

        /// <summary>
        /// Gets the most recent case number.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>
        /// A long integer.
        /// </returns>
        public long GetMostRecentCaseNumber ( long patientKey )
        {
            string sql =
                "select MAX(cCase.ClinicalCaseNumber) " +
                "from ClinicalCase as cCase " +
                "where cCase.Patient.Key = :patientKey ";

            IQuery patientQuery = Session.CreateQuery ( sql );
            patientQuery.SetParameter ( "patientKey", patientKey );


            var number = ( long? ) patientQuery.UniqueResult ();
            long nextNumber = ( number == null || number.Value == 0 ) ? 1 : number.Value + 1;
            return nextNumber;
        }

        /// <summary>
        /// Gets all clinical cases by patient key.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>
        /// An IEnumerable&lt;ClinicalCase&gt;
        /// </returns>
        public IEnumerable<ClinicalCase> GetAllClinicalCasesByPatientKey ( long patientKey )
        {
            Patient patient = Session.Query<Patient> ()
                .SingleOrDefault ( p => p.Key == patientKey );

            if ( patient == null )
            {
                return null;
            }

            return patient.ClinicalCases;
        }

        /// <summary>
        /// Gets all associated problems by clinical case key.
        /// </summary>
        /// <param name="clinicalCaseKey">The clinical case key.</param>
        /// <returns>
        /// An IList&lt;Problem&gt;.
        /// </returns>
        public IList<Problem> GetAllAssociatedProblemByClinicalCaseKey ( long clinicalCaseKey )
        {
            IQueryable<Problem> problems =
                Session.Query<VisitProblem> ()
                    .Where ( p => p.Problem.ClinicalCase.Key == clinicalCaseKey )
                    .Select ( vd => vd.Problem ).Distinct ();

            return problems.ToList ();
        }

        /// <summary>
        /// Gets all not associated problems by clinical case key.
        /// </summary>
        /// <param name="clinicalCaseKey">The clinical case key.</param>
        /// <returns>
        /// An IList&lt;Problem&gt;.
        /// </returns>
        public IList<Problem> GetAllNotAssociatedProblemsByClinicalCaseKey ( long clinicalCaseKey )
        {
            IList<Problem> associatedProblems = GetAllAssociatedProblemByClinicalCaseKey ( clinicalCaseKey );

            List<Problem> problems = Session.Query<Problem> ().Where ( p => p.ClinicalCase.Key == clinicalCaseKey ).ToList ();

            IEnumerable<Problem> notAssociated = from d in problems
                                                   where !associatedProblems.Contains ( d )
                                                   select d;

            return notAssociated.ToList ();
        }

        /// <summary>
        /// Gets an active clinical case by patient.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>
        /// A ClinicalCase.
        /// </returns>
        public ClinicalCase GetActiveClinicalCaseByPatient ( long patientKey )
        {
            IQueryable<ClinicalCase> clinicalCases =
                Session.Query<ClinicalCase> ().Where ( p => p.Patient.Key == patientKey );

            // ( p.ClinicalCaseStatus == null || p.ClinicalCaseStatus.WellKnownName != "CL" ) );

            return Enumerable.FirstOrDefault ( clinicalCases,
                                               clinicalCase =>
                                               clinicalCase.ClinicalCaseStatus == null 
                                               || clinicalCase.ClinicalCaseStatus.WellKnownName != Rem.WellKnownNames.ClinicalCaseModule.ClinicalCaseStatus.Closed );
        }

        /// <summary>
        /// Gets the most recent closed clinical case by patient.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>
        /// A ClinicalCase.
        /// </returns>
        public ClinicalCase GetMostRecentClosedClinicalCaseByPatient(long patientKey)
        {
            var clinicalCase = Session.Query<ClinicalCase>()
                .Where(x => x.Patient.Key == patientKey
                    && x.ClinicalCaseStatus.WellKnownName == Rem.WellKnownNames.ClinicalCaseModule.ClinicalCaseStatus.Closed)
                .OrderByDescending(x => x.ClinicalCaseCloseInfo.ClinicalCaseCloseDate)
                .FirstOrDefault();

            return clinicalCase;
        }

        #endregion
    }
}