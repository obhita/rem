using System;

namespace Pillar.Domain
{
    /// <summary>
    /// RepositoryException class.
    /// </summary>
    public class RepositoryException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public RepositoryException ( string message )
            : base ( message )
        {
        }

        #endregion
    }
}
