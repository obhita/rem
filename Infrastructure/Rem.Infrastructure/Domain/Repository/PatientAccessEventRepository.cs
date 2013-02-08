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
using Microsoft.IdentityModel.Protocols.WSIdentity;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using Rem.Domain.Clinical.PatientModule;
using PatientAccessEventType = Rem.WellKnownNames.PatientModule.PatientAccessEventType;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Clinical.PatientModule.PatientAccessEvent">PatientAccessEvent</see>.
    /// </summary>
    public class PatientAccessEventRepository : NHibernateRepositoryBase<PatientAccessEvent>, IPatientAccessEventRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAccessEventRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public PatientAccessEventRepository ( ISessionProvider sessionProvider )
            : base ( sessionProvider )
        {
        }

        /// <summary>
        /// Gets a PatientAccessEvent by key.
        /// </summary>
        /// <param name="patientAccessEventKey">The patient access event key.</param>
        /// <returns>
        /// A PatientAccessEvent.
        /// </returns>
        public PatientAccessEvent GetByKey ( long patientAccessEventKey )
        {
            var patientAccessEvent = Helper.GetEntityByKey ( patientAccessEventKey );

            return patientAccessEvent;
        }

        /// <summary>
        /// Determines whether [is patient read access audited today] for [the specified patient access event].
        /// </summary>
        /// <param name="patientAccessEvent">The patient access event.</param>
        /// <param name="systemAcountKey">The system acount key.</param>
        /// <returns>
        ///   <c>true</c> if [is patient read access audited today] [the specified patient access event]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPatientReadAccessAuditedToday(PatientAccessEvent patientAccessEvent, long systemAcountKey)
        {
            if (patientAccessEvent.PatientAccessEventType.WellKnownName != PatientAccessEventType.ReadEvent)
            {
                throw new InvalidInputException("patientAccessEvent must be a ReadEvent type.");
            }

            var count = ( Session.Query<PatientAccessEvent> ().Where (
                p => (
                         p.Patient.Key == patientAccessEvent.Patient.Key &&
                         p.CreatedBySystemAccount.Key == systemAcountKey &&
                         p.PatientAccessEventType.Key == patientAccessEvent.PatientAccessEventType.Key &&
                         p.AuditedContextDescription == patientAccessEvent.AuditedContextDescription &&
                         p.CreatedTimestamp >= DateTime.Today &&
                         p.CreatedTimestamp < DateTime.Today.AddDays(1)
                     ) ) ).Count ();

            return count > 0;
        }

        /// <summary>
        /// Saves a PatientAccessEvent.
        /// </summary>
        /// <param name="patientAccessEvent">The patient access event.</param>
        /// <returns>
        /// A PatientAccessEvent.
        /// </returns>
        public PatientAccessEvent MakePersistent ( PatientAccessEvent patientAccessEvent )
        {
            return Helper.MakePersistent ( patientAccessEvent );
        }

        /// <summary>
        /// Finds the patient access events by search criteria.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <param name="userKey">The user key.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="accessType">Type of the access.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortingMemberName">Name of the sorting member.</param>
        /// <param name="secondOrderSortBy">The second order sort by.</param>
        /// <param name="secondOrderSortDirection">The second order sort direction.</param>
        /// <returns>
        /// A Tuple&lt;int, int, List&lt;PatientAccessEvent&gt;&gt;.
        /// </returns>
        public System.Tuple<int, int, List<PatientAccessEvent>> FindPatientAccessEventsBySearchCriteria (
            long? patientKey,
            long? userKey,
            DateTime? startDate,
            DateTime? endDate,
            string accessType,
            int pageIndex,
            int pageSize,
            string sortingMemberName,
            string secondOrderSortBy,
            string secondOrderSortDirection )
        {
            if ( pageIndex < 0 )
            {
                throw new ArgumentException ( "Invalid page index.", "pageIndex" );
            }

            if ( pageSize <= 0 )
            {
                throw new ArgumentException ( "Invalid page size.", "pageSize" );
            }

            DateTimeOffset startDateTimeOffset = startDate.HasValue
                                                     ? new DateTimeOffset ( startDate.Value, new TimeSpan ( 0 ) )
                                                     : DateTimeOffset.MinValue;

            DateTimeOffset endDateTimeOffset = endDate.HasValue
                                                   ? new DateTimeOffset (
                                                         endDate.Value.AddMilliseconds ( 86400000 - 1 ),
                                                         new TimeSpan ( 0 ) )
                                                   : DateTimeOffset.MaxValue;

            // Joins
            var patientAccessEventQuery = Session.CreateCriteria<PatientAccessEvent> ( "pae" )
                .SetFetchMode ( "PatientAccessEventType", FetchMode.Eager )
                .CreateAlias ( "PatientAccessEventType", "patientaccesseventtype" )
                .SetFetchMode ( "Patient", FetchMode.Eager )
                .CreateAlias ( "Patient", "patient" )
                .SetFetchMode ( "CreatedBySystemAccount", FetchMode.Eager )
                .CreateAlias ( "CreatedBySystemAccount", "systemaccount" )
                .Add ( Restrictions.Between ( "pae.CreatedTimestamp", startDateTimeOffset, endDateTimeOffset ) );

            if ( patientKey != 0 )
            {
                patientAccessEventQuery.Add ( Restrictions.Eq ( "patient.Key", patientKey ) );
            }

            if ( userKey != 0 )
            {
                patientAccessEventQuery.Add ( Restrictions.Eq ( "pae.CreatedBySystemAccount.Key", userKey ) );
            }

            if ( !string.IsNullOrWhiteSpace ( accessType ) )
            {
                patientAccessEventQuery.Add ( Restrictions.Eq ( "patientaccesseventtype.WellKnownName", accessType ) );
            }

            // Sorting
            FirstOrderGroupingSort ( patientAccessEventQuery, sortingMemberName );
            secondOrderSortBy = SecondOrderDirectionalSort ( patientAccessEventQuery, sortingMemberName, secondOrderSortBy, secondOrderSortDirection );

            // Default sort.
            if ( sortingMemberName == null && secondOrderSortBy == null )
            {
                patientAccessEventQuery.AddOrder ( Order.Desc ( "pae.CreatedTimestamp" ) );
            }

            var patientAccessEvents = patientAccessEventQuery.List<PatientAccessEvent> ();
            var resultTuple = GetResultTuple ( patientAccessEvents, pageIndex, pageSize );
            return resultTuple;
        }

        private static string SecondOrderDirectionalSort ( ICriteria patientAccessEventQuery, string sortingMemberName, string secondOrderSortBy, string secondOrderSortDirection )
        {
            if ( secondOrderSortBy != null && ( sortingMemberName != secondOrderSortBy ) )
            {
                switch ( secondOrderSortBy )
                {
                    case "PatientName":
                        secondOrderSortBy = "patient.Name.Last";
                        break;
                    case "UserName":
                        secondOrderSortBy = "systemaccount.Identifier";
                        break;
                    case "PatientAccessEventTypeName":
                        secondOrderSortBy = "patientaccesseventtype.WellKnownName";
                        break;
                    case "CreatedTimestamp":
                        secondOrderSortBy = "pae.CreatedTimestamp";
                        break;
                    default:
                        break;
                }

                patientAccessEventQuery.AddOrder ( secondOrderSortDirection == "Ascending"
                                                       ? Order.Asc ( secondOrderSortBy )
                                                       : Order.Desc ( secondOrderSortBy ) );
            }
            return secondOrderSortBy;
        }

        private static void FirstOrderGroupingSort ( ICriteria patientAccessEventQuery, string sortingMemberName )
        {
            if ( sortingMemberName == null )
            {
                return;
            }
            switch ( sortingMemberName )
            {
                case "PatientName":
                    patientAccessEventQuery.AddOrder ( Order.Asc ( "patient.Name.Last" ) );
                    break;
                case "UserName":
                    patientAccessEventQuery.AddOrder ( Order.Asc ( "systemaccount.Identifier" ) );
                    break;
                case "PatientAccessEventTypeName":
                    patientAccessEventQuery.AddOrder ( Order.Asc ( "patientaccesseventtype.WellKnownName" ) );
                    break;
                default:
                    break;
            }
        }

        private static System.Tuple<int, int, List<PatientAccessEvent>> GetResultTuple (
            IList<PatientAccessEvent> patientAccessEvents, int pageIndex, int pageSize )
        {
            var totalItemCount = patientAccessEvents.Count;
            var skippedItemCount = pageIndex * pageSize;

            if ( skippedItemCount >= totalItemCount )
            {
                pageIndex = ( int ) Math.Floor ( totalItemCount / ( ( decimal ) pageSize ) ) - 1;
                skippedItemCount = pageIndex * pageSize;
            }

            var pagedPatientList = patientAccessEvents.Skip ( skippedItemCount ).Take ( pageSize ).ToList ();
            var resultTuple = Tuple.Create ( totalItemCount, pageIndex, pagedPatientList );
            return resultTuple;
        }
    }
}