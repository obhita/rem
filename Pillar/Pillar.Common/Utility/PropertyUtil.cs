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
using System.Reflection;

namespace Pillar.Common.Utility
{
    /// <summary>
    /// The property util.
    /// </summary>
    public static class PropertyUtil
    {
        #region Public Methods

        /// <summary>
        /// Extracts the <see cref="PropertyInfo"/> from propertyExpression.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>PropertyInfo of the propertyExpression.</returns>
        /// <exception cref="ArgumentNullException">Property Expression cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">The expression is not a member access expression.
        /// </exception>
        public static PropertyInfo ExtractProperty<T, TProperty> ( Expression<Func<T, TProperty>> propertyExpression )
        {
            if ( propertyExpression == null )
            {
                throw new ArgumentNullException ( "propertyExpression" );
            }

            MemberExpression memberExpression = null;

            switch ( propertyExpression.Body.NodeType )
            {
                case ExpressionType.Convert:

                    var unaryExpression = propertyExpression.Body as UnaryExpression;
                    if ( unaryExpression != null )
                    {
                        memberExpression = unaryExpression.Operand as MemberExpression;
                    }

                    break;

                case ExpressionType.MemberAccess:
                    memberExpression = propertyExpression.Body as MemberExpression;
                    break;
            }

            if ( memberExpression == null )
            {
                throw new ArgumentException ( "The expression is not a member access expression.", "propertyExpression" );
            }

            var propertyOfBase = memberExpression.Member as PropertyInfo;
            if ( propertyOfBase == null )
            {
                throw new ArgumentException ( "The member access expression does not access a property.", "propertyExpression" );
            }

            var property = typeof( T ).GetProperty ( propertyOfBase.Name );

            return property;
        }

        /// <summary>
        /// Extracts the <see cref="PropertyInfo"/> from propertyExpression.
        /// </summary>
        /// <param name="T">The type of object.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>PropertyInfo of the propertyExpression.</returns>
        public static PropertyInfo ExtractProperty ( Type T, string propertyName )
        {
            return T.GetProperty ( propertyName );
        }

        /// <summary>
        /// Extracts Property Name from propertyExpression.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>The extracted property name.</returns>
        public static string ExtractPropertyName<T, TProperty> ( Expression<Func<T, TProperty>> propertyExpression )
        {
            var propertyName = ExtractPropertyName ( ( LambdaExpression )propertyExpression );
            return propertyName;
        }

        /// <summary>
        /// Extracts Property Name from propertyExpression.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>The extracted property name.</returns>
        public static string ExtractPropertyName<TProperty> ( Expression<Func<TProperty>> propertyExpression )
        {
            var propertyName = ExtractPropertyName ( ( LambdaExpression )propertyExpression );
            return propertyName;
        }

        /// <summary>
        /// Extracts Property Name from propertyExpression.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>The extracted property name.</returns>
        public static string ExtractPropertyName ( Expression<Func<object>> propertyExpression )
        {
            var propertyName = ExtractPropertyName ( ( LambdaExpression )propertyExpression );
            return propertyName;
        }

        /// <summary>
        /// Extracts the name of the property.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>The extracted property name.</returns>
        public static string ExtractPropertyName ( LambdaExpression propertyExpression )
        {
            if ( propertyExpression == null )
            {
                throw new ArgumentNullException ( "propertyExpression" );
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if ( propertyExpression.Body is UnaryExpression )
            {
                memberExpression = ( propertyExpression.Body as UnaryExpression ).Operand as MemberExpression;
            }
            if ( memberExpression == null )
            {
                throw new ArgumentException ( "The expression is not a member access expression.", "propertyExpression" );
            }

            var property = memberExpression.Member as PropertyInfo;
            if ( property == null )
            {
                throw new ArgumentException ( "The member access expression does not access a property.", "propertyExpression" );
            }

            var getMethod = property.GetGetMethod ( true );
            if ( getMethod.IsStatic )
            {
                throw new ArgumentException ( "The referenced property is a static property.", "propertyExpression" );
            }

            return memberExpression.Member.Name;
        }

        #endregion
    }
}
