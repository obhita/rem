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

using Pillar.Common.Bootstrapper;
using Rem.Domain.Billing.BillingOfficeModule;
using Rem.Domain.Billing.PayorModule;
using Rem.Infrastructure.Service.DataTransferObject.Mapping;
using Rem.Ria.BillingModule.Web.BillingAdministrationDashboard;
using Rem.Ria.BillingModule.Web.BillingOfficeEditor;
using Rem.Ria.BillingModule.Web.Common;
using Rem.Ria.BillingModule.Web.PayorEditor;

namespace Rem.Ria.BillingModule.Web
{
    /// <summary>
    /// AutoMapperConfig class.
    /// </summary>
    public class AutoMapperConfig : IBootstrapperTask
    {
        #region Public Methods

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void Execute ()
        {
            SetupBillingOffice ();
            SetupPayor ();
            SetupClaim ();
        }

        #endregion

        #region Methods

        private static void SetupBillingOffice ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<BillingOffice, BillingOfficeAddressesDto> ();
            AutoMapperSetup.CreateMapToEditableDto<BillingOfficeAddress, BillingOfficeAddressDto> ()
                .ForMember ( dest => dest.FirstStreetAddress, opt => opt.MapFrom ( src => src.Address.FirstStreetAddress ) )
                .ForMember ( dest => dest.SecondStreetAddress, opt => opt.MapFrom ( src => src.Address.SecondStreetAddress ) )
                .ForMember ( dest => dest.CityName, opt => opt.MapFrom ( src => src.Address.CityName ) )
                .ForMember ( dest => dest.StateProvince, opt => opt.MapFrom ( src => src.Address.StateProvince ) )
                .ForMember ( dest => dest.PostalCode, opt => opt.MapFrom ( src => src.Address.PostalCode.Code ) )
                .ForMember ( dest => dest.Country, opt => opt.MapFrom ( src => src.Address.Country ) )
                .ForMember ( dest => dest.CountyArea, opt => opt.MapFrom ( src => src.Address.CountyArea ) );

            AutoMapperSetup.CreateMapToAbstractDto<BillingOffice, BillingOfficePhonesDto> ();
            AutoMapperSetup.CreateMapToEditableDto<BillingOfficePhone, BillingOfficePhoneDto> ()
                .ForMember ( dest => dest.PhoneNumber, opt => opt.MapFrom ( src => src.Phone.PhoneNumber ) )
                .ForMember ( dest => dest.Extension, opt => opt.MapFrom ( src => src.Phone.PhoneExtensionNumber ) );

            AutoMapperSetup.CreateMapToEditableDto<BillingOffice, BillingOfficeProfileDto> ()
                .ForMember ( dest => dest.Name, opt => opt.MapFrom ( src => src.Profile.Name ) )
                .ForMember ( dest => dest.EffectiveDate, opt => opt.MapFrom ( src => src.Profile.EffectiveDateRange.StartDate ) )
                .ForMember ( dest => dest.EndDate, opt => opt.MapFrom ( src => src.Profile.EffectiveDateRange.EndDate ) )
                .ForMember ( dest => dest.EmailAddress, opt => opt.MapFrom ( src => src.Profile.EmailAddress.Address ) );

            AutoMapperSetup.CreateMapToAbstractDto<BillingOffice, BillingOfficeDto> ()
                .ForMember ( dest => dest.Profile, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.Addresses, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.PhoneNumbers, opt => opt.MapFrom ( src => src ) );

            AutoMapperSetup.CreateMapToAbstractDto<BillingOffice, BillingOfficeSummaryDto> ()
                .ForMember ( dest => dest.Name, opt => opt.MapFrom ( src => src.Profile.Name ) );
        }

