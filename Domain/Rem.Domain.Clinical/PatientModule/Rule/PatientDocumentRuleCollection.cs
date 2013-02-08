using Pillar.Common.Utility;
using Pillar.Domain.Primitives;
using Pillar.FluentRuleEngine;

namespace Rem.Domain.Clinical.PatientModule.Rule
{
    /// <summary>
    /// The PatientDocumentRuleCollection defines rules/rule sets for <see cref="PatientDocument">PatientDocument</see> entity.
    /// </summary>
    public class PatientDocumentRuleCollection : AbstractRuleCollection<PatientDocument>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientDocumentRuleCollection"/> class.
        /// </summary>
        public PatientDocumentRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            NewRule ( () => PatientDocumentTypeContextObjectNotNull )
                .OnContextObject<PatientDocumentType> ()
                .NotNull ();

            NewRule(() => DocumentProviderNameContextObjectNotNull).When(
                (s, ctx) =>
                {
                    var providerName = ctx.WorkingMemory.GetContextObject<string>();
                    return string.IsNullOrWhiteSpace(providerName);
                })
                .ThenReportRuleViolation(
                "Provider Name cannot be empty.");

            NewRule ( () => ClinicalDateRangeContextObjectStartDateNotNull )
                .OnContextObject<DateRange> ()
                .WithProperty ( c => c.StartDate )
                .NotNull ();

            NewRule ( () => OtherDocumentTypeNameContextObjectNotNull )
                .OnContextObject<string> ()
                .NotNull ();

            ShouldRunWhen (
                pd => pd.PatientDocumentType.WellKnownName == WellKnownNames.PatientModule.PatientDocumentType.Other,
                () => NewPropertyRule ( () => OtherDocumentTypeNameNotNull )
                          .WithProperty ( pd => pd.OtherDocumentTypeName )
                          .NotNull () );

            NewRuleSet ( () => RevisePatientDocumentTypeRuleSet, PatientDocumentTypeContextObjectNotNull );
            NewRuleSet ( () => ReviseDocumentProviderNameRuleSet, DocumentProviderNameContextObjectNotNull );
            NewRuleSet ( () => ReviseClinicalDateRangeRuleSet, ClinicalDateRangeContextObjectStartDateNotNull );
            NewRuleSet ( () => ReviseOtherDocumentTypeNameRuleSet, OtherDocumentTypeNameContextObjectNotNull );
            NewRuleSet ( () => SavePatientDocumentRuleSet, OtherDocumentTypeNameNotNull );
        }

        /// <summary>
        /// Gets the revise patient document type rule set.
        /// </summary>
        public IRuleSet RevisePatientDocumentTypeRuleSet { get; private set; }

        /// <summary>
        /// Gets the revise document provider name rule set.
        /// </summary>
        public IRuleSet ReviseDocumentProviderNameRuleSet { get; private set; }

        /// <summary>
        /// Gets the revise clinical date range rule set.
        /// </summary>
        public IRuleSet ReviseClinicalDateRangeRuleSet { get; private set; }

        /// <summary>
        /// Gets the revise other document type name rule set.
        /// </summary>
        public IRuleSet ReviseOtherDocumentTypeNameRuleSet { get; private set; }

        /// <summary>
        /// Gets the save patient document rule set.
        /// </summary>
        public IRuleSet SavePatientDocumentRuleSet { get; private set; }

        /// <summary>
        /// Gets the patient document type context object not null.
        /// </summary>
        public IRule PatientDocumentTypeContextObjectNotNull { get; private set; }

        /// <summary>
        /// Gets the document provider name context object not null.
        /// </summary>
        public IRule DocumentProviderNameContextObjectNotNull { get; private set; }

        /// <summary>
        /// Gets the clinical date range context object start date not null.
        /// </summary>
        public IRule ClinicalDateRangeContextObjectStartDateNotNull { get; private set; }

        /// <summary>
        /// Gets the other document type name context object not null.
        /// </summary>
        public IRule OtherDocumentTypeNameContextObjectNotNull { get; private set; }

        /// <summary>
        /// Gets the other document type name not null.
        /// </summary>
        public IPropertyRule OtherDocumentTypeNameNotNull { get; private set; }
    }
}
