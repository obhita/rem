using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Metadata;

namespace Pillar.Common.Tests.Metadata
{
    [TestClass]
    public class MetadataLayerFactoryTest : TestFixtureBase
    {
        [TestMethod]
        public void CreateMetadataLayer_WithValidParamters_CreatedMetadataLayer()
        {
            var metadataLayerRepository = new Mock<IMetadataLayerRepository>();
            var factory = new MetadataLayerFactory(metadataLayerRepository.Object);

            var metadataLayer = factory.CreateMetadataLayer("Default", 1);

            Assert.IsNotNull ( metadataLayer );
            Assert.AreEqual ( "Default", metadataLayer.Name );
            Assert.AreEqual ( 1, metadataLayer.Level );
        }

        [TestMethod]
        public void CreateMetadataLayer_WithValidParamters_MetadataLayerIsEditable()
        {
            var metadataLayerRepository = new Mock<IMetadataLayerRepository>();
            var factory = new MetadataLayerFactory(metadataLayerRepository.Object);

            var metadataLayer = factory.CreateMetadataLayer("Default", 1);

            metadataLayer.Name = "Changed";
        }

        [TestMethod]
        public void CreateMetadataLayer_WithValidParamters_MetadataLayerIsMadePersistent()
        {
            bool isPersistent = false;

            var metadataLayerRepository = new Mock<IMetadataLayerRepository>();
            metadataLayerRepository
                .Setup ( x => x.MakePersistent ( It.IsAny<MetadataLayer> () ) )
                .Callback ( () => isPersistent = true );

            var factory = new MetadataLayerFactory(metadataLayerRepository.Object);
            var metadataLayer = factory.CreateMetadataLayer("Default", 1);

            Assert.IsTrue ( isPersistent );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateMetadataLayer_WithNullLayerName_ThrowException()
        {
            var metadataLayerRepository = new Mock<IMetadataLayerRepository> ();
            var factory = new MetadataLayerFactory (metadataLayerRepository.Object);

            factory.CreateMetadataLayer ( null, 1 );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateMetadataLayer_WithEmptyLayerName_ThrowException()
        {
            var metadataLayerRepository = new Mock<IMetadataLayerRepository>();
            var factory = new MetadataLayerFactory(metadataLayerRepository.Object);

            factory.CreateMetadataLayer("", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateMetadataLayer_WithWhiteSpaceLayerName_ThrowException()
        {
            var metadataLayerRepository = new Mock<IMetadataLayerRepository>();
            var factory = new MetadataLayerFactory(metadataLayerRepository.Object);

            factory.CreateMetadataLayer("  ", 1);
        }

        [TestMethod]
        public void DestroyMetadataLayer_WithCorrectMetadataLayer_MetadataLayerIsTransient()
        {
            bool isTransient = false;

            var metadataLayerRepository = new Mock<IMetadataLayerRepository>();
            metadataLayerRepository
                .Setup(a => a.MakeTransient(It.IsAny<MetadataLayer>()))
                .Callback(() => isTransient = true);
            var factory = new MetadataLayerFactory ( metadataLayerRepository.Object );
            var metadataLayer = new MetadataLayer ( "Default", 1 );

            factory.DestroyMetadataLayer ( metadataLayer );

            Assert.IsTrue(isTransient);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DestroyMetadataLayer_WithNullMetadataLayer_ThrowException()
        {
            var metadataLayerRepository = new Mock<IMetadataLayerRepository> ();
            var factory = new MetadataLayerFactory ( metadataLayerRepository.Object );

            factory.DestroyMetadataLayer ( null );
        }
    }
}
