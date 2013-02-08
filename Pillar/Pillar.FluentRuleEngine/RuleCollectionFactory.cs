using System.Collections.Generic;
using System.Linq;
using Pillar.Common.InversionOfControl;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Factory for creating a <see cref="IRuleCollection{TSubject}">Rule Collection</see>.
    /// </summary>
    public class RuleCollectionFactory : IRuleCollectionFactory
    {
        #region IRuleCollectionFactory Members

        /// <summary>
        /// Creates a <see cref="IRuleCollection{TSubject}">Rule Collection</see> of TSubject.
        /// </summary>
        /// <typeparam name="TSubject">Type of the subject of the Rule Collection.</typeparam>
        /// <returns>
        /// A <see cref="IRuleCollection{TSubject}">RuleCollection&lt;TSubject&gt;</see>
        /// </returns>
        public IRuleCollection<TSubject> CreateRuleCollection<TSubject> ()
        {
            var ruleCollection = IoC.CurrentContainer.Resolve<IRuleCollection<TSubject>> ();

            if (!ruleCollection.IsInitialized)
            {
                var ruleCollectionCustomizers =
                    new RuleCollectionCustomizersHelper<TSubject> ().CreateRuleCollectionCustomizers ( this, ruleCollection );

                foreach ( var ruleCollectionCustomizer in ruleCollectionCustomizers.OrderBy ( c => c.Priority ) )
                {
                    ruleCollectionCustomizer.Customize ( ruleCollection );
                }

                ruleCollection.IsInitialized = true;
            }

            return ruleCollection;
        }

        #endregion

        #region Nested type: RuleCollectionCustomizersHelper

        private class RuleCollectionCustomizersHelper<TSubject>
        {
            public IEnumerable<IRuleCollectionCustomizer<TRuleCollection, TSubject>> CreateRuleCollectionCustomizers<TRuleCollection> (
                RuleCollectionFactory factory, TRuleCollection ruleCollection )
                where TRuleCollection : IRuleCollection<TSubject>
            {
                return IoC.CurrentContainer.ResolveAll<IRuleCollectionCustomizer<TRuleCollection, TSubject>> ();
            }
        }

        #endregion
    }
}
