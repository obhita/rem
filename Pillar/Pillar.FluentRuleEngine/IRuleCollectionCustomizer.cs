namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// An IRuleCollectionCustomizer is an interface that you can
    /// place over an existing rule collection to allow it to cutomize
    /// a base set of rules.  For example, Iowa could create a
    /// PatientRuleCollection that adds additional rules for
    /// Patient.  By adding the IRuleCollectionCustomizer interface
    /// allows it to change the rules in the base set of patient rules.
    /// </summary>
    /// <typeparam name="TRuleCollection">Actual type of the Rule Collection you are customizing</typeparam>
    /// <typeparam name="TSubject">Type of subject Rule Collection is written for.</typeparam>
    public interface IRuleCollectionCustomizer<in TRuleCollection, TSubject>
        where TRuleCollection : IRuleCollection<TSubject>
    {
        /// <summary>
        /// Gets the priority.
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Customizes the <see cref="IRuleCollection{TSubject}"/>
        /// </summary>
        /// <param name="ruleCollection"><see cref="IRuleCollection{TSubject}"/> to customize.</param>
        void Customize ( TRuleCollection ruleCollection );
    }
}
