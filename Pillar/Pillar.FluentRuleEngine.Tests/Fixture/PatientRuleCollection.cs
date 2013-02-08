using System;

namespace Pillar.FluentRuleEngine.Tests.Fixture
{
    public class PatientRuleCollection : AbstractRuleCollection<Patient>
    {
        public PatientRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            NewPropertyRule ( () => BirthDateCannotBeFutureDate ).WithProperty ( p => p.BirthDate ).Constrain ( p => p.Date <= DateTime.Today );

            NewRule ( () => CannotBePregnantIfMale ).When ( p => p.Gender == Gender.Male && p.IsPregnant ).ThenReportRuleViolation (
                "Can't be pregnant" );

            NewRule ( () => CannotRenamePatientToSameName ).When (
                ( p, ctx ) =>
                    {
                        var newName = ctx.WorkingMemory.GetContextObject<Name> ();
                        return newName == p.Name;
                    } ).ThenReportRuleViolation ( "Cannot rename to same name." );

            NewCollectionPropertyRule ( () => RunAddressRulesRule ).WithProperty ( p => p.Addresses ).WithRuleCollection (
                new AddressRuleCollection () );

            NewPropertyRule ( () => FirstNameCannotBeNull ).WithProperty ( p => p.Name.First ).NotNull ();

            NewRuleSet ( () => PatientNameRuleSet, RunAddressRulesRule, FirstNameCannotBeNull );

            NewSpecificationRule ( () => CannotHaveTwoAddressesWithSameZipCode ).WithSpecification ( new NoDuplicateZipCodeSpecification () );
        }

        // Rule on Property
        public IPropertyRule BirthDateCannotBeFutureDate { get; private set; }

        // Custom Rule on Subject
        public IRule CannotBePregnantIfMale { get; private set; }

        // Custom Rule on Subject and Context Object (rename)
        public IRule CannotRenamePatientToSameName { get; private set; }

        // Rule on Address Collection
        public ICollectionPropertyRule RunAddressRulesRule { get; private set; }

        // Rule on Sub-Object
        public IPropertyRule FirstNameCannotBeNull { get; private set; }

        // Specification Rule
        public ISpecificationRule CannotHaveTwoAddressesWithSameZipCode { get; private set; }

        public IRuleSet PatientNameRuleSet { get; private set; }
    }
}
