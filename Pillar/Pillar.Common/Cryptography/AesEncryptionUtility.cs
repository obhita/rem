using System;
using System.IO;
using System.Security.Cryptography;
using Pillar.Common.Configuration;
using Pillar.Common.Utility;

namespace Pillar.Common.Cryptography
{
    /// <summary>
    /// Utility for aes encryption.
    /// </summary>
    public class AesEncryptionUtility : IEncryptionUtility
    {
        #region Constants and Fields

        private const string MeaningfulUseEncryptionIvPropertyName = "MeaningfulUseEncryptionIV";

        private const string MeaningfulUseEncryptionKeyPropertyName = "MeaningfulUseEncryptionKey";

        private readonly IConfigurationPropertiesProvider _configurationPropertiesProvider;
        private readonly int _ivSize = 16;
        private readonly int _keySize = 32;

        private bool _isKeyLoaded;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AesEncryptionUtility"/> class.
        /// </summary>
        /// <param name="configurationPropertiesProvider">The configuration properties provider.</param>
        public AesEncryptionUtility ( IConfigurationPropertiesProvider configurationPropertiesProvider )
        {
            _configurationPropertiesProvider = configurationPropertiesProvider;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this instance is key loaded.
        /// </summary>
        public bool IsKeyLoaded
        {
            get { return _isKeyLoaded; }
        }

        /// <summary>
        /// Gets or sets the iv cypher.
        /// </summary>
        /// <value>The iv cypher.</value>
        public byte[] IvCypher { get; set; }

        /// <summary>
        /// Gets the size of the iv.
        /// </summary>
        public int IvSize
        {
            get { return _ivSize; }
        }

        /// <summary>
        /// Gets or sets the key cypher.
        /// </summary>
        /// <value>The key cypher.</value>
        public byte[] KeyCypher { get; set; }

        /// <summary>
        /// Gets the size of the key.
        /// </summary>
        public int KeySize
        {
            get { return _keySize; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs symmetric dencryption on the payload provided. If key or iv are not provided, it uses the
        /// pre-configured cryptographic key and IV retrieved from the Configuration Store.
        /// </summary>
        /// <param name="data">Byte[] containing the data to be dencrypted</param>
        /// <param name="keyCypher">A byte[] containing the symmetric key</param>
        /// <param name="ivCypher">A byte[] containing the initialization vector</param>
        /// <returns>A byte[] containing the cleartext data</returns>
        public byte[] Decrypt ( byte[] data, byte[] keyCypher = null, byte[] ivCypher = null )
        {
            // If crypto is not provided, load cryto from Config Store
            if ( keyCypher == null || ivCypher == null )
            {
                LoaderKeyFromConfiguration ();
            }
            else
            {
                KeyCypher = keyCypher;
                IvCypher = ivCypher;
            }

            var dataCypher = new byte[data.Length];
            byte[] cleartextData;

            // Copy just the Data - without the Crypto - from the array
            Array.Copy ( data, 0, dataCypher, 0, dataCypher.Length );

            using ( var aes = new AesManaged () )
            {
                // Configure AES Crypto Provider
                ConfigureEncryptionProvider ( aes, KeyCypher, IvCypher );

                using ( var ms = new MemoryStream () )
                {
                    using ( var cs = new CryptoStream ( ms, aes.CreateDecryptor (), CryptoStreamMode.Write ) )
                    {
                        cs.Write ( dataCypher, 0, dataCypher.Length );
                    }

                    cleartextData = ms.ToArray ();
                }
            }
            return cleartextData;
        }

        /// <summary>
        /// Performs symmetric encryption on the payload provided. If key or iv are not provided, it uses the
        /// pre-configured cryptographic key and IV retrieved from the Configuration Store.
        /// </summary>
        /// <param name="data">Byte[] containing the data to be encrypted</param>
        /// <param name="keyCypher">A byte[] containing the symmetric key</param>
        /// <param name="ivCypher">A byte[] containing the initialization vector</param>
        /// <returns>A byte[] containing the cyphered data</returns>
        public byte[] Encrypt ( byte[] data, byte[] keyCypher = null, byte[] ivCypher = null )
        {
            // If crypto is not provided, load cryto from Config Store
            if ( keyCypher == null || ivCypher == null )
            {
                LoaderKeyFromConfiguration ();
            }
            else
            {
                KeyCypher = keyCypher;
                IvCypher = ivCypher;
            }

            byte[] dataCypher = null;

            using ( var aes = new AesManaged () )
            {
                // Configure AES Crypto Provider
                ConfigureEncryptionProvider ( aes, KeyCypher, IvCypher );

                using ( var ms = new MemoryStream () )
                {
                    using ( var cs = new CryptoStream ( ms, aes.CreateEncryptor (), CryptoStreamMode.Write ) )
                    {
                        cs.Write ( data, 0, data.Length );
                    }

                    dataCypher = ms.ToArray ();
                }
            }
            return dataCypher;
        }

        /// <summary>
        /// Loaders the key from configuration.
        /// </summary>
        public void LoaderKeyFromConfiguration ()
        {
            // Retrieve the Keys from Configuration Store
            var key = _configurationPropertiesProvider.GetProperty ( MeaningfulUseEncryptionKeyPropertyName );
            var iv = _configurationPropertiesProvider.GetProperty ( MeaningfulUseEncryptionIvPropertyName );

            //TODO: This is not how we want to deal with this beyond Meaningful Use Certification
            Check.IsNotNullOrWhitespace ( key, "The Configuration Store did not contain an AES Key for Document Encryption" );
            Check.IsNotNullOrWhitespace ( iv, "The Configuration Store did not contain an AES IV for Document Encryption" );

            KeyCypher = Convert.FromBase64String ( key );
            IvCypher = Convert.FromBase64String ( iv );

            _isKeyLoaded = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Configures the encryption provider.
        /// </summary>
        /// <param name="aes">The managed aes.</param>
        /// <param name="keyCypher">The key cypher.</param>
        /// <param name="ivCypher">The iv cypher.</param>
        private void ConfigureEncryptionProvider ( AesManaged aes, byte[] keyCypher, byte[] ivCypher )
        {
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC; // Cypher Block Chaining 
            aes.KeySize = KeySize * 8; // Bits
            aes.Key = keyCypher;
            aes.IV = ivCypher;
        }

        #endregion
    }
}
