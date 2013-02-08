using System.Linq;
using Pillar.Common;
using Pillar.Domain;
using Pillar.FluentRuleEngine;

namespace Rem.Domain.Core.AgencyModule.Rule
{
    /// <summary>
    /// The LocationRuleCollection defines ro rule sets for <see cref="Location">Location</see>.
    /// </summary>
    public class LocationRuleCollection : AbstractRuleCollection<Location>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationRuleCollection"/> class.
        /// </summary>
        public LocationRuleCollection ()
        {
            BuildAddAddressAndPhoneRuleSet ();
            BuildAddIdentifierRuleSet ();
            BuildAddContactRuleSet ();
            BuildAddOperationScheduleRuleSet ();
            BuildAddEmailAddressRuleSet ();
            BuildAddPhoneRuleSet ();
            BuildReviseLocationAddressRuleSet ();
            BuildAddWorkHourRuleSet ();
            BuildReviseNameRuleSet ();

            BuildCreateLocationRuleSet ();
        }

        /// <summary>
        /// Gets the add address and phone rule set.
        /// </summary>
        public IRuleSet AddAddressAndPhoneRuleSet { get; private set; }

        /// <summary>
        /// Gets the add identifier rule set.
        /// </summary>
        public IRuleSet AddIdentifierRuleSet { get; private set; }

        /// <summary>
        /// Gets the add contact rule set.
        /// </summary>
        public IRuleSet AddContactRuleSet { get; private set; }

        /// <summary>
        /// Gets the add operation schedule rule set.
        /// </summary>
        public IRuleSet AddOperationScheduleRuleSet { get; private set; }

        /// <summary>
        /// Gets the add email address rule set.
        /// </summary>
        public IRuleSet AddEmailAddressRuleSet { get; private set; }

        /// <summary>
        /// Gets the add phone rule set.
        /// </summary>
        public IRuleSet AddPhoneRuleSet { get; private set; }

        /// <summary>
        /// Gets the revise location address rule set.
        /// </summary>
        public IRuleSet ReviseLocationAddressRuleSet { get; private set; }

        /// <summary>
        /// Gets the add work hour rule set.
        /// </summary>
        public IRuleSet AddWorkHourRuleSet { get; private set; }

        /// <summary>
        /// Gets the revise name rule set.
        /// </summary>
        public IRuleSet ReviseNameRuleSet { get; private set; }

        /// <summary>
        /// Gets the create location rule set.
        /// </summary>
        public IRuleSet CreateLocationRuleSet { get; private set; }

        /// <summary>
        /// Gets the no duplicate address and phones with context.
        /// </summary>
        public IRule NoDuplicateAddressAndPhonesWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate identifierss with context.
        /// </summary>
        public IRule NoDuplicateIdentifierssWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate contacts with context.
        /// </summary>
        public IRule NoDuplicateContactsWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate operation schedules with context.
        /// </summary>
        public IRule NoDuplicateOperationSchedulesWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate email addresses with context.
        /// </summary>
        public IRule NoDuplicateEmailAddressesWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate phone numbers with context.
        /// </summary>
        public IRule NoDuplicatePhoneNumbersWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate addresses with context.
        /// </summary>
        public IRule NoDuplicateAddressesWithContext { get; private set; }

        /// <summary>
        /// Gets the no work hour overlap on context.
        /// </summary>
        public IRule NoWorkHourOverlapOnContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate operating schedule names with context.
        /// </summary>
        public IRule NoDuplicateOperatingScheduleNamesWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate locations.
        /// </summary>
        public IRule NoDuplicateLocations { get; private set; }

        private void BuildCreateLocationRuleSet ()
        {
            NewRule ( () => NoDuplicateLocations ).NoDuplicates ( ctx => ctx.Subject.Agency.Locations );

            NewRuleSet ( () => CreateLocationRuleSet, NoDuplicateLocations );
        }

        private void BuildReviseNameRuleSet ()
        {
            NewRule ( () => NoDuplicateOperatingScheduleNamesWithContext )
                .When (
                    ( s, ctx ) =>
                        {
                            var name = ctx.WorkingMemory.GetContextObject<string> ();
                            return Enumerable.Any<LocationOperationSchedule> ( s.LocationOperationSchedules, item => item.Name.Equals ( name ) );
                        } )
                .ThenReportRuleViolation (
                    ( s, ctx ) =>
                    string.Format (
                        "Cannot have duplicate {0} {1}.",
                        ctx.NameProvider.GetName ( ctx.WorkingMemory.GetContextObject<LocationOperationSchedule> () ),
                        ctx.NameProvider.GetName ( ctx.WorkingMemory.GetContextObject<LocationOperationSchedule> (), os => os.Name ) ) );

            NewRuleSet ( () => ReviseNameRuleSet, NoDuplicateOperatingScheduleNamesWithContext );
        }

