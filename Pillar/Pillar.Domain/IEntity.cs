using System;

namespace Pillar.Domain
{
    /// <summary>
    /// Interface of entity.
    /// </summary>
    public interface IEntity : IEquatable<IEntity>
    {
        #region Public Properties

        /// <summary>
        /// Gets the key.
        /// </summary>
        long Key { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        int Version { get; }

        #endregion
    }
}
