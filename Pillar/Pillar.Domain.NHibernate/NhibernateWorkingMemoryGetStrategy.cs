using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Proxy;
using Pillar.FluentRuleEngine;

namespace Pillar.Domain.NHibernate
{
    /// <summary>
    /// Strategy for getting nhibernate working memory.
    /// </summary>
    public class NhibernateWorkingMemoryGetStrategy : IWorkingMemoryGetStrategy
    {
        #region Public Methods

        /// <summary>
        /// Gets the entry.
        /// </summary>
        /// <param name="workingMemory">The working memory.</param>
        /// <param name="contextObjectType">Type of the context object.</param>
        /// <param name="name">The name of the context object.</param>
        /// <returns>A <see cref="ContextEntry"/> or null if no entry exists.</returns>
        public ContextEntry GetEntry ( IList<ContextEntry> workingMemory, Type contextObjectType, string name = null )
        {
            return workingMemory.SingleOrDefault (
                r =>
                    {
                        var compareType = r.ContextObject.GetType ();
                        if ( r.ContextObject is INHibernateProxy )
                        {
                            var proxy = r.ContextObject as INHibernateProxy;
                            compareType = proxy.HibernateLazyInitializer.PersistentClass;
                        }
                        return compareType.Equals ( contextObjectType ) && ( name == null || r.Name == name );
                    } );
        }

        #endregion
    }
}
