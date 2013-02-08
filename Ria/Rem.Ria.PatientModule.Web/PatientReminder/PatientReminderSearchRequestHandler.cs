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
using AutoMapper;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.WellKnownNames.ClinicalCaseModule;
using Rem.WellKnownNames.CommonModule;
using AllergyStatus = Rem.WellKnownNames.PatientModule.AllergyStatus;
using MedicationStatus = Rem.WellKnownNames.PatientModule.MedicationStatus;

namespace Rem.Ria.PatientModule.Web.PatientReminder
{
    /// <summary>
    /// Class for handling patient reminder search request.
    /// </summary>
    public class PatientReminderSearchRequestHandler :
        NHibernateSessionRequestHandler<PatientReminderSearchRequest, PatientReminderSearchResponse>
    {
        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( PatientReminderSearchRequest request )
        {
            var addressCriteria = DetachedCriteria.For<Patient> ( "p" )
                .CreateCriteria ( "p.Addresses", "addy", JoinType.LeftOuterJoin )
                .CreateCriteria ( "addy.PatientAddressType", JoinType.LeftOuterJoin );

            var phoneCriteria = DetachedCriteria.For<Patient> ( "p" )
                .CreateCriteria ( "p.PhoneNumbers", "ph", JoinType.LeftOuterJoin )
                .CreateCriteria ( "ph.PatientPhoneType", JoinType.LeftOuterJoin );

            var criteria = DetachedCriteria.For<Patient> ( "p" );

            var totalCountCriteria = DetachedCriteria.For<Patient> ( "p" )
                .SetProjection ( Projections.RowCount () );

            FilteredListCriteria ( request, criteria );

            criteria.CreateCriteria ( "Profile.PatientGender", "g", JoinType.LeftOuterJoin );
            ApplyGenderCriteria ( request, criteria );

            FilteredListCriteria ( request, totalCountCriteria );

            //FilteredListCriteria ( request, addressCriteria );
            //FilteredListCriteria ( request, phoneCriteria );

            if ( !string.IsNullOrEmpty ( request.PatientReminderCriteriaDto.SortBy ) )
            {
                AddOrdersForPropertyName (
                    criteria,
                    request.PatientReminderCriteriaDto.SortBy,
                    request.PatientReminderCriteriaDto.SortDirection == ListSortDirection.Ascending );
                AddOrdersForPropertyName (
                    addressCriteria,
                    request.PatientReminderCriteriaDto.SortBy,
                    request.PatientReminderCriteriaDto.SortDirection == ListSortDirection.Ascending );
                AddOrdersForPropertyName (
                    phoneCriteria,
                    request.PatientReminderCriteriaDto.SortBy,
                    request.PatientReminderCriteriaDto.SortDirection == ListSortDirection.Ascending );
            }

            if ( request.PatientReminderCriteriaDto.PageSize > 0 )
            {
                var pageSize = request.PatientReminderCriteriaDto.PageSize;
                var skippedItemCount = request.PatientReminderCriteriaDto.PageIndex * pageSize;
                criteria.SetFirstResult ( skippedItemCount ).SetMaxResults ( pageSize );
                addressCriteria.SetFirstResult ( skippedItemCount ).SetMaxResults ( pageSize );
                phoneCriteria.SetFirstResult ( skippedItemCount ).SetMaxResults ( pageSize );
            }

            var multiCriteria = Session.CreateMultiCriteria ()
                .Add ( criteria )
                .Add ( addressCriteria )
                .Add ( phoneCriteria )
                .Add ( totalCountCriteria );

            multiCriteria.SetResultTransformer ( new DistinctRootEntityResultTransformer () );

            var response = CreateTypedResponse ();

            var criteriaList = multiCriteria.List ();

            var resultList = new List<PatientReminderResultDto> ();
            Mapper.Map ( ( ( IList )criteriaList[0] ).OfType<Patient> (), resultList );
            var resultsDto = new PatientReminderResultsDto
                {
                    Results = resultList,
                    PageIndex = request.PatientReminderCriteriaDto.PageIndex,
                    SortBy = request.PatientReminderCriteriaDto.SortBy,
                    SortDirection = request.PatientReminderCriteriaDto.SortDirection,
                    TotalCount = ( int )( ( ArrayList )criteriaList[3] )[0]
                };
            response.PatientReminderResultsDto = resultsDto;
            return response;
        }

        #endregion

        #region Methods

        private static void AddOrdersForPropertyName ( DetachedCriteria criteria, string name, bool ascending )
        {
            switch ( name )
            {
                case "PatientIdentifier":
                    criteria.AddOrder ( new Order ( Projections.Property ( "p.UniqueIdentifier" ), ascending ) );
                    break;
                case "PatientName":
                    criteria.AddOrder ( new Order ( Projections.Property ( "p.LastName" ), ascending ) )
                        .AddOrder ( new Order ( Projections.Property ( "p.FirstName" ), ascending ) );
                    break;
                case "PatientPrefixName":
                    criteria.AddOrder ( new Order ( Projections.Property ( "p.PrefixName" ), ascending ) );
                    break;
                case "PatientContactPreference":
                    criteria.AddOrder ( new Order ( Projections.Property ( "p.ContactPreference" ), ascending ) );
                    break;
            }
        }

        private static void ApplyGenderCriteria ( PatientReminderSearchRequest request, DetachedCriteria criteria )
        {
            if ( request.PatientReminderCriteriaDto.Gender != null )
            {
                criteria.Add (
                    Restrictions.Eq (
                        Projections.Property ( "g.Key" ),
                        request.PatientReminderCriteriaDto.Gender.Key ) );
            }
        }

        private static void FilteredListCriteria ( PatientReminderSearchRequest request, DetachedCriteria criteria )
        {
            if ( request.PatientReminderCriteriaDto.Age.HasValue )
            {
                var ageDateEnd = DateTime.Now.Date.AddYears ( -1 * request.PatientReminderCriteriaDto.Age.Value );
                var ageDateStart = ageDateEnd.AddYears ( -1 );
                var ageDate = ageDateEnd;
                if ( request.PatientReminderCriteriaDto.AgeFilterModifier == FilterModifier.EqualTo )
                {
                    criteria.Add ( Restrictions.Between ( Projections.Property ( "Profile.BirthDate" ), ageDateStart, ageDateEnd ) );
                }
                else
                {
                    if ( request.PatientReminderCriteriaDto.AgeFilterModifier == FilterModifier.GreaterThan ||
                         request.PatientReminderCriteriaDto.AgeFilterModifier == FilterModifier.LessThenOrEqualTo )
                    {
                        ageDate = ageDateStart;
                    }
                    criteria.Add (
                        Restrictions.Not (
                            PatientListHelper.GetFilterModifierCriterion (
                                Projections.Property ( "Profile.BirthDate" ),
                                request.PatientReminderCriteriaDto.AgeFilterModifier,
                                ageDate ) ) );
                }
            }

            if ( request.PatientReminderCriteriaDto.Medication != null )
            {
                criteria.CreateAlias ( "p.Medications", "m" )
                    .CreateAlias ( "m.MedicationStatus", "ms" )
                    .Add ( Restrictions.Eq ( Projections.Property ( "ms.WellKnownName" ), MedicationStatus.Active ) )
                    .Add (
                        Restrictions.Eq (
                            Projections.Property ( "m.RootMedicationCodedConcept.CodedConceptCode" ),
                            request.PatientReminderCriteriaDto.Medication.CodedConceptCode ) );
            }

            if ( request.PatientReminderCriteriaDto.Allergy != null )
            {
                criteria.CreateAlias ( "p.Allergies", "a" )
                    .CreateAlias ( "a.AllergyStatus", "as" )
                    .Add ( Restrictions.Eq ( Projections.Property ( "as.WellKnownName" ), AllergyStatus.Active ) )
                    .Add (
                        Restrictions.Eq (
                            Projections.Property ( "a.AllergenCodedConcept.CodedConceptCode" ),
                            request.PatientReminderCriteriaDto.Allergy.CodedConceptCode ) );
            }

            var setClinicalCaseAlias = false;
            if ( request.PatientReminderCriteriaDto.Problem != null )
            {
                criteria.CreateAlias ( "p.ClinicalCases", "cc" )
                    .CreateAlias ( "cc.Problems", "pr" )
                    .CreateAlias ( "pr.ProblemStatus", "prs" )
                    .Add ( Restrictions.Eq ( Projections.Property ( "prs.WellKnownName" ), ProblemStatus.Active ) )
                    .Add (
                        Restrictions.Eq (
                            Projections.Property ( "pr.ProblemCodeCodedConcept.CodedConceptCode" ),
                            request.PatientReminderCriteriaDto.Problem.ProblemCodeCodedConcept.CodedConceptCode ) );
                setClinicalCaseAlias = true;
            }

            if ( request.PatientReminderCriteriaDto.LabTest != null || request.PatientReminderCriteriaDto.LabResultValue.HasValue )
            {
                if ( !setClinicalCaseAlias )
                {
                    criteria.CreateAlias ( "p.ClinicalCases", "cc" );
                }
                criteria.CreateAlias ( "cc.Visits", "v" )
                    .CreateAlias ( "v.Activities", "ac" )
                    .CreateAlias ( "ac.LabTests", "test" )
                    .CreateAlias ( "test.LabTestName", "tcc" );
                if ( request.PatientReminderCriteriaDto.LabTest != null )
                {
                    criteria
                        .Add ( Restrictions.Eq ( Projections.Property ( "tcc.Key" ), request.PatientReminderCriteriaDto.LabTest.Key ) );
                }
                if ( request.PatientReminderCriteriaDto.LabResultValue.HasValue )
                {
                    criteria.CreateAlias ( "test.LabResults", "result" );
                    criteria.Add (
                        PatientListHelper.GetFilterModifierCriterion (
                            Projections.Property ( "result.Value" ),
                            request.PatientReminderCriteriaDto.LabResultFilterModifier,
                            request.PatientReminderCriteriaDto.LabResultValue.Value ) );
                }
            }
        }

        #endregion
    }
}
