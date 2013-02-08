using System.Linq;
using Pillar.FluentRuleEngine;

namespace Rem.Domain.Clinical.ProgramModule.Rule
{
    /// <summary>
    /// The ProgramRuleCollection defines rules/rule sets for <see cref="Program">Program</see> entity.
    /// </summary>
    public class ProgramRuleCollection : AbstractRuleCollection<Program>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramRuleCollection"/> class.
        /// </summary>
        public ProgramRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            BuildRenameProgramRuleSet ();
            BuildDestroyProgramRuleSet();
        }

        /// <summary>
        /// Gets the rename program rule set.
        /// </summary>
        public IRuleSet RenameProgramRuleSet { get; private set; }

        /// <summary>
        /// Gets the destroy program rule set.
        /// </summary>
        public IRuleSet DestroyProgramRuleSet { get; private set; }

        /// <summary>
        /// Gets the name not empty.
        /// </summary>
        public IRule NameNotEmpty { get; private set; }

        /// <summary>
        /// Gets the no duplicate program names with context.
        /// </summary>
        public IRule NoDuplicateProgramNamesWithContext { get; private set; }

        /// <summary>
        /// Gets the cannot delete if has program offerings.
        /// </summary>
        public IRule CannotDeleteIfHasProgramOfferings { get; private set; }

        private void BuildRenameProgramRuleSet ()
        {
            NewRule ( () => NameNotEmpty )
                .OnContextObject<string> ()
                .NotNullOrWhitespace ();

            //NewRule ( () => NoDuplicateProgramNamesWithContext )
            //    .OnContextObject<string> ()
            //    .NoDuplicates ( ctx => ctx.Subject.Agency.Programs.Select ( p => p.Name ) );

            NewRuleSet ( () => RenameProgramRuleSet, NameNotEmpty, NoDuplicateProgramNamesWithContext );
        }

        private void BuildDestroyProgramRuleSet()
        {
            NewRule ( () => CannotDeleteIfHasProgramOfferings )
                .When ( s => s.ProgramOfferings.Any() )
                .ThenReportRuleViolation (
                    ( s, ctx ) =>
                    string.Format (
                        "{0} cannot be deleted if has {1}.", ctx.NameProvider.GetName ( s ), ctx.NameProvider.GetName ( s, p => p.ProgramOfferings ) ) );

            NewRuleSet ( () => DestroyProgramRuleSet, CannotDeleteIfHasProgramOfferings );
        }
    }
}
