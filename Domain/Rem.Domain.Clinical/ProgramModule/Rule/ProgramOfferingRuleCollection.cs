using Pillar.FluentRuleEngine;

namespace Rem.Domain.Clinical.ProgramModule.Rule
{
    /// <summary>
    /// The ProgramOfferingRuleCollection defines rules/rule sets for <see cref="ProgramOffering">ProgramOffering</see> entity.
    /// </summary>
    public class ProgramOfferingRuleCollection : AbstractRuleCollection<ProgramOffering>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramOfferingRuleCollection"/> class.
        /// </summary>
        /// <param name="programOfferingRepository">The program offering repository.</param>
        public ProgramOfferingRuleCollection ( IProgramOfferingRepository programOfferingRepository )
        {
            AutoValidatePropertyRules = true;

            NewRule ( () => CannotDeleteIfAttachedToEnrollment )
                .When ( s => programOfferingRepository.IsAttachedToProgramEnrollment ( s.Key ) )
                .ThenReportRuleViolation (
                    ( s, ctx ) => string.Format (
                        "You cannot delete a {0} when attached to a {1}.",
                        ctx.NameProvider.GetName ( s ),
                        ctx.NameProvider.GetName<ProgramEnrollment> () ) );

            NewRuleSet ( () => DestroyProgramOfferingRuleSet, CannotDeleteIfAttachedToEnrollment );
        }

        /// <summary>
        /// Gets the destroy program offering rule set.
        /// </summary>
        public IRuleSet DestroyProgramOfferingRuleSet { get; private set; }

        /// <summary>
        /// Gets the cannot delete if attached to enrollment.
        /// </summary>
        public IRule CannotDeleteIfAttachedToEnrollment { get; private set; }
    }
}
