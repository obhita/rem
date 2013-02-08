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

using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Pillar.FluentRuleEngine.RuleSelectors;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.FrontDeskDashboard;
using Rem.WellKnownNames.PatientAccountModule;

namespace Rem.Ria.PatientModule.FrontDeskDashboard
{
    /// <summary>
    /// Rule collection for EditPayorCoverageViewModel class.
    /// </summary>
    public class EditPayorCoverageViewModelRuleCollection : AbstractRuleCollection<EditPayorCoverageViewModel>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditPayorCoverageViewModelRuleCollection"/> class.
        /// </summary>
        public EditPayorCoverageViewModelRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            string requiredStringFormat = "{0} is required.";

            var payorError = new DataErrorInfo (
                string.Format ( requiredStringFormat, "Payor" ),
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PayorCoverageCacheDto, object>(dto => dto.PayorCache));

            NewPropertyRule ( () => PayorIsRequired )
                .WithProperty ( s => s.PayorCoverage.PayorCache )
                .NotNull ()
                .ElseThen ( s => s.PayorCoverage.AddDataErrorInfo ( payorError ) )
                .Then ( s => s.PayorCoverage.RemoveDataErrorInfo ( payorError ) );

            var startDateError = new DataErrorInfo (
                string.Format ( requiredStringFormat, "Start Date" ),
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PayorCoverageCacheDto, object>(dto => dto.StartDate));

            NewPropertyRule ( () => StartDateIsRequired )
                .WithProperty ( s => s.PayorCoverage.StartDate )
                .NotNull ()
                .ElseThen ( s => s.PayorCoverage.AddDataErrorInfo ( startDateError ) )
                .Then ( s => s.PayorCoverage.RemoveDataErrorInfo ( startDateError ) );

            var payorTypeError = new DataErrorInfo (
                string.Format ( requiredStringFormat, "Payor Type" ),
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PayorCoverageCacheDto, object>(dto => dto.PayorCoverageCacheType));

            NewPropertyRule ( () => PayorTypeIsRequired )
                .WithProperty ( s => s.PayorCoverage.PayorCoverageCacheType )
                .NotNull ()
                .ElseThen ( s => s.PayorCoverage.AddDataErrorInfo ( payorTypeError ) )
                .Then ( s => s.PayorCoverage.RemoveDataErrorInfo ( payorTypeError ) );

            var memberNumberError = new DataErrorInfo (
                string.Format ( requiredStringFormat, "Member Number" ),
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PayorCoverageCacheDto, object>(dto => dto.MemberNumber));

            NewPropertyRule ( () => MemberNumberIsRequired )
                .WithProperty ( s => s.PayorCoverage.MemberNumber )
                .NotEmpty ()
                .ElseThen ( s => s.PayorCoverage.AddDataErrorInfo ( memberNumberError ) )
                .Then ( s => s.PayorCoverage.RemoveDataErrorInfo ( memberNumberError ) );

            var subscriberFirstNameError = new DataErrorInfo (
                string.Format ( requiredStringFormat, "First Name" ),
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PayorSubscriberCacheDto, object> ( dto => dto.FirstName ) );

            NewPropertyRule ( () => SubscriberFirstNameIsRequired )
                .WithProperty ( s => s.PayorCoverage.PayorSubscriberCache.FirstName )
                .NotEmpty()
                .ElseThen(s => s.PayorCoverage.PayorSubscriberCache.AddDataErrorInfo(subscriberFirstNameError))
                .Then(s => s.PayorCoverage.PayorSubscriberCache.RemoveDataErrorInfo(subscriberFirstNameError));

            var subscriberLastNameError = new DataErrorInfo (
                string.Format ( requiredStringFormat, "Last Name" ),
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PayorSubscriberCacheDto, object>(dto => dto.LastName));

            NewPropertyRule ( () => SubscriberLastNameIsRequired )
                .WithProperty ( s => s.PayorCoverage.PayorSubscriberCache.LastName )
                .NotEmpty()
                .ElseThen(s => s.PayorCoverage.PayorSubscriberCache.AddDataErrorInfo(subscriberLastNameError))
                .Then(s => s.PayorCoverage.PayorSubscriberCache.RemoveDataErrorInfo(subscriberLastNameError));

            var subscriberGenderError = new DataErrorInfo (
                string.Format ( requiredStringFormat, "Gender" ),
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PayorSubscriberCacheDto, object>(dto => dto.AdministrativeGender));

