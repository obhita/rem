using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Pillar.Common.Cryptography
{
    /// <summary>
    /// Utility for hashing.
    /// </summary>
    public class HashingUtility : IHashingUtility
    {
        #region Public Methods

        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="data">The data to hash.</param>
        /// <returns>The computed hash for the data.</returns>
        public string ComputeHash ( byte[] data )
        {
            string hash;

            using ( var sha1 = new SHA1Managed () )
            {
                var sb = new StringBuilder ();
                var hashParts =
                    ( BitConverter.ToString ( ( new SHA1CryptoServiceProvider ().ComputeHash ( data ) ) ).Split ( '-' ) ).ToList ();
                hashParts.ForEach ( p => sb.Append ( p ) );

                hash = sb.ToString ();
            }

            return hash;
        }

        #endregion
    }
}
