using System.Linq;
using Pillar.Domain;
using Pillar.FluentRuleEngine;

namespace Rem.Domain.Core.SecurityModule.Rule
{
    /// <summary>
    /// The SystemAccountRuleCollection defines rules/rule sets for <see cref="SystemAccount">SystemAccount</see> entity.
    /// </summary>
    public class SystemAccountRuleCollection : AbstractRuleCollection<SystemAccount>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemAccountRuleCollection"/> class.
        /// </summary>
        public SystemAccountRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            NewRule ( () => NoDuplicateSecurityQuestionsWithContext )
                .OnContextObject<SecurityQuestion> ()
                .NoDuplicates ( ctx => ctx.Subject.SecurityQuestions );

            NewRuleSet ( () => AddSecurityQuestionRuleSet, NoDuplicateSecurityQuestionsWithContext );

            BuildGrantSystemRoleRuleSet();
        }

        /// <summary>
        /// Gets the add security question rule set.
        /// </summary>
        public IRuleSet AddSecurityQuestionRuleSet { get; private set; }

        /// <summary>
        /// Gets the no duplicate security questions with context.
        /// </summary>
        public IRule NoDuplicateSecurityQuestionsWithContext { get; private set; }

        /// <summary>
        /// Gets the grant system role rule set.
        /// </summary>
        public IRuleSet GrantSystemRoleRuleSet { get; private set; }

        /// <summary>
        /// Gets the no duplicate system account role with context.
        /// </summary>
        public IRule NoDuplicateSystemAccountRoleWithContext { get; private set; }

        private void BuildGrantSystemRoleRuleSet ()
        {
            NewRule ( () => NoDuplicateSystemAccountRoleWithContext )
                .OnContextObject<SystemRole> ()
                .NoDuplicates ( ctx => Enumerable.Select<SystemAccountRole, SystemRole> ( ctx.Subject.SystemAccountRoles, sar => sar.SystemRole ) );

            NewRuleSet (
                () => GrantSystemRoleRuleSet,
                NoDuplicateSystemAccountRoleWithContext );
        }
    }
}
