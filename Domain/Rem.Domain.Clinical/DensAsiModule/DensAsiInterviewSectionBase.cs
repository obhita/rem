using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Pillar.Domain;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// DensAsiInterviewSectionBase provides a base class for DensAsi sections.
    /// </summary>
    public abstract class DensAsiInterviewSectionBase
    {
        /// <summary>
        /// Gets the default DensAsi non response filters.
        /// </summary>
        [IgnoreMapping]
        public virtual IEnumerable<string> DefaultDensAsiNonResponseFilters
        {
            get
            {
                return new List<string>
                           {
                               WellKnownNames.DensAsiModule.DensAsiNonResponse.NotAnswered
                           };
            }
        }

        /// <summary>
        /// Gets the possible dens asi non response well known names.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1">
        /// IEnumerable&lt;string&gt;</see>.
        /// </returns>
        public virtual IEnumerable<string> GetPossibleDensAsiNonResponseWellKnownNames<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            return new List<string>
                       {
                           WellKnownNames.DensAsiModule.DensAsiNonResponse.NotAnswered
                       };
        }

        /// <summary>
        /// Gets the well known names filters dictionary.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IDictionary`2">Dictionary&lt;string,
        /// IEnumerable&lt;string&gt;</see>
        /// </returns>
        public virtual Dictionary<string, IEnumerable<string>> GetFiltersDictionary()
        {
            return new Dictionary<string, IEnumerable<string>>();
        }
    }
}