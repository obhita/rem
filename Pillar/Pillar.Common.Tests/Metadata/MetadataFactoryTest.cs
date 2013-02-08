using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Metadata;

namespace Pillar.Common.Tests.Metadata
{
    [TestClass]
    public class MetadataFactoryTest : TestFixtureBase
    {
        [TestMethod]
        public void CreateMetadataRoot_WithValidParamters_CreatedMetadataRoot()
        {
            var metadataRepository = new Mock<IMetadataRepository>();
            var factory = new MetadataFactory(metadataRepository.Object);

            var metadataRoot = factory.CreateMetadataRoot("MyResource", new MetadataLayer { Id = 1 });

            Assert.IsNotNull(metadataRoot);
            Assert.AreEqual("MyResource", metadataRoot.ResourceName);
        }

        [TestMethod]
        public void CreateMetadataRoot_WithValidParamters_MetadataRootIsMadePersistent()
        {
            bool isPersistent = false;

            var metadataRepository = new Mock<IMetadataRepository>();
            metadataRepository
                .Setup(x => x.MakePersistent(It.IsAny<MetadataRoot>()))
                .Callback(() => isPersistent = true);

            var factory = new MetadataFactory(metadataRepository.Object);
            var metadataRoot = factory.CreateMetadataRoot("MyResource", new MetadataLayer { Id = 1 });

            Assert.IsTrue(isPersistent);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateMetadataRoot_WithNullResourceName_ThrowException()
        {
            var metadataRepository = new Mock<IMetadataRepository>();
            var factory = new MetadataFactory(metadataRepository.Object);

            factory.CreateMetadataRoot(null, new MetadataLayer { Id = 1 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateMetadataRoot_WithEmptyResourceName_ThrowException()
        {
            var metadataRepository = new Mock<IMetadataRepository>();
            var factory = new MetadataFactory(metadataRepository.Object);

            factory.CreateMetadataRoot("", new MetadataLayer { Id = 1 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateMetadataRoot_WithWhiteSpaceResourceName_ThrowException()
        {
            var metadataRepository = new Mock<IMetadataRepository>();
            var factory = new MetadataFactory(metadataRepository.Object);

            factory.CreateMetadataRoot("  ", new MetadataLayer { Id = 1 });
        }

        [TestMethod]
        public void DestroyMetadataRoot_WithValidMetadataRoot_MetadataRootIsTransient()
        {
            bool isTransient = false;

            var metadataRepository = new Mock<IMetadataRepository>();
            metadataRepository
                .Setup(a => a.MakeTransient(It.IsAny<MetadataRoot>()))
                .Callback(() => isTransient = true);
            var factory = new MetadataFactory(metadataRepository.Object);
            var metadataRoot = new MetadataRoot("MyResource", 1);

            factory.DestroyMetadataRoot(metadataRoot);

            Assert.IsTrue(isTransient);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DestroyMetadataRoot_WithNullMetadataRoot_ThrowException()
        {
            var metadataRepository = new Mock<IMetadataRepository>();
            var factory = new MetadataFactory(metadataRepository.Object);

            factory.DestroyMetadataRoot(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DestroyMetadataRoot_WithMetadataNode_ThrowException()
        {
            var metadataRepository = new Mock<IMetadataRepository>();
            var factory = new MetadataFactory(metadataRepository.Object);
            var metadataNode = new MetadataNode("MyResource");

            factory.DestroyMetadataRoot(metadataNode);
        }
    }
}
