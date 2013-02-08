using HibernatingRhinos.Profiler.Appender.NHibernate;
using Moq;
using NHibernate;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using Pillar.Domain.Primitives;
using Pillar.Domain.Tests;
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Domain;

namespace Rem.Infrastructure.Tests.Domain
{
    public abstract class NHibernateFixtureBase : DomainTestBase
    {
        protected ISessionFactory SessionFactory
        {
            get { return NHibernateConfigurator.SessionFactory; }
        }

        protected ISession Session
        {
            get
            {
                var session = SessionFactory.GetCurrentSession ();
                if (session.IsOpen == false)
                {
                    session = SessionFactory.OpenSession ();
                }
                return session;
            }
        }

        protected ISessionProvider SessionProvider
        {
            get 
            {
                var sessionProvider = new Mock<ISessionProvider> ();
                sessionProvider.Setup ( s => s.GetSession () ).Returns ( Session );
                return sessionProvider.Object;
            }
        }

        protected override void OnSetup ()
        {
            base.OnSetup();

            NHibernateProfiler.Initialize();

            SetupNHibernateSession();
            SetupSystemAccountProvider();
        }

        protected override void OnTeardown ()
        {
            TearDownNHibernateSession ();
            base.OnTeardown ();
        }

        protected void SetupNHibernateSession ()
        {
            TestConnectionProvider.CloseDatabase ();
            SetupContextualSession ();
            BuildSchema ();
        }

        protected virtual void SetupSystemAccountProvider()
        {
            var systemAccount = new SystemAccount ( "{2342-23434593-345345-345-345-3}","sytemuser displayname", new EmailAddress("test@test.com"), "uri:FakeProvider","FakeProvider" );

            using ( ITransaction trans = Session.BeginTransaction () )
            {
                Session.SaveOrUpdate ( systemAccount );
                trans.Commit ();
            }

            var systemAccountProviderMock = new Mock<ISystemAccountProvider> ();
            systemAccountProviderMock
                .SetupGet ( x => x.SystemAccount )
                .Returns ( systemAccount );

            StructureMapContainer.Configure ( s => s
                .For<ISystemAccountProvider> ()
                .Singleton ()
                .Use ( systemAccountProviderMock.Object ) );
        }

        protected void TearDownNHibernateSession ()
        {
            TearDownContextualSession ();
            TestConnectionProvider.CloseDatabase ();
        }

        private void SetupContextualSession ()
        {
            var session = SessionFactory.OpenSession ();
            CurrentSessionContext.Bind ( session );
        }

        private static void TearDownContextualSession ()
        {
            var sessionFactory = NHibernateConfigurator.SessionFactory;
            var session = CurrentSessionContext.Unbind ( sessionFactory );
            if ( session != null && session.IsOpen)
            {
                session.Close ();
            }
        }

        private static void BuildSchema ()
        {
            var cfg = NHibernateConfigurator.Configuration;
            var schemaExport = new SchemaExport ( cfg );
            schemaExport.Create ( false, true );
                
            //// A new session is created implicitly to run the create scripts. But this new session is not the context session
        }
    }
}