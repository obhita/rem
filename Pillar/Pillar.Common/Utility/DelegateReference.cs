using System;
using System.Reflection;

namespace Pillar.Common.Utility
{
    /// <summary>
    /// Represents a reference to a <see cref="Delegate"/> that may contain a
    /// <see cref="WeakReference"/> to the target. This class is used
    /// internally by the Composite Application Library.
    /// </summary>
    public class DelegateReference : IDelegateReference
    {
        #region Constants and Fields

        private readonly Delegate _delegate;
        private readonly Type _delegateType;
        private readonly MethodInfo _method;
        private readonly WeakReference _weakReference;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateReference"/> class.
        /// </summary>
        /// <param name="delegate">The delegate.</param>
        /// <param name="keepReferenceAlive">If set to <c>true</c> [keep reference alive].</param>
        /// <exception cref="ArgumentNullException">If the passed <paramref name="delegate"/> is not assignable to <see cref="Delegate"/>.</exception>
        public DelegateReference ( Delegate @delegate, bool keepReferenceAlive )
        {
            if ( @delegate == null )
            {
                throw new ArgumentNullException ( "delegate" );
            }

            if ( keepReferenceAlive )
            {
                _delegate = @delegate;
            }
            else
            {
                _weakReference = new WeakReference ( @delegate.Target );
                _method = @delegate.Method;
                _delegateType = @delegate.GetType ();
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the <see cref="Delegate"/> (the target) referenced by the current <see cref="DelegateReference"/> object.
        /// </summary>
        /// <value><see langword="null"/> if the object referenced by the current <see cref="DelegateReference"/> object has been garbage collected; otherwise, a reference to the <see cref="Delegate"/> referenced by the current <see cref="DelegateReference"/> object.</value>
        public Delegate Target
        {
            get
            {
                if ( _delegate != null )
                {
                    return _delegate;
                }
                else
                {
                    return TryGetDelegate ();
                }
            }
        }

        #endregion

        #region Methods

        private Delegate TryGetDelegate ()
        {
            if ( _method.IsStatic )
            {
                return Delegate.CreateDelegate ( _delegateType, null, _method );
            }
            var target = _weakReference.Target;
            if ( target != null )
            {
                return Delegate.CreateDelegate ( _delegateType, target, _method );
            }
            return null;
        }

        #endregion
    }
}
