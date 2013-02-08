using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.Primitives;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiPatientProfileSection contains patient information from the General Information section of the DensAsi. 
    /// <remarks>
    /// Included in each of these sections is the interviewer's severity rating, suggesting the client's need for treatment or additional treatment. 
    /// This is based on the information provided by the client. 
    /// </remarks>
    /// </summary>
    [Component]
    public class DensAsiPatientProfileSection : DensAsiInterviewSectionBase
    {
        private readonly DensAsiNonResponseType<bool?> _buddhismReligionIndicator;
        private readonly string _buddhismReligionIndicatorNote;
        private readonly DensAsiNonResponseType<bool?> _christianReligionIndicator;
        private readonly string _christianReligionIndicatorNote;
        private readonly DensAsiInterviewClass _densAsiInterviewClass;
        private readonly string _densAsiInterviewClassNote;
        private readonly DensAsiNonResponseType<DensAsiInterviewContactType> _densAsiInterviewContactType;
        private readonly string _densAsiInterviewContactTypeNote;
        private readonly DateTime? _interviewDate;
        private readonly string _interviewDateNote;
        private readonly DensAsiNonResponseType<int?> _lastThirtyDaysControlledEnvironmentDayCount;
        private readonly string _lastThirtyDaysControlledEnvironmentDayCountNote;
        private readonly DensAsiNonResponseType<DensAsiControlledEnvironment> _lastThirtyDaysDensAsiControlledEnvironment;
        private readonly string _lastThirtyDaysDensAsiControlledEnvironmentNote;
        private readonly DensAsiNonResponseType<bool?> _noParticularReligiousSectIndicator;
        private readonly string _noParticularReligiousSectIndicatorNote;
        private readonly DensAsiNonResponseType<DensAsiReligion> _preferredDensAsiReligion;
        private readonly string _preferredDensAsiReligionNote;
        private readonly DensAsiNonResponseType<bool?> _residenceOwnedByYouOrFamilyIndicator;
        private readonly string _residenceOwnedByYouOrFamilyIndicatorNote;
        private readonly DensAsiNonResponseType<TimeSpan?> _yearsAndMonthsAtCurrentAddressTimeSpan;
        private readonly string _yearsAndMonthsAtCurrentAddressTimeSpanNote;

        private DensAsiPatientProfileSection ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiPatientProfileSection"/> class.
        /// </summary>
        /// <param name="interviewDate">The interview date.</param>
        /// <param name="interviewDateNote">The interview date note.</param>
        /// <param name="densAsiInterviewClass">The dens asi interview class.</param>
        /// <param name="densAsiInterviewClassNote">The dens asi interview class note.</param>
        /// <param name="densAsiInterviewContactType">Type of the dens asi interview contact.</param>
        /// <param name="densAsiInterviewContactTypeNote">The dens asi interview contact type note.</param>
        /// <param name="yearsAndMonthsAtCurrentAddressTimeSpan">The years and months at current address time span.</param>
        /// <param name="yearsAndMonthsAtCurrentAddressTimeSpanNote">The years and months at current address time span note.</param>
        /// <param name="residenceOwnedByYouOrFamilyIndicator">The residence owned by you or family indicator.</param>
        /// <param name="residenceOwnedByYouOrFamilyIndicatorNote">The residence owned by you or family indicator note.</param>
        /// <param name="preferredDensAsiReligion">The preferred dens asi religion.</param>
        /// <param name="preferredDensAsiReligionNote">The preferred dens asi religion note.</param>
        /// <param name="lastThirtyDaysDensAsiControlledEnvironment">The last thirty days dens asi controlled environment.</param>
        /// <param name="lastThirtyDaysDensAsiControlledEnvironmentNote">The last thirty days dens asi controlled environment note.</param>
        /// <param name="lastThirtyDaysControlledEnvironmentDayCount">The last thirty days controlled environment day count.</param>
        /// <param name="lastThirtyDaysControlledEnvironmentDayCountNote">The last thirty days controlled environment day count note.</param>
        /// <param name="christianReligionIndicator">The christian religion indicator.</param>
        /// <param name="christianReligionIndicatorNote">The christian religion indicator note.</param>
        /// <param name="buddhismReligionIndicator">The buddhism religion indicator.</param>
        /// <param name="buddhismReligionIndicatorNote">The buddhism religion indicator note.</param>
        /// <param name="noParticularReligiousSectIndicator">The no particular religious sect indicator.</param>
        /// <param name="noParticularReligiousSectIndicatorNote">The no particular religious sect indicator note.</param>
        public DensAsiPatientProfileSection(
                                                DateTime? interviewDate,
                                                string interviewDateNote,
                                                DensAsiInterviewClass densAsiInterviewClass,
                                                string densAsiInterviewClassNote,
                                                DensAsiNonResponseType<DensAsiInterviewContactType> densAsiInterviewContactType,
                                                string densAsiInterviewContactTypeNote,
                                                DensAsiNonResponseType<TimeSpan?> yearsAndMonthsAtCurrentAddressTimeSpan,
                                                string yearsAndMonthsAtCurrentAddressTimeSpanNote,
                                                DensAsiNonResponseType<bool?> residenceOwnedByYouOrFamilyIndicator,
                                                string residenceOwnedByYouOrFamilyIndicatorNote,
                                                DensAsiNonResponseType<DensAsiReligion> preferredDensAsiReligion,
                                                string preferredDensAsiReligionNote,
                                                DensAsiNonResponseType<DensAsiControlledEnvironment> lastThirtyDaysDensAsiControlledEnvironment,
                                                string lastThirtyDaysDensAsiControlledEnvironmentNote,
                                                DensAsiNonResponseType<int?> lastThirtyDaysControlledEnvironmentDayCount,
                                                string lastThirtyDaysControlledEnvironmentDayCountNote,
                                                DensAsiNonResponseType<bool?> christianReligionIndicator,
                                                string christianReligionIndicatorNote,
                                                DensAsiNonResponseType<bool?> buddhismReligionIndicator,
                                                string buddhismReligionIndicatorNote,
                                                DensAsiNonResponseType<bool?> noParticularReligiousSectIndicator,
                                                string noParticularReligiousSectIndicatorNote )
        {
            if ( densAsiInterviewContactType.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DensAsiInterviewContactType ).Contains ( densAsiInterviewContactType.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DensAsiInterviewContactType DensAsiNonResponse value '" + densAsiInterviewContactType.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( yearsAndMonthsAtCurrentAddressTimeSpan.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => YearsAndMonthsAtCurrentAddressTimeSpan ).Contains ( yearsAndMonthsAtCurrentAddressTimeSpan.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "YearsAndMonthsAtCurrentAddressTimeSpan DensAsiNonResponse value '" + yearsAndMonthsAtCurrentAddressTimeSpan.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( residenceOwnedByYouOrFamilyIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ResidenceOwnedByYouOrFamilyIndicator ).Contains ( residenceOwnedByYouOrFamilyIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ResidenceOwnedByYouOrFamilyIndicator DensAsiNonResponse value '" + residenceOwnedByYouOrFamilyIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( preferredDensAsiReligion.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PreferredDensAsiReligion ).Contains ( preferredDensAsiReligion.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PreferredDensAsiReligion DensAsiNonResponse value '" + preferredDensAsiReligion.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( lastThirtyDaysDensAsiControlledEnvironment.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => LastThirtyDaysDensAsiControlledEnvironment ).Contains ( lastThirtyDaysDensAsiControlledEnvironment.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "LastThirtyDaysDensAsiControlledEnvironment DensAsiNonResponse value '" + lastThirtyDaysDensAsiControlledEnvironment.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( lastThirtyDaysControlledEnvironmentDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => LastThirtyDaysControlledEnvironmentDayCount ).Contains ( lastThirtyDaysControlledEnvironmentDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "LastThirtyDaysControlledEnvironmentDayCount DensAsiNonResponse value '" + lastThirtyDaysControlledEnvironmentDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( christianReligionIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ChristianReligionIndicator ).Contains ( christianReligionIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ChristianReligionIndicator DensAsiNonResponse value '" + christianReligionIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( buddhismReligionIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => BuddhismReligionIndicator ).Contains ( buddhismReligionIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "BuddhismReligionIndicator DensAsiNonResponse value '" + buddhismReligionIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( noParticularReligiousSectIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => NoParticularReligiousSectIndicator ).Contains ( noParticularReligiousSectIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "NoParticularReligiousSectIndicator DensAsiNonResponse value '" + noParticularReligiousSectIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            _interviewDate = interviewDate;
            _interviewDateNote = interviewDateNote;
            _densAsiInterviewClass = densAsiInterviewClass;
            _densAsiInterviewClassNote = densAsiInterviewClassNote;
            _densAsiInterviewContactType = densAsiInterviewContactType;
            _densAsiInterviewContactTypeNote = densAsiInterviewContactTypeNote;
            _yearsAndMonthsAtCurrentAddressTimeSpan = yearsAndMonthsAtCurrentAddressTimeSpan;
            _yearsAndMonthsAtCurrentAddressTimeSpanNote = yearsAndMonthsAtCurrentAddressTimeSpanNote;
            _residenceOwnedByYouOrFamilyIndicator = residenceOwnedByYouOrFamilyIndicator;
            _residenceOwnedByYouOrFamilyIndicatorNote = residenceOwnedByYouOrFamilyIndicatorNote;
            _preferredDensAsiReligion = preferredDensAsiReligion;
            _preferredDensAsiReligionNote = preferredDensAsiReligionNote;
            _lastThirtyDaysDensAsiControlledEnvironment = lastThirtyDaysDensAsiControlledEnvironment;
            _lastThirtyDaysDensAsiControlledEnvironmentNote = lastThirtyDaysDensAsiControlledEnvironmentNote;
            _lastThirtyDaysControlledEnvironmentDayCount = lastThirtyDaysControlledEnvironmentDayCount;
            _lastThirtyDaysControlledEnvironmentDayCountNote = lastThirtyDaysControlledEnvironmentDayCountNote;
            _christianReligionIndicator = christianReligionIndicator;
            _christianReligionIndicatorNote = christianReligionIndicatorNote;
            _buddhismReligionIndicator = buddhismReligionIndicator;
            _buddhismReligionIndicatorNote = buddhismReligionIndicatorNote;
            _noParticularReligiousSectIndicator = noParticularReligiousSectIndicator;
            _noParticularReligiousSectIndicatorNote = noParticularReligiousSectIndicatorNote;
        }

        /// <summary>
        /// Gets the patient admission date.
        /// </summary>
        public DateTime? AdmissionDate { get; internal set; }

        /// <summary>
        /// Gets the patient gender.
        /// </summary>
        public AdministrativeGender PatientAdministrativeGender { get; internal set; }

        /// <summary>
        /// Gets the  patient birth date.
        /// </summary>
        public DateTime? BirthDate { get; internal set; }

        /// <summary>
        /// Gets the patient first name.
        /// </summary>
        public string FirstName { get; internal set; }

        /// <summary>
        /// Gets the patient last name.
        /// </summary>
        public string LastName { get; internal set; }

        /// <summary>
        /// Gets the patient first street address.
        /// </summary>
        public string FirstStreetAddress { get; internal set; }

        /// <summary>
        /// Gets the patient second street address.
        /// </summary>
        public string SecondStreetAddress { get; internal set; }

        /// <summary>
        /// Gets the patient city of residence.
        /// </summary>
        public string CityName { get; internal set; }

        /// <summary>
        /// Gets the patient province of residence.
        /// </summary>
        public StateProvince StateProvince  { get; internal set; }

        /// <summary>
        /// Gets patient postal code.
        /// </summary>
        public PostalCode PostalCode   { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating if the patient is hispanic or latino.
        /// </summary>
        public bool? HispanicOrLatinoIndicator   { get; internal set; }

        /// <summary>
        /// Gets the patient race.
        /// </summary>
        public Race Race { get; internal set; }

        /// <summary>
        /// Gets the interview date.
        /// Question Number: G4
        /// </summary> 
        public DateTime? InterviewDate
        {
            get { return _interviewDate; }
            private set { }
        }

        /// <summary>
        /// Gets the interview date note.
        /// Question Number: G5
        /// </summary>
        public string InterviewDateNote
        {
            get { return _interviewDateNote; }
            private set { }
        }

        /// <summary>
        /// Gets the interview classification <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiInterviewClass">DensAsiInterviewClass</see>
        /// Question Number: G8
        /// <remarks>
        /// Most ASIs for the DENS study will be
        /// <para>coded “intake”. ASI’s done on or near</para>
        /// <para>admission are “intakes” even if the person</para>
        /// <para>has been in your treatment program before.</para>
        /// <para>Follow-up ASIs are generally completed by</para>
        /// <para>interviewers completing follow-up studies</para>
        /// </remarks>
        /// </summary>
        public DensAsiInterviewClass DensAsiInterviewClass
        {
            get { return _densAsiInterviewClass; }
            private set { }
        }

        /// <summary>
        /// Gets the interview class note.
        /// <remarks>Question Number: G8</remarks>
        /// </summary>
        public string DensAsiInterviewClassNote
        {
            get { return _densAsiInterviewClassNote; }
            private set { }
        }

        /// <summary>
        /// Gets the interview contact type
        /// <see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiInterviewContactType">DensAsiInterviewContactType</see>
        /// Question Number: G9
        /// </summary>
        public DensAsiNonResponseType<DensAsiInterviewContactType> DensAsiInterviewContactType
        {
            get { return _densAsiInterviewContactType; }
            private set { }
        }

        /// <summary>
        /// Gets the interview contact type note.
        /// Question Number: G9
        /// </summary>
        public string DensAsiInterviewContactTypeNote
        {
            get { return _densAsiInterviewContactTypeNote; }
            private set { }
        }

        /// <summary>
        /// Gets the years and months that the patient has resided at current address.
        /// Question Number: G14
        /// </summary>
        public DensAsiNonResponseType<TimeSpan?> YearsAndMonthsAtCurrentAddressTimeSpan
        {
            get { return _yearsAndMonthsAtCurrentAddressTimeSpan; }
            private set { }
        }

        /// <summary>
        /// Gets the years and months at current address time span note.
        /// Question Number: G14
        /// </summary>
        public string YearsAndMonthsAtCurrentAddressTimeSpanNote
        {
            get { return _yearsAndMonthsAtCurrentAddressTimeSpanNote; }
            private set { }
        }

        /// <summary>
        /// Gets the boolean value indicating if a residence owned by you or family indicator.
        /// Question Number: G15
        /// </summary>
        public DensAsiNonResponseType<bool?> ResidenceOwnedByYouOrFamilyIndicator
        {
            get { return _residenceOwnedByYouOrFamilyIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the residence owned by you or family indicator note.
        /// Question Number: G15
        /// </summary>
        public string ResidenceOwnedByYouOrFamilyIndicatorNote
        {
            get { return _residenceOwnedByYouOrFamilyIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the preferred patient religion <see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiReligion">PreferredDensAsiReligion</see>.
        /// Question Number: G18
        /// </summary>
        public DensAsiNonResponseType<DensAsiReligion> PreferredDensAsiReligion
        {
            get { return _preferredDensAsiReligion; }
            private set { }
        }

        /// <summary>
        /// Gets the preferred religion note.
        /// Question Number: G18
        /// </summary>
        public string PreferredDensAsiReligionNote
        {
            get { return _preferredDensAsiReligionNote; }
            private set { }
        }

        /// <summary>
        /// Gets the last thirty days the patient has been in a <see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiControlledEnvironment">DensAsiControlledEnvironment</see> without access to drugs/alcohol.
        /// Question Number: G19
        /// </summary>
        public DensAsiNonResponseType<DensAsiControlledEnvironment> LastThirtyDaysDensAsiControlledEnvironment
        {
            get { return _lastThirtyDaysDensAsiControlledEnvironment; }
            private set { }
        }

        /// <summary>
        /// Gets the last thirty days controlled environment note.
        /// Question Number: G19
        /// </summary>
        public string LastThirtyDaysDensAsiControlledEnvironmentNote
        {
            get { return _lastThirtyDaysDensAsiControlledEnvironmentNote; }
            private set { }
        }

        /// <summary>
        /// Gets the last thirty days controlled environment day count.
        /// Question Number: G20
        /// <remarks>
        /// Refers to the total number of days in
        /// any controlled environments in the past 30
        /// days. If the patient has been in two environments
        /// total the number of days in both and clarify
        /// in the comments. Code "N" if Question G19
        /// is “No.”
        /// </remarks>
        /// </summary>
        public DensAsiNonResponseType<int?> LastThirtyDaysControlledEnvironmentDayCount
        {
            get { return _lastThirtyDaysControlledEnvironmentDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the last thirty days controlled environment day count note.
        /// Question Number: G20
        /// </summary>
        public string LastThirtyDaysControlledEnvironmentDayCountNote
        {
            get { return _lastThirtyDaysControlledEnvironmentDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the boolean value indicating a christian religion preference.
        /// Question Number: G101
        /// </summary>
        public DensAsiNonResponseType<bool?> ChristianReligionIndicator
        {
            get { return _christianReligionIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the boolean value indicating a christian religion preference note.
        /// Question Number: G101
        /// </summary>
        public string ChristianReligionIndicatorNote
        {
            get { return _christianReligionIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating a buddhism preference.
        /// Question Number: G102
        /// </summary>
        public DensAsiNonResponseType<bool?> BuddhismReligionIndicator
        {
            get { return _buddhismReligionIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating a buddhism preference note.
        /// Question Number: G102
        /// </summary>
        public string BuddhismReligionIndicatorNote
        {
            get { return _buddhismReligionIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating no particular religious sect.
        /// Question Number: G103
        /// </summary>
        public DensAsiNonResponseType<bool?> NoParticularReligiousSectIndicator
        {
            get { return _noParticularReligiousSectIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating no particular religious sect note.
        /// Question Number: G103
        /// </summary>
        public string NoParticularReligiousSectIndicatorNote
        {
            get { return _noParticularReligiousSectIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the possible dens asi non response well known names.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1">
        /// IEnumerable&lt;string&gt;</see>.
        /// </returns>
        public override IEnumerable<string> GetPossibleDensAsiNonResponseWellKnownNames<TProperty> ( Expression<Func<TProperty>> propertyExpression )
        {
            IEnumerable<string> possibleDensAsiNonResponseWellKnownNames;
            string propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );

            if ( propertyName == PropertyUtil.ExtractPropertyName ( () => ResidenceOwnedByYouOrFamilyIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => LastThirtyDaysControlledEnvironmentDayCount ) )
            {
                possibleDensAsiNonResponseWellKnownNames = new List<string>
                                                               {
                                                                   WellKnownNames.DensAsiModule.DensAsiNonResponse.NotAnswered,
                                                                   WellKnownNames.DensAsiModule.DensAsiNonResponse.NotApplicable
                                                               };
            }
            else if ( propertyName == PropertyUtil.ExtractPropertyName ( () => ChristianReligionIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => BuddhismReligionIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => NoParticularReligiousSectIndicator ) )
            {
                possibleDensAsiNonResponseWellKnownNames = new List<string>
                                                               {
                                                                   WellKnownNames.DensAsiModule.DensAsiNonResponse.NotApplicable,
                                                                   WellKnownNames.DensAsiModule.DensAsiNonResponse.NotAnswered
                                                               };
            }
            else
            {
                possibleDensAsiNonResponseWellKnownNames = base.GetPossibleDensAsiNonResponseWellKnownNames ( propertyExpression );
            }

            return possibleDensAsiNonResponseWellKnownNames;
        }

        /// <summary>
        /// Gets the well known names filters dictionary.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IDictionary`2">Dictionary&lt;string,
        /// IEnumerable&lt;string&gt;</see>
        /// </returns>
        public override Dictionary<string, IEnumerable<string>> GetFiltersDictionary ()
        {
            return new Dictionary<string, IEnumerable<string>>
                       {
                           { PropertyUtil.ExtractPropertyName ( () => ResidenceOwnedByYouOrFamilyIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ResidenceOwnedByYouOrFamilyIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => LastThirtyDaysControlledEnvironmentDayCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => LastThirtyDaysControlledEnvironmentDayCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => ChristianReligionIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ChristianReligionIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => BuddhismReligionIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => BuddhismReligionIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => NoParticularReligiousSectIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => NoParticularReligiousSectIndicator ) }
                       };
        }
    }
}