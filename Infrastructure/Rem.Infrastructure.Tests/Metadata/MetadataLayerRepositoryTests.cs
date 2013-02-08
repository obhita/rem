using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Metadata;
using Rem.Infrastructure.Metadata;

namespace Rem.Infrastructure.Tests.Metadata
{
    [TestClass]
    public class MetadataLayerRepositoryTests : RavenDbTestBase
    {
        private static readonly string LayerName = "Default";
        private static readonly int LayerLevel = 1;

        [TestMethod]
        public void GetMetadataLayerByName_NameExist_ReturnedTheMetadataLayer()
        {
            var expectedMetadataLayer = CreateMetadataLayer ( LayerName, LayerLevel );

            var repository = new MetadataLayerRepository ( new DocumentSessionProvider ( Store ) );

            MetadataLayer layer = repository.GetMetadataLayerByName ( LayerName );

            Assert.IsNotNull ( layer );
            Assert.AreEqual ( expectedMetadataLayer.Id, layer.Id );
        }

        [TestMethod]
        public void GetMetadataLayerByName_NameNotExist_ReturnedNull()
        {
            var expectedMetadataLayer = CreateMetadataLayer ( LayerName, LayerLevel );

            var repository = new MetadataLayerRepository ( new DocumentSessionProvider ( Store ) );

            MetadataLayer layer = repository.GetMetadataLayerByName ( "NotExistingLayer" );

            Assert.IsNull ( layer );
        }

        [TestMethod]
        public void GetAllMetadataLayers_Succeed()
        {
            CreateMetadataLayer ( LayerName, LayerLevel );

            var repository = new MetadataLayerRepository ( new DocumentSessionProvider ( Store ) );

            IEnumerable<MetadataLayer> layers = repository.GetAllMetadataLayers ();

            Assert.AreEqual ( 1, layers.Count () );
        }

        private MetadataLayer CreateMetadataLayer(string layerName, int layerLevel)
        {
            using (var session = Store.OpenSession())
            {
                var metadataLayer = new MetadataLayer(layerName, layerLevel);

                session.Store(metadataLayer);
                session.SaveChanges();

                return metadataLayer;
            }
        }
    }
}