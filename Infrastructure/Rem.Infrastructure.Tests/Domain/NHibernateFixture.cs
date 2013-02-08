using System;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace Rem.Infrastructure.Tests.Domain
{
    public sealed class NHibernateFixture : IDisposable
    {
        private readonly ISessionFactory _sessionFactory;

        public NHibernateFixture()
        {
            _sessionFactory = NHibernateConfigurator.SessionFactory;

            NHibernateProfiler.Initialize();

            SetupNHibernateSession();
        }

        public ISession Session
        {
            get
            {
                var session = _sessionFactory.GetCurrentSession();
                if (session.IsOpen == false)
                {
                    SetupContextualSession();
                }
                return session;
            }
        }

        private void SetupNHibernateSession()
        {
            TestConnectionProvider.CloseDatabase();
            SetupContextualSession();
            BuildSchema();
        }

        private void SetupContextualSession()
        {
            ISession session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        private static void TearDownNHibernateSession()
        {
            TearDownContextualSession();
            TestConnectionProvider.CloseDatabase();
        }

        private static void TearDownContextualSession()
        {
            ISessionFactory sessionFactory = NHibernateConfigurator.SessionFactory;
            ISession session = CurrentSessionContext.Unbind(sessionFactory);
            if (session.IsOpen)
            {
                session.Close();
            }
        }

        private static void BuildSchema()
        {
            NHibernate.Cfg.Configuration cfg = NHibernateConfigurator.Configuration;
            var schemaExport = new SchemaExport(cfg);
            schemaExport.Create(false, true);
            // A new session is created implicitly to run the create scripts. But this new session is not the context session
        }

        public void Dispose()
        {
            TearDownNHibernateSession(); 
            GC.SuppressFinalize(this);
        }
    }
}