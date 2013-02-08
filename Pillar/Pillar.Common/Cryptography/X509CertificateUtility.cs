using System;
using System.Security.Cryptography.X509Certificates;

namespace Pillar.Common.Cryptography
{
    /// <summary>
    /// Utility for X509 certificate.
    /// </summary>
    public class X509CertificateUtility
    {
        #region Constants and Fields

        /// <summary>
        /// Certificate Name Used For Encryption ThumbPrint
        /// </summary>
        public static readonly string CertificateNameUsedForEncryptionThumbPrint = "40A1D2622BFBDAC80A38858AD8001E094547369B";

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the certificate.
        /// </summary>
        /// <param name="name">The store name.</param>
        /// <param name="location">The location.</param>
        /// <param name="identifier">The identifier.</param>
        /// <returns>A <see cref="System.Security.Cryptography.X509Certificates.X509Certificate2"/></returns>
        public static X509Certificate2 GetCertificate ( StoreName name, StoreLocation location, string identifier )
        {
            var store = new X509Store ( name, location );
            X509Certificate2Collection certificates = null;
            store.Open ( OpenFlags.ReadOnly );

            try
            {
                X509Certificate2 result = null;

                // Every time we call store.Certificates property, a new collection will be returned.
                certificates = store.Certificates;

                foreach ( var cert in certificates )
                {
                    if ( cert.Thumbprint.ToLower () == identifier.ToLower () )
                    {
                        result = new X509Certificate2 ( cert );
                    }
                }

                if ( result == null )
                {
                    throw new ApplicationException ( string.Format ( "No certificate were  Name {0}", identifier ) );
                }

                return result;
            }
            finally
            {
                if ( certificates != null )
                {
                    foreach ( var cert in certificates )
                    {
                        cert.Reset ();
                    }
                }

                store.Close ();
            }
        }

        /// <summary>
        /// Gets the default certificate.
        /// </summary>
        /// <returns>A <see cref="System.Security.Cryptography.X509Certificates.X509Certificate2"/></returns>
        public static X509Certificate2 GetDefaultCertificate ()
        {
            return
                GetCertificate (
                    StoreName.My,
                    StoreLocation.LocalMachine,
                    CertificateNameUsedForEncryptionThumbPrint );
        }

        #endregion
    }
}
