using System;
using System.Collections.Generic;
using System.Linq;

#if SILVERLIGHT
using Pillar.Common.Extension;
#endif

namespace Pillar.Common.Utility
{
    /// <summary>
    /// Class for managing weak delegates.
    /// </summary>
    public class WeakDelegatesManager
    {
        #region Constants and Fields

        private readonly List<DelegateReference> _listeners = new List<DelegateReference> ();

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the listener.
        /// </summary>
        /// <param name="listener">The listener.</param>
        /// <param name="keepReferenceAlive">If set to <c>true</c> [keep reference alive].</param>
        public void AddListener ( Delegate listener, bool keepReferenceAlive = false )
        {
            _listeners.Add ( new DelegateReference ( listener, keepReferenceAlive ) );
        }

        /// <summary>
        /// Raises the event.
        /// </summary>
        /// <param name="args">The args to use when raising.</param>
        public void Raise ( params object[] args )
        {
            _listeners.RemoveAll ( listener => listener.Target == null );

            foreach ( var handler in _listeners.ToList ().Select ( listener => listener.Target ).Where ( listener => listener != null ) )
            {
                handler.DynamicInvoke ( args );
            }
        }

        /// <summary>
        /// Removes the listener.
        /// </summary>
        /// <param name="listener">The listener.</param>
        public void RemoveListener ( Delegate listener )
        {
            _listeners.RemoveAll (
                reference =>
                    {
                        //Remove the listener, and prune collected listeners
                        var target = reference.Target;
                        return listener.Equals ( target ) || target == null;
                    } );
        }

        #endregion
    }
}