        private void BuildAddWorkHourRuleSet ()
        {
            NewRule ( () => NoWorkHourOverlapOnContext )
                .When (
                    ( s, ctx ) =>
                        {
                            var workHour = ctx.WorkingMemory.GetContextObject<LocationWorkHour> ();
                            var schedule = ctx.WorkingMemory.GetContextObject<LocationOperationSchedule> ();
                            var hasOverlap = Enumerable.Any<LocationWorkHour> ( 
                                schedule.LocationWorkHours, 
                                p => p.DayOfWeek.Equals ( workHour.DayOfWeek ) && p.WorkHourTimeRange.Overlaps ( workHour.WorkHourTimeRange ) );
                            return hasOverlap;
                        } )
                .ThenReportRuleViolation (
                    ( s, ctx ) =>
                    string.Format (
                        "{0} cannot overlap any other {0} in the {1}.",
                        ctx.NameProvider.GetName ( ctx.WorkingMemory.GetContextObject<LocationWorkHour> () ),
                        ctx.NameProvider.GetName ( ctx.WorkingMemory.GetContextObject<LocationOperationSchedule> () ) ) );

            NewRuleSet ( () => AddWorkHourRuleSet, NoWorkHourOverlapOnContext );
        }

        private void BuildReviseLocationAddressRuleSet ()
        {
            NewRule ( () => NoDuplicateAddressesWithContext )
                .When (
                    ( s, ctx ) =>
                        {
                            var contextObject = ctx.WorkingMemory.GetContextObject<LocationAddress> ();
                            bool isDuplicate;
                            if ( contextObject is IValuesEquatable )
                            {
                                var aggregateNodeValueObject = contextObject as IValuesEquatable;
                                isDuplicate = Enumerable.Any<LocationAddressAndPhone> ( s.LocationAddressesAndPhones, item => aggregateNodeValueObject.ValuesEqual ( item.LocationAddress ) );
                            }
                            else
                            {
                                isDuplicate = Enumerable.Any<LocationAddressAndPhone> ( s.LocationAddressesAndPhones, item => item.Equals ( contextObject ) );
                            }
                            return isDuplicate;
                        } )
                .ThenReportRuleViolation (
                    ( s, ctx ) =>
                    string.Format (
                        "Cannot have duplicate {0}.", ( object )ctx.NameProvider.GetName ( ctx.WorkingMemory.GetContextObject<LocationAddress> () ) ) );

            NewRuleSet ( () => ReviseLocationAddressRuleSet, NoDuplicateAddressesWithContext );
        }

        private void BuildAddPhoneRuleSet ()
        {
            NewRule ( () => NoDuplicatePhoneNumbersWithContext )
                .OnContextObject<LocationPhone> ()
                .NoDuplicates ( ctx => ctx.WorkingMemory.GetContextObject<LocationAddressAndPhone> ().PhoneNumbers );

            NewRuleSet ( () => AddPhoneRuleSet, NoDuplicatePhoneNumbersWithContext );
        }

        private void BuildAddEmailAddressRuleSet ()
        {
            NewRule ( () => NoDuplicateEmailAddressesWithContext )
                .OnContextObject<LocationEmailAddress> ()
                .NoDuplicates ( ctx => ctx.Subject.EmailAddresses );

            NewRuleSet ( () => AddEmailAddressRuleSet, NoDuplicateEmailAddressesWithContext );
        }

        private void BuildAddOperationScheduleRuleSet ()
        {
            NewRule ( () => NoDuplicateOperationSchedulesWithContext )
                .OnContextObject<LocationOperationSchedule> ()
                .NoDuplicates ( ctx => ctx.Subject.LocationOperationSchedules );

            NewRuleSet ( () => AddOperationScheduleRuleSet, NoDuplicateOperationSchedulesWithContext );
        }

        private void BuildAddContactRuleSet ()
        {
            NewRule ( () => NoDuplicateContactsWithContext )
                .OnContextObject<LocationContact> ()
                .NoDuplicates ( ctx => ctx.Subject.LocationContacts );

            NewRuleSet ( () => AddEmailAddressRuleSet, NoDuplicateEmailAddressesWithContext );
        }

        private void BuildAddIdentifierRuleSet ()
        {
            NewRule ( () => NoDuplicateIdentifierssWithContext )
                .OnContextObject<LocationIdentifier> ()
                .NoDuplicates ( ctx => ctx.Subject.LocationIdentifiers );

            NewRuleSet ( () => AddIdentifierRuleSet, NoDuplicateIdentifierssWithContext );
        }

        private void BuildAddAddressAndPhoneRuleSet ()
        {
            NewRule ( () => NoDuplicateAddressAndPhonesWithContext )
                .OnContextObject<LocationAddressAndPhone> ()
                .NoDuplicates ( ctx => ctx.Subject.LocationAddressesAndPhones );

            NewRuleSet ( () => AddAddressAndPhoneRuleSet, NoDuplicateAddressAndPhonesWithContext );
        }
    }
}
