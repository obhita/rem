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

using System;
using Pillar.FluentRuleEngine;

namespace Rem.Domain.Clinical.TedsModule.Rule
{
    /// <summary>
    /// Contains the business rules that TEDS discharge interview should comply.
    /// </summary>
    public class TedsDischargeInterviewRuleCollection : AbstractRuleCollection<TedsDischargeInterview>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TedsDischargeInterviewRuleCollection"/> class.
        /// </summary>
        public TedsDischargeInterviewRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            BuildReviseTedsDischargeKeyFieldsRuleSet();

            BuildReviseLastFaceToFaceContactDateRuleSet ();

            //BuildRevisePrimarySubstanceProblemAndFrequency ();

            //BuildReviseSecondarySubstanceProblemAndFrequency ();

            //BuildReviseTertiarySubstanceProblemAndFrequency();
        }
      

        /// <summary>
        /// Gets the revise tertiary substance problem and frequency.
        /// </summary>
        protected IRuleSet ReviseTertiarySubstanceProblemAndFrequency { get; private set; }

        /// <summary>
        /// Gets the revise secondary substance problem and frequency.
        /// </summary>
        protected IRuleSet ReviseSecondarySubstanceProblemAndFrequency { get; private set; }

        /// <summary>
        /// Gets the revise last contact date rule set.
        /// </summary>
        public IRuleSet ReviseLastFaceToFaceContactDateRuleSet { get; private set; }   

        /// <summary>
        /// Gets the revise system data set rule set.
        /// </summary>
        public IRuleSet ReviseTedsDischargeKeyFieldsRuleSet { get; private set; }

        /// <summary>
        /// Gets the system transaction type cannot be null.
        /// </summary>
        public IRule SystemTransactionTypeCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the state province cannot be null.
        /// </summary>
        public IRule StateProvinceCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the reporting date cannot be null.
        /// </summary>
        public IRule ReportingDateCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the teds admission interview cannot be null.
        /// </summary>
        public IRule TedsAdmissionInterviewCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the provider identifier cannot be null.
        /// </summary>
        public IPropertyRule ProviderIdentifierCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the client identifier cannot be null.
        /// </summary>
        public IPropertyRule ClientIdentifierCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the co dependent indicator cannot be null.
        /// </summary>
        public IPropertyRule CoDependentIndicatorCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the teds service type cannot be null.
        /// </summary>
        public IPropertyRule TedsServiceTypeCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the last contact date cannot be null.
        /// </summary>
        public IPropertyRule LastContactDateCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the discharge date cannot be null.
        /// </summary>
        public IPropertyRule DischargeDateCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the teds discharge reason cannot be null.
        /// </summary>
        public IPropertyRule TedsDischargeReasonCannotBeNull { get; private set; }

        /// <summary>
        /// Gets the provider identifier not match admission.
        /// </summary>
        public IRule ProviderIdentifierNotMatchAdmission { get; private set; }

        /// <summary>
        /// Gets the client identifier not match admission.
        /// </summary>
        public IRule ClientIdentifierNotMatchAdmission { get; private set; }

        /// <summary>
        /// Gets the service type not match admission.
        /// </summary>
        public IRule ServiceTypeNotMatchAdmission { get; private set; }

        /// <summary>
        /// Gets the last contact date should after admission date.
        /// </summary>
        public IRule LastContactDateShouldAfterAdmissionDate { get; private set; }

        /// <summary>
        /// Gets the last contact date cannot after discharge date.
        /// </summary>
        public IRule LastContactDateCannotAfterDischargeDate { get; private set; }

        /// <summary>
        /// Gets the last contact date cannot be null rule.
        /// </summary>
        public IRule LastContactDateCannotBeNullRule { get; private set; }

        /// <summary>
        /// Gets the primary substance problem type and frequency same as secondary.
        /// </summary>
        public IRule PrimarySubstanceProblemTypeAndFrequencySameAsSecondary { get; private set; }

        /// <summary>
        /// Gets the secondary substance problem type and frequency same as tertiary.
        /// </summary>
        public IRule SecondarySubstanceProblemTypeAndFrequencySameAsTertiary { get; private set; }

        /// <summary>
        /// Gets the revise primary substance problem and frequency.
        /// </summary>
        protected IRuleSet RevisePrimarySubstanceProblemAndFrequency { get; private set; }

        private void BuildReviseTedsDischargeKeyFieldsRuleSet()
        {
            //NewRule(() => SystemTransactionTypeCannotBeNull)
            //    .OnContextObject<SystemDataSet>()
            //    .WithProperty(sds => sds.SystemTransactionType)
            //    .NotNull();

            //NewRule(() => StateProvinceCannotBeNull)
            //    .OnContextObject<SystemDataSet>()
            //    .WithProperty(sds => sds.StateProvince)
            //    .NotNull();

            //NewRule(() => ReportingDateCannotBeNull)
            //    .OnContextObject<SystemDataSet>()
            //    .WithProperty(sds => sds.ReportingDate)
            //    .NotNull();

            //NewRule(() => ServiceTypeNotMatchAdmission).When(
            //   (d, ctx) =>
            //   {
            //       var tesServiceType = ctx.WorkingMemory.GetContextObject<TedsAnswer<TedsServiceType>>();
            //       return tesServiceType == d.TedsDischargeKeyFields.TedsServiceType;
            //   })
            //   .ThenReportRuleViolation("Service types is different from the service type in Admission interview.");


            // NewRule(() => ClientIdentifierNotMatchAdmission).When(
            //    (d, ctx) =>
            //    {
            //        var clientIdentifier = ctx.WorkingMemory.GetContextObject<TedsIdentifier>();
            //        return clientIdentifier == d.TedsAdmissionInterview.TedsAdmissionKeyFields.ClientIdentifier;
            //    })
            //    .ThenReportRuleViolation("Client identifier is different from the client identifier in Admission interview.");

            //  NewRule(() => ProviderIdentifierNotMatchAdmission).When(
            //   (d, ctx) =>
            //   {
            //       var providerIdentifier = ctx.WorkingMemory.GetContextObject<TedsIdentifier>();
            //       return providerIdentifier != d.TedsAdmissionInterview.TedsAdmissionKeyFields.ProviderIdentifier;
            //   })
            //   .ThenReportRuleViolation("Provider identifier is different from the provider identifier in Admission interview.");

            //NewRuleSet (
            //    () => ReviseTedsDischargeKeyFieldsRuleSet,
            //    StateProvinceCannotBeNull,
            //    ReportingDateCannotBeNull,
            //    ServiceTypeNotMatchAdmission,
            //    ClientIdentifierNotMatchAdmission,
            //    ProviderIdentifierNotMatchAdmission );
        }

        //private void BuildRevisePrimarySubstanceProblemAndFrequency()
        //{
        //    NewRule ( () => PrimarySubstanceProblemTypeAndFrequencySameAsSecondary ).When (
        //        ( d, ctx ) =>
        //            {
        //                var primarySubstanceProblemAndFrequency = ctx.WorkingMemory.GetContextObject<SubstanceProblemAndFrequency> ();
        //                return primarySubstanceProblemAndFrequency == d.SecondarySubstanceProblemAndFrequency;
        //            } )
        //        .ThenReportRuleViolation ( "Primary substance problem and frequency are the same as second substance." );

        //    NewRuleSet(() => RevisePrimarySubstanceProblemAndFrequency, PrimarySubstanceProblemTypeAndFrequencySameAsSecondary);
        //}
      
        //private void BuildReviseSecondarySubstanceProblemAndFrequency ()
        //{
        //    NewRule ( () => PrimarySubstanceProblemTypeAndFrequencySameAsSecondary ).When (
        //        ( d, ctx ) =>
        //            {
        //                var secondarySubstanceProblemAndFrequency = ctx.WorkingMemory.GetContextObject<SubstanceProblemAndFrequency> ();
        //                return secondarySubstanceProblemAndFrequency == d.PrimarySubstanceProblemAndFrequency;
        //            } )
        //        .ThenReportRuleViolation ( "Primary substance problem and frequency are the same as second substance." );

        //    NewRuleSet(() => ReviseSecondarySubstanceProblemAndFrequency, PrimarySubstanceProblemTypeAndFrequencySameAsSecondary);
        //}

        //private void BuildReviseTertiarySubstanceProblemAndFrequency()
        //{
        //    NewRule ( () => SecondarySubstanceProblemTypeAndFrequencySameAsTertiary ).When (
        //        ( d, ctx ) =>
        //            {
        //                var tertiarubstanceProblemAndFrequency = ctx.WorkingMemory.GetContextObject<SubstanceProblemAndFrequency> ();
        //                return d.SecondarySubstanceProblemAndFrequency == tertiarubstanceProblemAndFrequency;
        //            } )
        //        .ThenReportRuleViolation ( "Second substance problem and frequency are the same as tertiary substance." );

        //    NewRuleSet(() => ReviseTertiarySubstanceProblemAndFrequency, SecondarySubstanceProblemTypeAndFrequencySameAsTertiary);
        //}

        private void BuildReviseLastFaceToFaceContactDateRuleSet()
        {
            //    NewRule(() => LastContactDateShouldAfterAdmissionDate).When(
            //        (d, ctx) =>
            //        {
            //            var lastContactDate = ctx.WorkingMemory.GetContextObject<DateTime>();
            //            return d.TedsAdmissionInterview.TedsAdmissionKeyFields.AdmissionDate > lastContactDate;
            //        }).ThenReportRuleViolation("Last Contact Date should be after the Admission date.");

            //    NewRule(() => LastContactDateCannotAfterDischargeDate).When(
            //        (d, ctx) =>
            //        {
            //            var lastContactDate = ctx.WorkingMemory.GetContextObject<DateTime>();
            //            return d.TedsDischargeKeyFields.DischargeDate < lastContactDate;
            //        }).ThenReportRuleViolation("Last Contact Date cannot be after the discharge date.");

            //    NewRuleSet(
            //        () => ReviseLastContactDateRuleSet,
            //        LastContactDateShouldAfterAdmissionDate,
            //        LastContactDateCannotAfterDischargeDate);

            NewRule ( () => LastContactDateCannotBeNullRule )
                .When (
                    ( s, ctx ) =>
                        {
                            var lastFaceToFaceContactDate = ctx.WorkingMemory.GetContextObject<DateTime?> ();
                            return lastFaceToFaceContactDate == null || !lastFaceToFaceContactDate.HasValue;
                        }
                ).ThenReportRuleViolation ( "Last Contact Date cannot be null." );

            NewRuleSet (
                () => ReviseLastFaceToFaceContactDateRuleSet, LastContactDateCannotBeNullRule );
        }
    }
}
