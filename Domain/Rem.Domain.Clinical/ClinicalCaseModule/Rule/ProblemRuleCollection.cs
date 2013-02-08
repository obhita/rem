using System;
using Pillar.FluentRuleEngine;

namespace Rem.Domain.Clinical.ClinicalCaseModule.Rule
{
    /// <summary>
    /// The ProblemRuleCollection defines rules/rule sets for <see cref="Problem">Problem</see> entity.
    /// </summary>
    public class ProblemRuleCollection : AbstractRuleCollection<Problem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemRuleCollection"/> class.
        /// </summary>
        public ProblemRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            ShouldRunWhen (
                ( s, ctx ) => ctx.WorkingMemory.GetContextObject<DateTime?> () != null,
                () =>
                NewRule ( () => ObservationDateCannotBeInTheFuture )
                    .OnContextObject<DateTime?> ()
                    .CannotBeFutureDate () );

            NewRuleSet ( () => ReviseObservationInfoRuleSet, ObservationDateCannotBeInTheFuture );
        }

        /// <summary>
        /// Gets the revise observation info rule set.
        /// </summary>
        public IRuleSet ReviseObservationInfoRuleSet { get; private set; }

        /// <summary>
        /// Gets the observation date cannot be in the future.
        /// </summary>
        public IRule ObservationDateCannotBeInTheFuture { get; private set; }
    }
}
