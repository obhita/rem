using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Metadata;
using Pillar.Common.Tests;
using Raven.Client;
using Raven.Client.Embedded;

namespace Rem.Infrastructure.Tests.Metadata
{
    [TestClass]
    public class InitializeTestDataToRavenDbTests : TestFixtureBase
    {
        protected IDocumentStore Store;

        protected override void OnSetup()
        {
            base.OnSetup();
            //Store = new DocumentStore {Url = "http://localhost:8080"};
            Store = new EmbeddableDocumentStore { RunInMemory = true };
            Store.Initialize();
        }

        protected override void OnTeardown()
        {
            base.OnTeardown();
            if (!Store.WasDisposed)
            {
                Store.Dispose();
            }
        }

        [TestMethod]
        public void InitializeTestDataToRavenDb_Succeed()
        {
            var metadataLayer = new MetadataLayer ( "Default", 1 );
            using (var session = Store.OpenSession())
            {
                session.Store ( metadataLayer );
                session.SaveChanges ();
            }

            var metadataRoot = new MetadataRoot ( "Rem.Ria.PatientModule.Web.Common.PatientProfileDto", metadataLayer.Id );

            var firstNameNode = metadataRoot.AddChild ( "FirstName" );
            firstNameNode.MetadataItems = new List<IMetadataItem>
                                              {
                                                  new DisplayNameMetadataItem { Name = "The Patient's First Name" },
                                                  new ReadonlyMetadataItem { IsReadonly = true }
                                              };

            var middleNameNode = metadataRoot.AddChild ( "MiddleName" );
            middleNameNode.MetadataItems = new List<IMetadataItem>
                                               {
                                                   new HiddenMetadataItem { IsHidden = true }
                                               };

            var lastNameNode = metadataRoot.AddChild ( "LastName" );
            lastNameNode.MetadataItems = new List<IMetadataItem>
                                             {
                                                 new RequiredMetadataItem { IsRequired = true }
                                             };

            using ( var sesssion = Store.OpenSession() )
            {
                sesssion.Store ( metadataRoot );
                sesssion.SaveChanges ();
            }
        }
    }
}
