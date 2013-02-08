using System.Linq;
using Pillar.Domain;
using Pillar.FluentRuleEngine;

namespace Rem.Domain.Clinical.PatientModule.Rule
{
    /// <summary>
    /// The PatientRuleCollection defines rules/rule sets for <see cref="Patient">Patient</see> entity.
    /// </summary>
    public class PatientRuleCollection : AbstractRuleCollection<Patient>
    {
        /// <summary>
        /// FourMegaBytes constant
        /// </summary>
        public const int FourMegaBytes = 4 * 1000 * 1000;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientRuleCollection"/> class.
        /// </summary>
        public PatientRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            BuildRenameRuleSet ();

            BuildPatientProfileRuleSet ();

            BuildAddAddressRuleSet ();

            BuildAddAliasRuleSet ();

            BuildAddPatientDisabilityRuleSet ();

            BuildAddPatientIdentifierRuleSet ();

            BuildAddPhoneNumberRuleSet ();

            BuildAddPatientPhotoRuleSet ();

            BuildSetPrimaryPhotoRuleSet ();

            BuildAddPatientSpecialNeedRuleSet ();

            BuildAddPatientRaceRuleSet ();

            BuildSetPrimaryRaceRuleSet ();

            BuildCreatePatientRuleSet ();

            BuildReviseVeteranInformationRuleSet();
        }

        /// <summary>
        /// Gets the create patient rule set.
        /// </summary>
        public IRuleSet CreatePatientRuleSet { get; private set; }

        /// <summary>
        /// Gets the rename rule set.
        /// </summary>
        public IRuleSet RenameRuleSet { get; private set; }

        /// <summary>
        /// Gets the revise profile rule set.
        /// </summary>
        public IRuleSet ReviseProfileRuleSet { get; private set; }

        /// <summary>
        /// Gets the add address rule set.
        /// </summary>
        public IRuleSet AddAddressRuleSet { get; private set; }

        /// <summary>
        /// Gets the add alias rule set.
        /// </summary>
        public IRuleSet AddAliasRuleSet { get; private set; }

        /// <summary>
        /// Gets the add patient disability rule set.
        /// </summary>
        public IRuleSet AddPatientDisabilityRuleSet { get; private set; }

        /// <summary>
        /// Gets the add patient identifier rule set.
        /// </summary>
        public IRuleSet AddPatientIdentifierRuleSet { get; private set; }

        /// <summary>
        /// Gets the add phone number rule set.
        /// </summary>
        public IRuleSet AddPhoneNumberRuleSet { get; private set; }

        /// <summary>
        /// Gets the add patient photo rule set.
        /// </summary>
        public IRuleSet AddPatientPhotoRuleSet { get; private set; }

        /// <summary>
        /// Gets the set primary photo rule set.
        /// </summary>
        public IRuleSet SetPrimaryPhotoRuleSet { get; private set; }

        /// <summary>
        /// Gets the add patient special need rule set.
        /// </summary>
        public IRuleSet AddPatientSpecialNeedRuleSet { get; private set; }

        /// <summary>
        /// Gets the add patient race rule set.
        /// </summary>
        public IRuleSet AddPatientRaceRuleSet { get; private set; }

        /// <summary>
        /// Gets the set primary race rule set.
        /// </summary>
        public IRuleSet SetPrimaryRaceRuleSet { get; private set; }

        /// <summary>
        /// Gets the patient name cannot be the same.
        /// </summary>
        public IPropertyRule PatientNameCannotBeTheSame { get; private set; }

        /// <summary>
        /// Gets the patient alias first name cannot be empty.
        /// </summary>
        public IRule PatientAliasFirstNameCannotBeEmpty { get; private set; }

        /// <summary>
        /// Gets the first street address not empty.
        /// </summary>
        public IRule FirstStreetAddressNotEmpty { get; private set; }

        /// <summary>
        /// Gets the city name not empty.
        /// </summary>
        public IRule CityNameNotEmpty { get; private set; }

