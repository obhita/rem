#region License
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
using FluentNHibernate.Cfg.Db;
using Pillar.Common.Configuration;
using Rem.Infrastructure.Configuration;

namespace Rem.Infrastructure.Domain
{
    /// <summary>
    /// The <see cref="Rem.Infrastructure.Domain.MsSql2008PersistenceConfigurerProvider">MsSql2008PersistenceConfigurerProvider </see> continas the service to configur MS SQL 2008 provider. 
    /// </summary>
    public class MsSql2008PersistenceConfigurerProvider : IPersistenceConfigurerProvider
    {
        private readonly IConfigurationPropertiesProvider _configurationPropertiesProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSql2008PersistenceConfigurerProvider"/> class.
        /// </summary>
        /// <param name="configurationPropertiesProvider">The configuration properties provider.</param>
        public MsSql2008PersistenceConfigurerProvider ( IConfigurationPropertiesProvider configurationPropertiesProvider )
        {
            _configurationPropertiesProvider = configurationPropertiesProvider;
        }

        #region IPersistenceConfigurerProvider Members

        /// <summary>
        /// Configures this instance.
        /// </summary>
        /// <returns>A IPersistenceConfigurer object.</returns>
        public IPersistenceConfigurer Configure ()
        {
            IPersistenceConfigurer database;
            
            bool useTrustedConnection = true;
            string databasePassword = string.Empty;
            string databaseUsername = string.Empty;

            string databaseServer = _configurationPropertiesProvider.GetProperty ( SettingKeyNames.DatabaseServerName );
            string databaseName = _configurationPropertiesProvider.GetProperty ( SettingKeyNames.DatabaseCatalogName );

            bool.TryParse ( _configurationPropertiesProvider.GetProperty ( SettingKeyNames.UseTrustedConnection ), out useTrustedConnection );

            if ( useTrustedConnection )
            {
                database = MsSqlConfiguration.MsSql2008.ConnectionString (
                    c => c
                             .Server ( databaseServer )
                             .Database ( databaseName )
                             .TrustedConnection () )
                    .AdoNetBatchSize ( 10 )
                    .UseOuterJoin ()
                    .QuerySubstitutions ( "true 1, false 0, yes 'Y', no 'N'" );
            }
            else
            {
                databasePassword = _configurationPropertiesProvider.GetProperty ( SettingKeyNames.DatabasePassword );
                databaseUsername = _configurationPropertiesProvider.GetProperty ( SettingKeyNames.DatabaseUsername );

                database = MsSqlConfiguration.MsSql2008.ConnectionString (
                    c => c
                             .Server ( databaseServer )
                             .Database ( databaseName )
                             .Username ( databaseUsername )
                             .Password ( databasePassword ) )
                    .AdoNetBatchSize ( 10 )
                    .UseOuterJoin ()
                    .QuerySubstitutions ( "true 1, false 0, yes 'Y', no 'N'" );
            }


            return database;
        }

        #endregion
    }
}