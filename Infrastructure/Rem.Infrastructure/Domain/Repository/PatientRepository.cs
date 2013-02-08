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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Rem.Domain.Clinical.PatientModule;
using LinqExtensionMethods = NHibernate.Linq.LinqExtensionMethods;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Clinical.PatientModule.Patient">Patient</see>.
    /// </summary>
    public class PatientRepository : NHibernateRepositoryBase<Patient>, IPatientRepository
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public PatientRepository ( ISessionProvider sessionProvider )
            : base ( sessionProvider )
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Finds the patients by advanced search.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">Name of the middle.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="genderWellKnownName">Name of the gender well known.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="motherMaidenName">Name of the mother maiden.</param>
        /// <param name="identifierTypeWellKnownName">Name of the identifier type well known.</param>
        /// <param name="identifier">The identifier.</param>
        /// <param name="addressLineOne">The address line one.</param>
        /// <param name="city">The  city.</param>
        /// <param name="stateWellKnownName">Name of the state well known.</param>
        /// <param name="suffixName">Name of the suffix.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="uniqueIdentifier">The unique identifier.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// A Tuple&lt;int, int, List&lt;Patient&gt;&gt;.
        /// </returns>
        public Tuple<int, int, List<Patient>> FindPatientsByAdvancedSearch (
            string firstName,
            string middleName,
            string lastName,
            string genderWellKnownName,
            DateTime? birthDate,
            string motherMaidenName,
            string identifierTypeWellKnownName,
            string identifier,
            string addressLineOne,
            string city,
            string stateWellKnownName,
            string suffixName,
            string zipCode,
            string uniqueIdentifier,
            int pageIndex,
            int pageSize )
        {
            if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(middleName)
                 && string.IsNullOrWhiteSpace(lastName)
                 && string.IsNullOrWhiteSpace(genderWellKnownName) && !birthDate.HasValue
                 && string.IsNullOrWhiteSpace(motherMaidenName)
                 && string.IsNullOrWhiteSpace(identifierTypeWellKnownName) && string.IsNullOrWhiteSpace(identifier)
                 && string.IsNullOrWhiteSpace(addressLineOne) && string.IsNullOrWhiteSpace(city)
                 && string.IsNullOrWhiteSpace(stateWellKnownName) && string.IsNullOrWhiteSpace(suffixName)
                 && string.IsNullOrWhiteSpace(zipCode)
                 && string.IsNullOrWhiteSpace(uniqueIdentifier))
            {
                throw new ArgumentException ( "No search criteria were specified." );
            }

            if ( pageIndex < 0 )
            {
                throw new ArgumentException ( "Invalid page index.", "pageIndex" );
            }

            if ( pageSize <= 0 )
            {
                throw new ArgumentException ( "Invalide page size.", "pageSize" );
            }

            var sql = new StringBuilder ();
            sql.Append ( "select p from Patient as p " );
            sql.Append ( "join fetch p.Profile.PatientGender Gender " );
            sql.Append ( "where 1 = 1  " );

            if ( !string.IsNullOrWhiteSpace ( firstName ) )
            {
                if ( firstName.Contains ( '*' ) )
                {
                    sql.Append ( "and p.Name.First like :FirstName " );
                }
                else
                {
                    sql.Append ( "and p.Name.First = :FirstName " );
                }
            }
            if ( !string.IsNullOrWhiteSpace ( middleName ) )
            {
                if ( middleName.Contains ( '*' ) )
                {
                    sql.Append ( "and p.Name.Middle like :MiddleName " );
                }
                else
                {
                    sql.Append ( "and p.Name.Middle = :MiddleName " );
                }
            }
            if ( !string.IsNullOrWhiteSpace ( lastName ) )
            {
                if ( lastName.Contains ( '*' ) )
                {
                    sql.Append ( "and p.Name.Last like :LastName " );
                }
                else
                {
                    sql.Append ( "and p.Name.Last = :LastName " );
                }
            }
            if ( !string.IsNullOrWhiteSpace ( genderWellKnownName ) )
            {
                sql.Append ( "and p.Profile.PatientGender.WellKnownName = :GenderWellKnownName " );
            }
            if ( birthDate.HasValue )
            {
                sql.Append ( "and p.Profile.BirthDate = :BirthDate " );
            }
            if ( !string.IsNullOrWhiteSpace ( motherMaidenName ) )
            {
                if ( motherMaidenName.Contains ( '*' ) )
                {
                    sql.Append ( "and p.MotherName.Maiden like :MotherMaidenName " );
                }
                else
                {
                    sql.Append ( "and p.MotherName.Maiden = :MotherMaidenName " );
                }
            }
            if ( !string.IsNullOrWhiteSpace ( identifierTypeWellKnownName ) && !string.IsNullOrWhiteSpace ( identifier ) )
            {
                sql.Append (
                    "and p.Key in (select pi.Patient.Key from PatientIdentifier pi where pi.PatientIdentifierType.WellKnownName = : IdentifierTypeWellKnownName " );
                if ( identifier.Contains ( '*' ) )
                {
                    sql.Append ( "and pi.Identifier like :Identifier " );
                }
                else
                {
                    sql.Append ( "and pi.Identifier = :Identifier " );
                }
                sql.Append ( ")  " );
            }

            if ( !string.IsNullOrWhiteSpace ( suffixName ) )
            {
                if ( suffixName.Contains ( '*' ) )
                {
                    sql.Append ( "and p.Name.Suffix like :SuffixName " );
                }
                else
                {
                    sql.Append ( "and p.Name.Suffix = :SuffixName " );
                }
            }

            if ( !string.IsNullOrWhiteSpace ( addressLineOne ) || !string.IsNullOrWhiteSpace ( city )
                 || !string.IsNullOrWhiteSpace ( stateWellKnownName ) || !string.IsNullOrWhiteSpace ( zipCode ) )
            {
                sql.Append ( "and p.Key in (select pa.Patient.Key from PatientAddress pa where 1=1 " );
                if ( !string.IsNullOrWhiteSpace ( addressLineOne ) )
                {
                    if ( addressLineOne.Contains ( '*' ) )
                    {
                        sql.Append ( "and pa.Address.FirstStreetAddress like :FirstStreetAddress " );
                    }
                    else
                    {
                        sql.Append ( "and pa.Address.FirstStreetAddress = :FirstStreetAddress " );
                    }
                }
                if ( !string.IsNullOrWhiteSpace ( city ) )
                {
                    if ( city.Contains ( '*' ) )
                    {
                        sql.Append("and pa.Address.CityName like :CityName ");
                    }
                    else
                    {
                        sql.Append("and pa.Address.CityName = :CityName ");
                    }
                }
                if ( !string.IsNullOrWhiteSpace ( stateWellKnownName ) )
                {
                    sql.Append("and pa.Address.StateProvince.WellKnownName = :StateProvinceWellKnownName ");
                }
                if ( !string.IsNullOrWhiteSpace ( zipCode ) )
                {
                    if ( zipCode.Contains ( '*' ) )
                    {
                        sql.Append("and pa.Address.PostalCode.Code like :PostalCode ");
                    }
                    else
                    {
                        sql.Append("and pa.Address.PostalCode.Code = :PostalCode ");
                    }
                }
                sql.Append ( ") " );
            }

            if ( !string.IsNullOrWhiteSpace ( uniqueIdentifier ) )
            {
                if ( uniqueIdentifier.Contains ( '*' ) )
                {
                    sql.Append ( "and p.UniqueIdentifier like :UniqueIdentifier " );
                }
                else
                {
                    sql.Append ( "and p.UniqueIdentifier = :UniqueIdentifier " );
                }
            }

            IQuery patientQuery = Session.CreateQuery ( sql.ToString () );

            if ( !string.IsNullOrWhiteSpace ( firstName ) )
            {
                patientQuery.SetParameter ( "FirstName", firstName.Replace ( '*', '%' ) );
            }
            if ( !string.IsNullOrWhiteSpace ( middleName ) )
            {
                patientQuery.SetParameter ( "MiddleName", middleName.Replace ( '*', '%' ) );
            }
            if ( !string.IsNullOrWhiteSpace ( lastName ) )
            {
                patientQuery.SetParameter ( "LastName", lastName.Replace ( '*', '%' ) );
            }
            if ( !string.IsNullOrWhiteSpace ( genderWellKnownName ) )
            {
                patientQuery.SetParameter ( "GenderWellKnownName", genderWellKnownName );
            }
            if ( birthDate.HasValue )
            {
                patientQuery.SetParameter ( "BirthDate", Convert.ToDateTime ( birthDate.Value.ToShortDateString () ) );
            }
            if ( !string.IsNullOrWhiteSpace ( motherMaidenName ) )
            {
                patientQuery.SetParameter ( "MotherMaidenName", motherMaidenName.Replace ( '*', '%' ) );
            }
            if ( !string.IsNullOrWhiteSpace ( identifierTypeWellKnownName ) && !string.IsNullOrWhiteSpace ( identifier ) )
            {
                patientQuery.SetParameter ( "IdentifierTypeWellKnownName", identifierTypeWellKnownName );
                patientQuery.SetParameter ( "Identifier", identifier.Replace ( '*', '%' ) );
            }
            if ( !string.IsNullOrWhiteSpace ( addressLineOne ) )
            {
                patientQuery.SetParameter ( "FirstStreetAddress", addressLineOne.Replace ( '*', '%' ) );
            }
            if ( !string.IsNullOrWhiteSpace ( city ) )
            {
                patientQuery.SetParameter ( "CityName", city.Replace ( '*', '%' ) );
            }
            if ( !string.IsNullOrWhiteSpace ( stateWellKnownName ) )
            {
                patientQuery.SetParameter ( "StateProvinceWellKnownName", stateWellKnownName );
            }
            if ( !string.IsNullOrWhiteSpace ( suffixName ) )
            {
                patientQuery.SetParameter ( "SuffixName", suffixName );
            }
            if ( !string.IsNullOrWhiteSpace ( zipCode ) )
            {
                patientQuery.SetParameter ( "PostalCode", zipCode.Replace ( '*', '%' ) );
            }
            if ( !string.IsNullOrWhiteSpace ( uniqueIdentifier ) )
            {
                patientQuery.SetParameter ( "UniqueIdentifier", uniqueIdentifier.Replace ( '*', '%' ) );
            }

            IList<Patient> patients = patientQuery.List<Patient> ();
            Tuple<int, int, List<Patient>> resultTuple = GetResultTuple ( patients, pageIndex, pageSize );

            return resultTuple;
        }

        /// <summary>
        /// Gets all medications by patient key.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>
        /// A IList&lt;Medication&gt;
        /// </returns>
        public IList<Medication> GetAllMedicationsByPatientKey ( long patientKey )
        {
            IQueryable<Medication> medications = LinqExtensionMethods.Query<Medication> ( Session ).Where ( p => p.Patient.Key == patientKey );
            return medications.ToList ();
        }

        /// <summary>
        /// Gets a Patient by key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>A Patient object.</returns>
        public Patient GetByKey ( long key )
        {
            return Helper.GetEntityByKey ( key );
        }

        /// <summary>
        /// Saves a Patient.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <returns>A Patient object.</returns>
        public Patient MakePersistent ( Patient entity )
        {
            return Helper.MakePersistent ( entity );
        }

        /// <summary>
        /// Deletes a Patient.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void MakeTransient ( Patient entity )
        {
            Helper.MakeTransient ( entity );
        }

        #endregion

        #region Methods

        private Tuple<int, int, List<Patient>> GetResultTuple ( IList<Patient> patients, int pageIndex, int pageSize )
        {
            int totalPatientCount = patients.Count;
            int skippedPatientCount = pageIndex * pageSize;
            if ( skippedPatientCount >= totalPatientCount )
            {
                pageIndex = ( int )Math.Floor ( ( totalPatientCount ) / ( ( decimal )pageSize ) ) - 1;
                skippedPatientCount = pageIndex * pageSize;
            }
            List<Patient> pagedPatientList = patients.Skip ( skippedPatientCount ).Take ( pageSize ).ToList ();

            Tuple<int, int, List<Patient>> resultTuple = Tuple.Create ( totalPatientCount, pageIndex, pagedPatientList );

            return resultTuple;
        }

        #endregion
    }
}
