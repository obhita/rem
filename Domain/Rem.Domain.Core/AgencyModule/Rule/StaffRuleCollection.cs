using System.Linq;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.FluentRuleEngine;

namespace Rem.Domain.Core.AgencyModule.Rule
{
    /// <summary>
    /// The StaffRuleCollection defines rules/rule sets for <see cref="Staff">Staff</see>.
    /// </summary>
    public class StaffRuleCollection : AbstractRuleCollection<Staff>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaffRuleCollection"/> class.
        /// </summary>
        public StaffRuleCollection ()
        {
            BuildAddAddressRuleSet ();
            BuildAddCertificationRuleSet ();
            BuildAddCollegeDegreeRuleSet ();
            BuildAddLanguageRuleSet ();
            BuildAddIdentifierRuleSet ();
            BuildAddLicenseRuleSet ();
            BuildAddPhoneRuleSet ();
            BuildAddTrainingCourseRuleSet ();
            BuildAssignLocationRuleSet ();
            BuildSetPrimaryLocationRuleSet ();
        }

        /// <summary>
        /// Gets the add address rule set.
        /// </summary>
        public IRuleSet AddAddressRuleSet { get; private set; }

        /// <summary>
        /// Gets the add certification rule set.
        /// </summary>
        public IRuleSet AddCertificationRuleSet { get; private set; }

        /// <summary>
        /// Gets the add college degree rule set.
        /// </summary>
        public IRuleSet AddCollegeDegreeRuleSet { get; private set; }

        /// <summary>
        /// Gets the add language rule set.
        /// </summary>
        public IRuleSet AddLanguageRuleSet { get; private set; }

        /// <summary>
        /// Gets the add identifier rule set.
        /// </summary>
        public IRuleSet AddIdentifierRuleSet { get; private set; }

        /// <summary>
        /// Gets the add license rule set.
        /// </summary>
        public IRuleSet AddLicenseRuleSet { get; private set; }

        /// <summary>
        /// Gets the add phone rule set.
        /// </summary>
        public IRuleSet AddPhoneRuleSet { get; private set; }

        /// <summary>
        /// Gets the add training course rule set.
        /// </summary>
        public IRuleSet AddTrainingCourseRuleSet { get; private set; }

        /// <summary>
        /// Gets the assign location rule set.
        /// </summary>
        public IRuleSet AssignLocationRuleSet { get; private set; }

        /// <summary>
        /// Gets the set primary location rule set.
        /// </summary>
        public IRuleSet SetPrimaryLocationRuleSet { get; private set; }

        /// <summary>
        /// Gets the no duplicate addresses with context.
        /// </summary>
        public IRule NoDuplicateAddressesWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate certifications with context.
        /// </summary>
        public IRule NoDuplicateCertificationsWithContext { get; private set; }

        /// <summary>
        /// Gets the certification start date not empty.
        /// </summary>
        public IRule CertificationStartDateNotEmpty { get; private set; }

        /// <summary>
        /// Gets the no duplicate college degrees with context.
        /// </summary>
        public IRule NoDuplicateCollegeDegreesWithContext { get; private set; }

        /// <summary>
        /// Gets the college degree earned date not empty.
        /// </summary>
        public IRule CollegeDegreeEarnedDateNotEmpty { get; private set; }

        /// <summary>
        /// Gets the no duplicate languages with context.
        /// </summary>
        public IRule NoDuplicateLanguagesWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate identifiers with context.
        /// </summary>
        public IRule NoDuplicateIdentifiersWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate licenses with context.
        /// </summary>
        public IRule NoDuplicateLicensesWithContext { get; private set; }

        /// <summary>
        /// Gets the license start date not empty.
        /// </summary>
        public IRule LicenseStartDateNotEmpty { get; private set; }

        /// <summary>
        /// Gets the no duplicate phone numbers with context.
        /// </summary>
        public IRule NoDuplicatePhoneNumbersWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate training courses with context.
        /// </summary>
        public IRule NoDuplicateTrainingCoursesWithContext { get; private set; }

