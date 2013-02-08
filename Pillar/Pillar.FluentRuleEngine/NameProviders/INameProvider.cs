using System;
using System.Linq.Expressions;

namespace Pillar.FluentRuleEngine.NameProviders
{
    /// <summary>
    /// Interface for Name Providers
    /// Used by <see cref="RuleEngine{TSubject}">Rule Engine</see> to get names of objects and properties.
    /// </summary>
    public interface INameProvider
    {
        /// <summary>
        /// Gets the Name of the property of a subject.
        /// </summary>
        /// <typeparam name="TSubject">Type of subject of whose property getting the name for.</typeparam>
        /// <typeparam name="TProperty">Type of the property of the <paramref name="subject">subject</paramref>.</typeparam>
        /// <param name="subject">Object of whom the property is of.</param>
        /// <param name="propertyExpression">Property Expression of the property.</param>
        /// <returns>A <see cref="string">String</see> which is the property name.</returns>
        string GetName<TSubject, TProperty> ( TSubject subject, Expression<Func<TSubject, TProperty>> propertyExpression );

        /// <summary>
        /// Gets the name of an Object.
        /// </summary>
        /// <param name="subject">Object to get name of.</param>
        /// <returns>A <see cref="string">String</see> which is the object name.</returns>
        string GetName ( object subject );

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <typeparam name="T">The type to get the name of.</typeparam>
        /// <returns>A <see cref="string">String</see> which is the types name.</returns>
        string GetName<T> ();
    }
}
