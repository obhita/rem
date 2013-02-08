namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Interface for the factory of <see cref="IRuleCollection{TSubject}">Rule Collections.</see>
    /// </summary>
    public interface IRuleCollectionFactory
    {
        /// <summary>
        /// Creates a <see cref="IRuleCollection{TSubject}">Rule Collection</see> of TSubject.
        /// </summary>
        /// <typeparam name="TSubject">Type of the subject of the Rule Collection.</typeparam>
        /// <returns>A <see cref="IRuleCollection{TSubject}">RuleCollection&lt;TSubject&gt;</see></returns>
        IRuleCollection<TSubject> CreateRuleCollection<TSubject> ();
    }
}
