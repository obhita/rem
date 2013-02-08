using System;
using System.Collections.Generic;
using System.Linq;
using Pillar.Common.Extension;

namespace Pillar.FluentRuleEngine.RuleSelectors
{
    /// <summary>
    ///   <see cref="IRuleSelector">Rule Selector</see> that selects all property rules whose property chain contains a member name.
    /// </summary>
    /// <typeparam name="TParameter">The type of the parameter.</typeparam>
    public class PropertyChainContainsMemberRuleSelector<TParameter> : IRuleSelector, ITakeParameter, IMemberNameRuleSelector
    {
        #region Constants and Fields

        private readonly Func<TParameter, string> _memberNameFunc;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChainContainsMemberRuleSelector{TParameter}"/> class.
        /// </summary>
        /// <param name="memberName">Name of the member.</param>
        public PropertyChainContainsMemberRuleSelector ( string memberName )
            : this ( o => memberName )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChainContainsMemberRuleSelector{TParameter}"/> class.
        /// </summary>
        /// <param name="memberNameFunc">The member name func.</param>
        public PropertyChainContainsMemberRuleSelector ( Func<TParameter, string> memberNameFunc )
        {
            _memberNameFunc = memberNameFunc;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        public string MemberName
        {
            get { return _memberNameFunc ( ( TParameter )Parameter ); }
        }

        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>
        /// The parameter.
        /// </value>
        public object Parameter { get; set; }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public IEnumerable<IRule> SelectRules<TSubject> ( IRuleCollection<TSubject> ruleCollection, IRuleEngineContext context )
        {
            object parameter = Equals ( Parameter, typeof( TParameter ).GetDefault () )
                                   ? context.WorkingMemory.GetContextObject<TParameter> ()
                                   : Parameter;
            return ruleCollection.Where ( r => SelectRule ( r, _memberNameFunc ( ( TParameter )parameter ) ) );
        }

        #endregion

        #region Methods

        private bool SelectRule ( IRule rule, string memberName )
        {
            bool selected = false;

            if ( rule is IPropertyRule )
            {
                selected = ( rule as IPropertyRule ).PropertyChain.Contains ( memberName );
            }

            if ( !selected )
            {
                selected = rule.Attributes["PropertyChain"].OfType<PropertyChain> ().Any ( pc => pc.Contains ( memberName ) );
            }

            return selected;
        }

        #endregion
    }
}
