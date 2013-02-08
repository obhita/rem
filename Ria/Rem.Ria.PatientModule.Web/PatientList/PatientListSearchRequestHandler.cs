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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Agatha.Common;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Pillar.Common.Utility;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.LabModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Extension;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.WellKnownNames.CommonModule;
using MedicationStatus = Rem.WellKnownNames.PatientModule.MedicationStatus;
using ProblemStatus = Rem.WellKnownNames.ClinicalCaseModule.ProblemStatus;

namespace Rem.Ria.PatientModule.Web.PatientList
{
    /// <summary>
    /// Class for handling patient list search request.
    /// </summary>
    public class PatientListSearchRequestHandler :
        NHibernateSessionRequestHandler<PatientListSearchRequest, PatientListSearchResponse>
    {
        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( PatientListSearchRequest request )
        {
            var criteria = DetachedCriteria.For<Patient> ()
                .SetFetchMode ( "PatientGender", FetchMode.Eager );
            SetRestrictions ( criteria, request );

            var problemCriteria = DetachedCriteria.For<Problem> ()
                .CreateAlias ( "ClinicalCase", "cc" )
                .CreateAlias ( "cc.Patient", "p" )
                .SetFetchMode ( "ProblemCodeCodedConcept", FetchMode.Join )
                .CreateAlias ( "ProblemStatus", "ps" )
                .Add (
                    Restrictions.And (
                        Restrictions.Eq ( "ps.WellKnownName", ProblemStatus.Active ),
                        Subqueries.PropertyIn (
                            "p.Key",
                            SetRestrictions (
                                DetachedCriteria.For<Patient> ().SetProjection ( Projections.Property<Patient> ( p => p.Key ) ), request ) ) ) );

            var medicationCriteria = DetachedCriteria.For<Medication> ()
                .SetFetchMode ( "MedicationCodeCodedConcept", FetchMode.Join )
                .CreateAlias ( "MedicationStatus", "ms" )
                .CreateAlias ( "Patient", "p" )
                .Add (
                    Restrictions.And (
                        Restrictions.Eq ( "ms.WellKnownName", MedicationStatus.Active ),
                        Subqueries.PropertyIn (
                            "p.Key",
                            SetRestrictions (
                                DetachedCriteria.For<Patient> ().SetProjection ( Projections.Property<Patient> ( p => p.Key ) ), request ) ) ) );

            var labCriteria = DetachedCriteria.For<LabResult> ()
                .SetFetchMode ( "LabTestResultNameCodedConcept", FetchMode.Join )
                .CreateAlias ( "LabTest", "lt" )
                .CreateAlias ( "lt.LabSpecimen", "ls" )
                .CreateAlias ( "ls.Visit", "v" )
                .CreateAlias ( "v.ClinicalCase", "cc" )
                .CreateAlias ( "cc.Patient", "p" )
                .Add (
                    Subqueries.PropertyIn (
                        "p.Key",
                        SetRestrictions ( DetachedCriteria.For<Patient> ().SetProjection ( Projections.Property<Patient> ( p => p.Key ) ), request ) ) );

            var multiCriteria = Session.CreateMultiCriteria ().Add ( criteria )
                .Add ( problemCriteria )
                .Add ( medicationCriteria )
                .Add ( labCriteria );
            multiCriteria.SetResultTransformer ( new DistinctRootEntityResultTransformer () );
            var multiCriteriaList = multiCriteria.List ();
            var patients = ( IList )( multiCriteriaList )[0];
            var problems = ( ( IList )( multiCriteriaList )[1] ).OfType<Problem> ();
            var meds = ( ( IList )( multiCriteriaList )[2] ).OfType<Medication> ();
            var labs = ( ( IList )( multiCriteriaList )[3] ).OfType<LabResult> ();
            var results = new List<PatientListResultDto> ();
            foreach ( Patient patient in patients )
            {
                results.Add (
                    new PatientListResultDto
                        {
                            Key = patient.Key,
                            PatientIdentifier = patient.UniqueIdentifier,
                            PatientName = string.Format ( "{0}, {1}", patient.Name.Last, patient.Name.First ),
                            PatientAge = PatientListHelper.GetAge ( patient.Profile.BirthDate ),
                            PatientGender = patient.Profile.PatientGender == null ? "-" : patient.Profile.PatientGender.Name,
                            PatientActiveMedications =
                                meds.Where ( m => m.Patient.Key == patient.Key ).Select ( m => m.MedicationCodeCodedConcept.DisplayName ).OrderBy (
                                    n => n ).ToList (),
                            PatientActiveProblems =
                                problems.Where ( p => p.ClinicalCase.Patient.Key == patient.Key ).Select (
                                    p => p.ProblemCodeCodedConcept.DisplayName ).OrderBy ( n => n ).ToList (),
                            PatientLabTests =
                                labs.Where (
                                    l => patient.Key == l.LabTest.LabSpecimen.ClinicalCase.Patient.Key )
                                    .Select (
                                              lr => new PatientLabTestSummaryDto
                                                  {
                                                      Key = lr.Key,
                                                      LabTest = lr.LabTestResultNameCodedConcept.DisplayName,
                                                      Results =
                                                          new List<string>
                                                              {
                                                                  string.Format (
                                                                      "{0} {1}",
                                                                      lr.Value,
                                                                      lr.UnitOfMeasureCode )
                                                              }
                                                  } ).ToList ()
                        } );
            }
            if ( results.Count > 0 && !string.IsNullOrEmpty ( request.PatientListCriteriaDto.SortBy ) )
            {
                if ( request.PatientListCriteriaDto.SortBy ==
                     PropertyUtil.ExtractPropertyName ( () => results[0].PatientActiveMedications ) + "String" ||
                     request.PatientListCriteriaDto.SortBy == PropertyUtil.ExtractPropertyName ( () => results[0].PatientActiveProblems ) + "String" ||
                     request.PatientListCriteriaDto.SortBy == PropertyUtil.ExtractPropertyName ( () => results[0].PatientLabTests ) + "String" )
                {
                    results = results.OrderBy (
                        request.PatientListCriteriaDto.SortBy.Replace ( "String", string.Empty ),
                        request.PatientListCriteriaDto.SortDirection == ListSortDirection.Ascending,
                        new EnumerableToStringComparer () ).ToList ();
                }
                else
                {
                    results = results.OrderBy (
                        request.PatientListCriteriaDto.SortBy,
                        request.PatientListCriteriaDto.SortDirection == ListSortDirection.Ascending ).ToList ();
                }
            }
            var response = CreateTypedResponse ();
            response.PatientList = new PatientListResultsDto ();
            if ( request.PatientListCriteriaDto.PageSize > 0 )
            {
                var pageIndex = request.PatientListCriteriaDto.PageIndex == 0 ? 1 : request.PatientListCriteriaDto.PageIndex;
                var pageSize = request.PatientListCriteriaDto.PageSize;
                response.PatientList.Results =
                    results.Skip ( ( pageIndex - 1 ) * pageSize ).Take ( pageSize ).ToList ();
            }
            else
            {
                response.PatientList.Results = results;
            }
            response.PatientList.PageIndex = request.PatientListCriteriaDto.PageIndex;
            response.PatientList.PageSize = Math.Max ( request.PatientListCriteriaDto.PageSize, results.Count );
            response.PatientList.TotalCount = patients.Count;
            return response;
        }

        #endregion

        #region Methods

        private static DetachedCriteria SetRestrictions ( DetachedCriteria criteria, PatientListSearchRequest request )
        {
            if ( request.PatientListCriteriaDto.Age.HasValue )
            {
                var ageDateEnd = DateTime.Now.Date.AddYears ( -1 * request.PatientListCriteriaDto.Age.Value );
                var ageDateStart = ageDateEnd.AddYears ( -1 );
                var ageDate = ageDateEnd;
                if ( request.PatientListCriteriaDto.AgeFilterModifier == FilterModifier.EqualTo )
                {
                    criteria.Add ( Restrictions.Between ( Projections.Property<Patient> ( p => p.Profile.BirthDate ), ageDateStart, ageDateEnd ) );
                }
                else
                {
                    if ( request.PatientListCriteriaDto.AgeFilterModifier == FilterModifier.GreaterThan ||
                         request.PatientListCriteriaDto.AgeFilterModifier == FilterModifier.LessThenOrEqualTo )
                    {
                        ageDate = ageDateStart;
                    }
                    criteria.Add (
                        Restrictions.Not (
                            PatientListHelper.GetFilterModifierCriterion (
                                Projections.Property<Patient> ( p => p.Profile.BirthDate ),
                                request.PatientListCriteriaDto.AgeFilterModifier,
                                ageDate ) ) );
                }
            }
            if ( request.PatientListCriteriaDto.Gender != null )
            {
                criteria.Add (
                    Restrictions.Eq (
                        Projections.Property ( "Gender.Key" ),
                        request.PatientListCriteriaDto.Gender.Key ) );
            }
            if ( request.PatientListCriteriaDto.Medication != null )
            {
                criteria.CreateAlias ( "Medications", "m" )
                    .CreateAlias ( "m.MedicationStatus", "ms" )
                    .Add ( Restrictions.Eq ( Projections.Property ( "ms.WellKnownName" ), MedicationStatus.Active ) )
                    .Add (
                        Restrictions.Or (
                            Restrictions.Eq (
                                Projections.Property ( "m.RootMedicationCodedConcept.CodedConceptCode" ),
                                request.PatientListCriteriaDto.Medication.CodedConceptCode ),
                            Restrictions.Eq (
                                Projections.Property ( "m.MedicationCodeCodedConcept.CodedConceptCode" ),
                                request.PatientListCriteriaDto.Medication.CodedConceptCode ) ) );
            }
            var setClinicalCaseAlias = false;
            if ( request.PatientListCriteriaDto.Problem != null )
            {
                setClinicalCaseAlias = true;
                criteria.CreateAlias ( "ClinicalCases", "cc" )
                    .CreateAlias ( "cc.Problems", "pr" )
                    .CreateAlias ( "pr.ProblemStatus", "prs" )
                    .Add ( Restrictions.Eq ( Projections.Property ( "prs.WellKnownName" ), ProblemStatus.Active ) )
                    .Add (
                        Restrictions.Eq (
                            Projections.Property ( "pr.ProblemCodeCodedConcept.CodedConceptCode" ),
                            request.PatientListCriteriaDto.Problem.ProblemCodeCodedConcept.CodedConceptCode ) );
            }
            if ( request.PatientListCriteriaDto.LabTest != null || request.PatientListCriteriaDto.LabResultValue.HasValue )
            {
                if ( !setClinicalCaseAlias )
                {
                    criteria.CreateAlias ( "ClinicalCases", "cc" );
                }
                criteria.CreateAlias ( "cc.Visits", "v" )
                    .CreateAlias ( "v.Activities", "ac" )
                    .CreateAlias ( "ac.LabTests", "test" )
                    .CreateAlias ( "test.LabResults", "result" )
                    .CreateAlias ( "test.LabTestName", "resultName" );
                if ( request.PatientListCriteriaDto.LabTest != null )
                {
                    criteria
                        .Add (
                            Restrictions.Eq (
                                Projections.Property ( "resultName.Key" ),
                                request.PatientListCriteriaDto.LabTest.Key ) );
                }
                if ( request.PatientListCriteriaDto.LabResultValue.HasValue )
                {
                    criteria.Add (
                        PatientListHelper.GetFilterModifierCriterion (
                            Projections.Property ( "result.Value" ),
                            request.PatientListCriteriaDto.LabResultFilterModifier,
                            request.PatientListCriteriaDto.LabResultValue.Value ) );
                }
            }
            return criteria;
        }

        #endregion
    }
}
