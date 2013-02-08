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

using Pillar.Domain.Primitives;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.AgencyModule.Web.StaffEditor
{
    /// <summary>
    /// Class for handling save staff profile request.
    /// </summary>
    public class SaveStaffProfileRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<StaffProfileDto>, DtoResponse<StaffProfileDto>, StaffProfileDto, Staff>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveStaffProfileRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveStaffProfileRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="staff">The staff.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( StaffProfileDto dto, Staff staff )
        {
            _mappingResult &= MapProperties ( staff, dto );
            return _mappingResult;
        }

        private static void RemoveStaffLanguage ( StaffLanguageDto staffLanguageDto, Staff staff, StaffLanguage staffLanguage )
        {
            staff.RemoveLanguage ( staffLanguage );
        }

        private void AddStaffLanguage ( StaffLanguageDto staffLanguageDto, Staff staff )
        {
            var language = _mappingHelper.MapLookupField<Language> ( staffLanguageDto.Language );
            var languageFluency = _mappingHelper.MapLookupField<LanguageFluency> ( staffLanguageDto.LanguageFluency );

            staff.AddLanguage ( new StaffLanguage ( language, languageFluency ) );
        }

        private void ChangeStaffLanguage ( StaffLanguageDto staffLanguageDto, Staff staff, StaffLanguage staffLanguage )
        {
            RemoveStaffLanguage ( staffLanguageDto, staff, staffLanguage );
            AddStaffLanguage ( staffLanguageDto, staff );
        }

        private bool MapProperties ( Staff staff, StaffProfileDto dto )
        {
            var staffType = _mappingHelper.MapLookupField<StaffType> ( dto.StaffType );
            var gender = _mappingHelper.MapLookupField<Gender> ( dto.Gender );

            staff.ReviseStaffProfile (
                new StaffProfileBuilder ().WithStaffName (
                    new PersonNameBuilder ().WithPrefix ( dto.PrefixName ).WithFirst ( dto.FirstName ).WithMiddle( dto.MiddleName )
                        .WithLast (
                            dto.LastName ).WithSuffix ( dto.SuffixName ) ).WithProfessionalCredentialNote ( dto.ProfessionalCredentialNote )
                    .WithBirthDate (
                        dto.BirthDate ).WithSocialSecurityNumber ( dto.SocialSecurityNumber ).WithEmailAddress (
                            string.IsNullOrWhiteSpace ( dto.EmailAddress ) ? null : new EmailAddress ( dto.EmailAddress ) ).WithNote ( dto.Note )
                    .WithGender ( gender ).WithStaffType ( staffType ).WithEmploymentDateRange ( new DateRange ( dto.StartDate, dto.EndDate ) ) );

            _mappingResult &=
                new AggregateNodeCollectionMapper<StaffLanguageDto, Staff, StaffLanguage> (
                    dto.Languages, staff, staff.Languages ).MapRemovedItem ( RemoveStaffLanguage ).MapAddedItem (
                        AddStaffLanguage ).MapChangedItem ( ChangeStaffLanguage ).Map ();

            return _mappingResult;
        }

        #endregion
    }
}
