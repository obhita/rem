using System;
using System.Collections.Generic;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Class that contains the working memory for the current execution of a Rule Collection.
    /// </summary>
    public class WorkingMemory
    {
        private readonly IList<ContextEntry> _contextObjects = new List<ContextEntry> ();
        private static readonly IWorkingMemoryGetStrategy WorkingMemoryGetStrategy;

        /// <summary>
        /// Initializes static members of the <see cref="WorkingMemory"/> class.
        /// </summary>
        static WorkingMemory()
        {
            if (IoC.CurrentContainer != null)
            {
                WorkingMemoryGetStrategy = IoC.CurrentContainer.TryResolve<IWorkingMemoryGetStrategy> ();
            }
            
            if (WorkingMemoryGetStrategy == null)
            {
                WorkingMemoryGetStrategy = new DefaultWorkingMemoryGetStrategy ();
            }
        }

        /// <summary>
        /// Adds a context object to the working memory.
        /// </summary>
        /// <typeparam name="TContextObject">Type of Context Object to add.</typeparam>
        /// <param name="contextObject">Context Object to add.</param>
        /// <returns>A <see cref="WorkingMemory"/></returns>
        public WorkingMemory AddContextObject<TContextObject> ( TContextObject contextObject )
        {
            return AddContextObject ( contextObject, "Default" );
        }

        /// <summary>
        /// Adds a context object to the working memory.
        /// </summary>
        /// <typeparam name="TContextObject">Type of Context Object to add.</typeparam>
        /// <param name="contextObject">Context Object to add.</param>
        /// <param name="name">Name to use for context object when added to working memory.</param>
        /// <returns>A <see cref="WorkingMemory"/></returns>
        public WorkingMemory AddContextObject<TContextObject> ( TContextObject contextObject, string name )
        {
            Check.IsNotNull ( contextObject, "Context Object is required." );

            _contextObjects.Add ( new ContextEntry { ContextObject = contextObject, Name = name } );

            return this;
        }

        /// <summary>
        /// Gets a context object from the working memory based on type.
        /// </summary>
        /// <typeparam name="TContextObject">Type of object to get from working memory.</typeparam>
        /// <returns>Context object from working memory.</returns>
        public TContextObject GetContextObject<TContextObject> ()
        {
            var contextObjectType = typeof( TContextObject );
            return ( TContextObject )GetContextObject ( contextObjectType );
        }

        /// <summary>
        /// Gets a context object from the working memory based on type.
        /// </summary>
        /// <param name="contextObjectType">Type of ContextObject.</param>
        /// <param name="name">Optional Name of ContextObject. If no name the Type is used to find the ContextObject.</param>
        /// <returns><see cref="object">Object</see> that is the Context Object.</returns>
        public object GetContextObject ( Type contextObjectType, string name = null )
        {
            var contextEntry = WorkingMemoryGetStrategy.GetEntry ( _contextObjects, contextObjectType, name );
            if (contextEntry != null)
            {
                return contextEntry.ContextObject;
            }
            return null;
        }

        /// <summary>
        /// Gets a context object from the working memory based on a name.
        /// </summary>
        /// <typeparam name="TContextObject">Type of object to get from working memory.</typeparam>
        /// <param name="name">Name of object to get from working memeory.</param>
        /// <returns>Context object from working memory.</returns>
        public TContextObject GetContextObject<TContextObject> ( string name )
        {
            var contextObjectType = typeof( TContextObject );
            return ( TContextObject )GetContextObject ( contextObjectType, name );
        }
    }
}
