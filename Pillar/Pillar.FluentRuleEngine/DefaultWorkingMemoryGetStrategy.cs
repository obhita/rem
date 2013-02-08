using System;
using System.Collections.Generic;
using System.Linq;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Default strategy for geting context entries.
    /// </summary>
    public class DefaultWorkingMemoryGetStrategy : IWorkingMemoryGetStrategy
    {
        /// <summary>
        /// Gets the entry.
        /// </summary>
        /// <param name="workingMemory">The working memory.</param>
        /// <param name="contextObjectType">Type of the context object.</param>
        /// <param name="name">The name of the context object.</param>
        /// <returns>
        /// A <see cref="ContextEntry"/> or null if no entry exists.
        /// </returns>
        public ContextEntry GetEntry(IList<ContextEntry> workingMemory, Type contextObjectType, string name = null)
        {
            return workingMemory.SingleOrDefault(r => r.ContextObject.GetType().Equals(contextObjectType) && (name == null || r.Name == name));
        }
    }
}