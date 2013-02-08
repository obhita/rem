using System;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;

namespace Rem.Domain.Clinical.ClinicalCaseModule.Rule
{
    /// <summary>
    /// The ClinicalCaseRuleCollection defines rules/rule sets for <see cref="ClinicalCase">ClinicalCase</see> entity.
    /// </summary>
    public class ClinicalCaseRuleCollection : AbstractRuleCollection<ClinicalCase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalCaseRuleCollection"/> class.
        /// </summary>
        public ClinicalCaseRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            BuildAdmitRuleSet ();
            BuildReviseClinicalCaseProfileRuleSet ();
            BuildCloseRuleSet ();
            BuildDischargeRuleSet ();
        }

        /// <summary>
        /// Gets the admit rule set.
        /// </summary>
        public IRuleSet AdmitRuleSet { get; private set; }

        /// <summary>
        /// Gets the revise clinical case profile rule set.
        /// </summary>
        public IRuleSet ReviseClinicalCaseProfileRuleSet { get; private set; }

        /// <summary>
        /// Gets the close rule set.
        /// </summary>
        public IRuleSet CloseRuleSet { get; private set; }

        /// <summary>
        /// Gets the discharge rule set.
        /// </summary>
        public IRuleSet DischargeRuleSet { get; private set; }

        /// <summary>
        /// Gets the admission date not empty.
        /// </summary>
        public IRule AdmissionDateNotEmpty { get; private set; }

        /// <summary>
        /// Gets the closed date after start date.
        /// </summary>
        public IRule ClosedDateAfterStartDate { get; private set; }

        /// <summary>
        /// Gets the discharge date not empty.
        /// </summary>
        public IRule DischargeDateNotEmpty { get; private set; }

        private void BuildAdmitRuleSet ()
        {
            NewRule ( () => AdmissionDateNotEmpty )
                .OnContextObject<ClinicalCaseAdmission> ()
                .WithProperty ( cca => cca.AdmissionDate )
                .NotNull ();

            NewRuleSet ( () => AdmitRuleSet, AdmissionDateNotEmpty );
        }

        private void BuildReviseClinicalCaseProfileRuleSet ()
        {
            ShouldRunWhen (
                ( s, ctx ) =>
                    {
                        var profile = ctx.WorkingMemory.GetContextObject<ClinicalCaseProfile> ();
                        var closeInfo = ctx.WorkingMemory.GetContextObject<ClinicalCaseCloseInfo> ();
                        return profile != null && profile.ClinicalCaseStartDate.HasValue &&
                               closeInfo != null && closeInfo.ClinicalCaseCloseDate.HasValue;
                    },
                () =>
                NewRule ( () => ClosedDateAfterStartDate )
                    .When (
                        ( s, ctx ) =>
                            {
                                var profile = ctx.WorkingMemory.GetContextObject<ClinicalCaseProfile> ();
                                var closeInfo = ctx.WorkingMemory.GetContextObject<ClinicalCaseCloseInfo> ();
                                return closeInfo.ClinicalCaseCloseDate < profile.ClinicalCaseStartDate;
                            } )
                    .ThenReportRuleViolation (
                        ( s, ctx ) =>
                        string.Format (
                            "{0} must be greater then {1}.",
                            ctx.NameProvider.GetName ( s, cc => cc.ClinicalCaseCloseInfo.ClinicalCaseCloseDate ),
                            ctx.NameProvider.GetName ( s, cc => cc.ClinicalCaseProfile.ClinicalCaseStartDate ) ),
                        null,
                        PropertyUtil.ExtractPropertyName<ClinicalCase, DateTime?> ( cc => cc.ClinicalCaseProfile.ClinicalCaseStartDate ),
                        PropertyUtil.ExtractPropertyName<ClinicalCase, DateTime?> ( cc => cc.ClinicalCaseCloseInfo.ClinicalCaseCloseDate ) ) );

            NewRuleSet ( () => ReviseClinicalCaseProfileRuleSet, ClosedDateAfterStartDate );
        }

        private void BuildCloseRuleSet ()
        {
            NewRuleSet ( () => CloseRuleSet, ClosedDateAfterStartDate );
        }

        private void BuildDischargeRuleSet ()
        {
            NewRule ( () => DischargeDateNotEmpty )
                .OnContextObject<ClinicalCaseDischarge> ()
                .WithProperty ( ccd => ccd.DischargeDate )
                .NotNull ();

            NewRuleSet ( () => DischargeRuleSet, DischargeDateNotEmpty );
        }
    }
}