        private static void SetupClaim ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<Domain.Billing.ClaimModule.Claim, ClaimSummaryDto> ()
                .ForMember ( dest => dest.PayorName, opt => opt.MapFrom ( src => src.Payor.Name ) )
                .ForMember ( dest => dest.PatientFirstName, opt => opt.MapFrom ( src => src.PatientAccount.Name.First ) )
                .ForMember ( dest => dest.PatientLastName, opt => opt.MapFrom ( src => src.PatientAccount.Name.Last ) )
                .ForMember ( dest => dest.PayorType, opt => opt.MapFrom ( src => src.ClaimBatch.PayorType.Name ) );
        }

        private static void SetupPayor ()
        {
            AutoMapperSetup.CreateMapToAbstractDto<PayorTypeMember, PayorTypeDto> ()
                .ForMember ( dest => dest.Name, opt => opt.MapFrom ( src => src.PayorType.Name ) )
                .ForMember ( dest => dest.Key, opt => opt.MapFrom ( src => src.PayorType.Key ) )
                .ForMember ( dest => dest.BillingForm, opt => opt.MapFrom ( src => src.PayorType.BillingForm ) )
                .ForMember ( dest => dest.SubmitterIdentifier, opt => opt.MapFrom ( src => src.PayorType.SubmitterIdentifier ) )
                .ForMember ( dest => dest.BillingOfficeKey, opt => opt.MapFrom ( src => src.PayorType.BillingOffice.Key ) )
                .ForMember ( dest => dest.CityName, opt => opt.MapFrom ( src => src.PayorType.BillingAddress.CityName ) )
                .ForMember ( dest => dest.Country, opt => opt.MapFrom ( src => src.PayorType.BillingAddress.Country ) )
                .ForMember ( dest => dest.CountyArea, opt => opt.MapFrom ( src => src.PayorType.BillingAddress.CountyArea ) )
                .ForMember ( dest => dest.FirstStreetAddress, opt => opt.MapFrom ( src => src.PayorType.BillingAddress.FirstStreetAddress ) )
                .ForMember ( dest => dest.PostalCode, opt => opt.MapFrom ( src => src.PayorType.BillingAddress.PostalCode.Code ) )
                .ForMember ( dest => dest.SecondStreetAddress, opt => opt.MapFrom ( src => src.PayorType.BillingAddress.SecondStreetAddress ) )
                .ForMember ( dest => dest.StateProvince, opt => opt.MapFrom ( src => src.PayorType.BillingAddress.StateProvince ) )
                .ForMember ( dest => dest.PhoneNumber, opt => opt.MapFrom ( src => src.PayorType.BillingPhone.PhoneNumber ) )
                .ForMember ( dest => dest.PhoneExtensionNumber, opt => opt.MapFrom ( src => src.PayorType.BillingPhone.PhoneExtensionNumber ) )
                .ForMember ( dest => dest.FtpAddress, opt => opt.MapFrom ( src => src.PayorType.BillingFtp.HostValue ) )
                .ForMember (
                    dest => dest.InterchangeReceiverNumber,
                    opt => opt.MapFrom ( src => src.PayorType.HealthCareClaim837Setup.InterchangeReceiverNumber ) )
                .ForMember (
                    dest => dest.InterchangeSenderNumber, opt => opt.MapFrom ( src => src.PayorType.HealthCareClaim837Setup.InterchangeSenderNumber ) )
                .ForMember (
                    dest => dest.CompositeDelimiter,
                    opt => opt.MapFrom ( src => src.PayorType.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter ) )
                .ForMember (
                    dest => dest.ElementDelimiter, opt => opt.MapFrom ( src => src.PayorType.HealthCareClaim837Setup.X12Delimiters.ElementDelimiter ) )
                .ForMember (
                    dest => dest.SegmentDelimiter, opt => opt.MapFrom ( src => src.PayorType.HealthCareClaim837Setup.X12Delimiters.SegmentDelimiter ) )
                .ForMember (
                    dest => dest.RepetitionDelimiter,
                    opt => opt.MapFrom ( src => src.PayorType.HealthCareClaim837Setup.X12Delimiters.RepetitionDelimiter ) );

            AutoMapperSetup.CreateMapToEditableDto<Payor, PayorProfileDto> ()
                .ForMember ( dest => dest.BillingOfficeKey, opt => opt.MapFrom ( src => src.BillingOffice.Key ) )
                .ForMember ( dest => dest.EffectiveDate, opt => opt.MapFrom ( src => src.EffectiveDateRange.StartDate ) )
                .ForMember ( dest => dest.EndDate, opt => opt.MapFrom ( src => src.EffectiveDateRange.EndDate ) )
                .ForMember ( dest => dest.EmailAddress, opt => opt.MapFrom ( src => src.EmailAddress.Address ) )
                .ForMember ( dest => dest.PayorTypes, opt => opt.MapFrom ( src => src.PayorTypeMembers ) )
                .ForMember ( dest => dest.PrimaryPayorType, opt => opt.MapFrom ( src => src.PrimaryPayorTypeMember ) );

            AutoMapperSetup.CreateMapToEditableDto<Payor, PayorAddressesDto> ();
            AutoMapperSetup.CreateMapToEditableDto<PayorAddress, PayorAddressDto> ()
                .ForMember ( dest => dest.FirstStreetAddress, opt => opt.MapFrom ( src => src.Address.FirstStreetAddress ) )
                .ForMember ( dest => dest.SecondStreetAddress, opt => opt.MapFrom ( src => src.Address.SecondStreetAddress ) )
                .ForMember ( dest => dest.CityName, opt => opt.MapFrom ( src => src.Address.CityName ) )
                .ForMember ( dest => dest.StateProvince, opt => opt.MapFrom ( src => src.Address.StateProvince ) )
                .ForMember ( dest => dest.PostalCode, opt => opt.MapFrom ( src => src.Address.PostalCode.Code ) )
                .ForMember ( dest => dest.Country, opt => opt.MapFrom ( src => src.Address.Country ) )
                .ForMember ( dest => dest.CountyArea, opt => opt.MapFrom ( src => src.Address.CountyArea ) );

            AutoMapperSetup.CreateMapToEditableDto<Payor, PayorPhoneNumbersDto> ();
            AutoMapperSetup.CreateMapToEditableDto<PayorPhone, PayorPhoneDto> ()
                .ForMember ( dest => dest.PhoneNumber, opt => opt.MapFrom ( src => src.Phone.PhoneNumber ) )
                .ForMember ( dest => dest.PhoneExtensionNumber, opt => opt.MapFrom ( src => src.Phone.PhoneExtensionNumber ) );

            AutoMapperSetup.CreateMapToAbstractDto<Payor, PayorDto> ()
                .ForMember ( dest => dest.Profile, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.Addresses, opt => opt.MapFrom ( src => src ) )
                .ForMember ( dest => dest.PhoneNumbers, opt => opt.MapFrom ( src => src ) );

            AutoMapperSetup.CreateMapToEditableDto<PayorType, PayorTypeDto> ()
                .ForMember ( dest => dest.BillingOfficeKey, opt => opt.MapFrom ( src => src.BillingOffice.Key ) )
                .ForMember ( dest => dest.CityName, opt => opt.MapFrom ( src => src.BillingAddress.CityName ) )
                .ForMember ( dest => dest.Country, opt => opt.MapFrom ( src => src.BillingAddress.Country ) )
                .ForMember ( dest => dest.CountyArea, opt => opt.MapFrom ( src => src.BillingAddress.CountyArea ) )
                .ForMember ( dest => dest.FirstStreetAddress, opt => opt.MapFrom ( src => src.BillingAddress.FirstStreetAddress ) )
                .ForMember ( dest => dest.PostalCode, opt => opt.MapFrom ( src => src.BillingAddress.PostalCode.Code ) )
                .ForMember ( dest => dest.SecondStreetAddress, opt => opt.MapFrom ( src => src.BillingAddress.SecondStreetAddress ) )
                .ForMember ( dest => dest.StateProvince, opt => opt.MapFrom ( src => src.BillingAddress.StateProvince ) )
                .ForMember ( dest => dest.PhoneNumber, opt => opt.MapFrom ( src => src.BillingPhone.PhoneNumber ) )
                .ForMember ( dest => dest.PhoneExtensionNumber, opt => opt.MapFrom ( src => src.BillingPhone.PhoneExtensionNumber ) )
                .ForMember ( dest => dest.FtpAddress, opt => opt.MapFrom ( src => src.BillingFtp.HostValue ) )
                .ForMember (
                    dest => dest.InterchangeReceiverNumber, opt => opt.MapFrom ( src => src.HealthCareClaim837Setup.InterchangeReceiverNumber ) )
                .ForMember ( dest => dest.InterchangeSenderNumber, opt => opt.MapFrom ( src => src.HealthCareClaim837Setup.InterchangeSenderNumber ) )
                .ForMember (
                    dest => dest.CompositeDelimiter, opt => opt.MapFrom ( src => src.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter ) )
                .ForMember ( dest => dest.ElementDelimiter, opt => opt.MapFrom ( src => src.HealthCareClaim837Setup.X12Delimiters.ElementDelimiter ) )
                .ForMember ( dest => dest.SegmentDelimiter, opt => opt.MapFrom ( src => src.HealthCareClaim837Setup.X12Delimiters.SegmentDelimiter ) )
                .ForMember (
                    dest => dest.RepetitionDelimiter, opt => opt.MapFrom ( src => src.HealthCareClaim837Setup.X12Delimiters.RepetitionDelimiter ) );
        }

        #endregion
    }
}
