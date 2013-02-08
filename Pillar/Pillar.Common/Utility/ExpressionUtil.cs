#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Linq.Expressions;

namespace Pillar.Common.Utility
{
    /// <summary>
    /// Utility methods for various expressions.
    /// </summary>
    public class ExpressionUtil
    {
        #region Public Methods

        /// <summary>
        /// Creates a boxed expression from the given expression
        /// </summary>
        /// <typeparam name="TInput">The input type for the <see cref="Func{TInput, TResult}"/> expression.</typeparam>
        /// <typeparam name="TOutput">The result type of the <see cref="Func{TInput, TResult}"/> expression.</typeparam>
        /// <param name="expression">The given expression.</param>
        /// <returns>A boxed expression</returns>
        public static Expression<Func<TInput, object>> AddBox<TInput, TOutput> ( Expression<Func<TInput, TOutput>> expression )
        {
            Expression converted = Expression.Convert ( expression.Body, typeof( object ) );

            var boxedExpression = Expression.Lambda<Func<TInput, object>> ( converted, expression.Parameters );

            return boxedExpression;
        }

        /// <summary>
        /// Returns a <see cref="MemberExpression"/> for the given expression.
        /// </summary>
        /// <typeparam name="T">The input type for the <see cref="Func{TInput, TResult}"/> expression.</typeparam>
        /// <param name="expression">The given expression.</param>
        /// <returns>A <see cref="MemberExpression"/>.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the given expression is not a <see cref="MemberExpression"/>.
        /// </exception>
        public static MemberExpression GetMemberExpression<T> ( Expression<Func<T, object>> expression )
        {
            MemberExpression memberExpression = null;

            // Convert
            if ( expression.Body.NodeType == ExpressionType.Convert )
            {
                var body = ( UnaryExpression )expression.Body;
                memberExpression = body.Operand as MemberExpression;
            }
            else if ( expression.Body.NodeType == ExpressionType.MemberAccess )
            {
                memberExpression = expression.Body as MemberExpression;
            }

            // Not a member access
            if ( memberExpression == null )
            {
                throw new ArgumentException ( "Not a member access", "expression" );
            }

            // Return the member expression
            return memberExpression;
        }

        #endregion
    }
}