        /// <summary>
        /// Gets the training course completed date not empty.
        /// </summary>
        public IRule TrainingCourseCompletedDateNotEmpty { get; private set; }

        /// <summary>
        /// Gets the location must be in same agency as staff.
        /// </summary>
        public IRule LocationMustBeInSameAgencyAsStaff { get; private set; }

        /// <summary>
        /// Gets the no duplicate locations with context.
        /// </summary>
        public IRule NoDuplicateLocationsWithContext { get; private set; }

        /// <summary>
        /// Gets the primary location must be in assigned locations.
        /// </summary>
        public IRule PrimaryLocationMustBeInAssignedLocations { get; private set; }

        private void BuildSetPrimaryLocationRuleSet ()
        {
            NewRule ( () => PrimaryLocationMustBeInAssignedLocations )
                .When (
                    ( s, ctx ) =>
                        {
                            var location = ctx.WorkingMemory.GetContextObject<Location> ();
                            return !Enumerable.Select<StaffLocationAssignment, Location> ( s.StaffLocationAssignments, sla => sla.Location ).Contains ( location );
                        } )
                .ThenReportRuleViolation (
                    ( s, ctx ) =>
                    string.Format (
                        "{0} must be in the list of {1}",
                        ctx.NameProvider.GetName ( s, st => st.PrimaryLocation ),
                        ctx.NameProvider.GetName ( s, st => st.StaffLocationAssignments ) ),
                    null,
                    PropertyUtil.ExtractPropertyName<Staff, Location> ( s => s.PrimaryLocation ) );

            NewRuleSet ( () => SetPrimaryLocationRuleSet, PrimaryLocationMustBeInAssignedLocations );
        }

        private void BuildAssignLocationRuleSet ()
        {
            NewRule ( () => NoDuplicateLocationsWithContext )
                .OnContextObject<Location> ()
                .NoDuplicates ( ctx => Enumerable.Select<StaffLocationAssignment, Location> ( ctx.Subject.StaffLocationAssignments, sla => sla.Location ) );

            NewRule ( () => LocationMustBeInSameAgencyAsStaff )
                .When (
                    ( s, ctx ) =>
                        {
                            var location = ctx.WorkingMemory.GetContextObject<Location> ();
                            return location.Agency.Key != s.Agency.Key;
                        } )
                .ThenReportRuleViolation (
                    ( s, ctx ) => string.Format (
                        "The {0} for {1} and {2} must be the same.",
                        ctx.NameProvider.GetName ( s.Agency ),
                        ctx.NameProvider.GetName ( s ),
                        ctx.NameProvider.GetName ( ctx.WorkingMemory.GetContextObject<Location> () ) ) );

            NewRuleSet ( () => AssignLocationRuleSet, NoDuplicateLocationsWithContext, LocationMustBeInSameAgencyAsStaff );
        }

        private void BuildAddTrainingCourseRuleSet ()
        {
            NewRule ( () => NoDuplicateTrainingCoursesWithContext )
                .OnContextObject<StaffTrainingCourse> ()
                .NoDuplicates ( ctx => ctx.Subject.TrainingCourses );

            ShouldRunWhen (
                ( s, ctx ) =>
                    {
                        var course = ctx.WorkingMemory.GetContextObject<StaffTrainingCourse> ();
                        return course != null;
                    },
                () =>
                    {
                        NewRule ( () => TrainingCourseCompletedDateNotEmpty )
                            .OnContextObject<StaffTrainingCourse> ()
                            .WithProperty ( tc => tc.CompletedDate )
                            .NotNull ();
                    } );

            NewRuleSet ( () => AddTrainingCourseRuleSet, NoDuplicateTrainingCoursesWithContext, TrainingCourseCompletedDateNotEmpty );
        }

