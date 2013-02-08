using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Tests;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Storage.Managed.Backup;

namespace Rem.Infrastructure.Tests.Metadata
{
    [TestClass]
    public abstract class RavenDbTestBase : TestFixtureBase
    {
        protected IDocumentStore Store;

        protected override void OnSetup()
        {
            base.OnSetup ();

            ForceLoadingAssembliesForMsTestRunner();

            Store = new EmbeddableDocumentStore { RunInMemory = true };
            //Store = new DocumentStore {Url = "http://localhost:8080"};
            Store.Initialize ();
        }

        protected override void OnTeardown()
        {
            base.OnTeardown ();
            if ( !Store.WasDisposed )
            {
                Store.Dispose ();
            }
        }

        private static void ForceLoadingAssembliesForMsTestRunner ()
        {
            // Just to make sure the Raven.Storage.Managed assembly is loaded for Microsft Unit Test Runner
            RestoreOperation restoreOperation = new RestoreOperation("", "");
        }
    }
}