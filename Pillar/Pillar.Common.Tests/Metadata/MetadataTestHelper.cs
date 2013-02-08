using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Metadata;

namespace Pillar.Common.Tests.Metadata
{
    public class MetadataTestHelper
    {
        public static void AssertMetadatasAreEqual(IEnumerable<IMetadata> expectedMetadatas, IEnumerable<IMetadata> testedMetadatas)
        {
            Assert.AreEqual ( expectedMetadatas.Count(), testedMetadatas.Count() );

            for (int i = 0; i < expectedMetadatas.Count(); i++)
            {
                var expectedMetadata = expectedMetadatas.ElementAt ( i );
                var testedMetadata = testedMetadatas.ElementAt ( i );

                Assert.AreEqual ( expectedMetadata.ResourceName, testedMetadata.ResourceName );

                AssertMetadataItemsAreEqual(expectedMetadata.MetadataItems, testedMetadata.MetadataItems);

                AssertMetadatasAreEqual ( expectedMetadata.Children, testedMetadata.Children );
            }
        }

        public static void AssertMetadataAreEqual(IMetadata expectedMetadata, IMetadata testedMetadata)
        {
            AssertMetadatasAreEqual ( new List<IMetadata> { expectedMetadata }, new List<IMetadata> { testedMetadata } );
        }

        public static void AssertMetadataItemsAreEqual(IEnumerable<IMetadataItem> expectedMetadataItems, IEnumerable<IMetadataItem> testedMetadataItems)
        {
            Assert.AreEqual ( expectedMetadataItems.Count(), testedMetadataItems.Count() );

            for (int i = 0; i < expectedMetadataItems.Count(); i++)
            {
                var expectedMetadataItem = expectedMetadataItems.ElementAt ( i );
                var testedMetadataItem = testedMetadataItems.ElementAt ( i );

                AssertMetadataItemAreEqual(expectedMetadataItem, testedMetadataItem);
            }
        }

        public static void AssertMetadataItemAreEqual(IMetadataItem expectedMetadataItem, IMetadataItem testedMetadataItem)
        {
            Assert.AreEqual(expectedMetadataItem.GetType(), testedMetadataItem.GetType());

            if (expectedMetadataItem is RequiredMetadataItem)
            {
                Assert.AreEqual((expectedMetadataItem as RequiredMetadataItem).IsRequired, (testedMetadataItem as RequiredMetadataItem).IsRequired);
            }
            else if (expectedMetadataItem is ReadonlyMetadataItem)
            {
                Assert.AreEqual((expectedMetadataItem as ReadonlyMetadataItem).IsReadonly, (testedMetadataItem as ReadonlyMetadataItem).IsReadonly);
            }
            else if (expectedMetadataItem is HiddenMetadataItem)
            {
                Assert.AreEqual((expectedMetadataItem as HiddenMetadataItem).IsHidden, (testedMetadataItem as HiddenMetadataItem).IsHidden);
            }
            else if (expectedMetadataItem is DisplayNameMetadataItem)
            {
                Assert.AreEqual((expectedMetadataItem as DisplayNameMetadataItem).Name, (testedMetadataItem as DisplayNameMetadataItem).Name);
            }
            else
            {
                throw new NotSupportedException("Not supported yet.");
            }
        }
    }
}