        /// <summary>
        /// Gets the state province not null.
        /// </summary>
        public IRule StateProvinceNotNull { get; private set; }

        /// <summary>
        /// Gets the postal code not null.
        /// </summary>
        public IRule PostalCodeNotNull { get; private set; }

        /// <summary>
        /// Gets the birth date cannot be null.
        /// </summary>
        public IRule BirthDateCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the patient gender cannot be null.
        /// </summary>
        public IRule PatientGenderCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the must contain primary photo.
        /// </summary>
        public IRule MustContainPrimaryPhoto { get; private set; }

        /// <summary>
        /// Gets the photo size less then4 MB.
        /// </summary>
        public IRule PhotoSizeLessThen4MB { get; private set; }

        /// <summary>
        /// Gets the must contain primary race.
        /// </summary>
        public IRule MustContainPrimaryRace { get; private set; }

        /// <summary>
        /// Gets the no duplicate patient identifiers with context.
        /// </summary>
        public IRule NoDuplicatePatientIdentifiersWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate patient disabilities with context.
        /// </summary>
        public IRule NoDuplicatePatientDisabilitiesWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate patient aliases with context.
        /// </summary>
        public IRule NoDuplicatePatientAliasesWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate patient addresses with context.
        /// </summary>
        public IRule NoDuplicatePatientAddressesWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate patient phones with context.
        /// </summary>
        public IRule NoDuplicatePatientPhonesWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate patient special needs with context.
        /// </summary>
        public IRule NoDuplicatePatientSpecialNeedsWithContext { get; private set; }

        /// <summary>
        /// Gets the no duplicate patient races with context.
        /// </summary>
        public IRule NoDuplicatePatientRacesWithContext { get; private set; }

        /// <summary>
        /// Gets the revise veteran information rule set.
        /// </summary>
        public IRuleSet ReviseVeteranInformationRuleSet { get; private set; }

        /// <summary>
        /// Gets the veteran service branch cannot be null.
        /// </summary>
        public IRule VeteranServiceBranchCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the veteran status cannot be null.
        /// </summary>
        public IRule VeteranStatusCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the veteran discharge status cannot be null.
        /// </summary>
        public IRule VeteranDischargeStatusCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the service start date cannot be null.
        /// </summary>
        public IRule ServiceStartDateCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the service end date cannot be null.
        /// </summary>
        public IRule ServiceEndDateCannotBeNull { get; private set; }

        private void BuildSetPrimaryRaceRuleSet ()
        {
            NewRule ( () => MustContainPrimaryRace ).When (
                ( s, ctx ) =>
                    {
                        var patientRace = ctx.WorkingMemory.GetContextObject<PatientRace> ();
                        return !s.Races.Contains ( patientRace );
                    } ).ThenReportRuleViolation (
                        ( s, ctx ) =>
                        string.Format (
                            "The primary {0} must be in the {0} list.",
                            ctx.NameProvider.GetName ( ctx.WorkingMemory.GetContextObject<PatientRace> () ) ) );

            NewRuleSet ( () => SetPrimaryRaceRuleSet, MustContainPrimaryRace );
        }

        private void BuildAddPatientRaceRuleSet ()
        {
            NewRule ( () => NoDuplicatePatientRacesWithContext )
                .OnContextObject<PatientRace> ()
                .NoDuplicates ( ctx => ctx.Subject.Races );

            NewRuleSet ( () => AddPatientRaceRuleSet, NoDuplicatePatientRacesWithContext );
        }

        private void BuildAddPatientSpecialNeedRuleSet ()
        {
            NewRule ( () => NoDuplicatePatientSpecialNeedsWithContext )
                .OnContextObject<PatientSpecialNeed> ()
                .NoDuplicates ( ctx => ctx.Subject.SpecialNeeds );

            NewRuleSet ( () => AddPatientSpecialNeedRuleSet, NoDuplicatePatientSpecialNeedsWithContext );
        }

