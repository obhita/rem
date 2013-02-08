using System;
using System.Collections.Generic;
using Pillar.Common.Utility;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Defines a When <see cref="Predicate{T}"/> for an <see cref="IRuleEngineContext"/> and Lists of 
    /// Then and ElseThen <see cref="Action{T}">Actions</see>.
    /// </summary>
    public class Rule : IRule
    {
        #region Constants and Fields

        private readonly List<Action<IRuleEngineContext>> _elseThenClauses = new List<Action<IRuleEngineContext>> ();

        private readonly List<Predicate<IRuleEngineContext>> _shouldRunClauses = new List<Predicate<IRuleEngineContext>> ();
        private readonly List<Action<IRuleEngineContext>> _thenClauses = new List<Action<IRuleEngineContext>>();
        private readonly AttributeCollection _attributes;

        #endregion

        #region Constructors and Destructors

        internal Rule ( string name )
        {
            Check.IsNotNullOrWhitespace ( name, () => Name );
            Name = name;

            _attributes = new AttributeCollection ();
        }

        #endregion

        #region Public Properties

        /// <inheritdoc/>
        public int Priority { get; internal set; }

        /// <inheritdoc/>
        public bool IsDisabled { get; set; }

        /// <inheritdoc/>
        public IEnumerable<Action<IRuleEngineContext>> ElseThenClauses
        {
            get { return _elseThenClauses.AsReadOnly (); }
        }

        /// <inheritdoc/>
        public IAttributeCollection Attributes
        {
            get { return _attributes; }
        }

        /// <inheritdoc/>
        public string Name { get; protected internal set; }

        /// <inheritdoc/>
        public IEnumerable<Action<IRuleEngineContext>> ThenClauses
        {
            get { return _thenClauses.AsReadOnly (); }
        }

        /// <inheritdoc/>
        public Predicate<IRuleEngineContext> WhenClause { get; protected internal set; }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public bool ShouldRunRule ( IRuleEngineContext ruleEngineContext )
        {
            var shouldRun = true;
            foreach ( var shouldRunClause in _shouldRunClauses )
            {
                shouldRun &= shouldRunClause ( ruleEngineContext );
            }
            return shouldRun;
        }

        /// <summary>
        /// Adds the attribute.
        /// </summary>
        /// <param name="key">The key to add.</param>
        /// <param name="value">The value to add.</param>
        public void AddAttribute(string key, object value)
        {
            _attributes.Add ( new KeyValuePair<string, object> (key, value) );
        }

        /// <summary>
        /// Removes the attribute.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        /// <param name="value">The value to remove.</param>
        public void RemoveAttribute(string key, object value)
        {
            _attributes.ForEach ( kvp =>
                {
                    if ( kvp.Key == key && kvp.Value == value )
                    {
                        _attributes.Remove ( kvp );
                    }
                } );
        }

        /// <summary>
        /// Removes all attributes with key.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        public void RemoveAllAttributesWithKey(string key)
        {
            _attributes.ForEach(kvp =>
            {
                if (kvp.Key == key)
                {
                    _attributes.Remove(kvp);
                }
            });
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds ElseThenClause to Rule.
        /// </summary>
        /// <param name="elseThenClause"><see cref="Action{T}"/> to add as ElseThenClause</param>
        protected internal void AddElseThenClause ( Action<IRuleEngineContext> elseThenClause )
        {
            if ( elseThenClause == null )
            {
                throw new ArgumentNullException ( "elseThenClause" );
            }

            _elseThenClauses.Add ( elseThenClause );
        }

        /// <summary>
        /// Adds ThenClause to Rule.
        /// </summary>
        /// <param name="thenClause"><see cref="Action{T}"/> to add as ThenClause.</param>
        protected internal void AddThenClause ( Action<IRuleEngineContext> thenClause )
        {
            if ( thenClause == null )
            {
                throw new ArgumentNullException ( "thenClause" );
            }

            _thenClauses.Add ( thenClause );
        }

        /// <summary>
        /// Adds ShouldRunClause to Rule.
        /// </summary>
        /// <param name="shouldRunClause"><see cref="Predicate{T}"/> to add as ShouldRunClause</param>
        protected internal void AddShouldRunClause ( Predicate<IRuleEngineContext> shouldRunClause )
        {
            Check.IsNotNull ( shouldRunClause, "ShouldRunClause is required." );

            _shouldRunClauses.Add ( shouldRunClause );
        }

        #endregion
    }
}
