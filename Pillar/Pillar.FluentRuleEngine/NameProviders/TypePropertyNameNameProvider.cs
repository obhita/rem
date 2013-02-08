using System;
using System.Linq.Expressions;
using Pillar.Common.Extension;
using Pillar.Common.Utility;

namespace Pillar.FluentRuleEngine.NameProviders
{
    /// <summary>
    /// Name Provider that returns the actual property name for properties and the Type name for objects.
    /// </summary>
    public class TypePropertyNameNameProvider : INameProvider
    {
        #region INameProvider Members

        /// <summary>
        /// Gets the Name of the property of a subject.
        /// </summary>
        /// <typeparam name="TSubject">Type of subject of whose property getting the name for.</typeparam>
        /// <typeparam name="TProperty">Type of the property of the <paramref name="subject">subject</paramref>.</typeparam>
        /// <param name="subject">Object of whom the property is of.</param>
        /// <param name="propertyExpression">Property Expression of the property.</param>
        /// <returns>
        /// A <see cref="string">String</see> which is the property name.
        /// </returns>
        public string GetName<TSubject, TProperty> ( TSubject subject, Expression<Func<TSubject, TProperty>> propertyExpression )
        {
            Check.IsNotNull ( propertyExpression, "propertyExpression is required." );
            return PropertyUtil.ExtractPropertyName ( propertyExpression ).SeparatePascalCaseWords ();
        }

        /// <summary>
        /// Gets the name of an Object.
        /// </summary>
        /// <param name="subject">Object to get name of.</param>
        /// <returns>
        /// A <see cref="string">String</see> which is the object name.
        /// </returns>
        public string GetName ( object subject )
        {
            Check.IsNotNull ( subject, "subject is required." );
            return subject.GetType ().Name.SeparatePascalCaseWords ();
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <typeparam name="T">The type to get the name of.</typeparam>
        /// <returns>
        /// A <see cref="string">String</see> which is the types name.
        /// </returns>
        public string GetName<T> ()
        {
            return typeof( T ).Name.SeparatePascalCaseWords ();
        }

        #endregion
    }
}
