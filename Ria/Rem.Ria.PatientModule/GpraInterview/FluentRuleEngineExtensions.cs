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
using Pillar.FluentRuleEngine.Constraints;
using Pillar.FluentRuleEngine.Resources;
using Pillar.FluentRuleEngine.Rules;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.GpraInterview;

namespace Rem.Ria.PatientModule.GpraInterview
{
    /// <summary>
    /// FluentRuleEngineExtensions class.
    /// </summary>
    public static class FluentRuleEngineExtensions
    {
        #region Public Methods

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of EqualTo to the Rule comparing the WellKnownName of the <see cref="GpraNonResponseTypeDto{LookupValueDto}"/>
        /// </summary>
        /// <typeparam name="TSubject">The type of the subject.</typeparam>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyRuleBuilder">The property rule builder.</param>
        /// <param name="wellKnownName">Name of the well known.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> EqualToWellKnownName<TSubject, TContext, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder,
            string wellKnownName )
            where TContext : RuleEngineContext<TSubject>
            where TProperty : GpraNonResponseTypeDto<LookupValueDto>
        {
            Check.IsNotNull ( wellKnownName, "wellKnownName is required." );
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( wellKnownName, "=" );
            propertyRuleBuilder.Constrain (
                new InlineConstraint<TProperty> (
                    lhs =>
                        {
                            if ( lhs != null && lhs.HasValue () )
                            {
                                return string.Compare ( wellKnownName, lhs.Value.WellKnownName ) == 0;
                            }
                            return false;
                        },
                    message ) );
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of NotEqualTo to the Rule comparing the WellKnownName of the <see cref="GpraNonResponseTypeDto{LookupValueDto}"/>
        /// </summary>
        /// <typeparam name="TSubject">The type of the subject.</typeparam>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyRuleBuilder">The property rule builder.</param>
        /// <param name="wellKnownName">Name of the well known.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> NotEqualToWellKnownName<TSubject, TContext, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder,
            string wellKnownName )
            where TContext : RuleEngineContext<TSubject>
            where TProperty : GpraNonResponseTypeDto<LookupValueDto>
        {
            Check.IsNotNull ( wellKnownName, "wellKnownName is required." );
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( wellKnownName, "!=" );
            propertyRuleBuilder.Constrain (
                new InlineConstraint<TProperty> (
                    lhs =>
                        {
                            if ( lhs != null && lhs.HasValue () )
                            {
                                return string.Compare ( wellKnownName, lhs.Value.WellKnownName ) != 0;
                            }
                            return false;
                        },
                    message ) );
            return propertyRuleBuilder;
        }

        #endregion
    }
}
