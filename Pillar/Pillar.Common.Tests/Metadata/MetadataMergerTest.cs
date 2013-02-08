using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Metadata;

namespace Pillar.Common.Tests.Metadata
{
    [TestClass]
    public class MetadataMergerTest : TestFixtureBase
    {
        #region Private fields
        
        private static readonly IList<MetadataLayer> MetadataLayers = new List<MetadataLayer>
                                                                          {
                                                                              new MetadataLayer ( "Default", 0 ) { Id = 1 },
                                                                              new MetadataLayer ( "MD Default", 1 ) { Id = 2 },
                                                                              new MetadataLayer ( "MD Customization", 2 ) { Id = 3}
                                                                          };

        private IMetadataLayerRepository _metadataLayerRepository;

        #endregion

        protected override void OnSetup()
        {
            var mock = new Mock<IMetadataLayerRepository>();
            mock.Setup ( x => x.GetAllMetadataLayers () )
                .Returns(MetadataLayers);
            _metadataLayerRepository = mock.Object;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MergeMetadata_RequiredMetadataLayerRepositoryIsNotProvided_ThrowException()
        {
            new MetadataMerger(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MergeMetadata_WithParameterNull_ThrowException()
        {
            var metadataMerger = new MetadataMerger ( _metadataLayerRepository );
            metadataMerger.MergeMetadata ( null );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MergeMetadata_WithParameterEmpty_ThrowException()
        {
            var metadataMerger = new MetadataMerger(_metadataLayerRepository);
            metadataMerger.MergeMetadata(new List<MetadataRoot>());
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void MergeMetadata_MetadataForDifferentResources_ThrowException()
        {
            var metadataRootList = new List<MetadataRoot>
                                       {
                                           new MetadataRoot ( "Resource1", 1 ),
                                           new MetadataRoot ( "Resource2", 1 )
                                       };
            var metadataMerger = new MetadataMerger(_metadataLayerRepository);
            metadataMerger.MergeMetadata(metadataRootList);
        }

        [TestMethod]
        public void MergeMetadata_SingleMetadataInSameLayer_Succeed()
        {
            var metadataRootList = MetadataMergerTestFixture.SingleMetadataInSameLayer_TestValue ();
            var metadataMerger = new MetadataMerger ( _metadataLayerRepository );
            
            var metadata = metadataMerger.MergeMetadata ( metadataRootList );

            var expectedMetadata = MetadataMergerTestFixture.SingleMetadataInSameLayer_ExpectedResult();
            MetadataTestHelper.AssertMetadataAreEqual(expectedMetadata, metadata);
        }

        [TestMethod]
        public void MergeMetadata_MultiMetadataInSameLayer_Succeed()
        {
            var metadataRootList = MetadataMergerTestFixture.MultiMetadataInSameLayer_TestValue ();
            var metadataMerger = new MetadataMerger ( _metadataLayerRepository );

            var metadata = metadataMerger.MergeMetadata ( metadataRootList );

            var expectedMetadata = MetadataMergerTestFixture.MultiMetadataInSameLayer_ExpectedResult ();
            MetadataTestHelper.AssertMetadataAreEqual ( expectedMetadata, metadata  );
        }

        [TestMethod]
        public void MergeMetadata_MultiMetadataInThreeLayers_Succeed()
        {
            var metadataRootList = MetadataMergerTestFixture.MultiMetadataInThreeLayers_TestValue ();
            var metadataMerger = new MetadataMerger(_metadataLayerRepository);

            var metadata = metadataMerger.MergeMetadata(metadataRootList);

            var expectedMetadata = MetadataMergerTestFixture.MultiMetadataInThreeLayers_ExpectedResult ();
            MetadataTestHelper.AssertMetadataAreEqual ( expectedMetadata, metadata );
        }

        [TestMethod]
        public void MergeMetadata_MultiMetadataInThreeLayersWithOneLevelChildren_Succeed()
        {
            var metadataRootList = MetadataMergerTestFixture.MultiMetadataInThreeLayersWithOneLevelChildren_TestValue ();
            var metadataMerger = new MetadataMerger(_metadataLayerRepository);
            
            var metadata = metadataMerger.MergeMetadata(metadataRootList);

            var expectedMetadata = MetadataMergerTestFixture.MultiMetadataInThreeLayersWithOneLevelChildren_ExpectedResult ();
            MetadataTestHelper.AssertMetadataAreEqual ( expectedMetadata, metadata );
        }

        [TestMethod]
        public void MergeMetadata_MultiMetadataInThreeLayersWithTwoLevelsChildren_Succeed()
        {
            var metadataRootList = MetadataMergerTestFixture.MultiMetadataInThreeLayersWithTwoLevelsChildren_TestValue ();
            var metadataMerger = new MetadataMerger(_metadataLayerRepository);

            var metadata = metadataMerger.MergeMetadata(metadataRootList);

            var expectedMetadata = MetadataMergerTestFixture.MultiMetadataInThreeLayersWithTwoLevelsChildren_ExpectedResult ();
            MetadataTestHelper.AssertMetadataAreEqual ( expectedMetadata, metadata );
        }
    }
}