        private void BuildAddPhoneRuleSet ()
        {
            NewRule ( () => NoDuplicatePhoneNumbersWithContext )
                .OnContextObject<StaffPhone> ()
                .NoDuplicates ( ctx => ctx.Subject.PhoneNumbers );

            NewRuleSet ( () => AddPhoneRuleSet, NoDuplicatePhoneNumbersWithContext );
        }

        private void BuildAddLicenseRuleSet ()
        {
            NewRule ( () => NoDuplicateLicensesWithContext )
                .OnContextObject<StaffLicense> ()
                .NoDuplicates ( ctx => ctx.Subject.Licenses );

            ShouldRunWhen (
                ( s, ctx ) =>
                    {
                        var license = ctx.WorkingMemory.GetContextObject<StaffLicense> ();
                        return license != null && license.EffectiveDateRange != null;
                    },
                () =>
                    {
                        NewRule ( () => LicenseStartDateNotEmpty )
                            .OnContextObject<StaffLicense> ()
                            .WithProperty ( l => l.EffectiveDateRange.StartDate )
                            .NotNull ();
                    } );

            NewRuleSet ( () => AddLicenseRuleSet, NoDuplicateLicensesWithContext, LicenseStartDateNotEmpty );
        }

        private void BuildAddIdentifierRuleSet ()
        {
            NewRule ( () => NoDuplicateIdentifiersWithContext )
                .OnContextObject<StaffIdentifier> ()
                .NoDuplicates ( ctx => ctx.Subject.Identifiers );

            NewRuleSet ( () => AddIdentifierRuleSet, NoDuplicateIdentifiersWithContext );
        }

        private void BuildAddLanguageRuleSet ()
        {
            NewRule ( () => NoDuplicateLanguagesWithContext )
                .OnContextObject<StaffLanguage> ()
                .NoDuplicates ( ctx => ctx.Subject.Languages );

            NewRuleSet ( () => AddLanguageRuleSet, NoDuplicateLanguagesWithContext );
        }

        private void BuildAddCollegeDegreeRuleSet ()
        {
            NewRule ( () => NoDuplicateCollegeDegreesWithContext )
                .OnContextObject<StaffCollegeDegree> ()
                .NoDuplicates ( ctx => ctx.Subject.CollegeDegrees );

            ShouldRunWhen (
                ( s, ctx ) =>
                    {
                        var degree = ctx.WorkingMemory.GetContextObject<StaffCollegeDegree> ();
                        return degree != null;
                    },
                () =>
                    {
                        NewRule ( () => CollegeDegreeEarnedDateNotEmpty )
                            .OnContextObject<StaffCollegeDegree> ()
                            .WithProperty ( cd => cd.EarnedDate )
                            .NotNull ();
                    } );

            NewRuleSet ( () => AddCollegeDegreeRuleSet, NoDuplicateCollegeDegreesWithContext, CollegeDegreeEarnedDateNotEmpty );
        }

        private void BuildAddCertificationRuleSet ()
        {
            NewRule ( () => NoDuplicateCertificationsWithContext )
                .OnContextObject<StaffCertification> ()
                .NoDuplicates ( ctx => ctx.Subject.Certifications );

            ShouldRunWhen (
                ( s, ctx ) =>
                    {
                        var certification = ctx.WorkingMemory.GetContextObject<StaffCertification> ();
                        return certification != null && certification.EffectiveDateRange != null;
                    },
                () =>
                    {
                        NewRule ( () => CertificationStartDateNotEmpty )
                            .OnContextObject<StaffCertification> ()
                            .WithProperty ( sc => sc.EffectiveDateRange.StartDate )
                            .NotNull ();
                    } );

            NewRuleSet ( () => AddCertificationRuleSet, NoDuplicateCertificationsWithContext, CertificationStartDateNotEmpty );
        }

        private void BuildAddAddressRuleSet ()
        {
            NewRule ( () => NoDuplicateAddressesWithContext )
                .OnContextObject<StaffAddress> ()
                .NoDuplicates ( ctx => ctx.Subject.Addresses );

            NewRuleSet ( () => AddAddressRuleSet, NoDuplicateAddressesWithContext );
        }
    }
}
