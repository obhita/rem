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

using System.Text.RegularExpressions;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Pillar.FluentRuleEngine.RuleSelectors;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.BillingModule.Web.PayorEditor;

namespace Rem.Ria.BillingModule.PayorEditor
{
    /// <summary>
    /// Contains client side business rules for a payor.
    /// </summary>
    public class PayorEditorViewModelRuleCollection : AbstractRuleCollection<PayorEditorViewModel>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorEditorViewModelRuleCollection"/> class.
        /// </summary>
        public PayorEditorViewModelRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            var nameError = new DataErrorInfo (
                "Name is required.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PayorProfileDto, object> ( dto => dto.Name ) );
            NewPropertyRule ( () => NameRequired )
                .WithProperty ( s => s.EditingDto.Profile.Name )
                .NotEmpty ()
                .ElseThen (
                    ( s, ctx ) =>
                        {
                            if ( !( ctx.RuleSelector is SelectAllRuleSelector ) )
                            {
                                s.EditingDto.Profile.TryAddDataErrorInfo ( nameError );
                            }
                        } )
                .Then ( s => s.EditingDto.Profile.RemoveDataErrorInfo ( nameError ) );

            var primaryPayorTypeError = new DataErrorInfo (
                "Primary payor type is required.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PayorProfileDto, object> ( dto => dto.PrimaryPayorType ) );
            NewPropertyRule ( () => PrimaryPayorTypeRequired )
                .WithProperty ( s => s.EditingDto.Profile.PrimaryPayorType )
                .NotNull ()
                .ElseThen (
                    ( s, ctx ) =>
                        {
                            if ( !( ctx.RuleSelector is SelectAllRuleSelector ) )
                            {
                                s.EditingDto.Profile.TryAddDataErrorInfo ( primaryPayorTypeError );
                            }
                        } )
                .Then ( s => s.EditingDto.Profile.RemoveDataErrorInfo ( primaryPayorTypeError ) );

            var regex = new Regex ( @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" );
            var emailAddressInvalidError = new DataErrorInfo (
                "Email address is not valid.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<PayorProfileDto, object> ( dto => dto.EmailAddress ) );
            NewRule ( () => EmailAddressInvalid )
                .RunForProperty ( s => s.EditingDto.Profile.EmailAddress )
                .When (
                    s => ( s.EditingDto.Profile.EmailAddress != null
                           && !regex.IsMatch ( s.EditingDto.Profile.EmailAddress ) ) )
                .ThenReportRuleViolation ( emailAddressInvalidError.Message )
                .Then ( s => s.EditingDto.Profile.TryAddDataErrorInfo ( emailAddressInvalidError ) )
                .ElseThen ( s => s.EditingDto.Profile.RemoveDataErrorInfo ( emailAddressInvalidError ) );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the email address invalid.
        /// </summary>
        /// <value>The email address invalid.</value>
        public IRule EmailAddressInvalid { get; set; }

        /// <summary>
        /// Gets or sets the name required.
        /// </summary>
        /// <value>The name required.</value>
        public IPropertyRule NameRequired { get; set; }

        /// <summary>
        /// Gets or sets the primary payor type required.
        /// </summary>
        /// <value>The primary payor type required.</value>
        public IPropertyRule PrimaryPayorTypeRequired { get; set; }

        #endregion
    }
}
