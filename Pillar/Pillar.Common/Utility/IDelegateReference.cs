using System;

namespace Pillar.Common.Utility
{
    /// <summary>
    /// Represents a reference to a <see cref="Delegate"/>.
    /// </summary>
    public interface IDelegateReference
    {
        #region Public Properties

        /// <summary>
        /// Gets the referenced <see cref="Delegate"/> object.
        /// </summary>
        /// <value>A <see cref="Delegate"/> instance if the target is valid; otherwise <see langword="null"/>.</value>
        Delegate Target { get; }

        #endregion
    }
}
