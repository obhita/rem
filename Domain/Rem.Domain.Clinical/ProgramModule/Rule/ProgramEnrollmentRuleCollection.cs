using System;
using Pillar.FluentRuleEngine;
using Rem.Domain.Core.AgencyModule;
using Rem.WellKnownNames.ClinicalCaseModule;

namespace Rem.Domain.Clinical.ProgramModule.Rule
{
    /// <summary>
    /// The ProgramEnrollmentRuleCollection defines rules/rule sets for <see cref="ProgramEnrollment">ProgramEnrollment</see> entity.
    /// </summary>
    public class ProgramEnrollmentRuleCollection : AbstractRuleCollection<ProgramEnrollment>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramEnrollmentRuleCollection"/> class.
        /// </summary>
        public ProgramEnrollmentRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            NewPropertyRule ( () => ClinicalCaseCannotBeClosed )
                .WithProperty ( pe => pe.ClinicalCase.ClinicalCaseStatus.WellKnownName ).NotEqualTo (
                    ClinicalCaseStatus.Closed );

            NewRule ( () => DisenrollmentDateBetweenEnrollmentDateAndProgramOfferingEndDate )
                .When (
                    ( s, ctx ) =>
                        {
                            var disenrollmentDate = ctx.WorkingMemory.GetContextObject<DateTime?> ();
                            return disenrollmentDate < s.EnrollmentDate
                                   ||
                                   ( s.ProgramOffering != null && s.ProgramOffering.EndDate.HasValue && disenrollmentDate > s.ProgramOffering.EndDate );
                        } )
                .ThenReportRuleViolation (
                    ( s, ctx ) => string.Format (
                        "{0} should be between {1} and {2} {3}",
                        ctx.NameProvider.GetName ( s, pe => pe.DisenrollmentDate ),
                        ctx.NameProvider.GetName ( s, pe => pe.EnrollmentDate ),
                        ctx.NameProvider.GetName ( s, pe => pe.ProgramOffering ),
                        ctx.NameProvider.GetName ( s.ProgramOffering, po => po.EndDate ) ) );

            NewPropertyRule ( () => NotDisenrolled )
                .WithProperty ( pe => pe.DisenrollmentDate )
                .LessThen ( DateTime.Today );

            NewRule ( () => EnrollmentDateInStaffEmploymentDateRange )
                .When (
                    ( s, ctx ) =>
                        {
                            var enrollmentDate = ctx.WorkingMemory.GetContextObject<DateTime> ();
                            return !s.EnrollingStaff.StaffProfile.EmploymentDateRange.Includes ( enrollmentDate );
                        } )
                .ThenReportRuleViolation (
                    ( s, ctx ) => string.Format (
                        "{0} {1} is not in {2} {3}.",
                        ctx.NameProvider.GetName ( s ),
                        ctx.NameProvider.GetName ( s, pe => pe.EnrollmentDate ),
                        ctx.NameProvider.GetName ( s.EnrollingStaff ),
                        ctx.NameProvider.GetName ( s.EnrollingStaff.StaffProfile, sp => sp.EmploymentDateRange ) ) );

            NewRule ( () => EnrollmentDateInProgramOfferingDateRange )
                .When (
                    ( s, ctx ) =>
                        {
                            var enrollmentDate = ctx.WorkingMemory.GetContextObject<DateTime> ();
                            return enrollmentDate < s.ProgramOffering.StartDate || enrollmentDate > s.ProgramOffering.EndDate;
                        } )
                .ThenReportRuleViolation (
                    ( s, ctx ) => string.Format (
                        "{0} {1} is not in {2} Date Range.",
                        ctx.NameProvider.GetName ( s ),
                        ctx.NameProvider.GetName ( s, pe => pe.EnrollmentDate ),
                        ctx.NameProvider.GetName ( s.ProgramOffering ) ) );

            NewRule ( () => EnrollingStaffActive )
                .When (
                    ( s, ctx ) =>
                        {
                            var staff = ctx.WorkingMemory.GetContextObject<Staff> ();
                            return !staff.StaffProfile.EmploymentDateRange.Includes ( DateTime.Now );
                        } )
                .ThenReportRuleViolation (
                    ( s, ctx ) => string.Format ( "{0} is not Active.", ctx.NameProvider.GetName ( s, pe => pe.EnrollingStaff ) ) );

            NewRuleSet ( () => DisenrollRuleSet, NotDisenrolled, DisenrollmentDateBetweenEnrollmentDateAndProgramOfferingEndDate );

            NewRuleSet (
                () => CreateProgramEnrollmentRuleSet,
                ClinicalCaseCannotBeClosed,
                EnrollmentDateInProgramOfferingDateRange,
                EnrollmentDateInStaffEmploymentDateRange,
                EnrollingStaffActive );

            NewRuleSet (
                () => ReviseEnrollmentDateRuleSet,
                ClinicalCaseCannotBeClosed,
                EnrollmentDateInProgramOfferingDateRange,
                EnrollmentDateInStaffEmploymentDateRange );

            NewRuleSet (
                () => ReviseEnrollingStaffRuleSet,
                ClinicalCaseCannotBeClosed,
                EnrollingStaffActive );

            NewRuleSet ( () => ReviseCommentsRuleSet, ClinicalCaseCannotBeClosed );

            NewRuleSet ( () => ReviseDaysOnWaitingListCountRuleSet, ClinicalCaseCannotBeClosed );
        }

        /// <summary>
        /// Gets the disenroll rule set.
        /// </summary>
        public IRuleSet DisenrollRuleSet { get; private set; }

        /// <summary>
        /// Gets the revise enrollment date rule set.
        /// </summary>
        public IRuleSet ReviseEnrollmentDateRuleSet { get; private set; }

        /// <summary>
        /// Gets the revise enrolling staff rule set.
        /// </summary>
        public IRuleSet ReviseEnrollingStaffRuleSet { get; private set; }

        /// <summary>
        /// Gets the revise comments rule set.
        /// </summary>
        public IRuleSet ReviseCommentsRuleSet { get; private set; }

        /// <summary>
        /// Gets the revise days on waiting list count rule set.
        /// </summary>
        public IRuleSet ReviseDaysOnWaitingListCountRuleSet { get; private set; }

        /// <summary>
        /// Gets the create program enrollment rule set.
        /// </summary>
        public IRuleSet CreateProgramEnrollmentRuleSet { get; private set; }

        /// <summary>
        /// Gets the clinical case cannot be closed.
        /// </summary>
        public IPropertyRule ClinicalCaseCannotBeClosed { get; private set; }

        /// <summary>
        /// Gets the disenrollment date between enrollment date and program offering end date.
        /// </summary>
        public IRule DisenrollmentDateBetweenEnrollmentDateAndProgramOfferingEndDate { get; private set; }

        /// <summary>
        /// Gets the not disenrolled.
        /// </summary>
        public IPropertyRule NotDisenrolled { get; private set; }

        /// <summary>
        /// Gets the enrollment date in staff employment date range.
        /// </summary>
        public IRule EnrollmentDateInStaffEmploymentDateRange { get; private set; }

        /// <summary>
        /// Gets the enrollment date in program offering date range.
        /// </summary>
        public IRule EnrollmentDateInProgramOfferingDateRange { get; private set; }

        /// <summary>
        /// Gets the enrolling staff active.
        /// </summary>
        public IRule EnrollingStaffActive { get; private set; }
    }
}
