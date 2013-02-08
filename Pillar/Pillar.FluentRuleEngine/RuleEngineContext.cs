using System.Diagnostics.CodeAnalysis;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine.NameProviders;
using Pillar.FluentRuleEngine.RuleSelectors;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Generic Context used when executing a Collection of Rules.
    /// </summary>
    /// <typeparam name="TSubject">The type of the subject.</typeparam>
    public class RuleEngineContext<TSubject> : RuleEngineContext
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngineContext&lt;TSubject&gt;"/> class.
        /// </summary>
        /// <param name="subject">Subject for the context.</param>
        /// <param name="parentContext">Optional Parent RuleEngineContext.</param>
        public RuleEngineContext ( TSubject subject, IRuleEngineContext parentContext = null )
            : this ( subject, new RuleViolationCollection (), parentContext )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngineContext&lt;TSubject&gt;"/> class.
        /// </summary>
        /// <param name="subject">Subject for the context.</param>
        /// <param name="ruleSelector"><see cref="IRuleSelector">RuleSelector</see> of the context.</param>
        /// <param name="parentContext">Optional Parent RuleEngineContext.</param>
        public RuleEngineContext ( TSubject subject, IRuleSelector ruleSelector, IRuleEngineContext parentContext = null )
            : this ( subject, new RuleViolationCollection (), ruleSelector, null, parentContext )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngineContext&lt;TSubject&gt;"/> class.
        /// </summary>
        /// <param name="subject">Subject for the context.</param>
        /// <param name="ruleSelector"><see cref="IRuleSelector">RuleSelector</see> of the context.</param>
        /// <param name="nameProvider"><see cref="INameProvider">NameProvider</see> of the context.</param>
        /// <param name="parentContext">Optional Parent RuleEngineContext.</param>
        public RuleEngineContext ( TSubject subject, IRuleSelector ruleSelector, INameProvider nameProvider, IRuleEngineContext parentContext = null )
            : this ( subject, new RuleViolationCollection (), ruleSelector, nameProvider, parentContext )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngineContext&lt;TSubject&gt;"/> class.
        /// </summary>
        /// <param name="subject">Subject for the context.</param>
        /// <param name="ruleViolationReporter"><see cref="IRuleViolationReporter">RuleViolationReporter</see> of the context.</param>
        /// <param name="parentContext">Optional Parent RuleEngineContext.</param>
        public RuleEngineContext ( TSubject subject, IRuleViolationReporter ruleViolationReporter, IRuleEngineContext parentContext = null )
            : this ( subject, ruleViolationReporter, null, null, parentContext )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngineContext&lt;TSubject&gt;"/> class.
        /// </summary>
        /// <param name="subject">Subject for the context.</param>
        /// <param name="ruleViolationReporter"><see cref="IRuleViolationReporter">RuleViolationReporter</see> of the context.</param>
        /// <param name="nameProvider"><see cref="INameProvider">NameProvider</see> of the context.</param>
        /// <param name="parentContext">Optional Parent RuleEngineContext.</param>
        public RuleEngineContext (
            TSubject subject, IRuleViolationReporter ruleViolationReporter, INameProvider nameProvider, IRuleEngineContext parentContext = null )
            : this ( subject, ruleViolationReporter, null, nameProvider, parentContext )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngineContext&lt;TSubject&gt;"/> class.
        /// </summary>
        /// <param name="subject">Subject for the context.</param>
        /// <param name="nameProvider"><see cref="INameProvider">NameProvider</see> of the context.</param>
        /// <param name="parentContext">Optional Parent RuleEngineContext.</param>
        public RuleEngineContext ( TSubject subject, INameProvider nameProvider, IRuleEngineContext parentContext = null )
            : this ( subject, new RuleViolationCollection (), null, nameProvider, parentContext )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngineContext&lt;TSubject&gt;"/> class.
        /// </summary>
        /// <param name="subject">Subject for the context.</param>
        /// <param name="ruleViolationReporter"><see cref="IRuleViolationReporter">RuleViolationReporter</see> of the context.</param>
        /// <param name="ruleSelector"><see cref="IRuleSelector">RuleSelector</see> of the context.</param>
        /// <param name="parentContext">Optional Parent RuleEngineContext.</param>
        public RuleEngineContext (
            TSubject subject, IRuleViolationReporter ruleViolationReporter, IRuleSelector ruleSelector, IRuleEngineContext parentContext = null )
            : this ( subject, ruleViolationReporter, ruleSelector, null, parentContext )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngineContext&lt;TSubject&gt;"/> class.
        /// </summary>
        /// <param name="subject">Subject for the context.</param>
        /// <param name="ruleViolationReporter"><see cref="IRuleViolationReporter">RuleViolationReporter</see> of the context.</param>
        /// <param name="ruleSelector"><see cref="IRuleSelector">RuleSelector</see> of the context.</param>
        /// <param name="nameProvider"><see cref="INameProvider">NameProvider</see> of the context.</param>
        /// <param name="parentContext">Optional Parent RuleEngineContext.</param>
        public RuleEngineContext (
            TSubject subject,
            IRuleViolationReporter ruleViolationReporter,
            IRuleSelector ruleSelector,
            INameProvider nameProvider,
            IRuleEngineContext parentContext = null )
            : base ( subject, ruleViolationReporter, ruleSelector, nameProvider, parentContext )
        {
            Subject = subject;
        }

        #endregion

        #region Public Properties

        /// <inheritdoc/>
        public new TSubject Subject { get; private set; }

        #endregion
    }

    /// <summary>
    /// Generic Context used when executing a Collection of Rules.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here.")]
    public class RuleEngineContext : IRuleEngineContext
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngineContext"/> class.
        /// </summary>
        /// <param name="subject">Subject for the context.</param>
        public RuleEngineContext ( object subject )
            : this ( subject, new RuleViolationCollection (), null, null )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngineContext"/> class.
        /// </summary>
        /// <param name="subject">Subject for the context.</param>
        /// <param name="ruleViolationReporter"><see cref="IRuleViolationReporter">RuleViolationReporter</see> of the context.</param>
        /// <param name="ruleSelector"><see cref="IRuleSelector">RuleSelector</see> of the context.</param>
        /// <param name="nameProvider"><see cref="INameProvider">NameProvider</see> of the context.</param>
        /// <param name="parentContext">Optional Parent RuleEngineContext.</param>
        public RuleEngineContext (
            object subject,
            IRuleViolationReporter ruleViolationReporter,
            IRuleSelector ruleSelector,
            INameProvider nameProvider,
            IRuleEngineContext parentContext = null )
        {
            Check.IsNotNull ( subject, () => Subject );
            Check.IsNotNull ( ruleViolationReporter, () => RuleViolationReporter );

            if ( ruleSelector == null )
            {
                ruleSelector = new SelectAllRuleSelector ();
            }
            if ( nameProvider == null )
            {
                nameProvider = new TypePropertyNameNameProvider ();
            }

            Subject = subject;
            RuleSelector = ruleSelector;
            RuleViolationReporter = ruleViolationReporter;
            NameProvider = nameProvider;
            WorkingMemory = new WorkingMemory();
            WorkingMemory.AddContextObject(RuleViolationReporter);
            WorkingMemory.AddContextObject(NameProvider);
            ParentContext = parentContext;
        }

        #endregion

        #region Public Properties

        /// <inheritdoc/>
        public INameProvider NameProvider { get; private set; }

        /// <inheritdoc/>
        public IRuleEngineContext ParentContext { get; private set; }

        /// <inheritdoc/>
        public IRuleSelector RuleSelector { get; private set; }

        /// <inheritdoc/>
        public IRuleViolationReporter RuleViolationReporter { get; private set; }

        /// <inheritdoc/>
        public object Subject { get; private set; }

        /// <inheritdoc/>
        public WorkingMemory WorkingMemory { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the rule engine context.
        /// </summary>
        /// <typeparam name="T">Type of subject.</typeparam>
        /// <param name="subject">The subject.</param>
        /// <param name="ruleSelector">The rule selector.</param>
        /// <returns>
        /// A <see cref="RuleEngineContext{T}"/>
        /// </returns>
        public static IRuleEngineContext CreateRuleEngineContext<T> ( T subject, IRuleSelector ruleSelector = null )
        {
            return new RuleEngineContext<T> ( subject, ruleSelector );
        }

        /// <inheritdoc/>
        public void AddParentContext ( IRuleEngineContext ruleEngineContext )
        {
            ParentContext = ruleEngineContext;
        }

        /// <summary>
        /// Refreshes the context.
        /// </summary>
        public void Refresh()
        {
            WorkingMemory = new WorkingMemory();
            WorkingMemory.AddContextObject(RuleViolationReporter);
            WorkingMemory.AddContextObject(NameProvider);

            RuleViolationReporter.ClearViolations ();
        }

        #endregion
    }
}
