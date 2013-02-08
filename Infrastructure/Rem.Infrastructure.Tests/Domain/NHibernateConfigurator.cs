using System.Data.SQLite;
using NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg.Loquacious;
using Rem.Infrastructure.Bootstrapper;
using Rem.Infrastructure.Domain;
using Environment = NHibernate.Cfg.Environment;

namespace Rem.Infrastructure.Tests.Domain
{
    public static class NHibernateConfigurator
    {
        private static readonly NHibernate.Cfg.Configuration _configuration;
        private static readonly ISessionFactory _sessionFactory;

        static NHibernateConfigurator ()
        {
            var sqlitePersistenceConfigurerProvider = new SqlitePersistenceConfigurerProvider ();
#if DEBUG
            FluentConfigurationSerializer.IsEnabled = false;
#endif
            var configurationProvider = new FluentConfigurationProvider ( sqlitePersistenceConfigurerProvider , new AssemblyLocator());

            _configuration = configurationProvider.Configure ().GetEnumerator ().Current;

            _configuration = _configuration.DataBaseIntegration ( db => db.ConnectionProvider<TestConnectionProvider> () );
            _configuration.SetProperty ( Environment.CurrentSessionContextClass, "thread_static" );

            var props = _configuration.Properties;
            if ( props.ContainsKey ( Environment.ConnectionStringName ) )
                props.Remove ( Environment.ConnectionStringName );

            // This line is only for TFS integration build MSTest runner
            ForceLoadingAssembliesForMsTestRunner();

            _sessionFactory = _configuration.BuildSessionFactory ();
        }

        public static NHibernate.Cfg.Configuration Configuration
        {
            get { return _configuration; }
        }

        public static ISessionFactory SessionFactory
        {
            get { return _sessionFactory; }
        }

        private static void ForceLoadingAssembliesForMsTestRunner ()
        {
            //Just to make sure the Castle.Core assembly is loaded for Microsoft Unit Test Runner
            var castleConfig = typeof(Castle.Core.Configuration.IConfiguration);

            //Just to make sure the NHibernate.ByteCode.Castle assembly and Castle.Core assembly is loaded for Microsft Unit Test Runner
            var proxyFactory = new ProxyFactory();

            //Just to make sure the ByteCodeCastle assembly is loaded for Microsft Unit Test Runner
            var sqLiteParameter = new SQLiteParameter();
        }
    }
}