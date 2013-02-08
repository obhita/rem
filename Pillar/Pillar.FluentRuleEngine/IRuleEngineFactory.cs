namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Interface for class that is the factory for <see cref="IRuleEngine{TSubject}">Rule Engines</see>
    /// </summary>
    public interface IRuleEngineFactory
    {
        /// <summary>
        /// Creates the <see cref="RuleEngine{TSubject}">Rule Engine</see> using the <see cref="IRuleCollection{TSubject}">Rule Collection</see> passed into the constructor.
        /// </summary>
        /// <typeparam name="TSubject">The Type of the subject for the rules.</typeparam>
        /// <returns>A <see cref="IRuleEngine{TSubject}">Rule Engine</see>.</returns>
        IRuleEngine<TSubject> CreateRuleEngine<TSubject> ();

        /// <summary>
        /// Creates the <see cref="RuleEngine{TSubject}">Rule Engine</see> using the <see cref="IRuleCollection{TSubject}">Rule Collection</see> passed into the constructor.
        /// </summary>
        /// <typeparam name="TSubject">The Type of the subject for the rules.</typeparam>
        /// <param name="subject">The subject.</param>
        /// <returns>
        /// A <see cref="IRuleEngine{TSubject}">Rule Engine</see>.
        /// </returns>
        IRuleEngine<TSubject> CreateRuleEngine<TSubject> ( TSubject subject );
    }
}
