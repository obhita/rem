using System.Linq;
using Pillar.Common;
using Pillar.Domain;
using Pillar.FluentRuleEngine;

namespace Rem.Domain.Core.AgencyModule.Rule
{
    /// <summary>
    /// The AgencyRuleCollection defines rules/rule sets for <see cref="Agency">Agency</see> entity.
    /// </summary>
    public class AgencyRuleCollection : AbstractRuleCollection<Agency>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyRuleCollection"/> class.
        /// </summary>
        public AgencyRuleCollection ()
        {
            BuildAddAddAddressAndPhoneRuleSet ();

            BuildAddEmailAddressRuleSet ();

            BuildAddIdentifierRuleSet ();

            BuildAddFrequentlyAskedQuestionRuleSet ();

            BuildAddContactRuleSet ();

            BuildAddPhoneRuleSet ();

            BuildReviseAgencyAddressRuleSet ();
        }

        /// <summary>
        /// Gets the add address and phone rule set.
        /// </summary>
        public IRuleSet AddAddressAndPhoneRuleSet { get; private set; }

        /// <summary>
        /// Gets the add email address rule set.
        /// </summary>
        public IRuleSet AddEmailAddressRuleSet { get; private set; }

        /// <summary>
        /// Gets the add identifier rule set.
        /// </summary>
        public IRuleSet AddIdentifierRuleSet { get; private set; }

        /// <summary>
        /// Gets the add frequently asked question rule set.
        /// </summary>
        public IRuleSet AddFrequentlyAskedQuestionRuleSet { get; private set; }

        /// <summary>
        /// Gets the add contact rule set.
        /// </summary>
        public IRuleSet AddContactRuleSet { get; private set; }

        /// <summary>
        /// Gets the add phone rule set.
        /// </summary>
        public IRuleSet AddPhoneRuleSet { get; private set; }

        /// <summary>
        /// Gets the revise agency address rule set.
        /// </summary>
        public IRuleSet ReviseAgencyAddressRuleSet { get; private set; }

        /// <summary>
        /// Gets the no duplicate address and phones with context.
        /// </summary>
        public IRule NoDuplicateAddressAndPhonesWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate email addresses with context.
        /// </summary>
        public IRule NoDuplicateEmailAddressesWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate identifiers with context.
        /// </summary>
        public IRule NoDuplicateIdentifiersWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate frequently asked questions with context.
        /// </summary>
        public IRule NoDuplicateFrequentlyAskedQuestionsWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate phone numbers with context.
        /// </summary>
        public IRule NoDuplicatePhoneNumbersWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate contacts with context.
        /// </summary>
        public IRule NoDuplicateContactsWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate addresses with context.
        /// </summary>
        public IRule NoDuplicateAddressesWithContext { get; private set; }

        private void BuildReviseAgencyAddressRuleSet ()
        {
            NewRule ( () => NoDuplicateAddressesWithContext )
                .When (
                    ( s, ctx ) =>
                        {
                            var contextObject = ctx.WorkingMemory.GetContextObject<AgencyAddress> ();
                            bool isDuplicate;
                            if ( contextObject is IValuesEquatable )
                            {
                                var aggregateNodeValueObject = contextObject as IValuesEquatable;
                                isDuplicate = Enumerable.Any<AgencyAddressAndPhone> ( s.AddressesAndPhones, item => aggregateNodeValueObject.ValuesEqual ( item.AgencyAddress ) );
                            }
                            else
                            {
                                isDuplicate = Enumerable.Any<AgencyAddressAndPhone> ( s.AddressesAndPhones, item => item.Equals ( contextObject ) );
                            }
                            return isDuplicate;
                        } )
                .ThenReportRuleViolation (
                    ( s, ctx ) =>
                    string.Format (
                        "Cannot not have duplicate {0}.", ( object )ctx.NameProvider.GetName ( ctx.WorkingMemory.GetContextObject<AgencyAddress> () ) ) );
        }

        private void BuildAddIdentifierRuleSet ()
        {
            NewRule ( () => NoDuplicateIdentifiersWithContext )
                .OnContextObject<AgencyIdentifier> ()
                .NoDuplicates ( ctx => ctx.Subject.AgencyIdentifiers );

            NewRuleSet ( () => AddIdentifierRuleSet, NoDuplicateIdentifiersWithContext );
        }

        private void BuildAddFrequentlyAskedQuestionRuleSet ()
        {
            NewRule ( () => NoDuplicateFrequentlyAskedQuestionsWithContext )
                .OnContextObject<AgencyFrequentlyAskedQuestion> ()
                .NoDuplicates ( ctx => ctx.Subject.AgencyFrequentlyAskedQuestions );

            NewRuleSet ( () => AddFrequentlyAskedQuestionRuleSet, NoDuplicateFrequentlyAskedQuestionsWithContext );
        }

        private void BuildAddContactRuleSet ()
        {
            NewRule ( () => NoDuplicateContactsWithContext )
                .OnContextObject<AgencyContact> ()
                .NoDuplicates ( ctx => ctx.Subject.AgencyContacts );

            NewRuleSet ( () => AddContactRuleSet, NoDuplicateContactsWithContext );
        }

        private void BuildAddPhoneRuleSet ()
        {
            NewRule ( () => NoDuplicatePhoneNumbersWithContext )
                .OnContextObject<AgencyPhone> ()
                .NoDuplicates ( ctx => ctx.WorkingMemory.GetContextObject<AgencyAddressAndPhone> ().PhoneNumbers );

            NewRuleSet ( () => AddPhoneRuleSet, NoDuplicatePhoneNumbersWithContext );
        }

        private void BuildAddEmailAddressRuleSet ()
        {
            NewRule ( () => NoDuplicateEmailAddressesWithContext )
                .OnContextObject<AgencyEmailAddress> ()
                .NoDuplicates ( ctx => ctx.Subject.EmailAddresses );

            NewRuleSet ( () => AddEmailAddressRuleSet, NoDuplicateEmailAddressesWithContext );
        }

        private void BuildAddAddAddressAndPhoneRuleSet ()
        {
            NewRule ( () => NoDuplicateAddressAndPhonesWithContext )
                .OnContextObject<AgencyAddressAndPhone> ()
                .NoDuplicates ( ctx => ctx.Subject.AddressesAndPhones );

            NewRuleSet ( () => AddAddressAndPhoneRuleSet, NoDuplicateAddressAndPhonesWithContext );
        }
    }
}