            NewPropertyRule ( () => SubscriberGenderIsRequired )
                .WithProperty ( s => s.PayorCoverage.PayorSubscriberCache.AdministrativeGender )
                .NotNull ()
                .ElseThen(s => s.PayorCoverage.PayorSubscriberCache.AddDataErrorInfo(subscriberGenderError))
                .Then(s => s.PayorCoverage.PayorSubscriberCache.RemoveDataErrorInfo(subscriberGenderError));

            var subscriberBirthDateError = new DataErrorInfo (
                string.Format ( requiredStringFormat, "Birth Date" ),
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PayorSubscriberCacheDto, object>(dto => dto.BirthDate));

            NewPropertyRule ( () => SubscriberBirthDateIsRequired )
                .WithProperty ( s => s.PayorCoverage.PayorSubscriberCache.BirthDate )
                .NotNull ()
                .ElseThen(s => s.PayorCoverage.PayorSubscriberCache.AddDataErrorInfo(subscriberBirthDateError))
                .Then(s => s.PayorCoverage.PayorSubscriberCache.RemoveDataErrorInfo(subscriberBirthDateError));

            var subscriberRelationshipError = new DataErrorInfo (
                string.Format ( requiredStringFormat, "Relationship Status" ),
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PayorSubscriberCacheDto, object>(dto => dto.PayorSubscriberRelationshipCacheType));

            NewPropertyRule ( () => SubscriberRelationshipIsRequired )
                .WithProperty ( s => s.PayorCoverage.PayorSubscriberCache.PayorSubscriberRelationshipCacheType )
                .NotNull ()
                .ElseThen(s => s.PayorCoverage.PayorSubscriberCache.AddDataErrorInfo(subscriberRelationshipError))
                .Then ( s => s.PayorCoverage.PayorSubscriberCache.RemoveDataErrorInfo ( subscriberRelationshipError ) );

            var subscriberAddressLine1Error = new DataErrorInfo (
                string.Format ( requiredStringFormat, "Address Line 1" ),
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<AddressDto, object>(dto => dto.FirstStreetAddress));

            NewPropertyRule ( () => SubscriberAddressLine1IsRequired )
                .WithProperty ( s => s.PayorCoverage.PayorSubscriberCache.Address.FirstStreetAddress )
                .NotEmpty()
                .ElseThen(s => s.PayorCoverage.PayorSubscriberCache.Address.AddDataErrorInfo(subscriberAddressLine1Error))
                .Then(s => s.PayorCoverage.PayorSubscriberCache.Address.RemoveDataErrorInfo(subscriberAddressLine1Error));

            var subscriberCityError = new DataErrorInfo (
                string.Format ( requiredStringFormat, "City" ),
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<AddressDto, object>(dto => dto.CityName));

            NewPropertyRule ( () => SubscriberCityIsRequired )
                .WithProperty ( s => s.PayorCoverage.PayorSubscriberCache.Address.CityName )
                .NotEmpty()
                .ElseThen(s => s.PayorCoverage.PayorSubscriberCache.Address.AddDataErrorInfo(subscriberCityError))
                .Then(s => s.PayorCoverage.PayorSubscriberCache.Address.RemoveDataErrorInfo(subscriberCityError));

            var subscriberStateError = new DataErrorInfo (
                string.Format ( requiredStringFormat, "State" ),
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<AddressDto, object>(dto => dto.StateProvince));

            NewPropertyRule ( () => SubscriberStateIsRequired )
                .WithProperty ( s => s.PayorCoverage.PayorSubscriberCache.Address.StateProvince )
                .NotNull ()
                .ElseThen(s => s.PayorCoverage.PayorSubscriberCache.Address.AddDataErrorInfo(subscriberStateError))
                .Then(s => s.PayorCoverage.PayorSubscriberCache.Address.RemoveDataErrorInfo(subscriberStateError));

            var subscriberPostalCodeError = new DataErrorInfo (
                string.Format ( requiredStringFormat, "Postal Code" ),
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<AddressDto, object>(dto => dto.PostalCode));

            NewPropertyRule ( () => SubscriberPostalCodeIsRequired )
                .WithProperty ( s => s.PayorCoverage.PayorSubscriberCache.Address.PostalCode )
                .NotEmpty()
                .ElseThen(s => s.PayorCoverage.PayorSubscriberCache.Address.AddDataErrorInfo(subscriberPostalCodeError))
                .Then ( s => s.PayorCoverage.PayorSubscriberCache.Address.RemoveDataErrorInfo ( subscriberPostalCodeError ) );

