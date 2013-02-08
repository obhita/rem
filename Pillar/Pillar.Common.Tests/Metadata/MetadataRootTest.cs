using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Metadata;

namespace Pillar.Common.Tests.Metadata
{
    [TestClass]
    public class MetadataRootTest
    {
        [TestMethod]
        public void Constructor_WithCorrectParameters_CreatedMetadataNode()
        {
            var metadataRoot = new MetadataRoot ( "Default", 1 );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithNullResourceName_ThrowException()
        {
            var metadataRoot = new MetadataRoot ( null, 1 );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithEmptyResourceName_ThrowException()
        {
            var metadataRoot = new MetadataRoot("", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithWhiteSpaceResourceName_ThrowException()
        {
            var metadataRoot = new MetadataRoot("  ", 1);
        }

        [TestMethod]
        public void ResourceName_Get_Succeed()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            Assert.AreEqual("Default", metadataRoot.ResourceName);
        }

        [TestMethod]
        public void MetadataLayerId_Get_Succeed()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            Assert.AreEqual(1, metadataRoot.MetadataLayerId);
        }

        [TestMethod]
        public void AddChild_WithValidResourceName_CreatedAMetadataNode()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            var child = metadataRoot.AddChild("Child.Resource");

            Assert.IsNotNull ( child );
            Assert.IsInstanceOfType ( child, typeof(MetadataNode) );
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void AddChild_WithExistingResourceName_ThrowException()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            var child = metadataRoot.AddChild("Child.Resource");
            metadataRoot.AddChild ("Child.Resource");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddChild_WithNullResourceName_ThrowException()
        {
            var metadataRoot = new MetadataRoot ( "Default", 1 );
            metadataRoot.AddChild ( null );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddChild_WithEmptyResourceName_ThrowException()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            metadataRoot.AddChild("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddChild_WithWhiteSpacesResourceName_ThrowException()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            metadataRoot.AddChild("  ");
        }

        [TestMethod]
        public void AddChild_WithValidParameter_ChildrenPropertyHasOneItem()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            metadataRoot.AddChild("Child.Resource");

            Assert.AreEqual ( 1, metadataRoot.Children.Count );
        }

        [TestMethod]
        public void AddChild_WithValidParameterTwice_ChildrenPropertyHasTwoItems()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            metadataRoot.AddChild("Child.Resource1");
            metadataRoot.AddChild("Child.Resource2");

            Assert.AreEqual(2, metadataRoot.Children.Count);
        }

        [TestMethod]
        public void HasChild_NoChildAdded_ReturnFalse()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            Assert.IsFalse ( metadataRoot.HasChild ( "Child.Resource1" ) );
        }

        [TestMethod]
        public void HasChild_NoChildForSpecificResource_ReturnFalse()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            metadataRoot.AddChild ( "Child.Resource1" );

            Assert.IsFalse(metadataRoot.HasChild("Child.Resource2"));
        }

        [TestMethod]
        public void HasChild_SpecificResourceIsThere_ReturnTrue()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            metadataRoot.AddChild("Child.Resource1");
            metadataRoot.AddChild("Child.Resource2");

            Assert.IsTrue(metadataRoot.HasChild("Child.Resource2"));
        }

        [TestMethod]
        public void FindChildMetadata_NoChildAdded_ReturnNull()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            Assert.IsNull(metadataRoot.FindChildMetadata("Child.Resource1"));
        }

        [TestMethod]
        public void FindChildMetadata_NoChildForSpecificResource_ReturnNull()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            metadataRoot.AddChild("Child.Resource1");

            Assert.IsNull(metadataRoot.FindChildMetadata("Child.Resource2"));
        }

        [TestMethod]
        public void FindChildMetadata_SpecificResourceIsThere_ReturnMetadataNode()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            metadataRoot.AddChild("Child.Resource1");
            metadataRoot.AddChild("Child.Resource2");

            var result = metadataRoot.FindChildMetadata ( "Child.Resource2" );

            Assert.IsNotNull ( result );
            Assert.IsInstanceOfType ( result, typeof(MetadataNode) );
        }

        [TestMethod]
        public void MetadataItems_Set_Succeed()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            metadataRoot.MetadataItems = new List<IMetadataItem> { new Mock<IMetadataItem> ().Object };

            Assert.AreEqual ( 1, metadataRoot.MetadataItems.Count );
        }

        [TestMethod]
        public void MetadataItems_Add_Succeed()
        {
            var metadataRoot = new MetadataRoot("Default", 1);
            metadataRoot.MetadataItems.Add ( new Mock<IMetadataItem>().Object );

            Assert.AreEqual ( 1, metadataRoot.MetadataItems.Count );
        }
    }
}
