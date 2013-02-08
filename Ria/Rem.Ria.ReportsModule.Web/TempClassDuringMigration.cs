﻿#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Net;
using Pillar.Common.Configuration;
using Pillar.Common.InversionOfControl;

namespace Rem.Ria.ReportsModule.Web
{
    /// <summary>
    /// THIS IS A DANGEROUS TEMPORARY FIX.
    /// After migration of app server to rem domain, remqaidentity and remdemoidentity can be added
    /// in SSRS under 'Browser' Role and CredentialCache.DefaultNetworkCredentials can be used without
    /// checking for environment.
    /// </summary>
    internal static class TempClassDuringMigration
    {
        #region Constants and Fields

        private static readonly IConfigurationPropertiesProvider ConfigurationPropertiesProvider;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="TempClassDuringMigration"/> class.
        /// </summary>
        static TempClassDuringMigration ()
        {
            ConfigurationPropertiesProvider = IoC.CurrentContainer.Resolve<IConfigurationPropertiesProvider> ();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the network credential.
        /// </summary>
        internal static NetworkCredential NetworkCredential
        {
            get
            {
                return ConfigurationPropertiesProvider.GetProperty ( "Environment" ).Equals ( "Dev", StringComparison.InvariantCultureIgnoreCase )
                           ? CredentialCache.DefaultNetworkCredentials
                           : new NetworkCredential ( "tempreportbrowser", "Temp*000", "REM" );
            }
        }

        #endregion
    }
}