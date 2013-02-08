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

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Pillar.FluentRuleEngine.RuleSelectors;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.BillingModule.Web.PayorEditor;

namespace Rem.Ria.BillingModule.PayorEditor
{
    /// <summary>
    /// Contains client side business rules for Payor Type.
    /// </summary>
    public class PayorTypeEditorViewModelRuleCollection : AbstractRuleCollection<PayorTypeEditorViewModel>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorTypeEditorViewModelRuleCollection"/> class.
        /// </summary>
        public PayorTypeEditorViewModelRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            var nameError = new DataErrorInfo (
                "Name is required.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PayorTypeDto, object> ( dto => dto.Name ) );

            NewPropertyRule ( () => NameRequired )
                .WithProperty ( s => s.EditingDto.Name )
                .NotEmpty ()
                .ElseThen (
                    ( s, ctx ) =>
                        {
                            if ( !( ctx.RuleSelector is SelectAllRuleSelector ) )
                            {
                                s.EditingDto.TryAddDataErrorInfo ( nameError );
                            }
                        } )
                .Then ( s => s.EditingDto.RemoveDataErrorInfo ( nameError ) );

            var regex = new Regex ( @"^ftp\://[a-zA-Z0-9\-\.]+\.(com|org|net|mil|edu|COM|ORG|NET|MIL|EDU)$" );
            var ftpAddressInvalidError = new DataErrorInfo (
                "FTP address is not valid.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PayorTypeDto, object> ( dto => dto.FtpAddress ) );

            NewRule ( () => FtpAddressInvalid )
                .RunForProperty ( s => s.EditingDto.FtpAddress )
                .When (
                    s =>
                        {
                            if ( s.EditingDto.FtpAddress != null && !regex.IsMatch ( s.EditingDto.FtpAddress ) )
                            {
                                return true;
                            }
                            return false;
                        } )
                .ThenReportRuleViolation ( ftpAddressInvalidError.Message )
                .Then ( s => s.EditingDto.TryAddDataErrorInfo ( ftpAddressInvalidError ) )
                .ElseThen ( s => s.EditingDto.RemoveDataErrorInfo ( ftpAddressInvalidError ) );

            BuildHealthCareClaim837ProfessionalRules ();
            BuildAddressRules ();
        }

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Gets or sets the FTP address invalid.
        /// </summary>
        /// <value>The FTP address invalid.</value>
        public IRule FtpAddressInvalid { get; set; }

        /// <summary>
        /// Gets or sets the HCC setup required.
        /// </summary>
        /// <value>The HCC setup required.</value>
        public IRule HccSetupRequired { get; set; }

        /// <summary>
        /// Gets or sets the name required.
        /// </summary>
        /// <value>The name required.</value>
        public IPropertyRule NameRequired { get; set; }

        /// <summary>
        /// Gets or sets the submitter required.
        /// </summary>
        /// <value>The submitter required.</value>
        public IPropertyRule SubmitterRequired { get; set; }

        /// <summary>
        /// Gets or sets the first address required.
        /// </summary>
        /// <value>
        /// The first address required.
        /// </value>
        public IPropertyRule FirstAddressRequired { get; set; }

        /// <summary>
        /// Gets or sets the city required.
        /// </summary>
        /// <value>
        /// The city required.
        /// </value>
        public IPropertyRule CityRequired { get; set; }

        /// <summary>
        /// Gets or sets the state required.
        /// </summary>
        /// <value>
        /// The state required.
        /// </value>
        public IPropertyRule StateRequired { get; set; }

        #endregion

        #region Methods

        private static bool NoneOrAllRequired ( IList<string> input )
        {
            return input.All ( string.IsNullOrWhiteSpace ) || input.All ( s => !string.IsNullOrWhiteSpace ( s ) );
        }

        private void BuildAddressRules ()
        {
            var firstAddressError = new DataErrorInfo(
               "Address line 1 is required.",
               ErrorLevel.Error,
               PropertyUtil.ExtractPropertyName<PayorTypeDto, object>(dto => dto.FirstStreetAddress));

            NewPropertyRule(() => FirstAddressRequired)
                .WithProperty(s => s.EditingDto.FirstStreetAddress)
                .NotEmpty()
                .ElseThen(
                    (s, ctx) =>
                    {
                        if (!(ctx.RuleSelector is SelectAllRuleSelector))
                        {
                            s.EditingDto.TryAddDataErrorInfo(firstAddressError);
                        }
                    })
                .Then(s => s.EditingDto.RemoveDataErrorInfo(firstAddressError));

            var cityError = new DataErrorInfo(
               "City is required.",
               ErrorLevel.Error,
               PropertyUtil.ExtractPropertyName<PayorTypeDto, object>(dto => dto.CityName));

            NewPropertyRule(() => CityRequired)
                .WithProperty(s => s.EditingDto.CityName)
                .NotEmpty()
                .ElseThen(
                    (s, ctx) =>
                    {
                        if (!(ctx.RuleSelector is SelectAllRuleSelector))
                        {
                            s.EditingDto.TryAddDataErrorInfo(cityError);
                        }
                    })
                .Then(s => s.EditingDto.RemoveDataErrorInfo(cityError));

            var stateError = new DataErrorInfo(
               "State is required.",
               ErrorLevel.Error,
               PropertyUtil.ExtractPropertyName<PayorTypeDto, object>(dto => dto.StateProvince));

            NewPropertyRule(() => StateRequired)
                .WithProperty(s => s.EditingDto.FirstStreetAddress)
                .NotNull ()
                .ElseThen(
                    (s, ctx) =>
                    {
                        if (!(ctx.RuleSelector is SelectAllRuleSelector))
                        {
                            s.EditingDto.TryAddDataErrorInfo(stateError);
                        }
                    })
                .Then(s => s.EditingDto.RemoveDataErrorInfo(stateError));

        }

        private void BuildHealthCareClaim837ProfessionalRules ()
        {
            //if entered anything for X12 set up, then all of them required
            var hccSetupError = new DataErrorInfo (
                "All six pieces of Health Care Claim set up information are required if entering any one of them.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PayorTypeDto, object> ( dto => dto.InterchangeReceiverNumber ),
                PropertyUtil.ExtractPropertyName<PayorTypeDto, object> ( dto => dto.InterchangeSenderNumber ),
                PropertyUtil.ExtractPropertyName<PayorTypeDto, object> ( dto => dto.CompositeDelimiter ),
                PropertyUtil.ExtractPropertyName<PayorTypeDto, object> ( dto => dto.ElementDelimiter ),
                PropertyUtil.ExtractPropertyName<PayorTypeDto, object> ( dto => dto.SegmentDelimiter ),
                PropertyUtil.ExtractPropertyName<PayorTypeDto, object> ( dto => dto.RepetitionDelimiter ) );

            NewRule ( () => HccSetupRequired )
                .RunForProperty ( s => s.EditingDto.InterchangeReceiverNumber )
                .RunForProperty ( s => s.EditingDto.InterchangeSenderNumber )
                .RunForProperty ( s => s.EditingDto.CompositeDelimiter )
                .RunForProperty ( s => s.EditingDto.ElementDelimiter )
                .RunForProperty ( s => s.EditingDto.SegmentDelimiter )
                .RunForProperty ( s => s.EditingDto.RepetitionDelimiter )
                .When (
                    s =>
                        {
                            var input = new List<string>
                                {
                                    s.EditingDto.InterchangeReceiverNumber,
                                    s.EditingDto.InterchangeSenderNumber,
                                    s.EditingDto.CompositeDelimiter,
                                    s.EditingDto.ElementDelimiter,
                                    s.EditingDto.SegmentDelimiter,
                                    s.EditingDto.RepetitionDelimiter
                                };
                            return !NoneOrAllRequired ( input );
                        } )
                .ThenReportRuleViolation ( hccSetupError.Message )
                .Then ( s => s.EditingDto.TryAddDataErrorInfo ( hccSetupError ) )
                .ElseThen ( s => s.EditingDto.RemoveDataErrorInfo ( hccSetupError ) );
        }

        #endregion
    }
}