        private void BuildSetPrimaryPhotoRuleSet ()
        {
            NewRule ( () => MustContainPrimaryPhoto ).When (
                ( s, ctx ) =>
                    {
                        var patientPhoto = ctx.WorkingMemory.GetContextObject<PatientPhoto> ();
                        return !s.Photos.Contains ( patientPhoto );
                    } ).ThenReportRuleViolation ( "The primary photo must be in the photo list." );

            NewRuleSet ( () => SetPrimaryPhotoRuleSet, MustContainPrimaryPhoto );
        }

        private void BuildAddPatientPhotoRuleSet ()
        {
            NewRule ( () => PhotoSizeLessThen4MB )
                .OnContextObject<PatientPhoto> ()
                .WithProperty ( p => p.Picture )
                .Constrain ( p => ( ( byte[] )p ).Length < FourMegaBytes, "Photo must be less than 4MB" );

            NewRuleSet ( () => AddPatientPhotoRuleSet, PhotoSizeLessThen4MB );
        }

        private void BuildAddPhoneNumberRuleSet ()
        {
            NewRule ( () => NoDuplicatePatientPhonesWithContext )
                .OnContextObject<PatientPhone> ()
                .NoDuplicates ( ctx => ctx.Subject.PhoneNumbers );

            NewRuleSet ( () => AddPhoneNumberRuleSet, NoDuplicatePatientPhonesWithContext );
        }

        private void BuildAddPatientIdentifierRuleSet ()
        {
            NewRule ( () => NoDuplicatePatientIdentifiersWithContext )
                .OnContextObject<PatientIdentifier> ()
                .NoDuplicates ( ctx => ctx.Subject.Identifiers );

            NewRuleSet ( () => AddPatientIdentifierRuleSet, NoDuplicatePatientIdentifiersWithContext );
        }

        private void BuildAddPatientDisabilityRuleSet ()
        {
            NewRule ( () => NoDuplicatePatientDisabilitiesWithContext )
                .OnContextObject<PatientDisability> ()
                .NoDuplicates ( ctx => ctx.Subject.Disabilities );

            NewRuleSet ( () => AddPatientDisabilityRuleSet, NoDuplicatePatientDisabilitiesWithContext );
        }

        private void BuildAddAliasRuleSet ()
        {
            NewRule ( () => NoDuplicatePatientAliasesWithContext )
                .OnContextObject<PatientAlias> ()
                .NoDuplicates ( ctx => ctx.Subject.Aliases );

            NewRule ( () => PatientAliasFirstNameCannotBeEmpty )
                .OnContextObject<PatientAlias> ()
                .WithProperty ( pa => pa.FirstName )
                .NotNull ();

            NewRuleSet ( () => AddAliasRuleSet, NoDuplicatePatientAliasesWithContext, PatientAliasFirstNameCannotBeEmpty );
        }

        private void BuildAddAddressRuleSet ()
        {
            NewRule ( () => NoDuplicatePatientAddressesWithContext )
                .OnContextObject<PatientAddress> ()
                .NoDuplicates ( ctx => ctx.Subject.Addresses );

            ShouldRunWhen (
                ( p, ctx ) =>
                    {
                        var contextObject = ctx.WorkingMemory.GetContextObject<PatientAddress> ();
                        return !contextObject.IsHomelessAddress ();
                    },
                () =>
                    {
                        NewRule ( () => FirstStreetAddressNotEmpty ).OnContextObject<PatientAddress> ()
                            .WithProperty ( pa => pa.Address.FirstStreetAddress )
                            .NotNull ();
                        NewRule ( () => CityNameNotEmpty ).OnContextObject<PatientAddress> ()
                            .WithProperty ( pa => pa.Address.CityName ).NotNull ();
                        NewRule ( () => StateProvinceNotNull ).OnContextObject<PatientAddress> ()
                            .WithProperty ( pa => pa.Address.StateProvince )
                            .NotNull ();
                        NewRule ( () => PostalCodeNotNull ).OnContextObject<PatientAddress> ()
                            .WithProperty ( pa => pa.Address.PostalCode )
                            .NotNull ();
                    }
                );

            NewRuleSet (
                () => AddAddressRuleSet,
                NoDuplicatePatientAddressesWithContext,
                FirstStreetAddressNotEmpty,
                CityNameNotEmpty,
                StateProvinceNotNull,
                PostalCodeNotNull );
        }

