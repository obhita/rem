using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Class used to describe the Property Chain of an objects property.
    /// A list of string members in the property chain.
    /// </summary>
    public class PropertyChain : List<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChain"/> class.
        /// </summary>
        /// <param name="strings">List of member strings to initialize the propertyChain with.</param>
        public PropertyChain ( IEnumerable<string> strings )
            : base ( strings )
        {
        }

        /// <summary>
        /// Creates a property chain from a Lambda Expression
        /// </summary>
        /// <param name="lambdaExpression"><see cref="LambdaExpression">Lambda Expression</see> to create property chain from.</param>
        /// <returns>A <see cref="PropertyChain"/></returns>
        public static PropertyChain FromLambdaExpression ( LambdaExpression lambdaExpression )
        {
            var memberNames = new Stack<string> ();

            var getMemberExp = new Func<Expression, MemberExpression> (
                toUnwrap =>
                    {
                        if ( toUnwrap is UnaryExpression )
                        {
                            return ( ( UnaryExpression )toUnwrap ).Operand as MemberExpression;
                        }

                        return toUnwrap as MemberExpression;
                    } );

            var memberExp = getMemberExp ( lambdaExpression.Body );

            while ( memberExp != null )
            {
                memberNames.Push ( memberExp.Member.Name );
                memberExp = getMemberExp ( memberExp.Expression );
            }

            return new PropertyChain ( memberNames );
        }

        /// <summary>
        /// Froms the property expression.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>A <see cref="PropertyChain"/></returns>
        public static PropertyChain FromPropertyExpression<TProperty> ( Expression<Func<TProperty>> propertyExpression )
        {
            return FromLambdaExpression (propertyExpression );
        }

        /// <summary>
        /// Joins property members using ".".
        /// </summary>
        /// <returns>Joined string.</returns>
        public override string ToString ()
        {
            return string.Join ( ".", this );
        }
    }
}
