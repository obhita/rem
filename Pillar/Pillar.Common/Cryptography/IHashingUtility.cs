namespace Pillar.Common.Cryptography
{
    /// <summary>
    /// Interface for Utility that can compute a hash from data.
    /// </summary>
    public interface IHashingUtility
    {
        #region Public Methods

        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="data">The data to compute hash.</param>
        /// <returns>The computed hash for the data.</returns>
        string ComputeHash ( byte[] data );

        #endregion
    }
}
