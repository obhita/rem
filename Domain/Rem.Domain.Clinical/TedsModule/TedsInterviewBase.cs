using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Pillar.Domain;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// Defines the base class for Teds interview.
    /// </summary>
    public abstract class TedsInterviewBase
    {
        /// <summary>
        /// Gets the default teds non response filters.
        /// </summary>
        [IgnoreMapping]
        public virtual IEnumerable<string> DefaultTedsNonResponseFilters
        {
            get
            {
                return new List<string>
                           {
                               WellKnownNames.TedsModule.TedsNonResponse.Unknown
                           };
            }
        }

        /// <summary>
        /// Gets the possible teds non response well known names.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1">
        /// IEnumerable&lt;string&gt;</see>.
        /// </returns>
        public virtual IEnumerable<string> GetPossibleTedsNonResponseWellKnownNames<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            return new List<string>
                       {
                          //WellKnownNames.TedsModule.TedsNonResponse.NotApplicable
                       };
        }

        /// <summary>
        /// Gets the filters dictionary.
        /// </summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IDictionary`2">Dictionary&lt;string,
        /// IEnumerable&lt;string&gt;</see></returns>
        public virtual Dictionary<string, IEnumerable<string>> GetFiltersDictionary()
        {
            return new Dictionary<string, IEnumerable<string>>();
        }
    }
}
