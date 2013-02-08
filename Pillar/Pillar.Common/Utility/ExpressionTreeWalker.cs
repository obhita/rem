using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Pillar.Common.Utility
{
    /// <summary>
    /// Walks an expression tree.
    /// </summary>
    public static class ExpressionTreeWalker
    {
        #region Public Methods

        /// <summary>
        /// Walks the expression tree and calls a callback when expressions of type T are found.
        /// </summary>
        /// <typeparam name="T">Type of expression to find.</typeparam>
        /// <param name="exp">The expression tree to search.</param>
        /// <param name="foundCallback">Callback for when expression is found, should return true if should continue searching false if should stop.</param>
        public static void Find<T> ( Expression exp, Func<T, bool> foundCallback )
            where T : Expression
        {
            var stop = false;
            Find ( exp, foundCallback, ref stop );
        }

        /// <summary>
        /// Finds the first expression of type T in the expression tree.
        /// </summary>
        /// <typeparam name="T">Type of expression to find.</typeparam>
        /// <param name="expression">The expression tree to search.</param>
        /// <returns>The found Expression.</returns>
        public static T FindFirst<T> ( Expression expression )
            where T : Expression
        {
            T foundItem = null;
            Find<T> (
                expression,
                exp =>
                    {
                        foundItem = exp;
                        return false;
                    } );
            return foundItem;
        }

        #endregion

        #region Methods

        private static void Find<T> ( Expression exp, Func<T, bool> foundCallback, ref bool stop )
            where T : Expression
        {
            if ( exp == null || stop )
            {
                return;
            }

            if ( exp is T && !foundCallback ( exp as T ) )
            {
                stop = true;
                return;
            }

            switch ( exp.NodeType )
            {
                case ExpressionType.Parameter:
                case ExpressionType.Constant:
                    return;
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                    Find ( ( ( UnaryExpression )exp ).Operand, foundCallback, ref stop );
                    break;
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                    var binaryExp = ( BinaryExpression )exp;
                    Find ( binaryExp.Left, foundCallback, ref stop );
                    Find ( binaryExp.Right, foundCallback, ref stop );
                    Find ( binaryExp.Conversion, foundCallback, ref stop );
                    break;
                case ExpressionType.TypeIs:
                    Find ( ( ( TypeBinaryExpression )exp ).Expression, foundCallback, ref stop );
                    break;
                case ExpressionType.Conditional:
                    var conditionalExp = ( ConditionalExpression )exp;
                    Find ( conditionalExp.Test, foundCallback, ref stop );
                    Find ( conditionalExp.IfTrue, foundCallback, ref stop );
                    Find ( conditionalExp.IfFalse, foundCallback, ref stop );
                    break;
                case ExpressionType.MemberAccess:
                    Find ( ( ( MemberExpression )exp ).Expression, foundCallback, ref stop );
                    break;
                case ExpressionType.Call:
                    var methodCallExp = ( MethodCallExpression )exp;
                    Find ( methodCallExp.Object, foundCallback, ref stop );
                    foreach ( var expression in methodCallExp.Arguments )
                    {
                        Find ( expression, foundCallback, ref stop );
                    }
                    break;
                case ExpressionType.Lambda:
                    var lambdaExp = ( LambdaExpression )exp;
                    Find ( lambdaExp.Body, foundCallback, ref stop );
                    foreach ( var parameterExpression in lambdaExp.Parameters )
                    {
                        Find ( parameterExpression, foundCallback, ref stop );
                    }
                    break;
                case ExpressionType.New:
                    var newExp = ( NewExpression )exp;
                    foreach ( var expression in newExp.Arguments )
                    {
                        Find ( expression, foundCallback, ref stop );
                    }
                    break;
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                    var newArrayExp = ( NewArrayExpression )exp;
                    foreach ( var expression in newArrayExp.Expressions )
                    {
                        Find ( expression, foundCallback, ref stop );
                    }
                    break;
                case ExpressionType.Invoke:
                    var invocationExp = ( InvocationExpression )exp;
                    foreach ( var expression in invocationExp.Arguments )
                    {
                        Find ( expression, foundCallback, ref stop );
                    }
                    Find ( invocationExp.Expression, foundCallback, ref stop );
                    break;
                case ExpressionType.MemberInit:
                    var memberInit = ( MemberInitExpression )exp;
                    Find ( memberInit.NewExpression, foundCallback, ref stop );
                    foreach ( var expression in memberInit.Bindings.SelectMany ( GetBindingExpressions ) )
                    {
                        Find ( expression, foundCallback, ref stop );
                    }
                    break;
                case ExpressionType.ListInit:
                    var listInit = ( ListInitExpression )exp;
                    Find ( listInit.NewExpression, foundCallback, ref stop );
                    foreach ( var expression in listInit.Initializers.SelectMany ( init => init.Arguments ) )
                    {
                        Find ( expression, foundCallback, ref stop );
                    }
                    break;
            }
        }

        private static IEnumerable<Expression> GetBindingExpressions ( MemberBinding binding )
        {
            switch ( binding.BindingType )
            {
                case MemberBindingType.Assignment:
                    yield return ( ( MemberAssignment )binding ).Expression;
                    break;
                case MemberBindingType.MemberBinding:
                    foreach ( var expression in ( ( MemberMemberBinding )binding ).Bindings.SelectMany ( GetBindingExpressions ) )
                    {
                        yield return expression;
                    }
                    break;
                case MemberBindingType.ListBinding:
                    foreach ( var expression in ( ( MemberListBinding )binding ).Initializers.SelectMany ( ei => ei.Arguments ) )
                    {
                        yield return expression;
                    }
                    break;
            }
        }

        #endregion
    }
}
