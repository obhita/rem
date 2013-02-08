namespace Pillar.Common.Cryptography
{
    /// <summary>
    /// Interface for utility that encypts data.
    /// </summary>
    public interface IEncryptionUtility
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this instance is key loaded.
        /// </summary>
        bool IsKeyLoaded { get; }

        /// <summary>
        /// Gets or sets the iv cypher.
        /// </summary>
        /// <value>The iv cypher.</value>
        byte[] IvCypher { get; set; }

        /// <summary>
        /// Gets the size of the iv.
        /// </summary>
        int IvSize { get; }

        /// <summary>
        /// Gets or sets the key cypher.
        /// </summary>
        /// <value>The key cypher.</value>
        byte[] KeyCypher { get; set; }

        /// <summary>
        /// Gets the size of the key.
        /// </summary>
        int KeySize { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Decrypts the specified data.
        /// </summary>
        /// <param name="data">The data Decrypt.</param>
        /// <param name="key">The key to use when Decrypting.</param>
        /// <param name="iv">The iv to use when Decrypting.</param>
        /// <returns>The decrypted byte[].</returns>
        byte[] Decrypt ( byte[] data, byte[] key = null, byte[] iv = null );

        /// <summary>
        /// Encrypts the specified data.
        /// </summary>
        /// <param name="data">The data Encrypt.</param>
        /// <param name="key">The key to use when Encrypting.</param>
        /// <param name="iv">The iv to use when Encrypting.</param>
        /// <returns>Encrypted byte array.</returns>
        byte[] Encrypt ( byte[] data, byte[] key = null, byte[] iv = null );

        /// <summary>
        /// Loaders the key from configuration.
        /// </summary>
        void LoaderKeyFromConfiguration ();

        #endregion
    }
}
