using System;
using System.Collections.Generic;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Interface for Get Strategy of <see cref="WorkingMemory"/>
    /// </summary>
    public interface IWorkingMemoryGetStrategy
    {
        /// <summary>
        /// Gets the entry.
        /// </summary>
        /// <param name="workingMemory">The working memory.</param>
        /// <param name="contextObjectType">Type of the context object.</param>
        /// <param name="name">The name of the context object.</param>
        /// <returns>A <see cref="ContextEntry"/> or null if no entry exists.</returns>
        ContextEntry GetEntry(IList<ContextEntry> workingMemory, Type contextObjectType, string name = null);
    }
}
