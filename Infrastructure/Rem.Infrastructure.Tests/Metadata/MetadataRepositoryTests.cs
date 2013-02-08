using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Metadata;
using Raven.Client;
using Rem.Infrastructure.Metadata;
using System.Linq;

namespace Rem.Infrastructure.Tests.Metadata
{
    [TestClass]
    public class MetadataRepositoryTests : RavenDbTestBase
    {
        private static readonly string LayerName1 = "Default";
        private static readonly string LayerName2 = "Customized";
        private static readonly string ResourceName1 = "PatientModule.Web.PatientDto.PatientProfileDto";
        private static readonly string ResourceName2 = "PatientModule.Web.PatientDto.ContactInfoDto";

        protected override void OnSetup()
        {
            base.OnSetup ();

            var metadataLayer1 = CreateMetadataLayer ( LayerName1 );
            CreateMetadataRoot ( ResourceName1, metadataLayer1 );
            CreateMetadataRoot ( ResourceName2, metadataLayer1 );

            var metadataLayer2 = CreateMetadataLayer ( LayerName2 );
            CreateMetadataRoot ( ResourceName1, metadataLayer2 );
            CreateMetadataRoot ( ResourceName2, metadataLayer2 );
        }

        [TestMethod]
        public void GetMetadata_ResourceNameExist_ReturnedTheMetadata()
        {
            var documentSessionProvider = new DocumentSessionProvider ( Store );
            var repository = new MetadataRepository ( documentSessionProvider, new FakeMetadataMerger () );

            IMetadata metadata = repository.GetMetadata ( ResourceName1 );

            Assert.IsNotNull ( metadata );
            Assert.AreEqual ( ResourceName1, metadata.ResourceName );
        }

        [TestMethod]
        public void GetMetadata_ResourceNameNotExist_ReturnedNull()
        {
            var documentSessionProvider = new DocumentSessionProvider ( Store );
            var repository = new MetadataRepository ( documentSessionProvider, new FakeMetadataMerger () );

            IMetadata metadata = repository.GetMetadata ( "Not Excisted Resource" );

            Assert.IsNull ( metadata );
        }

        [TestMethod]
        public void GetMetadata_ResourceNameExistLayerNameExist_ReturnedTheMetadata()
        {
            var documentSessionProvider = new DocumentSessionProvider ( Store );
            var repository = new MetadataRepository ( documentSessionProvider, new FakeMetadataMerger () );

            IMetadata metadata = repository.GetMetadata ( ResourceName1, LayerName1 );

            Assert.IsNotNull ( metadata );
            Assert.AreEqual ( ResourceName1, metadata.ResourceName );
        }

        [TestMethod]
        public void GetMetadata_ResourceNameExistLayerNameNotExist_ReturnedNull()
        {
            var documentSessionProvider = new DocumentSessionProvider ( Store );
            var repository = new MetadataRepository ( documentSessionProvider, new FakeMetadataMerger () );

            IMetadata metadata = repository.GetMetadata ( ResourceName1, "Not Existed Layer" );

            Assert.IsNull ( metadata );
        }

        [TestMethod]
        public void GetMetadata_ResourceNameNotExistLayerNameExist_ReturnedTheMetadata()
        {
            var documentSessionProvider = new DocumentSessionProvider ( Store );
            var repository = new MetadataRepository ( documentSessionProvider, new FakeMetadataMerger () );

            IMetadata metadata = repository.GetMetadata ( "Not Existed Resource", LayerName1 );

            Assert.IsNull ( metadata );
        }

        [TestMethod]
        public void GetMetadata_ResourceNameNotExistLayerNameNotExist_ReturnedNull()
        {
            var documentSessionProvider = new DocumentSessionProvider ( Store );
            var repository = new MetadataRepository ( documentSessionProvider, new FakeMetadataMerger () );

            IMetadata metadata = repository.GetMetadata ( "Not Existed Resource", "Not Existed Layer" );

            Assert.IsNull ( metadata );
        }

        [TestMethod]
        public void FindMetadata_SearchStringIsResourceName_ReturnedOneMetadata()
        {
            var documentSessionProvider = new DocumentSessionProvider ( Store );
            var repository = new MetadataRepository ( documentSessionProvider, new FakeMetadataMerger () );

            var metadataList = repository.FindMetadata ( ResourceName1 );

            Assert.AreEqual ( 1, metadataList.Count () );
            Assert.AreEqual ( ResourceName1, metadataList.First ().ResourceName );
        }

        [TestMethod]
        public void FindMetadata_SearchStringIsWildchardResourceName_ReturnedTwoMetadata()
        {
            var documentSessionProvider = new DocumentSessionProvider ( Store );
            var repository = new MetadataRepository ( documentSessionProvider, new FakeMetadataMerger () );

            var metadataList = repository.FindMetadata ( "PatientModule.Web.PatientDto.*" );

            Assert.AreEqual ( 2, metadataList.Count () );
            Assert.AreEqual ( 1, metadataList.Where ( x => x.ResourceName == ResourceName1 ).Count () );
            Assert.AreEqual ( 1, metadataList.Where ( x => x.ResourceName == ResourceName2 ).Count () );
        }

        [TestMethod]
        public void FindMetadata_SearchStringIsNotExistedResourceName_ReturnedEmpty()
        {
            var documentSessionProvider = new DocumentSessionProvider ( Store );
            var repository = new MetadataRepository ( documentSessionProvider, new FakeMetadataMerger () );

            var metadataList = repository.FindMetadata ( "Not Existed Resource" );

            Assert.AreEqual ( 0, metadataList.Count () );
        }

        [TestMethod]
        public void FindMetadata_SearchStringIsResourceNameLayerNameIsValid_ReturnedOneMetadata()
        {
            var documentSessionProvider = new DocumentSessionProvider ( Store );
            var repository = new MetadataRepository ( documentSessionProvider, new FakeMetadataMerger () );

            var metadataList = repository.FindMetadata ( ResourceName1, LayerName1 );

            Assert.AreEqual ( 1, metadataList.Count () );
            Assert.AreEqual ( ResourceName1, metadataList.First ().ResourceName );
        }

        [TestMethod]
        public void FindMetadata_SearchStringIsResourceNameLayerNameIsInvalid_ReturnedEmpty()
        {
            var documentSessionProvider = new DocumentSessionProvider ( Store );
            var repository = new MetadataRepository ( documentSessionProvider, new FakeMetadataMerger () );

            var metadataList = repository.FindMetadata ( ResourceName1, "Not Existed Layer" );

            Assert.AreEqual ( 0, metadataList.Count () );
        }

        private MetadataLayer CreateMetadataLayer(string name)
        {
            using (IDocumentSession session = Store.OpenSession())
            {
                var metadataLayer = new MetadataLayer(name, 1);
                session.Store(metadataLayer);
                session.SaveChanges();

                return metadataLayer;
            }
        }

        private MetadataRoot CreateMetadataRoot(string resourceName, MetadataLayer metadataLayer)
        {
            using (IDocumentSession session = Store.OpenSession())
            {
                var metadata = new MetadataRoot ( resourceName, metadataLayer.Id );
                session.Store ( metadata );
                session.SaveChanges();

                return metadata;
            }
        }

        internal class FakeMetadataMerger : IMetadataMerger
        {
            public IMetadata MergeMetadata ( IEnumerable<MetadataRoot> metadataRootList )
            {
                return new MetadataNode(metadataRootList.First().ResourceName);
            }
        }
    }
}
