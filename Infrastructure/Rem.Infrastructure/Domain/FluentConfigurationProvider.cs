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
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Util;
using NLog;
using Pillar.Common.Bootstrapper;
using Pillar.Common.InversionOfControl;
using Pillar.Domain.NHibernate;
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Domain.Interceptor;
using uNhAddIns.SessionEasier;

namespace Rem.Infrastructure.Domain
{
    /// <summary>
    /// The <see cref="T:Rem.Infrastructure.Domain.FluentConfigurationProvider">FluentConfigurationProvider</see> contains utilities for fluent configuration.
    /// </summary>
    public class FluentConfigurationProvider : AbstractConfigurationProvider
    {
        private readonly IAssemblyLocator _assemblyLocator;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();
        private readonly IPersistenceConfigurer _persistenceConfigurer;
        private NHibernate.Cfg.Configuration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentConfigurationProvider"/> class.
        /// </summary>
        /// <param name="persistenceConfigurerProvider">The persistence configurer provider.</param>
        /// <param name="assemblyLocator">The assembly locator.</param>
        public FluentConfigurationProvider ( IPersistenceConfigurerProvider persistenceConfigurerProvider, IAssemblyLocator assemblyLocator)
        {
            _assemblyLocator = assemblyLocator;
            _persistenceConfigurer = persistenceConfigurerProvider.Configure ();
        }

        /// <summary>
        /// Configures this instance.
        /// </summary>
        /// <returns>IEnumerable &lt;Configuration&gt;</returns>
        public override IEnumerable<NHibernate.Cfg.Configuration> Configure()
        {
#if DEBUG
            Logger.Debug ( "Configuring NHibernate Profiler." );
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize ();
#endif
            Logger.Debug ( "Configuring Fluent NHibernate." );

            FluentlyConfigure ();
            return new SingletonEnumerable<NHibernate.Cfg.Configuration>(_configuration);
        }

        private void FluentlyConfigure ()
        {
#if DEBUG
            if ( _configuration == null )
            {
                _configuration = FluentConfigurationSerializer.LoadConfigurationFromFile ();
#endif
                if ( _configuration == null )
                {
                    _configuration = Fluently.Configure ()
                        .Database ( _persistenceConfigurer )
                        .Mappings(m => m.AutoMappings.Add(GetAutoPersistenceModel())
#if DEBUG
                                             .ExportTo ( "c:\\temp" )
#endif
                        )
                        .Cache (
                            p =>
                            p.UseSecondLevelCache ().UseQueryCache ().ProviderClass (
                                typeof ( global::NHibernate.Caches.SysCache.SysCacheProvider ).AssemblyQualifiedName ) )
                        .BuildConfiguration ()
                        .SetProperty ( Environment.CommandTimeout, "60" )
                        .SetProperty ( Environment.GenerateStatistics, "true" )
                        .SetProperty ( Environment.PrepareSql, "true" )
                        .SetInterceptor ( new AuditableInterceptor ( () => 
                            IoC.CurrentContainer.Resolve<ISystemAccountProvider>()) );
#if DEBUG
                    FluentConfigurationSerializer.SaveConfigurationToFile ( _configuration );
                }
#endif
            }
        }

        private AutoPersistenceModel GetAutoPersistenceModel()
        {
            var autoPersistenceModel = AutoMap.Assemblies(new AutomappingConfiguration(), _assemblyLocator.LocateDomainAssemblies().ToList().ToArray());

            // Add Pillar NHibernate conventions
            autoPersistenceModel = autoPersistenceModel.Conventions.AddFromAssemblyOf<AutomappingConfiguration> ();

            // To alllow two persistent classes with the same unqualified name
            //autoPersistenceModel = autoPersistenceModel.Conventions.Setup ( x => x.Add ( AutoImport.Never () ) );

            // Add conventions and overrides from infrastructure assebmlies
            var infrastructureAssebmlies = _assemblyLocator.LocateInfrastructureAssemblies();
            foreach ( var infrastructureAssebmly in infrastructureAssebmlies )
            {
                autoPersistenceModel = autoPersistenceModel
                    .Conventions.AddAssembly ( infrastructureAssebmly )
                    .UseOverridesFromAssembly ( infrastructureAssebmly );
            }

            return autoPersistenceModel;
        }
    }
}
