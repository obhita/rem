using Pillar.FluentRuleEngine.NameProviders;
using Pillar.FluentRuleEngine.RuleSelectors;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Interface for the context of a <see cref="IRuleEngine{TSubject}">Rule Engine</see>.
    /// </summary>
    public interface IRuleEngineContext
    {
        #region Public Properties

        /// <summary>
        /// Gets the Parent Rule Engine Context
        /// This will be set if a running rule engine triggered the running of another Rule Collection.
        /// </summary>
        IRuleEngineContext ParentContext { get; }

        /// <summary>
        /// Gets the <see cref="IRuleSelector">Rule Selector</see>.
        /// </summary>
        IRuleSelector RuleSelector { get; }

        /// <summary>
        /// Gets the <see cref="IRuleViolationReporter">Rule Violation Reporter</see>.
        /// </summary>
        IRuleViolationReporter RuleViolationReporter { get; }

        /// <summary>
        /// Gets the <see cref="INameProvider">Name Provider</see>.
        /// </summary>
        INameProvider NameProvider { get; }

        /// <summary>
        /// Gets the <see cref="WorkingMemory">Working Memory</see>.
        /// </summary>
        WorkingMemory WorkingMemory { get; }

        /// <summary>
        /// Gets the subject.
        /// </summary>
        object Subject { get; }

        #endregion

        /// <summary>
        /// Sets the ParentContext.
        /// </summary>
        /// <param name="ruleEngineContext"><see cref="IRuleEngineContext">RuleEngineContext</see> of the parent <see cref="IRuleEngine{TSubject}">Rule Engine</see>.</param>
        void AddParentContext ( IRuleEngineContext ruleEngineContext );

        /// <summary>
        /// Refreshes the context.
        /// </summary>
        void Refresh ();
    }
}
