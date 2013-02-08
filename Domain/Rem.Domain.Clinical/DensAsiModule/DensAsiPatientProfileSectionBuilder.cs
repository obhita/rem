using System;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// DensAsiPatientProfileSectionBuilder provides a fluent interface for creating a General Information section.
    /// </summary>
    public class DensAsiPatientProfileSectionBuilder
    {
        private DateTime? _interviewDate;
        private string _interviewDateNote;
        private DensAsiInterviewClass _densAsiInterviewClass;
        private string _densAsiInterviewClassNote;
        private DensAsiNonResponseType<DensAsiInterviewContactType> _densAsiInterviewContactType;
        private string _densAsiInterviewContactTypeNote;
        private DensAsiNonResponseType<TimeSpan?> _yearsAndMonthsAtCurrentAddressTimeSpan;
        private string _yearsAndMonthsAtCurrentAddressTimeSpanNote;
        private DensAsiNonResponseType<bool?> _residenceOwnedByYouOrFamilyIndicator;
        private string _residenceOwnedByYouOrFamilyIndicatorNote;
        private DensAsiNonResponseType<DensAsiReligion> _preferredDensAsiReligion;
        private string _preferredDensAsiReligionNote;
        private DensAsiNonResponseType<DensAsiControlledEnvironment> _lastThirtyDaysDensAsiControlledEnvironment;
        private string _lastThirtyDaysDensAsiControlledEnvironmentNote;
        private DensAsiNonResponseType<int?> _lastThirtyDaysControlledEnvironmentDayCount;
        private string _lastThirtyDaysControlledEnvironmentDayCountNote;
        private DensAsiNonResponseType<bool?> _christianReligionIndicator;
        private string _christianReligionIndicatorNote;
        private DensAsiNonResponseType<bool?> _buddhismReligionIndicator;
        private string _buddhismReligionIndicatorNote;
        private DensAsiNonResponseType<bool?> _noParticularReligiousSectIndicator;
        private string _noParticularReligiousSectIndicatorNote;

        /// <summary>
        /// Assigns the interview date.
        /// </summary>
        /// <param name="interviewDate">The interview date.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiPatientProfileSectionBuilder WithInterviewDate(DateTime? interviewDate)
        {
            _interviewDate = interviewDate;
            return this;
        }

        /// <summary>
        /// Assigns the interview date note.
        /// </summary>
        /// <param name="interviewDateNote">The interview date note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiPatientProfileSectionBuilder WithInterviewDateNote(string interviewDateNote)
        {
            _interviewDateNote = interviewDateNote;
            return this;
        }

        /// <summary>
        /// Assigns the DensAsi interview class.
        /// </summary>
        /// <param name="densAsiInterviewClass">The DensAsi interview class.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiPatientProfileSectionBuilder WithDensAsiInterviewClass(DensAsiInterviewClass densAsiInterviewClass)
        {
            _densAsiInterviewClass = densAsiInterviewClass;
            return this;
        }

        /// <summary>
        /// Assigns the DensAsi interview class note.
        /// </summary>
        /// <param name="densAsiInterviewClassNote">The DensAsi interview class note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiPatientProfileSectionBuilder WithDensAsiInterviewClassNote(string densAsiInterviewClassNote)
        {
            _densAsiInterviewClassNote = densAsiInterviewClassNote;
            return this;
        }

        /// <summary>
        /// Assigns the type of the DensAsi interview contact.
        /// </summary>
        /// <param name="densAsiInterviewContactType">Type of the DensAsi interview contact.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiPatientProfileSectionBuilder WithDensAsiInterviewContactType(DensAsiNonResponseType<DensAsiInterviewContactType> densAsiInterviewContactType)
        {
            _densAsiInterviewContactType = densAsiInterviewContactType;
            return this;
        }

        /// <summary>
        /// Assigns the DensAsi interview contact type note.
        /// </summary>
        /// <param name="densAsiInterviewContactTypeNote">The DensAsi interview contact type note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiPatientProfileSectionBuilder WithDensAsiInterviewContactTypeNote(string densAsiInterviewContactTypeNote)
        {
            _densAsiInterviewContactTypeNote = densAsiInterviewContactTypeNote;
            return this;
        }

        /// <summary>
        /// Assigns the years and months at current address time span.
        /// </summary>
        /// <param name="yearsAndMonthsAtCurrentAddressTimeSpan">The years and months at current address time span.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiPatientProfileSectionBuilder WithYearsAndMonthsAtCurrentAddressTimeSpan(DensAsiNonResponseType<TimeSpan?> yearsAndMonthsAtCurrentAddressTimeSpan)
        {
            _yearsAndMonthsAtCurrentAddressTimeSpan = yearsAndMonthsAtCurrentAddressTimeSpan;
            return this;
        }

        /// <summary>
        /// Assigns the years and months at current address time span note.
        /// </summary>
        /// <param name="yearsAndMonthsAtCurrentAddressTimeSpanNote">The years and months at current address time span note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiPatientProfileSectionBuilder WithYearsAndMonthsAtCurrentAddressTimeSpanNote(string yearsAndMonthsAtCurrentAddressTimeSpanNote)
        {
            _yearsAndMonthsAtCurrentAddressTimeSpanNote = yearsAndMonthsAtCurrentAddressTimeSpanNote;
            return this;
        }

        /// <summary>
        /// Assigns the residence owned by you or family indicator.
        /// </summary>
        /// <param name="residenceOwnedByYouOrFamilyIndicator">The residence owned by you or family indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiPatientProfileSectionBuilder WithResidenceOwnedByYouOrFamilyIndicator(DensAsiNonResponseType<bool?> residenceOwnedByYouOrFamilyIndicator)
        {
            _residenceOwnedByYouOrFamilyIndicator = residenceOwnedByYouOrFamilyIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the residence owned by you or family indicator note.
        /// </summary>
        /// <param name="residenceOwnedByYouOrFamilyIndicatorNote">The residence owned by you or family indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiPatientProfileSectionBuilder WithResidenceOwnedByYouOrFamilyIndicatorNote(string residenceOwnedByYouOrFamilyIndicatorNote)
        {
            _residenceOwnedByYouOrFamilyIndicatorNote = residenceOwnedByYouOrFamilyIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the preferred DensAsi religion.
        /// </summary>
        /// <param name="preferredDensAsiReligion">The preferred DensAsi religion.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiPatientProfileSectionBuilder WithPreferredDensAsiReligion(DensAsiNonResponseType<DensAsiReligion> preferredDensAsiReligion)
        {
            _preferredDensAsiReligion = preferredDensAsiReligion;
            return this;
        }

        /// <summary>
        /// Assigns the preferred DensAsi religion note.
        /// </summary>
        /// <param name="preferredDensAsiReligionNote">The preferred DensAsi religion note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiPatientProfileSectionBuilder WithPreferredDensAsiReligionNote(string preferredDensAsiReligionNote)
        {
            _preferredDensAsiReligionNote = preferredDensAsiReligionNote;
            return this;
        }

        /// <summary>
        /// Assigns the last thirty days DensAsi controlled environment.
        /// </summary>
        /// <param name="lastThirtyDaysDensAsiControlledEnvironment">The last thirty days DensAsi controlled environment.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiPatientProfileSectionBuilder WithLastThirtyDaysDensAsiControlledEnvironment(DensAsiNonResponseType<DensAsiControlledEnvironment> lastThirtyDaysDensAsiControlledEnvironment)
        {
            _lastThirtyDaysDensAsiControlledEnvironment = lastThirtyDaysDensAsiControlledEnvironment;
            return this;
        }


        /// <summary>
        /// Assigns the last thirty days dens asi controlled environment note.
        /// </summary>
        /// <param name="lastThirtyDaysDensAsiControlledEnvironmentNote">The last thirty days dens asi controlled environment note.</param>
        /// <returns>A DensAsiPatientProfileSectionBuilder.</returns>
        public DensAsiPatientProfileSectionBuilder WithLastThirtyDaysDensAsiControlledEnvironmentNote(string lastThirtyDaysDensAsiControlledEnvironmentNote)
        {
            _lastThirtyDaysDensAsiControlledEnvironmentNote = lastThirtyDaysDensAsiControlledEnvironmentNote;
            return this;
        }

        /// <summary>
        /// Assigns the last thirty days controlled environment day count.
        /// </summary>
        /// <param name="lastThirtyDaysControlledEnvironmentDayCount">The last thirty days controlled environment day count.</param>
        /// <returns>A DensAsiPatientProfileSectionBuilder.</returns>
        public DensAsiPatientProfileSectionBuilder WithLastThirtyDaysControlledEnvironmentDayCount(DensAsiNonResponseType<int?> lastThirtyDaysControlledEnvironmentDayCount)
        {
            _lastThirtyDaysControlledEnvironmentDayCount = lastThirtyDaysControlledEnvironmentDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the last thirty days controlled environment day count note.
        /// </summary>
        /// <param name="lastThirtyDaysControlledEnvironmentDayCountNote">The last thirty days controlled environment day count note.</param>
        /// <returns>A DensAsiPatientProfileSectionBuilder.</returns>
        public DensAsiPatientProfileSectionBuilder WithLastThirtyDaysControlledEnvironmentDayCountNote(string lastThirtyDaysControlledEnvironmentDayCountNote)
        {
            _lastThirtyDaysControlledEnvironmentDayCountNote = lastThirtyDaysControlledEnvironmentDayCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the christian religion indicator.
        /// </summary>
        /// <param name="christianReligionIndicator">The christian religion indicator.</param>
        /// <returns>A DensAsiPatientProfileSectionBuilder.</returns>
        public DensAsiPatientProfileSectionBuilder WithChristianReligionIndicator(DensAsiNonResponseType<bool?> christianReligionIndicator)
        {
            _christianReligionIndicator = christianReligionIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the christian religion indicator note.
        /// </summary>
        /// <param name="christianReligionIndicatorNote">The christian religion indicator note.</param>
        /// <returns>A DensAsiPatientProfileSectionBuilder.</returns>
        public DensAsiPatientProfileSectionBuilder WithChristianReligionIndicatorNote(string christianReligionIndicatorNote)
        {
            _christianReligionIndicatorNote = christianReligionIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the buddhism religion indicator.
        /// </summary>
        /// <param name="buddhismReligionIndicator">The buddhism religion indicator.</param>
        /// <returns>A DensAsiPatientProfileSectionBuilder.</returns>
        public DensAsiPatientProfileSectionBuilder WithBuddhismReligionIndicator(DensAsiNonResponseType<bool?> buddhismReligionIndicator)
        {
            _buddhismReligionIndicator = buddhismReligionIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the buddhism religion indicator note.
        /// </summary>
        /// <param name="buddhismReligionIndicatorNote">The buddhism religion indicator note.</param>
        /// <returns>A DensAsiPatientProfileSectionBuilder.</returns>
        public DensAsiPatientProfileSectionBuilder WithBuddhismReligionIndicatorNote(string buddhismReligionIndicatorNote)
        {
            _buddhismReligionIndicatorNote = buddhismReligionIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the no particular religious sect indicator.
        /// </summary>
        /// <param name="noParticularReligiousSectIndicator">The no particular religious sect indicator.</param>
        /// <returns>A DensAsiPatientProfileSectionBuilder.</returns>
        public DensAsiPatientProfileSectionBuilder WithNoParticularReligiousSectIndicator(DensAsiNonResponseType<bool?> noParticularReligiousSectIndicator)
        {
            _noParticularReligiousSectIndicator = noParticularReligiousSectIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the no particular religious sect indicator note.
        /// </summary>
        /// <param name="noParticularReligiousSectIndicatorNote">The no particular religious sect indicator note.</param>
        /// <returns>A DensAsiPatientProfileSectionBuilder.</returns>
        public DensAsiPatientProfileSectionBuilder WithNoParticularReligiousSectIndicatorNote(string noParticularReligiousSectIndicatorNote)
        {
            _noParticularReligiousSectIndicatorNote = noParticularReligiousSectIndicatorNote;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A DensAsiPatientProfileSectionBuilder.</returns>
        public DensAsiPatientProfileSection Build()
        {
            return new DensAsiPatientProfileSection(
                _interviewDate,
                _interviewDateNote,
                _densAsiInterviewClass,
                _densAsiInterviewClassNote,
                _densAsiInterviewContactType,
                _densAsiInterviewContactTypeNote,
                _yearsAndMonthsAtCurrentAddressTimeSpan,
                _yearsAndMonthsAtCurrentAddressTimeSpanNote,
                _residenceOwnedByYouOrFamilyIndicator,
                _residenceOwnedByYouOrFamilyIndicatorNote,
                _preferredDensAsiReligion,
                _preferredDensAsiReligionNote,
                _lastThirtyDaysDensAsiControlledEnvironment,
                _lastThirtyDaysDensAsiControlledEnvironmentNote,
                _lastThirtyDaysControlledEnvironmentDayCount,
                _lastThirtyDaysControlledEnvironmentDayCountNote,
                _christianReligionIndicator,
                _christianReligionIndicatorNote,
                _buddhismReligionIndicator,
                _buddhismReligionIndicatorNote,
                _noParticularReligiousSectIndicator,
                _noParticularReligiousSectIndicatorNote
                );
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="DensAsiPatientProfileSectionBuilder"/> to <see cref="DensAsiPatientProfileSection"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator DensAsiPatientProfileSection(DensAsiPatientProfileSectionBuilder builder)
        {
            return builder.Build();
        }
    }
}