        private void BuildPatientProfileRuleSet ()
        {
            NewRule ( () => BirthDateCannotBeNull )
                .OnContextObject<PatientProfile> ()
                .WithProperty ( pp => pp.BirthDate )
                .NotNull ();

            NewRule ( () => PatientGenderCannotBeNull )
                .OnContextObject<PatientProfile> ()
                .WithProperty ( pp => pp.PatientGender )
                .NotNull ();

            NewRuleSet ( () => ReviseProfileRuleSet, BirthDateCannotBeNull, PatientGenderCannotBeNull );
        }

        private void BuildRenameRuleSet ()
        {
            NewPropertyRule ( () => PatientNameCannotBeTheSame )
                .WithProperty ( p => p.Name )
                .NotEqualToContextObject ();

            NewRuleSet ( () => RenameRuleSet, PatientNameCannotBeTheSame );
        }

        private void BuildCreatePatientRuleSet ()
        {
            NewRuleSet ( () => CreatePatientRuleSet, BirthDateCannotBeNull, PatientGenderCannotBeNull );
        }

        private void BuildReviseVeteranInformationRuleSet ()
        {
            ShouldRunWhen (
                ( v, ctx ) =>
                    {
                        var contextObject = ctx.WorkingMemory.GetContextObject<PatientVeteranInformation> ();
                        return contextObject.HaveServedInMilitaryIndicator.HasValue && contextObject.HaveServedInMilitaryIndicator.Value;
                    },
                () =>
                    {
                        NewRule ( () => VeteranServiceBranchCannotBeNull ).OnContextObject<PatientVeteranInformation> ()
                            .WithProperty ( vi => vi.VeteranServiceBranch )
                            .NotNull ();
                        NewRule ( () => VeteranStatusCannotBeNull ).OnContextObject<PatientVeteranInformation> ()
                            .WithProperty ( vi => vi.VeteranStatus )
                            .NotNull ();
                        NewRule ( () => ServiceStartDateCannotBeNull ).OnContextObject<PatientVeteranInformation> ()
                            .WithProperty ( vi => vi.ServiceDateRange.StartDate )
                            .NotNull ();
                    }
                );

            ShouldRunWhen (
                ( v, ctx ) =>
                    {
                        var contextObject = ctx.WorkingMemory.GetContextObject<PatientVeteranInformation> ();
                        return contextObject.ServiceDateRange.EndDate.HasValue;
                    },
                () =>
                NewRule ( () => VeteranDischargeStatusCannotBeNull ).OnContextObject<PatientVeteranInformation> ()
                    .WithProperty ( vi => vi.VeteranDischargeStatus )
                    .NotNull ()
                );

            NewRule ( () => ServiceEndDateCannotBeNull ).When (
                ( v, ctx ) =>
                    {
                        var veteranInformation = ctx.WorkingMemory.GetContextObject<PatientVeteranInformation> ();
                        return veteranInformation.VeteranDischargeStatus != null && !veteranInformation.ServiceDateRange.EndDate.HasValue;
                    } )
                .ThenReportRuleViolation ( "Service end date is required if a discharge status is selected." );

            NewRuleSet (
                () => ReviseVeteranInformationRuleSet,
                VeteranServiceBranchCannotBeNull,
                VeteranStatusCannotBeNull,
                ServiceStartDateCannotBeNull,
                VeteranDischargeStatusCannotBeNull,
                ServiceEndDateCannotBeNull );
        }
    }
}