            NewPropertyRule ( () => SetDefaultsForSelf )
                .WithProperty ( s => s.PayorCoverage.PayorSubscriberCache.PayorSubscriberRelationshipCacheType.WellKnownName )
                .DoNotAutoValidate ()
                .EqualTo ( PayorSubscriberRelationshipType.Self )
                .Then (
                    ( s, ctx ) =>
                        {
                            if ( ctx.RuleSelector is IMemberNameRuleSelector &&
                                 ( ctx.RuleSelector as IMemberNameRuleSelector ).MemberName
                                 == PropertyUtil.ExtractPropertyName<PayorSubscriberCacheDto, LookupValueDto> ( ps => ps.PayorSubscriberRelationshipCacheType ) )
                            {
                                var patientSummary = s.PatientSummary;
                                s.PayorCoverage.PayorSubscriberCache.FirstName = patientSummary.FirstName;
                                s.PayorCoverage.PayorSubscriberCache.LastName = patientSummary.LastName;
                                s.PayorCoverage.PayorSubscriberCache.MiddleName = patientSummary.MiddleName;
                                s.PayorCoverage.PayorSubscriberCache.AdministrativeGender = patientSummary.Gender;
                                s.PayorCoverage.PayorSubscriberCache.BirthDate = patientSummary.BirthDate;

                                if ( patientSummary.PatientHomeAddress != null )
                                {
                                    s.PayorCoverage.PayorSubscriberCache.Address.FirstStreetAddress = patientSummary.PatientHomeAddress.FirstStreetAddress;
                                    s.PayorCoverage.PayorSubscriberCache.Address.SecondStreetAddress =
                                        patientSummary.PatientHomeAddress.SecondStreetAddress;
                                    s.PayorCoverage.PayorSubscriberCache.Address.CityName = patientSummary.PatientHomeAddress.CityName;
                                    s.PayorCoverage.PayorSubscriberCache.Address.StateProvince = patientSummary.PatientHomeAddress.StateProvince;
                                    s.PayorCoverage.PayorSubscriberCache.Address.PostalCode = patientSummary.PatientHomeAddress.PostalCode;
                                }
                            }
                        } );

            NewRuleSet (
                () => SaveCommandRuleSet,
                PayorIsRequired,
                PayorTypeIsRequired,
                MemberNumberIsRequired,
                StartDateIsRequired,
                SubscriberAddressLine1IsRequired,
                SubscriberBirthDateIsRequired,
                SubscriberCityIsRequired,
                SubscriberFirstNameIsRequired,
                SubscriberGenderIsRequired,
                SubscriberLastNameIsRequired,
                SubscriberPostalCodeIsRequired,
                SubscriberRelationshipIsRequired,
                SubscriberStateIsRequired );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the save command rule set.
        /// </summary>
        /// <value>The save command rule set.</value>
        public IRuleSet SaveCommandRuleSet { get; set; }

        /// <summary>
        /// Gets or sets the member number is required.
        /// </summary>
        /// <value>The member number is required.</value>
        public IPropertyRule MemberNumberIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the payor is required.
        /// </summary>
        /// <value>The payor is required.</value>
        public IPropertyRule PayorIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the payor type is required.
        /// </summary>
        /// <value>The payor type is required.</value>
        public IPropertyRule PayorTypeIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the start date is required.
        /// </summary>
        /// <value>The start date is required.</value>
        public IPropertyRule StartDateIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the subscriber address line1 is required.
        /// </summary>
        /// <value>The subscriber address line1 is required.</value>
        public IPropertyRule SubscriberAddressLine1IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the subscriber birth date is required.
        /// </summary>
        /// <value>The subscriber birth date is required.</value>
        public IPropertyRule SubscriberBirthDateIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the subscriber city is required.
        /// </summary>
        /// <value>The subscriber city is required.</value>
        public IPropertyRule SubscriberCityIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the subscriber first name is required.
        /// </summary>
        /// <value>The subscriber first name is required.</value>
        public IPropertyRule SubscriberFirstNameIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the subscriber gender is required.
        /// </summary>
        /// <value>The subscriber gender is required.</value>
        public IPropertyRule SubscriberGenderIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the subscriber last name is required.
        /// </summary>
        /// <value>The subscriber last name is required.</value>
        public IPropertyRule SubscriberLastNameIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the subscriber postal code is required.
        /// </summary>
        /// <value>The subscriber postal code is required.</value>
        public IPropertyRule SubscriberPostalCodeIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the subscriber relationship is required.
        /// </summary>
        /// <value>The subscriber relationship is required.</value>
        public IPropertyRule SubscriberRelationshipIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the subscriber state is required.
        /// </summary>
        /// <value>The subscriber state is required.</value>
        public IPropertyRule SubscriberStateIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the set defaults for self.
        /// </summary>
        /// <value>The set defaults for self.</value>
        public IPropertyRule SetDefaultsForSelf { get; set; }

        #endregion
    }
}
